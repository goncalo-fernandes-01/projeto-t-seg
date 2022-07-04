namespace Projeto
{
    partial class Form2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.txtCriarPassword = new System.Windows.Forms.TextBox();
            this.txtCriarUtilizador = new System.Windows.Forms.TextBox();
            this.btnRegistar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(524, 145);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 16);
            this.label1.TabIndex = 24;
            this.label1.Text = "Já tenho conta.";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // txtCriarPassword
            // 
            this.txtCriarPassword.Location = new System.Drawing.Point(330, 136);
            this.txtCriarPassword.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCriarPassword.Name = "txtCriarPassword";
            this.txtCriarPassword.Size = new System.Drawing.Size(159, 22);
            this.txtCriarPassword.TabIndex = 22;
            // 
            // txtCriarUtilizador
            // 
            this.txtCriarUtilizador.Location = new System.Drawing.Point(330, 80);
            this.txtCriarUtilizador.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCriarUtilizador.Name = "txtCriarUtilizador";
            this.txtCriarUtilizador.Size = new System.Drawing.Size(159, 22);
            this.txtCriarUtilizador.TabIndex = 21;
            // 
            // btnRegistar
            // 
            this.btnRegistar.Location = new System.Drawing.Point(524, 93);
            this.btnRegistar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnRegistar.Name = "btnRegistar";
            this.btnRegistar.Size = new System.Drawing.Size(80, 46);
            this.btnRegistar.TabIndex = 20;
            this.btnRegistar.Text = "&Registar";
            this.btnRegistar.UseVisualStyleBackColor = true;
            this.btnRegistar.Click += new System.EventHandler(this.btnRegistar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(253, 136);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 16);
            this.label4.TabIndex = 19;
            this.label4.Text = "Password:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(253, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 16);
            this.label3.TabIndex = 18;
            this.label3.Text = "Utilizador:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(253, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(274, 16);
            this.label2.TabIndex = 17;
            this.label2.Text = "Bem Vindo!! Por favor crie um novo utilizador.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Projeto.Properties.Resources.login_icon1;
            this.pictureBox1.Location = new System.Drawing.Point(49, 11);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(172, 160);
            this.pictureBox1.TabIndex = 23;
            this.pictureBox1.TabStop = false;
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 196);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.txtCriarPassword);
            this.Controls.Add(this.txtCriarUtilizador);
            this.Controls.Add(this.btnRegistar);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Name = "Form2";
            this.Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox txtCriarPassword;
        private System.Windows.Forms.TextBox txtCriarUtilizador;
        private System.Windows.Forms.Button btnRegistar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
    }
}