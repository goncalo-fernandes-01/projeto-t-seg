using EI.SI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class Form3 : Form
    {
        private const int PORT = 10000;
        NetworkStream networkStream;
        ProtocolSI protocolSI;
        TcpClient client;
        private RSACryptoServiceProvider rsa;
        private AesCryptoServiceProvider aes;
        private Thread thread;

        public Form3()
        {
            InitializeComponent();
            //CRIAR UM CONJUNTO IP+PORTO
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, PORT);

            //CRIAR O CLIENTE TCP
            client = new TcpClient();
            
            //CRIAR A LIGAÇÃO
            client.Connect(endPoint);

            //OBTER A LIGAÇÃO DO SERVIDOR
            networkStream = client.GetStream();
            protocolSI = new ProtocolSI();

            rsa = new RSACryptoServiceProvider();

            //Gerar Chave para encriptar mensagem
            //Criar e devolver uma string que contem a chave-pública
            string publicKey = rsa.ToXmlString(false);

            //Criar e devolver uma string que contém chave-privada
            string bothKey = rsa.ToXmlString(true);

            byte[] ack = protocolSI.Make(ProtocolSICmdType.PUBLIC_KEY, publicKey);
            networkStream.Write(ack, 0, ack.Length);

            networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
            byte[] symetricencrypted = protocolSI.GetData();
            byte[] symetricdecrypted = rsa.Decrypt(symetricencrypted, false);
            networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
            byte[] ivencrypted = protocolSI.GetData();
            byte[] ivdecrypted = rsa.Decrypt(ivencrypted, false);
            aes = new AesCryptoServiceProvider();
            aes.Key = symetricdecrypted;
            aes.IV = ivdecrypted;
            thread = new Thread(threadHandler);
            thread.Start();
        }

        private void threadHandler()
        {
            while (true)
            {
                networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
                switch (protocolSI.GetCmdType())
                {
                    case ProtocolSICmdType.DATA:
                        string mensagem = desencriptar(protocolSI.GetStringFromData());
                        Invoke((Action)delegate
                        {
                            lbChat.AppendText(mensagem + Environment.NewLine);
                        });
                        break;
                }
            }
        }

        private string desencriptar(string mensagem)
        {
            //VARIÁVEL PARA GUARDAR DO TEXTO CIFRADO EM BYTES
            byte[] txtCifrado = Convert.FromBase64String(mensagem);

            MemoryStream ms = new MemoryStream(txtCifrado);

            CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);

            //VARIÁVEL PARA GUARDAR O TEXTO DECIFRADO
            byte[] txtDecifrado = new byte[ms.Length];
            int byteslidos = 0;

            //DECIFRA OS DADOS
            byteslidos = cs.Read(txtDecifrado, 0, txtDecifrado.Length);
            cs.Close();

            //CONVERTER PARA TEXTO
            string textoDecifrado = Encoding.UTF8.GetString(txtDecifrado, 0, byteslidos);

            return textoDecifrado;
        }

        private void btnEnviar_Click(object sender, EventArgs e)
        {
            
            //Guardaar mensagem na variavel
            string mensagem = DateTime.Now.ToString("h:mm:ss") + " " + Form1.userName + " : " + txtMensagem.Text;
            txtMensagem.Clear();
            byte[] dados = Encoding.UTF8.GetBytes(mensagem);
            byte[] txtCifrado;
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write); 

            cs.Write(dados, 0, dados.Length);
            cs.Close();
            
            txtCifrado = ms.ToArray();
            
            string txtCifradoB64 = Convert.ToBase64String(txtCifrado);
            

            

            byte[] packet = protocolSI.Make(ProtocolSICmdType.DATA, txtCifradoB64);
            //ProtocolSICmdType - INTERPRETA O TIPO DE MENSAGEM RECEBIDO
            //protocolSI.Make - CRIAR A MENSAGEM DO TIPO ESPECÍFICO

            //ENVIAR A MENSAGEM PELA LIGAÇÃO
            networkStream.Write(packet, 0, packet.Length);

            
        }

        

        

        private void Form3_Load(object sender, EventArgs e)
        {
            txtvUtilizador.Text = Form1.userName;
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread.Abort();
            byte[] eot = protocolSI.Make(ProtocolSICmdType.EOT);
            networkStream.Write(eot, 0, eot.Length);
            //Fechar todas as ligações
            networkStream.Close();
            client.Close();
        }
    }
}
