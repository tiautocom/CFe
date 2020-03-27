namespace PVe
{
    partial class frmCliente
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
            this.gdvCliente = new System.Windows.Forms.DataGridView();
            this.CLIENTE_ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_END = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_FONE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NOME = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.APELIDO_FANTAZIA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CPF_CNPJ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.RG_INSC_EST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_CADASTRO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_NASC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BLOQ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LIMITE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GASTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalCliente = new System.Windows.Forms.ToolStripStatusLabel();
            this.txtPesquisa = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rbCnpj = new System.Windows.Forms.RadioButton();
            this.rbCpf = new System.Windows.Forms.RadioButton();
            this.rbNome = new System.Windows.Forms.RadioButton();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.gdvCliente)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gdvCliente
            // 
            this.gdvCliente.AllowUserToAddRows = false;
            this.gdvCliente.AllowUserToDeleteRows = false;
            this.gdvCliente.BackgroundColor = System.Drawing.Color.White;
            this.gdvCliente.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvCliente.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CLIENTE_ID,
            this.ID_END,
            this.ID_FONE,
            this.NOME,
            this.APELIDO_FANTAZIA,
            this.CPF_CNPJ,
            this.RG_INSC_EST,
            this.DT_CADASTRO,
            this.DT_NASC,
            this.BLOQ,
            this.LIMITE,
            this.GASTO});
            this.gdvCliente.Location = new System.Drawing.Point(12, 125);
            this.gdvCliente.MultiSelect = false;
            this.gdvCliente.Name = "gdvCliente";
            this.gdvCliente.ReadOnly = true;
            this.gdvCliente.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gdvCliente.Size = new System.Drawing.Size(646, 213);
            this.gdvCliente.TabIndex = 1;
            this.gdvCliente.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdvCliente_CellDoubleClick);
            this.gdvCliente.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gdvCliente_KeyDown);
            // 
            // CLIENTE_ID
            // 
            this.CLIENTE_ID.DataPropertyName = "CLIENTE_ID";
            this.CLIENTE_ID.HeaderText = "Codigo";
            this.CLIENTE_ID.Name = "CLIENTE_ID";
            this.CLIENTE_ID.ReadOnly = true;
            this.CLIENTE_ID.Visible = false;
            // 
            // ID_END
            // 
            this.ID_END.DataPropertyName = "ID_END";
            this.ID_END.HeaderText = "Codigo End.";
            this.ID_END.Name = "ID_END";
            this.ID_END.ReadOnly = true;
            this.ID_END.Visible = false;
            // 
            // ID_FONE
            // 
            this.ID_FONE.DataPropertyName = "ID_FONE";
            this.ID_FONE.HeaderText = "Codigo Tel.";
            this.ID_FONE.Name = "ID_FONE";
            this.ID_FONE.ReadOnly = true;
            this.ID_FONE.Visible = false;
            // 
            // NOME
            // 
            this.NOME.DataPropertyName = "NOME";
            this.NOME.HeaderText = "Nome";
            this.NOME.Name = "NOME";
            this.NOME.ReadOnly = true;
            this.NOME.Width = 200;
            // 
            // APELIDO_FANTAZIA
            // 
            this.APELIDO_FANTAZIA.DataPropertyName = "APELIDO_FANTAZIA";
            this.APELIDO_FANTAZIA.HeaderText = "Apelido/Fantasia";
            this.APELIDO_FANTAZIA.Name = "APELIDO_FANTAZIA";
            this.APELIDO_FANTAZIA.ReadOnly = true;
            this.APELIDO_FANTAZIA.Width = 200;
            // 
            // CPF_CNPJ
            // 
            this.CPF_CNPJ.DataPropertyName = "CPF_CNPJ";
            this.CPF_CNPJ.HeaderText = "CPF/CNPJ";
            this.CPF_CNPJ.Name = "CPF_CNPJ";
            this.CPF_CNPJ.ReadOnly = true;
            // 
            // RG_INSC_EST
            // 
            this.RG_INSC_EST.DataPropertyName = "RG_INSC_EST";
            this.RG_INSC_EST.HeaderText = "RG/IE";
            this.RG_INSC_EST.Name = "RG_INSC_EST";
            this.RG_INSC_EST.ReadOnly = true;
            // 
            // DT_CADASTRO
            // 
            this.DT_CADASTRO.DataPropertyName = "DT_CADASTRO";
            this.DT_CADASTRO.HeaderText = "DT_CADASTRO";
            this.DT_CADASTRO.Name = "DT_CADASTRO";
            this.DT_CADASTRO.ReadOnly = true;
            this.DT_CADASTRO.Visible = false;
            // 
            // DT_NASC
            // 
            this.DT_NASC.DataPropertyName = "DT_NASC";
            this.DT_NASC.HeaderText = "DT_NASC";
            this.DT_NASC.Name = "DT_NASC";
            this.DT_NASC.ReadOnly = true;
            this.DT_NASC.Visible = false;
            // 
            // BLOQ
            // 
            this.BLOQ.DataPropertyName = "BLOQ";
            this.BLOQ.HeaderText = "BLOQ";
            this.BLOQ.Name = "BLOQ";
            this.BLOQ.ReadOnly = true;
            this.BLOQ.Visible = false;
            // 
            // LIMITE
            // 
            this.LIMITE.DataPropertyName = "LIMITE";
            this.LIMITE.HeaderText = "LIMITE";
            this.LIMITE.Name = "LIMITE";
            this.LIMITE.ReadOnly = true;
            this.LIMITE.Visible = false;
            // 
            // GASTO
            // 
            this.GASTO.DataPropertyName = "GASTO";
            this.GASTO.HeaderText = "GASTO";
            this.GASTO.Name = "GASTO";
            this.GASTO.ReadOnly = true;
            this.GASTO.Visible = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblTotalCliente});
            this.statusStrip1.Location = new System.Drawing.Point(0, 352);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(670, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(93, 17);
            this.toolStripStatusLabel1.Text = "Total de Cliente:";
            // 
            // lblTotalCliente
            // 
            this.lblTotalCliente.Name = "lblTotalCliente";
            this.lblTotalCliente.Size = new System.Drawing.Size(13, 17);
            this.lblTotalCliente.Text = "0";
            // 
            // txtPesquisa
            // 
            this.txtPesquisa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPesquisa.Location = new System.Drawing.Point(13, 93);
            this.txtPesquisa.Name = "txtPesquisa";
            this.txtPesquisa.Size = new System.Drawing.Size(645, 29);
            this.txtPesquisa.TabIndex = 0;
            this.txtPesquisa.TextChanged += new System.EventHandler(this.txtPesquisa_TextChanged_1);
            this.txtPesquisa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPesquisa_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rbCnpj);
            this.groupBox1.Controls.Add(this.rbCpf);
            this.groupBox1.Controls.Add(this.rbNome);
            this.groupBox1.Location = new System.Drawing.Point(13, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(645, 62);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Pesquisas";
            // 
            // rbCnpj
            // 
            this.rbCnpj.AutoSize = true;
            this.rbCnpj.Location = new System.Drawing.Point(155, 31);
            this.rbCnpj.Name = "rbCnpj";
            this.rbCnpj.Size = new System.Drawing.Size(59, 17);
            this.rbCnpj.TabIndex = 2;
            this.rbCnpj.Text = "RG/IE:";
            this.rbCnpj.UseVisualStyleBackColor = true;
            this.rbCnpj.CheckedChanged += new System.EventHandler(this.rbCnpj_CheckedChanged);
            // 
            // rbCpf
            // 
            this.rbCpf.AutoSize = true;
            this.rbCpf.Location = new System.Drawing.Point(69, 31);
            this.rbCpf.Name = "rbCpf";
            this.rbCpf.Size = new System.Drawing.Size(80, 17);
            this.rbCpf.TabIndex = 1;
            this.rbCpf.Text = "CPF/CNPJ:";
            this.rbCpf.UseVisualStyleBackColor = true;
            this.rbCpf.CheckedChanged += new System.EventHandler(this.rbCpf_CheckedChanged);
            // 
            // rbNome
            // 
            this.rbNome.AutoSize = true;
            this.rbNome.Checked = true;
            this.rbNome.Location = new System.Drawing.Point(7, 31);
            this.rbNome.Name = "rbNome";
            this.rbNome.Size = new System.Drawing.Size(60, 17);
            this.rbNome.TabIndex = 0;
            this.rbNome.TabStop = true;
            this.rbNome.Text = "NOME:";
            this.rbNome.UseVisualStyleBackColor = true;
            this.rbNome.CheckedChanged += new System.EventHandler(this.rbNome_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 78);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "* Dê Duplo Click para Selecionar o Cliente.";
            // 
            // frmCliente
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(670, 374);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.gdvCliente);
            this.Controls.Add(this.txtPesquisa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmCliente";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Clientes";
            this.Load += new System.EventHandler(this.frmCliente_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gdvCliente)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gdvCliente;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalCliente;
        private System.Windows.Forms.DataGridViewTextBoxColumn CLIENTE_ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_END;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_FONE;
        private System.Windows.Forms.DataGridViewTextBoxColumn NOME;
        private System.Windows.Forms.DataGridViewTextBoxColumn APELIDO_FANTAZIA;
        private System.Windows.Forms.DataGridViewTextBoxColumn CPF_CNPJ;
        private System.Windows.Forms.DataGridViewTextBoxColumn RG_INSC_EST;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_CADASTRO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_NASC;
        private System.Windows.Forms.DataGridViewTextBoxColumn BLOQ;
        private System.Windows.Forms.DataGridViewTextBoxColumn LIMITE;
        private System.Windows.Forms.DataGridViewTextBoxColumn GASTO;
        private System.Windows.Forms.TextBox txtPesquisa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rbCnpj;
        private System.Windows.Forms.RadioButton rbCpf;
        private System.Windows.Forms.RadioButton rbNome;
        private System.Windows.Forms.Label label1;
    }
}