namespace PVe
{
    partial class frmFechamentoVenda
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFechamentoVenda));
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gdvTipoPgto = new System.Windows.Forms.DataGridView();
            this.Descricao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Valor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.lblCancelarVenda = new System.Windows.Forms.Label();
            this.lblFecharVenda = new System.Windows.Forms.Label();
            this.btnCancelarVenda = new System.Windows.Forms.Button();
            this.btnFecharVenda = new System.Windows.Forms.Button();
            this.txtTroco = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtRecebimento = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnDinheiro = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblOperador = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblNumCaixa = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblRetornoSat = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnVerificarImpressora = new System.Windows.Forms.ToolStripButton();
            this.pbFechamentoVenda = new System.Windows.Forms.ToolStripProgressBar();
            this.btnCartao = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnAberto = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.lblEntrada = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.btnCheques = new System.Windows.Forms.Button();
            this.Time = new System.Windows.Forms.Timer(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNomeCliente = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblKmPlaca = new System.Windows.Forms.Label();
            this.lblNomePlaca = new System.Windows.Forms.Label();
            this.lblCnpjCpf = new System.Windows.Forms.Label();
            this.lblCpfCliente = new System.Windows.Forms.Label();
            this.lblCpfCnpj = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbCupom = new System.Windows.Forms.ListBox();
            this.lblSomaCupom = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.gdvAssinatuDig = new System.Windows.Forms.DataGridView();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.lbVenda = new System.Windows.Forms.ListBox();
            this.printDocument2 = new System.Drawing.Printing.PrintDocument();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.groupBox2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvTipoPgto)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvAssinatuDig)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panel1);
            this.groupBox2.Controls.Add(this.lblCancelarVenda);
            this.groupBox2.Controls.Add(this.lblFecharVenda);
            this.groupBox2.Controls.Add(this.btnCancelarVenda);
            this.groupBox2.Controls.Add(this.btnFecharVenda);
            this.groupBox2.Controls.Add(this.txtTroco);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.txtRecebimento);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtTotal);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(162, 11);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(373, 467);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.Controls.Add(this.gdvTipoPgto);
            this.panel1.Location = new System.Drawing.Point(22, 153);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(345, 200);
            this.panel1.TabIndex = 11;
            // 
            // gdvTipoPgto
            // 
            this.gdvTipoPgto.AllowUserToAddRows = false;
            this.gdvTipoPgto.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gdvTipoPgto.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.gdvTipoPgto.BackgroundColor = System.Drawing.Color.White;
            this.gdvTipoPgto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvTipoPgto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Descricao,
            this.Valor});
            this.gdvTipoPgto.Location = new System.Drawing.Point(3, 3);
            this.gdvTipoPgto.Margin = new System.Windows.Forms.Padding(10);
            this.gdvTipoPgto.Name = "gdvTipoPgto";
            this.gdvTipoPgto.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.gdvTipoPgto.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.gdvTipoPgto.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToFirstHeader;
            this.gdvTipoPgto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.gdvTipoPgto.Size = new System.Drawing.Size(339, 197);
            this.gdvTipoPgto.TabIndex = 0;
            this.gdvTipoPgto.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.gdvTipoPgto_CellContentClick);
            this.gdvTipoPgto.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.gdvTipoPgto_KeyPress);
            this.gdvTipoPgto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gdvTipoPgto_KeyUp);
            // 
            // Descricao
            // 
            this.Descricao.DataPropertyName = "Descricao";
            this.Descricao.HeaderText = "Descrição";
            this.Descricao.Name = "Descricao";
            this.Descricao.ReadOnly = true;
            this.Descricao.Width = 160;
            // 
            // Valor
            // 
            this.Valor.DataPropertyName = "Valor";
            dataGridViewCellStyle2.Format = "N2";
            dataGridViewCellStyle2.NullValue = null;
            this.Valor.DefaultCellStyle = dataGridViewCellStyle2;
            this.Valor.HeaderText = "Valor";
            this.Valor.Name = "Valor";
            this.Valor.ReadOnly = true;
            this.Valor.Width = 130;
            // 
            // lblCancelarVenda
            // 
            this.lblCancelarVenda.AutoSize = true;
            this.lblCancelarVenda.Enabled = false;
            this.lblCancelarVenda.Location = new System.Drawing.Point(210, 404);
            this.lblCancelarVenda.Name = "lblCancelarVenda";
            this.lblCancelarVenda.Size = new System.Drawing.Size(166, 13);
            this.lblCancelarVenda.TabIndex = 10;
            this.lblCancelarVenda.Text = "Deseja Cancelar Venda tecla Esc";
            // 
            // lblFecharVenda
            // 
            this.lblFecharVenda.AutoSize = true;
            this.lblFecharVenda.Enabled = false;
            this.lblFecharVenda.Location = new System.Drawing.Point(27, 404);
            this.lblFecharVenda.Name = "lblFecharVenda";
            this.lblFecharVenda.Size = new System.Drawing.Size(158, 13);
            this.lblFecharVenda.TabIndex = 9;
            this.lblFecharVenda.Text = "Deseja Fechar Venda tecla End";
            // 
            // btnCancelarVenda
            // 
            this.btnCancelarVenda.Enabled = false;
            this.btnCancelarVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancelarVenda.Location = new System.Drawing.Point(211, 430);
            this.btnCancelarVenda.Name = "btnCancelarVenda";
            this.btnCancelarVenda.Size = new System.Drawing.Size(157, 29);
            this.btnCancelarVenda.TabIndex = 3;
            this.btnCancelarVenda.Text = "Cancelar  (Esc)";
            this.btnCancelarVenda.UseVisualStyleBackColor = true;
            this.btnCancelarVenda.Click += new System.EventHandler(this.btnCancelarVenda_Click);
            // 
            // btnFecharVenda
            // 
            this.btnFecharVenda.Enabled = false;
            this.btnFecharVenda.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFecharVenda.Location = new System.Drawing.Point(30, 430);
            this.btnFecharVenda.Name = "btnFecharVenda";
            this.btnFecharVenda.Size = new System.Drawing.Size(157, 29);
            this.btnFecharVenda.TabIndex = 2;
            this.btnFecharVenda.TabStop = false;
            this.btnFecharVenda.Text = "Fechar (Enter)";
            this.btnFecharVenda.UseVisualStyleBackColor = true;
            this.btnFecharVenda.Click += new System.EventHandler(this.btnFecharVenda_Click);
            this.btnFecharVenda.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnFecharVenda_KeyUp);
            // 
            // txtTroco
            // 
            this.txtTroco.BackColor = System.Drawing.Color.White;
            this.txtTroco.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTroco.Location = new System.Drawing.Point(146, 359);
            this.txtTroco.Name = "txtTroco";
            this.txtTroco.ReadOnly = true;
            this.txtTroco.Size = new System.Drawing.Size(221, 38);
            this.txtTroco.TabIndex = 5;
            this.txtTroco.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTroco_KeyPress);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Blue;
            this.label7.Location = new System.Drawing.Point(24, 364);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(104, 33);
            this.label7.TabIndex = 7;
            this.label7.Text = "Troco:";
            // 
            // txtRecebimento
            // 
            this.txtRecebimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRecebimento.Location = new System.Drawing.Point(192, 85);
            this.txtRecebimento.Name = "txtRecebimento";
            this.txtRecebimento.Size = new System.Drawing.Size(175, 38);
            this.txtRecebimento.TabIndex = 0;
            this.txtRecebimento.Text = "0,00";
            this.txtRecebimento.TextChanged += new System.EventHandler(this.txtRecebimento_TextChanged_1);
            this.txtRecebimento.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRecebimento_KeyDown);
            this.txtRecebimento.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtRecebimento_KeyPress);
            this.txtRecebimento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtRecebimento_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Blue;
            this.label3.Location = new System.Drawing.Point(24, 90);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 33);
            this.label3.TabIndex = 5;
            this.label3.Text = "Recebido:";
            // 
            // txtTotal
            // 
            this.txtTotal.BackColor = System.Drawing.Color.White;
            this.txtTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotal.Location = new System.Drawing.Point(192, 34);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.ReadOnly = true;
            this.txtTotal.Size = new System.Drawing.Size(175, 38);
            this.txtTotal.TabIndex = 1;
            this.txtTotal.Text = "0,00";
            this.txtTotal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTotal_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Blue;
            this.label1.Location = new System.Drawing.Point(24, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(94, 33);
            this.label1.TabIndex = 4;
            this.label1.Text = "Total:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(42, 383);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 20);
            this.label6.TabIndex = 5;
            this.label6.Text = "(End)";
            // 
            // btnDinheiro
            // 
            this.btnDinheiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDinheiro.Location = new System.Drawing.Point(9, 65);
            this.btnDinheiro.Name = "btnDinheiro";
            this.btnDinheiro.Size = new System.Drawing.Size(116, 37);
            this.btnDinheiro.TabIndex = 0;
            this.btnDinheiro.Text = "1-Dinheiro";
            this.btnDinheiro.UseVisualStyleBackColor = true;
            this.btnDinheiro.Click += new System.EventHandler(this.btnDinheiro_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(29, 295);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 20);
            this.label4.TabIndex = 3;
            this.label4.Text = "(PgUp )";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel2,
            this.lblOperador,
            this.toolStripStatusLabel3,
            this.lblNumCaixa,
            this.lblRetornoSat,
            this.btnVerificarImpressora,
            this.pbFechamentoVenda});
            this.statusStrip1.Location = new System.Drawing.Point(0, 530);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(993, 25);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(65, 20);
            this.toolStripStatusLabel1.Text = "ESC-S&air";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(76, 20);
            this.toolStripStatusLabel2.Text = "Operador:";
            // 
            // lblOperador
            // 
            this.lblOperador.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOperador.Name = "lblOperador";
            this.lblOperador.Size = new System.Drawing.Size(18, 20);
            this.lblOperador.Text = "...";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(68, 20);
            this.toolStripStatusLabel3.Text = "Nº.Caixa:";
            // 
            // lblNumCaixa
            // 
            this.lblNumCaixa.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNumCaixa.Name = "lblNumCaixa";
            this.lblNumCaixa.Size = new System.Drawing.Size(25, 20);
            this.lblNumCaixa.Text = "01";
            // 
            // lblRetornoSat
            // 
            this.lblRetornoSat.Name = "lblRetornoSat";
            this.lblRetornoSat.Size = new System.Drawing.Size(16, 20);
            this.lblRetornoSat.Text = "...";
            // 
            // btnVerificarImpressora
            // 
            this.btnVerificarImpressora.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.btnVerificarImpressora.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnVerificarImpressora.Name = "btnVerificarImpressora";
            this.btnVerificarImpressora.Size = new System.Drawing.Size(121, 23);
            this.btnVerificarImpressora.Text = "|Verificar Impressora|";
            this.btnVerificarImpressora.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnVerificarImpressora.Click += new System.EventHandler(this.btnVerificarImpressora_Click);
            // 
            // pbFechamentoVenda
            // 
            this.pbFechamentoVenda.Name = "pbFechamentoVenda";
            this.pbFechamentoVenda.Size = new System.Drawing.Size(100, 19);
            // 
            // btnCartao
            // 
            this.btnCartao.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCartao.Location = new System.Drawing.Point(9, 163);
            this.btnCartao.Name = "btnCartao";
            this.btnCartao.Size = new System.Drawing.Size(116, 37);
            this.btnCartao.TabIndex = 0;
            this.btnCartao.Text = "2-Cartão";
            this.btnCartao.UseVisualStyleBackColor = true;
            this.btnCartao.Click += new System.EventHandler(this.btnCheque_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.btnAberto);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblEntrada);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.btnCheques);
            this.groupBox1.Controls.Add(this.btnCartao);
            this.groupBox1.Controls.Add(this.btnDinheiro);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 476);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Tipo de Pagamento";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(29, 203);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 20);
            this.label10.TabIndex = 10;
            this.label10.Text = "(Home )";
            // 
            // btnAberto
            // 
            this.btnAberto.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAberto.Location = new System.Drawing.Point(9, 343);
            this.btnAberto.Name = "btnAberto";
            this.btnAberto.Size = new System.Drawing.Size(116, 37);
            this.btnAberto.TabIndex = 9;
            this.btnAberto.Text = "4-Aberto";
            this.btnAberto.UseVisualStyleBackColor = true;
            this.btnAberto.Click += new System.EventHandler(this.btnRgGerencial_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(29, 107);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(70, 20);
            this.label9.TabIndex = 8;
            this.label9.Text = "(PgDn )";
            // 
            // lblEntrada
            // 
            this.lblEntrada.AutoSize = true;
            this.lblEntrada.Location = new System.Drawing.Point(94, 438);
            this.lblEntrada.Name = "lblEntrada";
            this.lblEntrada.Size = new System.Drawing.Size(16, 18);
            this.lblEntrada.TabIndex = 7;
            this.lblEntrada.Text = "0";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 438);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 18);
            this.label8.TabIndex = 6;
            this.label8.Text = "Cupom Nº : ";
            // 
            // btnCheques
            // 
            this.btnCheques.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheques.Location = new System.Drawing.Point(9, 255);
            this.btnCheques.Name = "btnCheques";
            this.btnCheques.Size = new System.Drawing.Size(116, 37);
            this.btnCheques.TabIndex = 1;
            this.btnCheques.Text = "3-Cheque";
            this.btnCheques.UseVisualStyleBackColor = true;
            this.btnCheques.Click += new System.EventHandler(this.btnCartao_Click);
            // 
            // Time
            // 
            this.Time.Tick += new System.EventHandler(this.Time_Tick);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel2.Controls.Add(this.pictureBox3);
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Controls.Add(this.lblNomeCliente);
            this.panel2.Controls.Add(this.label14);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.lblKmPlaca);
            this.panel2.Controls.Add(this.lblNomePlaca);
            this.panel2.Controls.Add(this.lblCnpjCpf);
            this.panel2.Controls.Add(this.lblCpfCliente);
            this.panel2.Controls.Add(this.lblCpfCnpj);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.lbCupom);
            this.panel2.Controls.Add(this.lblSomaCupom);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(544, 23);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(439, 455);
            this.panel2.TabIndex = 7;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(390, 427);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(27, 20);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 18;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.Visible = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Location = new System.Drawing.Point(108, 392);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(230, 23);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox2.TabIndex = 17;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(189, 351);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 34);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // lblNomeCliente
            // 
            this.lblNomeCliente.AutoSize = true;
            this.lblNomeCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomeCliente.Location = new System.Drawing.Point(128, 86);
            this.lblNomeCliente.Name = "lblNomeCliente";
            this.lblNomeCliente.Size = new System.Drawing.Size(12, 16);
            this.lblNomeCliente.TabIndex = 15;
            this.lblNomeCliente.Text = ".";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(4, 86);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(123, 16);
            this.label14.TabIndex = 14;
            this.label14.Text = "NOME CLIENTE:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(155, 108);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(33, 16);
            this.label13.TabIndex = 13;
            this.label13.Text = "KM:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(4, 108);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(60, 16);
            this.label12.TabIndex = 12;
            this.label12.Text = "PLACA:";
            // 
            // lblKmPlaca
            // 
            this.lblKmPlaca.AutoSize = true;
            this.lblKmPlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKmPlaca.Location = new System.Drawing.Point(186, 108);
            this.lblKmPlaca.Name = "lblKmPlaca";
            this.lblKmPlaca.Size = new System.Drawing.Size(16, 16);
            this.lblKmPlaca.TabIndex = 11;
            this.lblKmPlaca.Text = "0";
            // 
            // lblNomePlaca
            // 
            this.lblNomePlaca.AutoSize = true;
            this.lblNomePlaca.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNomePlaca.Location = new System.Drawing.Point(63, 108);
            this.lblNomePlaca.Name = "lblNomePlaca";
            this.lblNomePlaca.Size = new System.Drawing.Size(12, 16);
            this.lblNomePlaca.TabIndex = 10;
            this.lblNomePlaca.Text = ".";
            // 
            // lblCnpjCpf
            // 
            this.lblCnpjCpf.AutoSize = true;
            this.lblCnpjCpf.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCnpjCpf.Location = new System.Drawing.Point(56, 64);
            this.lblCnpjCpf.Name = "lblCnpjCpf";
            this.lblCnpjCpf.Size = new System.Drawing.Size(109, 16);
            this.lblCnpjCpf.TabIndex = 9;
            this.lblCnpjCpf.Text = "000.000.000-00";
            // 
            // lblCpfCliente
            // 
            this.lblCpfCliente.AutoSize = true;
            this.lblCpfCliente.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCpfCliente.Location = new System.Drawing.Point(40, 59);
            this.lblCpfCliente.Name = "lblCpfCliente";
            this.lblCpfCliente.Size = new System.Drawing.Size(0, 16);
            this.lblCpfCliente.TabIndex = 8;
            // 
            // lblCpfCnpj
            // 
            this.lblCpfCnpj.AutoSize = true;
            this.lblCpfCnpj.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCpfCnpj.Location = new System.Drawing.Point(4, 64);
            this.lblCpfCnpj.Name = "lblCpfCnpj";
            this.lblCpfCnpj.Size = new System.Drawing.Size(55, 16);
            this.lblCpfCnpj.TabIndex = 7;
            this.lblCpfCnpj.Text = "CNPJ :";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(3, 423);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(149, 24);
            this.label11.TabIndex = 6;
            this.label11.Text = "SOMA TOTAL:";
            // 
            // lbCupom
            // 
            this.lbCupom.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbCupom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.lbCupom.ColumnWidth = 50;
            this.lbCupom.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbCupom.FormattingEnabled = true;
            this.lbCupom.ItemHeight = 15;
            this.lbCupom.Items.AddRange(new object[] {
            "   ITEM\tCÓDIGO\tDESCRIÇÃO\tQTDE\tX\t$(UNIT)",
            "   ------------------------------------------------------------------------------" +
                "---------------------",
            "   ------------------------------------------------------------------------------" +
                "---------------------"});
            this.lbCupom.Location = new System.Drawing.Point(6, 131);
            this.lbCupom.Name = "lbCupom";
            this.lbCupom.Size = new System.Drawing.Size(429, 214);
            this.lbCupom.TabIndex = 1;
            // 
            // lblSomaCupom
            // 
            this.lblSomaCupom.AutoSize = true;
            this.lblSomaCupom.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSomaCupom.Location = new System.Drawing.Point(158, 423);
            this.lblSomaCupom.Name = "lblSomaCupom";
            this.lblSomaCupom.Size = new System.Drawing.Size(35, 24);
            this.lblSomaCupom.TabIndex = 5;
            this.lblSomaCupom.Text = "R$";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(84, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 31);
            this.label2.TabIndex = 0;
            this.label2.Text = "CUPOM FISCAL";
            // 
            // gdvAssinatuDig
            // 
            this.gdvAssinatuDig.AllowUserToAddRows = false;
            this.gdvAssinatuDig.AllowUserToDeleteRows = false;
            this.gdvAssinatuDig.BackgroundColor = System.Drawing.SystemColors.ControlLight;
            this.gdvAssinatuDig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvAssinatuDig.Location = new System.Drawing.Point(544, 493);
            this.gdvAssinatuDig.Name = "gdvAssinatuDig";
            this.gdvAssinatuDig.ReadOnly = true;
            this.gdvAssinatuDig.Size = new System.Drawing.Size(437, 25);
            this.gdvAssinatuDig.TabIndex = 9;
            this.gdvAssinatuDig.Visible = false;
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.DefaultExt = "xml";
            this.saveFileDialog1.FileName = "BANCO\\VENDA\\";
            this.saveFileDialog1.Filter = "xml Files| *.xml";
            // 
            // lbVenda
            // 
            this.lbVenda.FormattingEnabled = true;
            this.lbVenda.Location = new System.Drawing.Point(12, 476);
            this.lbVenda.Name = "lbVenda";
            this.lbVenda.Size = new System.Drawing.Size(518, 43);
            this.lbVenda.TabIndex = 10;
            this.lbVenda.Visible = false;
            // 
            // printDocument2
            // 
            this.printDocument2.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument2_PrintPage);
            // 
            // printDocument1
            // 
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // frmFechamentoVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(993, 555);
            this.Controls.Add(this.lbVenda);
            this.Controls.Add(this.gdvAssinatuDig);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFechamentoVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tela de Fechamento Venda";
            this.Load += new System.EventHandler(this.frmFechamentoVenda_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdvTipoPgto)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvAssinatuDig)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblCancelarVenda;
        private System.Windows.Forms.Label lblFecharVenda;
        private System.Windows.Forms.Button btnCancelarVenda;
        public System.Windows.Forms.Button btnFecharVenda;
        private System.Windows.Forms.TextBox txtTroco;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtRecebimento;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnDinheiro;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.Button btnCartao;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblEntrada;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnCheques;
        private System.Windows.Forms.Timer Time;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbCupom;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblSomaCupom;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel lblOperador;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.ToolStripStatusLabel lblNumCaixa;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lblCpfCliente;
        private System.Windows.Forms.Label lblCpfCnpj;
        private System.Windows.Forms.Button btnAberto;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.DataGridView gdvAssinatuDig;
        private System.Windows.Forms.Label lblCnpjCpf;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView gdvTipoPgto;
        private System.Windows.Forms.Label lblKmPlaca;
        private System.Windows.Forms.Label lblNomePlaca;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripStatusLabel lblRetornoSat;
        private System.Windows.Forms.ListBox lbVenda;
        private System.Windows.Forms.Label lblNomeCliente;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.ToolStripButton btnVerificarImpressora;
        private System.Drawing.Printing.PrintDocument printDocument2;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.ToolStripProgressBar pbFechamentoVenda;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Descricao;
        private System.Windows.Forms.DataGridViewTextBoxColumn Valor;
    }
}