namespace PVe
{
    partial class frmCartao
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCartao));
            this.label3 = new System.Windows.Forms.Label();
            this.txtCartao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gdvCartao = new System.Windows.Forms.DataGridView();
            this.CARTAO_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRAZO_PGTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOME_CARTAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtData = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.gdvCartao)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(15, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 18);
            this.label3.TabIndex = 12;
            this.label3.Text = "Prazo:";
            // 
            // txtCartao
            // 
            this.txtCartao.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCartao.Location = new System.Drawing.Point(81, 39);
            this.txtCartao.Name = "txtCartao";
            this.txtCartao.Size = new System.Drawing.Size(269, 24);
            this.txtCartao.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(15, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 18);
            this.label2.TabIndex = 10;
            this.label2.Text = "Cartão:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(81, 6);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 24);
            this.txtCodigo.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(15, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 8;
            this.label1.Text = "Código:";
            // 
            // gdvCartao
            // 
            this.gdvCartao.AllowUserToAddRows = false;
            this.gdvCartao.AllowUserToDeleteRows = false;
            this.gdvCartao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvCartao.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CARTAO_ID,
            this.PRAZO_PGTO,
            this.NOME_CARTAO});
            this.gdvCartao.Location = new System.Drawing.Point(6, 105);
            this.gdvCartao.Name = "gdvCartao";
            this.gdvCartao.ReadOnly = true;
            this.gdvCartao.Size = new System.Drawing.Size(344, 150);
            this.gdvCartao.TabIndex = 9;
            // 
            // CARTAO_ID
            // 
            this.CARTAO_ID.DataPropertyName = "CARTAO_ID";
            this.CARTAO_ID.HeaderText = "CÓDIGO CARTÃO";
            this.CARTAO_ID.Name = "CARTAO_ID";
            this.CARTAO_ID.ReadOnly = true;
            this.CARTAO_ID.Width = 130;
            // 
            // PRAZO_PGTO
            // 
            this.PRAZO_PGTO.DataPropertyName = "PRAZO_PGTO";
            this.PRAZO_PGTO.HeaderText = "PRAZO PGTO";
            this.PRAZO_PGTO.Name = "PRAZO_PGTO";
            this.PRAZO_PGTO.ReadOnly = true;
            // 
            // NOME_CARTAO
            // 
            this.NOME_CARTAO.DataPropertyName = "NOME_CARTAO";
            this.NOME_CARTAO.HeaderText = "NOME CARTÃO";
            this.NOME_CARTAO.Name = "NOME_CARTAO";
            this.NOME_CARTAO.ReadOnly = true;
            this.NOME_CARTAO.Width = 130;
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(81, 72);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(269, 20);
            this.txtData.TabIndex = 13;
            // 
            // frmCartao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(359, 267);
            this.Controls.Add(this.txtData);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCartao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gdvCartao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCartao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tela Cadastro Cartao";
            this.Load += new System.EventHandler(this.frmCartao_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gdvCartao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtCartao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView gdvCartao;
        private System.Windows.Forms.DateTimePicker txtData;
        private System.Windows.Forms.DataGridViewTextBoxColumn CARTAO_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRAZO_PGTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOME_CARTAO;
    }
}