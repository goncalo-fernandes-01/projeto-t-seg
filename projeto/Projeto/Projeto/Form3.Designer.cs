namespace Projeto
{
    partial class Form3
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
            this.btnEnviar = new System.Windows.Forms.Button();
            this.txtMensagem = new System.Windows.Forms.TextBox();
            this.txtvUtilizador = new System.Windows.Forms.Label();
            this.lbChat = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnEnviar
            // 
            this.btnEnviar.Location = new System.Drawing.Point(400, 141);
            this.btnEnviar.Name = "btnEnviar";
            this.btnEnviar.Size = new System.Drawing.Size(89, 33);
            this.btnEnviar.TabIndex = 7;
            this.btnEnviar.Text = "Enviar";
            this.btnEnviar.UseVisualStyleBackColor = true;
            this.btnEnviar.Click += new System.EventHandler(this.btnEnviar_Click);
            // 
            // txtMensagem
            // 
            this.txtMensagem.Location = new System.Drawing.Point(400, 74);
            this.txtMensagem.Multiline = true;
            this.txtMensagem.Name = "txtMensagem";
            this.txtMensagem.Size = new System.Drawing.Size(369, 46);
            this.txtMensagem.TabIndex = 6;
            // 
            // txtvUtilizador
            // 
            this.txtvUtilizador.AutoSize = true;
            this.txtvUtilizador.Location = new System.Drawing.Point(397, 39);
            this.txtvUtilizador.Name = "txtvUtilizador";
            this.txtvUtilizador.Size = new System.Drawing.Size(108, 17);
            this.txtvUtilizador.TabIndex = 5;
            this.txtvUtilizador.Text = "Nome Utilizador";
            // 
            // lbChat
            // 
            this.lbChat.BackColor = System.Drawing.Color.White;
            this.lbChat.Location = new System.Drawing.Point(12, 39);
            this.lbChat.Multiline = true;
            this.lbChat.Name = "lbChat";
            this.lbChat.ReadOnly = true;
            this.lbChat.Size = new System.Drawing.Size(340, 386);
            this.lbChat.TabIndex = 9;
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lbChat);
            this.Controls.Add(this.btnEnviar);
            this.Controls.Add(this.txtMensagem);
            this.Controls.Add(this.txtvUtilizador);
            this.Name = "Form3";
            this.Text = "Form3";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form3_FormClosing);
            this.Load += new System.EventHandler(this.Form3_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnEnviar;
        private System.Windows.Forms.TextBox txtMensagem;
        private System.Windows.Forms.Label txtvUtilizador;
        private System.Windows.Forms.TextBox lbChat;
    }
}