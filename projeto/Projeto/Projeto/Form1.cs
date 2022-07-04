using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projeto
{
    public partial class Form1 : Form
    {
        private const int SALTSIZE = 8;
        private const int NUMBER_OF_ITERATIONS = 1000;
        public static string userName;

        public Form1()
        {
            InitializeComponent();
        }

        private bool VerifyLogin(string username, string password)
        {
            SqlConnection conn = null;
            try
            {
                // Configurar ligação à Base de Dados
                conn = new SqlConnection();
                conn.ConnectionString = String.Format(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\LENOVO\Desktop\TopSeg\Projeto\projeto\Projeto\Projeto\Database2.mdf';Integrated Security=True");

                // Abrir ligação à Base de Dados
                conn.Open();

                // Declaração do comando SQL
                String sql = "SELECT * FROM Users WHERE Username = @username";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;

                // Declaração dos parâmetros do comando SQL
                SqlParameter param = new SqlParameter("@username", username);

                // Introduzir valor ao parâmentro registado no comando SQL
                cmd.Parameters.Add(param);

                // Associar ligação à Base de Dados ao comando a ser executado
                cmd.Connection = conn;

                // Executar comando SQL
                SqlDataReader reader = cmd.ExecuteReader();

                if (!reader.HasRows)
                {
                    throw new Exception("Error while trying to access an user");
                }

                // Ler resultado da pesquisa
                reader.Read();

                // Obter Hash (password + salt)
                byte[] saltedPasswordHashStored = (byte[])reader["SaltedPasswordHash"];

                // Obter salt
                byte[] saltStored = (byte[])reader["Salt"];

                conn.Close();

                byte[] hash = GenerateSaltedHash(password, saltStored);
                return saltedPasswordHashStored.SequenceEqual(hash);

                //TODO: verificar se a password na base de dados 
                throw new NotImplementedException();
            }
            catch (Exception e)
            {
                MessageBox.Show("Erro ocurrido : " + e.Message);
                return false;
            }
        }

        private static byte[] GenerateSalt(int size)
        {
            //Generate a cryptographic random number.
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);
            return buff;
        }

        private static byte[] GenerateSaltedHash(string plainText, byte[] salt)
        {
            Rfc2898DeriveBytes rfc2898 = new Rfc2898DeriveBytes(plainText, salt, NUMBER_OF_ITERATIONS);
            return rfc2898.GetBytes(32);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            String password = txtPassword.Text;
            String username = txtUtilizador.Text;

            if (VerifyLogin(username, password))
            {
                MessageBox.Show("Utilizador Válido");
                userName = txtUtilizador.Text;
                Form3 form3 = new Form3();
                form3.Show();
            }
            else
            {
                MessageBox.Show("Utilizador não válido");
            }
            clearTextBox();
        }

        private void clearTextBox()
        {
            txtPassword.Text = "";
            txtUtilizador.Text = "";
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void label3_Click(object sender, EventArgs e)
        {
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
    }
}
