namespace PVe
{
    partial class frmProduto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmProduto));
            this.gdvProduto = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDescricao = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCodigoBarra = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotalProduto = new System.Windows.Forms.ToolStripStatusLabel();
            this.COD_BARRA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COD_INT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUM_DEPAR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DEPARTAM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRICAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UNID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRECO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESC = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TRIB = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.REDUCAO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ESTOQUE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EST_MIN = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TECLA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GRANEL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STRIB_A = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.STRIB_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ATUALIZA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EMBAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTO_CX = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LIXO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALIDADE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_AJUSTE = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TECLA_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SETOR = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.INDIC_B = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MARGEM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QT_COM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DT_COM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALOR_PIS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.VALOR_CONFINS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CST_PIS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CFOP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CST_COFINS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ORIGEM_PRODUTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ICMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ICMS_CST = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NCM = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gdvProduto)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gdvProduto
            // 
            this.gdvProduto.AllowUserToAddRows = false;
            this.gdvProduto.AllowUserToDeleteRows = false;
            this.gdvProduto.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gdvProduto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvProduto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.COD_BARRA,
            this.COD_INT,
            this.NUM_DEPAR,
            this.DEPARTAM,
            this.DESCRICAO,
            this.UNID,
            this.PRECO,
            this.DESC,
            this.TRIB,
            this.REDUCAO,
            this.ESTOQUE,
            this.EST_MIN,
            this.TECLA,
            this.GRANEL,
            this.STRIB_A,
            this.STRIB_B,
            this.CUSTO,
            this.ATUALIZA,
            this.EMBAL,
            this.CUSTO_CX,
            this.LIXO,
            this.VALIDADE,
            this.DT_AJUSTE,
            this.TECLA_B,
            this.SETOR,
            this.INDIC_B,
            this.MARGEM,
            this.QT_COM,
            this.DT_COM,
            this.VALOR_PIS,
            this.VALOR_CONFINS,
            this.CST_PIS,
            this.CFOP,
            this.CST_COFINS,
            this.ORIGEM_PRODUTO,
            this.ICMS,
            this.ICMS_CST,
            this.NCM});
            this.gdvProduto.GridColor = System.Drawing.SystemColors.ButtonFace;
            this.gdvProduto.Location = new System.Drawing.Point(5, 157);
            this.gdvProduto.MultiSelect = false;
            this.gdvProduto.Name = "gdvProduto";
            this.gdvProduto.ReadOnly = true;
            this.gdvProduto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gdvProduto.Size = new System.Drawing.Size(823, 350);
            this.gdvProduto.TabIndex = 1;
            this.gdvProduto.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdvProduto_CellDoubleClick);
            this.gdvProduto.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gdvProduto_KeyDown);
            this.gdvProduto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gdvProduto_KeyUp);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtDescricao);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCodigoBarra);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 28);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(823, 123);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Pesquisa";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(103, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Descrição";
            // 
            // txtDescricao
            // 
            this.txtDescricao.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtDescricao.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescricao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtDescricao.Location = new System.Drawing.Point(6, 61);
            this.txtDescricao.Name = "txtDescricao";
            this.txtDescricao.Size = new System.Drawing.Size(344, 35);
            this.txtDescricao.TabIndex = 0;
            this.txtDescricao.WordWrap = false;
            this.txtDescricao.TextChanged += new System.EventHandler(this.txtDescricao_TextChanged);
            this.txtDescricao.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDescricao_KeyUp);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(469, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(162, 24);
            this.label2.TabIndex = 4;
            this.label2.Text = "Código de Barra";
            // 
            // txtCodigoBarra
            // 
            this.txtCodigoBarra.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoBarra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtCodigoBarra.Location = new System.Drawing.Point(473, 59);
            this.txtCodigoBarra.Name = "txtCodigoBarra";
            this.txtCodigoBarra.Size = new System.Drawing.Size(344, 35);
            this.txtCodigoBarra.TabIndex = 1;
            this.txtCodigoBarra.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigoBarra_KeyUp);
            this.txtCodigoBarra.Layout += new System.Windows.Forms.LayoutEventHandler(this.txtCodigoBarra_Layout);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblTotalProduto});
            this.statusStrip1.Location = new System.Drawing.Point(0, 516);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(835, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(159, 17);
            this.toolStripStatusLabel1.Text = "Total Produtos Cadastrados: ";
            // 
            // lblTotalProduto
            // 
            this.lblTotalProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTotalProduto.Name = "lblTotalProduto";
            this.lblTotalProduto.Size = new System.Drawing.Size(13, 17);
            this.lblTotalProduto.Text = "0";
            // 
            // COD_BARRA
            // 
            this.COD_BARRA.DataPropertyName = "COD_BARRA";
            this.COD_BARRA.HeaderText = "Código Barra";
            this.COD_BARRA.Name = "COD_BARRA";
            this.COD_BARRA.ReadOnly = true;
            this.COD_BARRA.Width = 200;
            // 
            // COD_INT
            // 
            this.COD_INT.DataPropertyName = "COD_INT";
            this.COD_INT.HeaderText = "COD_INT";
            this.COD_INT.Name = "COD_INT";
            this.COD_INT.ReadOnly = true;
            this.COD_INT.Visible = false;
            // 
            // NUM_DEPAR
            // 
            this.NUM_DEPAR.DataPropertyName = "NUM_DEPAR";
            this.NUM_DEPAR.HeaderText = "NUM_DEPAR";
            this.NUM_DEPAR.Name = "NUM_DEPAR";
            this.NUM_DEPAR.ReadOnly = true;
            this.NUM_DEPAR.Visible = false;
            // 
            // DEPARTAM
            // 
            this.DEPARTAM.DataPropertyName = "DEPARTAM";
            this.DEPARTAM.HeaderText = "DEPARTAM";
            this.DEPARTAM.Name = "DEPARTAM";
            this.DEPARTAM.ReadOnly = true;
            this.DEPARTAM.Visible = false;
            // 
            // DESCRICAO
            // 
            this.DESCRICAO.DataPropertyName = "DESCRICAO";
            this.DESCRICAO.HeaderText = "Descrição";
            this.DESCRICAO.Name = "DESCRICAO";
            this.DESCRICAO.ReadOnly = true;
            this.DESCRICAO.Width = 300;
            // 
            // UNID
            // 
            this.UNID.DataPropertyName = "UNID";
            this.UNID.HeaderText = "Unid";
            this.UNID.Name = "UNID";
            this.UNID.ReadOnly = true;
            this.UNID.Width = 50;
            // 
            // PRECO
            // 
            this.PRECO.DataPropertyName = "PRECO";
            this.PRECO.HeaderText = "Preço";
            this.PRECO.Name = "PRECO";
            this.PRECO.ReadOnly = true;
            this.PRECO.Width = 150;
            // 
            // DESC
            // 
            this.DESC.DataPropertyName = "DESC";
            this.DESC.HeaderText = "DESC";
            this.DESC.Name = "DESC";
            this.DESC.ReadOnly = true;
            this.DESC.Visible = false;
            // 
            // TRIB
            // 
            this.TRIB.DataPropertyName = "TRIB";
            this.TRIB.HeaderText = "TRIB";
            this.TRIB.Name = "TRIB";
            this.TRIB.ReadOnly = true;
            this.TRIB.Visible = false;
            // 
            // REDUCAO
            // 
            this.REDUCAO.DataPropertyName = "REDUCAO";
            this.REDUCAO.HeaderText = "REDUCAO";
            this.REDUCAO.Name = "REDUCAO";
            this.REDUCAO.ReadOnly = true;
            this.REDUCAO.Visible = false;
            // 
            // ESTOQUE
            // 
            this.ESTOQUE.DataPropertyName = "ESTOQUE";
            this.ESTOQUE.HeaderText = "Estoque";
            this.ESTOQUE.Name = "ESTOQUE";
            this.ESTOQUE.ReadOnly = true;
            this.ESTOQUE.Width = 60;
            // 
            // EST_MIN
            // 
            this.EST_MIN.DataPropertyName = "EST_MIN";
            this.EST_MIN.HeaderText = "EST_MIN";
            this.EST_MIN.Name = "EST_MIN";
            this.EST_MIN.ReadOnly = true;
            this.EST_MIN.Visible = false;
            // 
            // TECLA
            // 
            this.TECLA.DataPropertyName = "TECLA";
            this.TECLA.HeaderText = "TECLA";
            this.TECLA.Name = "TECLA";
            this.TECLA.ReadOnly = true;
            this.TECLA.Visible = false;
            // 
            // GRANEL
            // 
            this.GRANEL.DataPropertyName = "GRANEL";
            this.GRANEL.HeaderText = "GRANEL";
            this.GRANEL.Name = "GRANEL";
            this.GRANEL.ReadOnly = true;
            this.GRANEL.Visible = false;
            // 
            // STRIB_A
            // 
            this.STRIB_A.DataPropertyName = "STRIB_A";
            this.STRIB_A.HeaderText = "STRIB_A";
            this.STRIB_A.Name = "STRIB_A";
            this.STRIB_A.ReadOnly = true;
            this.STRIB_A.Visible = false;
            // 
            // STRIB_B
            // 
            this.STRIB_B.DataPropertyName = "STRIB_B";
            this.STRIB_B.HeaderText = "STRIB_B";
            this.STRIB_B.Name = "STRIB_B";
            this.STRIB_B.ReadOnly = true;
            this.STRIB_B.Visible = false;
            // 
            // CUSTO
            // 
            this.CUSTO.DataPropertyName = "CUSTO";
            this.CUSTO.HeaderText = "CUSTO";
            this.CUSTO.Name = "CUSTO";
            this.CUSTO.ReadOnly = true;
            this.CUSTO.Visible = false;
            // 
            // ATUALIZA
            // 
            this.ATUALIZA.DataPropertyName = "ATUALIZA";
            this.ATUALIZA.HeaderText = "ATUALIZA";
            this.ATUALIZA.Name = "ATUALIZA";
            this.ATUALIZA.ReadOnly = true;
            this.ATUALIZA.Visible = false;
            // 
            // EMBAL
            // 
            this.EMBAL.DataPropertyName = "EMBAL";
            this.EMBAL.HeaderText = "EMBAL";
            this.EMBAL.Name = "EMBAL";
            this.EMBAL.ReadOnly = true;
            this.EMBAL.Visible = false;
            // 
            // CUSTO_CX
            // 
            this.CUSTO_CX.DataPropertyName = "CUSTO_CX";
            this.CUSTO_CX.HeaderText = "CUSTO_CX";
            this.CUSTO_CX.Name = "CUSTO_CX";
            this.CUSTO_CX.ReadOnly = true;
            this.CUSTO_CX.Visible = false;
            // 
            // LIXO
            // 
            this.LIXO.DataPropertyName = "LIXO";
            this.LIXO.HeaderText = "LIXO";
            this.LIXO.Name = "LIXO";
            this.LIXO.ReadOnly = true;
            this.LIXO.Visible = false;
            // 
            // VALIDADE
            // 
            this.VALIDADE.DataPropertyName = "VALIDADE";
            this.VALIDADE.HeaderText = "VALIDADE";
            this.VALIDADE.Name = "VALIDADE";
            this.VALIDADE.ReadOnly = true;
            this.VALIDADE.Visible = false;
            // 
            // DT_AJUSTE
            // 
            this.DT_AJUSTE.DataPropertyName = "DT_AJUSTE";
            this.DT_AJUSTE.HeaderText = "DT_AJUSTE";
            this.DT_AJUSTE.Name = "DT_AJUSTE";
            this.DT_AJUSTE.ReadOnly = true;
            this.DT_AJUSTE.Visible = false;
            // 
            // TECLA_B
            // 
            this.TECLA_B.DataPropertyName = "TECLA_B";
            this.TECLA_B.HeaderText = "TECLA_B";
            this.TECLA_B.Name = "TECLA_B";
            this.TECLA_B.ReadOnly = true;
            this.TECLA_B.Visible = false;
            // 
            // SETOR
            // 
            this.SETOR.DataPropertyName = "SETOR";
            this.SETOR.HeaderText = "SETOR";
            this.SETOR.Name = "SETOR";
            this.SETOR.ReadOnly = true;
            this.SETOR.Visible = false;
            // 
            // INDIC_B
            // 
            this.INDIC_B.DataPropertyName = "INDIC_B";
            this.INDIC_B.HeaderText = "INDIC_B";
            this.INDIC_B.Name = "INDIC_B";
            this.INDIC_B.ReadOnly = true;
            this.INDIC_B.Visible = false;
            // 
            // MARGEM
            // 
            this.MARGEM.DataPropertyName = "MARGEM";
            this.MARGEM.HeaderText = "MARGEM";
            this.MARGEM.Name = "MARGEM";
            this.MARGEM.ReadOnly = true;
            this.MARGEM.Visible = false;
            // 
            // QT_COM
            // 
            this.QT_COM.DataPropertyName = "QT_COM";
            this.QT_COM.HeaderText = "QT_COM";
            this.QT_COM.Name = "QT_COM";
            this.QT_COM.ReadOnly = true;
            this.QT_COM.Visible = false;
            // 
            // DT_COM
            // 
            this.DT_COM.DataPropertyName = "DT_COM";
            this.DT_COM.HeaderText = "DT_COM";
            this.DT_COM.Name = "DT_COM";
            this.DT_COM.ReadOnly = true;
            this.DT_COM.Visible = false;
            // 
            // VALOR_PIS
            // 
            this.VALOR_PIS.DataPropertyName = "VALOR_PIS";
            this.VALOR_PIS.HeaderText = "VALOR_PIS";
            this.VALOR_PIS.Name = "VALOR_PIS";
            this.VALOR_PIS.ReadOnly = true;
            this.VALOR_PIS.Visible = false;
            // 
            // VALOR_CONFINS
            // 
            this.VALOR_CONFINS.DataPropertyName = "VALOR_CONFINS";
            this.VALOR_CONFINS.HeaderText = "VALOR_CONFINS";
            this.VALOR_CONFINS.Name = "VALOR_CONFINS";
            this.VALOR_CONFINS.ReadOnly = true;
            this.VALOR_CONFINS.Visible = false;
            // 
            // CST_PIS
            // 
            this.CST_PIS.DataPropertyName = "CST_PIS";
            this.CST_PIS.HeaderText = "CST_PIS";
            this.CST_PIS.Name = "CST_PIS";
            this.CST_PIS.ReadOnly = true;
            this.CST_PIS.Visible = false;
            // 
            // CFOP
            // 
            this.CFOP.DataPropertyName = "CFOP";
            this.CFOP.HeaderText = "CFOP";
            this.CFOP.Name = "CFOP";
            this.CFOP.ReadOnly = true;
            this.CFOP.Visible = false;
            // 
            // CST_COFINS
            // 
            this.CST_COFINS.DataPropertyName = "CST_COFINS";
            this.CST_COFINS.HeaderText = "CST_COFINS";
            this.CST_COFINS.Name = "CST_COFINS";
            this.CST_COFINS.ReadOnly = true;
            this.CST_COFINS.Visible = false;
            // 
            // ORIGEM_PRODUTO
            // 
            this.ORIGEM_PRODUTO.DataPropertyName = "ORIGEM_PRODUTO";
            this.ORIGEM_PRODUTO.HeaderText = "ORIGEM_PRODUTO";
            this.ORIGEM_PRODUTO.Name = "ORIGEM_PRODUTO";
            this.ORIGEM_PRODUTO.ReadOnly = true;
            this.ORIGEM_PRODUTO.Visible = false;
            // 
            // ICMS
            // 
            this.ICMS.DataPropertyName = "ICMS";
            this.ICMS.HeaderText = "ICMS";
            this.ICMS.Name = "ICMS";
            this.ICMS.ReadOnly = true;
            this.ICMS.Visible = false;
            // 
            // ICMS_CST
            // 
            this.ICMS_CST.DataPropertyName = "ICMS_CST";
            this.ICMS_CST.HeaderText = "ICMS_CST";
            this.ICMS_CST.Name = "ICMS_CST";
            this.ICMS_CST.ReadOnly = true;
            this.ICMS_CST.Visible = false;
            // 
            // NCM
            // 
            this.NCM.DataPropertyName = "NCM";
            this.NCM.HeaderText = "NCM";
            this.NCM.Name = "NCM";
            this.NCM.ReadOnly = true;
            this.NCM.Visible = false;
            // 
            // frmProduto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(835, 538);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gdvProduto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmProduto";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Pesquisar Produto";
            this.Load += new System.EventHandler(this.frmProduto_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmProduto_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.gdvProduto)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView gdvProduto;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDescricao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCodigoBarra;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblTotalProduto;
        private System.Windows.Forms.DataGridViewTextBoxColumn COD_BARRA;
        private System.Windows.Forms.DataGridViewTextBoxColumn COD_INT;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUM_DEPAR;
        private System.Windows.Forms.DataGridViewTextBoxColumn DEPARTAM;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRICAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn UNID;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRECO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESC;
        private System.Windows.Forms.DataGridViewTextBoxColumn TRIB;
        private System.Windows.Forms.DataGridViewTextBoxColumn REDUCAO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ESTOQUE;
        private System.Windows.Forms.DataGridViewTextBoxColumn EST_MIN;
        private System.Windows.Forms.DataGridViewTextBoxColumn TECLA;
        private System.Windows.Forms.DataGridViewTextBoxColumn GRANEL;
        private System.Windows.Forms.DataGridViewTextBoxColumn STRIB_A;
        private System.Windows.Forms.DataGridViewTextBoxColumn STRIB_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ATUALIZA;
        private System.Windows.Forms.DataGridViewTextBoxColumn EMBAL;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTO_CX;
        private System.Windows.Forms.DataGridViewTextBoxColumn LIXO;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALIDADE;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_AJUSTE;
        private System.Windows.Forms.DataGridViewTextBoxColumn TECLA_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn SETOR;
        private System.Windows.Forms.DataGridViewTextBoxColumn INDIC_B;
        private System.Windows.Forms.DataGridViewTextBoxColumn MARGEM;
        private System.Windows.Forms.DataGridViewTextBoxColumn QT_COM;
        private System.Windows.Forms.DataGridViewTextBoxColumn DT_COM;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALOR_PIS;
        private System.Windows.Forms.DataGridViewTextBoxColumn VALOR_CONFINS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CST_PIS;
        private System.Windows.Forms.DataGridViewTextBoxColumn CFOP;
        private System.Windows.Forms.DataGridViewTextBoxColumn CST_COFINS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ORIGEM_PRODUTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn ICMS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ICMS_CST;
        private System.Windows.Forms.DataGridViewTextBoxColumn NCM;


    }
}