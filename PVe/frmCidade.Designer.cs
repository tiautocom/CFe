namespace PVe
{
    partial class frmCidade
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCidade));
            this.gdvCidade = new System.Windows.Forms.DataGridView();
            this.CIDADE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IBGE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtCidade = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtIbge = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gdvCidade)).BeginInit();
            this.SuspendLayout();
            // 
            // gdvCidade
            // 
            this.gdvCidade.AllowUserToAddRows = false;
            this.gdvCidade.AllowUserToDeleteRows = false;
            this.gdvCidade.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvCidade.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CIDADE_ID,
            this.CIDADE,
            this.IBGE});
            this.gdvCidade.Location = new System.Drawing.Point(3, 105);
            this.gdvCidade.Name = "gdvCidade";
            this.gdvCidade.ReadOnly = true;
            this.gdvCidade.Size = new System.Drawing.Size(344, 150);
            this.gdvCidade.TabIndex = 2;
            // 
            // CIDADE_ID
            // 
            this.CIDADE_ID.DataPropertyName = "CIDADE_ID";
            this.CIDADE_ID.HeaderText = "CÓDIGO CIDADE";
            this.CIDADE_ID.Name = "CIDADE_ID";
            this.CIDADE_ID.ReadOnly = true;
            this.CIDADE_ID.Width = 120;
            // 
            // CIDADE
            // 
            this.CIDADE.DataPropertyName = "CIDADE";
            this.CIDADE.HeaderText = "CIDADE";
            this.CIDADE.Name = "CIDADE";
            this.CIDADE.ReadOnly = true;
            this.CIDADE.Width = 130;
            // 
            // IBGE
            // 
            this.IBGE.DataPropertyName = "IBGE";
            this.IBGE.HeaderText = "IBGE";
            this.IBGE.Name = "IBGE";
            this.IBGE.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 18);
            this.label1.TabIndex = 1;
            this.label1.Text = "Código:";
            // 
            // txtCodigo
            // 
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.Location = new System.Drawing.Point(78, 6);
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(100, 24);
            this.txtCodigo.TabIndex = 3;
            // 
            // txtCidade
            // 
            this.txtCidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCidade.Location = new System.Drawing.Point(78, 39);
            this.txtCidade.Name = "txtCidade";
            this.txtCidade.Size = new System.Drawing.Size(269, 24);
            this.txtCidade.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Cidade:";
            // 
            // txtIbge
            // 
            this.txtIbge.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIbge.Location = new System.Drawing.Point(78, 75);
            this.txtIbge.Name = "txtIbge";
            this.txtIbge.Size = new System.Drawing.Size(269, 24);
            this.txtIbge.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 75);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 18);
            this.label3.TabIndex = 5;
            this.label3.Text = "IBGE:";
            // 
            // frmCidade
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(353, 262);
            this.Controls.Add(this.txtIbge);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtCidade);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gdvCidade);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmCidade";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tela de Cadastro Cidade";
            this.Load += new System.EventHandler(this.frmCidade_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gdvCidade)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gdvCidade;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtCidade;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtIbge;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIDADE_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn CIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn IBGE;
    }
}