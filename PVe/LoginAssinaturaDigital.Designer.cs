namespace PVe
{
    partial class LoginAssinaturaDigital
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
            this.btnSair = new System.Windows.Forms.Button();
            this.btnLogar = new System.Windows.Forms.Button();
            this.txtAcessoAssinaturaDigital = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnSair
            // 
            this.btnSair.Location = new System.Drawing.Point(8, 56);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(75, 23);
            this.btnSair.TabIndex = 8;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = true;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnLogar
            // 
            this.btnLogar.Location = new System.Drawing.Point(131, 56);
            this.btnLogar.Name = "btnLogar";
            this.btnLogar.Size = new System.Drawing.Size(75, 23);
            this.btnLogar.TabIndex = 7;
            this.btnLogar.Text = "Logar";
            this.btnLogar.UseVisualStyleBackColor = true;
            this.btnLogar.Click += new System.EventHandler(this.btnLogar_Click);
            // 
            // txtAcessoAssinaturaDigital
            // 
            this.txtAcessoAssinaturaDigital.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcessoAssinaturaDigital.Location = new System.Drawing.Point(3, 3);
            this.txtAcessoAssinaturaDigital.MaxLength = 10;
            this.txtAcessoAssinaturaDigital.Name = "txtAcessoAssinaturaDigital";
            this.txtAcessoAssinaturaDigital.PasswordChar = '*';
            this.txtAcessoAssinaturaDigital.Size = new System.Drawing.Size(213, 26);
            this.txtAcessoAssinaturaDigital.TabIndex = 6;
            this.txtAcessoAssinaturaDigital.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtAcessoPrincipal_KeyPress);
            // 
            // LoginAssinaturaDigital
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(218, 85);
            this.Controls.Add(this.btnSair);
            this.Controls.Add(this.btnLogar);
            this.Controls.Add(this.txtAcessoAssinaturaDigital);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LoginAssinaturaDigital";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login Assinatura Digital";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btnLogar;
        private System.Windows.Forms.TextBox txtAcessoAssinaturaDigital;
    }
}