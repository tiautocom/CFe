namespace PVe
{
    partial class frmVenda
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVenda));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pbVenda = new System.Windows.Forms.PictureBox();
            this.gdvEntrada = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ITEMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.COD_BARRA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_PROD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DESCRICAO_PRODUTO_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UNID_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.QUANT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PRECO_ = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CUSTO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DATA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ATUALIZADOS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NUM_VENDA = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ALIQUOTAS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ID_USUARIO = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TOTAL = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.txtCodigo = new System.Windows.Forms.TextBox();
            this.txtProduto = new System.Windows.Forms.TextBox();
            this.txtValorTotal = new System.Windows.Forms.TextBox();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProduto = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumVendas = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumeroVenda = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblItem = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblItens = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumeroCaixa = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel8 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsNome = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel9 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel10 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPeriodo = new System.Windows.Forms.ToolStripStatusLabel();
            this.pb = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel11 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblImpreissora = new System.Windows.Forms.ToolStripStatusLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.lblData = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtSomas = new System.Windows.Forms.TextBox();
            this.btnCancelaItem = new System.Windows.Forms.Button();
            this.btnCancelaVenda = new System.Windows.Forms.Button();
            this.btnCancelarUltimaVenda = new System.Windows.Forms.Button();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnSangria = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.lblVersao = new System.Windows.Forms.Label();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.printDocument3 = new System.Drawing.Printing.PrintDocument();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbVenda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvEntrada)).BeginInit();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pbVenda);
            this.panel1.Controls.Add(this.gdvEntrada);
            this.panel1.Location = new System.Drawing.Point(482, 191);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(699, 342);
            this.panel1.TabIndex = 121;
            // 
            // pbVenda
            // 
            this.pbVenda.Location = new System.Drawing.Point(0, 18);
            this.pbVenda.Name = "pbVenda";
            this.pbVenda.Size = new System.Drawing.Size(692, 338);
            this.pbVenda.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbVenda.TabIndex = 129;
            this.pbVenda.TabStop = false;
            this.pbVenda.Visible = false;
            // 
            // gdvEntrada
            // 
            this.gdvEntrada.AllowUserToAddRows = false;
            this.gdvEntrada.AllowUserToDeleteRows = false;
            this.gdvEntrada.BackgroundColor = System.Drawing.SystemColors.ControlLightLight;
            this.gdvEntrada.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.gdvEntrada.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.None;
            this.gdvEntrada.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gdvEntrada.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.gdvEntrada.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.ITEMS,
            this.COD_BARRA,
            this.ID_PROD,
            this.DESCRICAO_PRODUTO_,
            this.UNID_,
            this.QUANT,
            this.PRECO_,
            this.CUSTO,
            this.DATA,
            this.ATUALIZADOS,
            this.NUM_VENDA,
            this.ALIQUOTAS,
            this.ID_USUARIO,
            this.TOTAL});
            this.gdvEntrada.Cursor = System.Windows.Forms.Cursors.Default;
            this.gdvEntrada.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gdvEntrada.Location = new System.Drawing.Point(0, 0);
            this.gdvEntrada.MultiSelect = false;
            this.gdvEntrada.Name = "gdvEntrada";
            this.gdvEntrada.ReadOnly = true;
            this.gdvEntrada.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.gdvEntrada.RowHeadersVisible = false;
            this.gdvEntrada.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.gdvEntrada.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gdvEntrada.Size = new System.Drawing.Size(695, 338);
            this.gdvEntrada.TabIndex = 0;
            this.gdvEntrada.TabStop = false;
            this.gdvEntrada.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdvEntrada_CellContentClick);
            this.gdvEntrada.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gdvEntrada_KeyUp);
            // 
            // ID
            // 
            this.ID.DataPropertyName = "ID";
            this.ID.Frozen = true;
            this.ID.HeaderText = "ID";
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Visible = false;
            this.ID.Width = 50;
            // 
            // ITEMS
            // 
            this.ITEMS.DataPropertyName = "ITEM";
            this.ITEMS.Frozen = true;
            this.ITEMS.HeaderText = "ITEM";
            this.ITEMS.Name = "ITEMS";
            this.ITEMS.ReadOnly = true;
            this.ITEMS.Width = 50;
            // 
            // COD_BARRA
            // 
            this.COD_BARRA.DataPropertyName = "COD_BARRA";
            this.COD_BARRA.Frozen = true;
            this.COD_BARRA.HeaderText = "CÓDIGO DE BARRA";
            this.COD_BARRA.Name = "COD_BARRA";
            this.COD_BARRA.ReadOnly = true;
            this.COD_BARRA.Width = 130;
            // 
            // ID_PROD
            // 
            this.ID_PROD.DataPropertyName = "ID_PROD";
            this.ID_PROD.Frozen = true;
            this.ID_PROD.HeaderText = "CÓDIGO PRODUTO";
            this.ID_PROD.Name = "ID_PROD";
            this.ID_PROD.ReadOnly = true;
            this.ID_PROD.Visible = false;
            // 
            // DESCRICAO_PRODUTO_
            // 
            this.DESCRICAO_PRODUTO_.DataPropertyName = "DESCRICAO_PRODUTO";
            this.DESCRICAO_PRODUTO_.HeaderText = "DESCRICAO";
            this.DESCRICAO_PRODUTO_.Name = "DESCRICAO_PRODUTO_";
            this.DESCRICAO_PRODUTO_.ReadOnly = true;
            this.DESCRICAO_PRODUTO_.Width = 190;
            // 
            // UNID_
            // 
            this.UNID_.DataPropertyName = "UNID";
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            this.UNID_.DefaultCellStyle = dataGridViewCellStyle1;
            this.UNID_.HeaderText = "UNID";
            this.UNID_.Name = "UNID_";
            this.UNID_.ReadOnly = true;
            this.UNID_.Width = 50;
            // 
            // QUANT
            // 
            this.QUANT.DataPropertyName = "QUANT";
            this.QUANT.HeaderText = "QTDE";
            this.QUANT.Name = "QUANT";
            this.QUANT.ReadOnly = true;
            this.QUANT.Width = 50;
            // 
            // PRECO_
            // 
            this.PRECO_.DataPropertyName = "PRECO";
            dataGridViewCellStyle2.Format = "N3";
            dataGridViewCellStyle2.NullValue = null;
            this.PRECO_.DefaultCellStyle = dataGridViewCellStyle2;
            this.PRECO_.HeaderText = "PREÇO";
            this.PRECO_.Name = "PRECO_";
            this.PRECO_.ReadOnly = true;
            // 
            // CUSTO
            // 
            this.CUSTO.DataPropertyName = "CUSTO";
            this.CUSTO.HeaderText = "$ CUSTO";
            this.CUSTO.Name = "CUSTO";
            this.CUSTO.ReadOnly = true;
            this.CUSTO.Visible = false;
            // 
            // DATA
            // 
            this.DATA.DataPropertyName = "DATA";
            this.DATA.HeaderText = "DATA";
            this.DATA.Name = "DATA";
            this.DATA.ReadOnly = true;
            this.DATA.Visible = false;
            // 
            // ATUALIZADOS
            // 
            this.ATUALIZADOS.DataPropertyName = "ATUALIZADO";
            this.ATUALIZADOS.HeaderText = "ATUALIZADO";
            this.ATUALIZADOS.Name = "ATUALIZADOS";
            this.ATUALIZADOS.ReadOnly = true;
            this.ATUALIZADOS.Visible = false;
            // 
            // NUM_VENDA
            // 
            this.NUM_VENDA.DataPropertyName = "NUM_VENDA";
            this.NUM_VENDA.HeaderText = "NUM_VENDA";
            this.NUM_VENDA.Name = "NUM_VENDA";
            this.NUM_VENDA.ReadOnly = true;
            this.NUM_VENDA.Visible = false;
            // 
            // ALIQUOTAS
            // 
            this.ALIQUOTAS.DataPropertyName = "ALIQUOTA";
            this.ALIQUOTAS.HeaderText = "ALIQUOTA";
            this.ALIQUOTAS.Name = "ALIQUOTAS";
            this.ALIQUOTAS.ReadOnly = true;
            this.ALIQUOTAS.Visible = false;
            // 
            // ID_USUARIO
            // 
            this.ID_USUARIO.DataPropertyName = "ID_USUARIO";
            this.ID_USUARIO.HeaderText = "ID_USUARIO";
            this.ID_USUARIO.Name = "ID_USUARIO";
            this.ID_USUARIO.ReadOnly = true;
            this.ID_USUARIO.Visible = false;
            // 
            // TOTAL
            // 
            this.TOTAL.DataPropertyName = "TOTAL";
            this.TOTAL.HeaderText = "TOTAL";
            this.TOTAL.Name = "TOTAL";
            this.TOTAL.ReadOnly = true;
            this.TOTAL.Width = 110;
            // 
            // txtPreco
            // 
            this.txtPreco.BackColor = System.Drawing.Color.MediumBlue;
            this.txtPreco.Enabled = false;
            this.txtPreco.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreco.ForeColor = System.Drawing.SystemColors.Info;
            this.txtPreco.Location = new System.Drawing.Point(81, 375);
            this.txtPreco.MaxLength = 6;
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(390, 67);
            this.txtPreco.TabIndex = 2;
            this.txtPreco.Text = "0,00";
            this.txtPreco.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtPreco.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPreco_KeyDown);
            this.txtPreco.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPreco_KeyUp);
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.BackColor = System.Drawing.Color.MediumBlue;
            this.txtQuantidade.Enabled = false;
            this.txtQuantidade.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQuantidade.ForeColor = System.Drawing.SystemColors.Info;
            this.txtQuantidade.Location = new System.Drawing.Point(81, 281);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(390, 67);
            this.txtQuantidade.TabIndex = 1;
            this.txtQuantidade.Text = "1,00";
            this.txtQuantidade.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtQuantidade.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuantidade_KeyDown);
            this.txtQuantidade.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuantidade_KeyPress);
            this.txtQuantidade.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtQuantidade_KeyUp);
            // 
            // txtCodigo
            // 
            this.txtCodigo.BackColor = System.Drawing.Color.MediumBlue;
            this.txtCodigo.Enabled = false;
            this.txtCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigo.ForeColor = System.Drawing.SystemColors.Info;
            this.txtCodigo.Location = new System.Drawing.Point(81, 198);
            this.txtCodigo.MaxLength = 13;
            this.txtCodigo.Name = "txtCodigo";
            this.txtCodigo.Size = new System.Drawing.Size(390, 67);
            this.txtCodigo.TabIndex = 0;
            this.txtCodigo.TabStop = false;
            this.txtCodigo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtCodigo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyDown);
            this.txtCodigo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtCodigo_KeyUp);
            // 
            // txtProduto
            // 
            this.txtProduto.BackColor = System.Drawing.Color.MediumBlue;
            this.txtProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProduto.ForeColor = System.Drawing.SystemColors.Info;
            this.txtProduto.Location = new System.Drawing.Point(81, 118);
            this.txtProduto.Name = "txtProduto";
            this.txtProduto.ReadOnly = true;
            this.txtProduto.Size = new System.Drawing.Size(1101, 67);
            this.txtProduto.TabIndex = 8;
            this.txtProduto.TabStop = false;
            this.txtProduto.Text = "Nova Venda";
            this.txtProduto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtProduto_KeyUp);
            // 
            // txtValorTotal
            // 
            this.txtValorTotal.BackColor = System.Drawing.SystemColors.ControlText;
            this.txtValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorTotal.ForeColor = System.Drawing.Color.Red;
            this.txtValorTotal.Location = new System.Drawing.Point(607, 555);
            this.txtValorTotal.Name = "txtValorTotal";
            this.txtValorTotal.ReadOnly = true;
            this.txtValorTotal.Size = new System.Drawing.Size(569, 67);
            this.txtValorTotal.TabIndex = 7;
            this.txtValorTotal.TabStop = false;
            this.txtValorTotal.Text = "0,00";
            this.txtValorTotal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtValorTotal_KeyUp);
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 32.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblValorTotal.Location = new System.Drawing.Point(484, 565);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(136, 51);
            this.lblValorTotal.TabIndex = 112;
            this.lblValorTotal.Text = "Total:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(250, 437);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 42);
            this.label3.TabIndex = 120;
            this.label3.Text = "=";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(8, 387);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 20);
            this.label4.TabIndex = 119;
            this.label4.Text = "Preço:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Blue;
            this.label2.Location = new System.Drawing.Point(8, 293);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 20);
            this.label2.TabIndex = 118;
            this.label2.Text = "Qdade:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(8, 210);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 20);
            this.label1.TabIndex = 117;
            this.label1.Text = "Código:";
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduto.ForeColor = System.Drawing.Color.Blue;
            this.lblProduto.Location = new System.Drawing.Point(8, 131);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(77, 20);
            this.lblProduto.TabIndex = 116;
            this.lblProduto.Text = "Produto:";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.lblNumVendas,
            this.lblNumeroVenda,
            this.toolStripStatusLabel5,
            this.lblItem,
            this.lblItens,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel2,
            this.lblNumeroCaixa,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel3,
            this.tsStatus,
            this.toolStripStatusLabel8,
            this.toolStripStatusLabel4,
            this.tsNome,
            this.toolStripStatusLabel9,
            this.toolStripStatusLabel10,
            this.tsPeriodo,
            this.pb,
            this.toolStripStatusLabel11,
            this.lblImpreissora});
            this.statusStrip1.Location = new System.Drawing.Point(0, 654);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1255, 26);
            this.statusStrip1.TabIndex = 122;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(87, 21);
            this.toolStripStatusLabel1.Text = "Nº Cupom:";
            // 
            // lblNumVendas
            // 
            this.lblNumVendas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumVendas.Name = "lblNumVendas";
            this.lblNumVendas.Size = new System.Drawing.Size(0, 21);
            this.lblNumVendas.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNumeroVenda
            // 
            this.lblNumeroVenda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroVenda.Name = "lblNumeroVenda";
            this.lblNumeroVenda.Size = new System.Drawing.Size(0, 21);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(13, 21);
            this.toolStripStatusLabel5.Text = "|";
            // 
            // lblItem
            // 
            this.lblItem.BackColor = System.Drawing.SystemColors.Control;
            this.lblItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItem.Name = "lblItem";
            this.lblItem.Size = new System.Drawing.Size(91, 21);
            this.lblItem.Text = "Item Venda:";
            // 
            // lblItens
            // 
            this.lblItens.BackColor = System.Drawing.SystemColors.Control;
            this.lblItens.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItens.Name = "lblItens";
            this.lblItens.Size = new System.Drawing.Size(19, 21);
            this.lblItens.Text = "0";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(13, 21);
            this.toolStripStatusLabel6.Text = "|";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(72, 21);
            this.toolStripStatusLabel2.Text = "Nº.Caixa:";
            // 
            // lblNumeroCaixa
            // 
            this.lblNumeroCaixa.BackColor = System.Drawing.SystemColors.Control;
            this.lblNumeroCaixa.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumeroCaixa.Name = "lblNumeroCaixa";
            this.lblNumeroCaixa.Size = new System.Drawing.Size(19, 21);
            this.lblNumeroCaixa.Text = "0";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(13, 21);
            this.toolStripStatusLabel7.Text = "|";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(96, 21);
            this.toolStripStatusLabel3.Text = "Status Caixa:";
            // 
            // tsStatus
            // 
            this.tsStatus.BackColor = System.Drawing.SystemColors.Control;
            this.tsStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsStatus.Name = "tsStatus";
            this.tsStatus.Size = new System.Drawing.Size(80, 21);
            this.tsStatus.Text = "FECHADO";
            // 
            // toolStripStatusLabel8
            // 
            this.toolStripStatusLabel8.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel8.Name = "toolStripStatusLabel8";
            this.toolStripStatusLabel8.Size = new System.Drawing.Size(13, 21);
            this.toolStripStatusLabel8.Text = "|";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(80, 21);
            this.toolStripStatusLabel4.Text = "Operador:";
            // 
            // tsNome
            // 
            this.tsNome.BackColor = System.Drawing.SystemColors.Control;
            this.tsNome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsNome.Name = "tsNome";
            this.tsNome.Size = new System.Drawing.Size(19, 21);
            this.tsNome.Text = "...";
            // 
            // toolStripStatusLabel9
            // 
            this.toolStripStatusLabel9.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel9.Name = "toolStripStatusLabel9";
            this.toolStripStatusLabel9.Size = new System.Drawing.Size(13, 21);
            this.toolStripStatusLabel9.Text = "|";
            // 
            // toolStripStatusLabel10
            // 
            this.toolStripStatusLabel10.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel10.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel10.Name = "toolStripStatusLabel10";
            this.toolStripStatusLabel10.Size = new System.Drawing.Size(63, 21);
            this.toolStripStatusLabel10.Text = "Periodo:";
            // 
            // tsPeriodo
            // 
            this.tsPeriodo.BackColor = System.Drawing.SystemColors.Control;
            this.tsPeriodo.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsPeriodo.Name = "tsPeriodo";
            this.tsPeriodo.Size = new System.Drawing.Size(18, 21);
            this.tsPeriodo.Text = "...";
            // 
            // pb
            // 
            this.pb.BackColor = System.Drawing.SystemColors.Control;
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(100, 20);
            this.pb.ToolTipText = "Verificar Impressora Ligada";
            // 
            // toolStripStatusLabel11
            // 
            this.toolStripStatusLabel11.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel11.Name = "toolStripStatusLabel11";
            this.toolStripStatusLabel11.Size = new System.Drawing.Size(85, 21);
            this.toolStripStatusLabel11.Text = "Impressora:";
            // 
            // lblImpreissora
            // 
            this.lblImpreissora.BackColor = System.Drawing.SystemColors.Control;
            this.lblImpreissora.Name = "lblImpreissora";
            this.lblImpreissora.Size = new System.Drawing.Size(18, 21);
            this.lblImpreissora.Text = "...";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Blue;
            this.label5.Location = new System.Drawing.Point(253, 336);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 42);
            this.label5.TabIndex = 123;
            this.label5.Text = "x";
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.BackColor = System.Drawing.Color.White;
            this.lblData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblData.Location = new System.Drawing.Point(926, 658);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(48, 20);
            this.lblData.TabIndex = 126;
            this.lblData.Text = "Data:";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtSomas
            // 
            this.txtSomas.AccessibleRole = System.Windows.Forms.AccessibleRole.IpAddress;
            this.txtSomas.BackColor = System.Drawing.Color.MediumBlue;
            this.txtSomas.Enabled = false;
            this.txtSomas.Font = new System.Drawing.Font("Microsoft Sans Serif", 39.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSomas.ForeColor = System.Drawing.SystemColors.Info;
            this.txtSomas.Location = new System.Drawing.Point(81, 471);
            this.txtSomas.MaxLength = 6;
            this.txtSomas.Name = "txtSomas";
            this.txtSomas.Size = new System.Drawing.Size(390, 67);
            this.txtSomas.TabIndex = 3;
            this.txtSomas.Text = " ";
            this.txtSomas.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtSomas.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSomas_KeyPress);
            this.txtSomas.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSomas_KeyUp);
            // 
            // btnCancelaItem
            // 
            this.btnCancelaItem.BackColor = System.Drawing.Color.White;
            this.btnCancelaItem.Enabled = false;
            this.btnCancelaItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelaItem.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelaItem.Image")));
            this.btnCancelaItem.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelaItem.Location = new System.Drawing.Point(371, 565);
            this.btnCancelaItem.Name = "btnCancelaItem";
            this.btnCancelaItem.Size = new System.Drawing.Size(88, 73);
            this.btnCancelaItem.TabIndex = 5;
            this.btnCancelaItem.Text = "Cancelar Item";
            this.btnCancelaItem.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelaItem.UseVisualStyleBackColor = false;
            this.btnCancelaItem.Click += new System.EventHandler(this.btnCancelaItem_Click);
            this.btnCancelaItem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCancelaItem_KeyDown);
            // 
            // btnCancelaVenda
            // 
            this.btnCancelaVenda.BackColor = System.Drawing.Color.White;
            this.btnCancelaVenda.Enabled = false;
            this.btnCancelaVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelaVenda.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelaVenda.Image")));
            this.btnCancelaVenda.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelaVenda.Location = new System.Drawing.Point(282, 565);
            this.btnCancelaVenda.Name = "btnCancelaVenda";
            this.btnCancelaVenda.Size = new System.Drawing.Size(83, 73);
            this.btnCancelaVenda.TabIndex = 4;
            this.btnCancelaVenda.Text = "Cancelar Venda";
            this.btnCancelaVenda.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelaVenda.UseVisualStyleBackColor = false;
            this.btnCancelaVenda.Click += new System.EventHandler(this.btnCancelaVenda_Click);
            this.btnCancelaVenda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCancelaVenda_KeyDown);
            // 
            // btnCancelarUltimaVenda
            // 
            this.btnCancelarUltimaVenda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.btnCancelarUltimaVenda.Enabled = false;
            this.btnCancelarUltimaVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarUltimaVenda.Image = ((System.Drawing.Image)(resources.GetObject("btnCancelarUltimaVenda.Image")));
            this.btnCancelarUltimaVenda.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnCancelarUltimaVenda.Location = new System.Drawing.Point(77, 565);
            this.btnCancelarUltimaVenda.Name = "btnCancelarUltimaVenda";
            this.btnCancelarUltimaVenda.Size = new System.Drawing.Size(109, 73);
            this.btnCancelarUltimaVenda.TabIndex = 6;
            this.btnCancelarUltimaVenda.Text = "Cancelar Ultimo Cupom";
            this.btnCancelarUltimaVenda.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnCancelarUltimaVenda.UseVisualStyleBackColor = false;
            this.btnCancelarUltimaVenda.Click += new System.EventHandler(this.btnCancelarVenda_Click);
            this.btnCancelarUltimaVenda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCancelarUltimaVenda_KeyDown);
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(1, 595);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(58, 43);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 125;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(0, 540);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(58, 49);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 124;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.ErrorImage = ((System.Drawing.Image)(resources.GetObject("pictureBox1.ErrorImage")));
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1, -1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1181, 113);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 115;
            this.pictureBox1.TabStop = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // btnSangria
            // 
            this.btnSangria.BackColor = System.Drawing.Color.White;
            this.btnSangria.Enabled = false;
            this.btnSangria.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSangria.Image = ((System.Drawing.Image)(resources.GetObject("btnSangria.Image")));
            this.btnSangria.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnSangria.Location = new System.Drawing.Point(192, 565);
            this.btnSangria.Name = "btnSangria";
            this.btnSangria.Size = new System.Drawing.Size(84, 73);
            this.btnSangria.TabIndex = 128;
            this.btnSangria.Text = "Sangria";
            this.btnSangria.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.btnSangria.UseVisualStyleBackColor = false;
            this.btnSangria.Click += new System.EventHandler(this.button1_Click);
            this.btnSangria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnSangria_KeyDown);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick_1);
            // 
            // lblVersao
            // 
            this.lblVersao.AutoSize = true;
            this.lblVersao.ForeColor = System.Drawing.Color.Red;
            this.lblVersao.Location = new System.Drawing.Point(820, 625);
            this.lblVersao.Name = "lblVersao";
            this.lblVersao.Size = new System.Drawing.Size(106, 13);
            this.lblVersao.TabIndex = 129;
            this.lblVersao.Text = "Versão 2.0.54 - 2016";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.FileName = "BANCO\\VENDA\\";
            this.saveFileDialog1.Filter = "xml Files| *.xml";
            // 
            // serialPort1
            // 
            this.serialPort1.BaudRate = 2400;
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDocument2
            // 
            this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
            // 
            // printDocument3
            // 
            this.printDocument3.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument3_PrintPage);
            // 
            // frmVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1255, 680);
            this.Controls.Add(this.lblVersao);
            this.Controls.Add(this.btnSangria);
            this.Controls.Add(this.btnCancelaItem);
            this.Controls.Add(this.btnCancelaVenda);
            this.Controls.Add(this.btnCancelarUltimaVenda);
            this.Controls.Add(this.txtSomas);
            this.Controls.Add(this.lblData);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtPreco);
            this.Controls.Add(this.txtQuantidade);
            this.Controls.Add(this.txtCodigo);
            this.Controls.Add(this.txtProduto);
            this.Controls.Add(this.txtValorTotal);
            this.Controls.Add(this.lblValorTotal);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblProduto);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.label5);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "pv";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmVenda_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbVenda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvEntrada)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.TextBox txtCodigo;
        private System.Windows.Forms.TextBox txtProduto;
        private System.Windows.Forms.TextBox txtValorTotal;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblItem;
        private System.Windows.Forms.ToolStripStatusLabel lblItens;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblNumVendas;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolStripStatusLabel lblNumeroVenda;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblNumeroCaixa;
        private System.Windows.Forms.TextBox txtSomas;
        private System.Windows.Forms.Button btnCancelarUltimaVenda;
        private System.Windows.Forms.Button btnCancelaVenda;
        private System.Windows.Forms.Button btnCancelaItem;
        private System.Windows.Forms.ToolStripStatusLabel tsStatus;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel tsNome;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel8;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel9;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel10;
        private System.Windows.Forms.ToolStripStatusLabel tsPeriodo;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button btnSangria;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.ToolStripProgressBar pb;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Label lblVersao;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel11;
        private System.Windows.Forms.ToolStripStatusLabel lblImpreissora;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.Drawing.Printing.PrintDocument printDocument3;
        private System.Windows.Forms.PictureBox pbVenda;
        private System.Windows.Forms.DataGridView gdvEntrada;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn ITEMS;
        private System.Windows.Forms.DataGridViewTextBoxColumn COD_BARRA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_PROD;
        private System.Windows.Forms.DataGridViewTextBoxColumn DESCRICAO_PRODUTO_;
        private System.Windows.Forms.DataGridViewTextBoxColumn UNID_;
        private System.Windows.Forms.DataGridViewTextBoxColumn QUANT;
        private System.Windows.Forms.DataGridViewTextBoxColumn PRECO_;
        private System.Windows.Forms.DataGridViewTextBoxColumn CUSTO;
        private System.Windows.Forms.DataGridViewTextBoxColumn DATA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ATUALIZADOS;
        private System.Windows.Forms.DataGridViewTextBoxColumn NUM_VENDA;
        private System.Windows.Forms.DataGridViewTextBoxColumn ALIQUOTAS;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID_USUARIO;
        private System.Windows.Forms.DataGridViewTextBoxColumn TOTAL;
    }
}

