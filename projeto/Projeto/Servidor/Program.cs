using EI.SI;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Servidor
{
    internal class Program
    {
        private const int PORT = 10000;
        static List<ClientHandler> listaclientes = new List<ClientHandler>();
        static void Main(string[] args)
        {
            //CRIAR UM CONJUNTO IP+PORTO
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, PORT);

            //CRIAR TCP LISTENER
            TcpListener listener = new TcpListener(endPoint);
            listener.Start();
            Console.WriteLine("Servidor Pronto para receber mensagens!");
            int clientCounter = 0;

            while (true)
            {
                TcpClient client = listener.AcceptTcpClient();
                clientCounter++;

                AesCryptoServiceProvider aes = new AesCryptoServiceProvider();
                NetworkStream networkStream = client.GetStream();
                ProtocolSI protocolSI = new ProtocolSI();
                networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);
                string publickey = protocolSI.GetStringFromData();

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(publickey);

                byte[] encryptedkeys = rsa.Encrypt(aes.Key, false);
                byte[] ack = protocolSI.Make(ProtocolSICmdType.SECRET_KEY, encryptedkeys);
                networkStream.Write(ack, 0, ack.Length);

                byte[] ivencrypted = rsa.Encrypt(aes.IV, false);
                ack = protocolSI.Make(ProtocolSICmdType.SECRET_KEY, ivencrypted);
                networkStream.Write(ack, 0, ack.Length);

                Console.WriteLine("Clientes {0} conectados", clientCounter);

                ClientHandler clientHandler = new ClientHandler(client, clientCounter, aes);
                listaclientes.Add(clientHandler);
                clientHandler.Handle();

            }
        }
        class ClientHandler
        {
            private TcpClient client;
            private int clientID;
            private string clienteNome;
            private AesCryptoServiceProvider aes;

            //PARA CRIAR O CLIENTE E NÚMERO
            public ClientHandler(TcpClient client, int clieentID, AesCryptoServiceProvider aes)
            {
                this.client = client;
                this.clientID = clieentID;
                this.aes = aes;
            }

            public void Handle()
            {
                Thread thread = new Thread(threadHandler);
                thread.Start();
            }

            private void threadHandler()
            {
                NetworkStream networkStream = this.client.GetStream();
                ProtocolSI protocolSI = new ProtocolSI();

                while (protocolSI.GetCmdType() != ProtocolSICmdType.EOT)
                {
                    //LER OS DADOS DO CLIENTE
                    networkStream.Read(protocolSI.Buffer, 0, protocolSI.Buffer.Length);


                    switch (protocolSI.GetCmdType())
                    {
                        case ProtocolSICmdType.DATA:
                            string mensagem = protocolSI.GetStringFromData();
                            Console.WriteLine(mensagem);
                            mensagem = desencriptar(mensagem);
                            transmissaoMensagem(mensagem);

                            break;
                        case ProtocolSICmdType.EOT:
                            mensagem = string.Format("Fim do Thread do Cliente do Cliente {0}", clientID);
                            Console.WriteLine(mensagem);
                            networkStream.Close();
                            client.Close();
                            listaclientes.Remove(this);
                            transmissaoMensagem(mensagem);
                            break;
                    }
                }

                //FECHAR AS LIGAÇÕES
                networkStream.Close();
                client.Close();
            }

            public static void transmissaoMensagem(string mensagem)
            {
                foreach (ClientHandler cliente in listaclientes)
                {
                    NetworkStream networkStream = cliente.client.GetStream();
                    ProtocolSI protocolSI = new ProtocolSI();
                    byte[] dados = Encoding.UTF8.GetBytes(mensagem);
                    byte[] txtCifrado;
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, cliente.aes.CreateEncryptor(), CryptoStreamMode.Write);
                    cs.Write(dados, 0, dados.Length);
                    cs.Close();
                    txtCifrado = ms.ToArray();
                    string txtCifradoB64 = Convert.ToBase64String(txtCifrado);
                    byte[] ack = protocolSI.Make(ProtocolSICmdType.DATA, txtCifradoB64);
                    networkStream.Write(ack, 0, ack.Length);
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
        }
    }
}

    
