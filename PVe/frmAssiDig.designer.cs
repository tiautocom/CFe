namespace PVe
{
    partial class frmAssiDig
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
            this.txtAssinaturaDigital = new System.Windows.Forms.TextBox();
            this.btnAssiDigital = new System.Windows.Forms.Button();
            this.btnCodAtivacao = new System.Windows.Forms.Button();
            this.txtCodAtivavao = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtAssinaturaDigital
            // 
            this.txtAssinaturaDigital.Location = new System.Drawing.Point(12, 12);
            this.txtAssinaturaDigital.MaxLength = 344;
            this.txtAssinaturaDigital.Name = "txtAssinaturaDigital";
            this.txtAssinaturaDigital.Size = new System.Drawing.Size(1030, 20);
            this.txtAssinaturaDigital.TabIndex = 0;
            // 
            // btnAssiDigital
            // 
            this.btnAssiDigital.Location = new System.Drawing.Point(399, 47);
            this.btnAssiDigital.Name = "btnAssiDigital";
            this.btnAssiDigital.Size = new System.Drawing.Size(299, 23);
            this.btnAssiDigital.TabIndex = 1;
            this.btnAssiDigital.Text = "Salvar Assinatura Digital";
            this.btnAssiDigital.UseVisualStyleBackColor = true;
            this.btnAssiDigital.Click += new System.EventHandler(this.btnAssiDigital_Click);
            // 
            // btnCodAtivacao
            // 
            this.btnCodAtivacao.Location = new System.Drawing.Point(41, 128);
            this.btnCodAtivacao.Name = "btnCodAtivacao";
            this.btnCodAtivacao.Size = new System.Drawing.Size(131, 23);
            this.btnCodAtivacao.TabIndex = 5;
            this.btnCodAtivacao.Text = "Gerar Cod. ATivação";
            this.btnCodAtivacao.UseVisualStyleBackColor = true;
            this.btnCodAtivacao.Click += new System.EventHandler(this.btnCodAtivacao_Click);
            // 
            // txtCodAtivavao
            // 
            this.txtCodAtivavao.Location = new System.Drawing.Point(13, 102);
            this.txtCodAtivavao.MaxLength = 344;
            this.txtCodAtivavao.Name = "txtCodAtivavao";
            this.txtCodAtivavao.Size = new System.Drawing.Size(201, 20);
            this.txtCodAtivavao.TabIndex = 4;
            // 
            // frmAssiDig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1047, 179);
            this.Controls.Add(this.btnCodAtivacao);
            this.Controls.Add(this.txtCodAtivavao);
            this.Controls.Add(this.btnAssiDigital);
            this.Controls.Add(this.txtAssinaturaDigital);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAssiDig";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Formulário Assinatura Digital";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtAssinaturaDigital;
        private System.Windows.Forms.Button btnAssiDigital;
        private System.Windows.Forms.Button btnCodAtivacao;
        private System.Windows.Forms.TextBox txtCodAtivavao;
    }
}