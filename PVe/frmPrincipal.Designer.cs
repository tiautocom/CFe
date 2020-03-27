namespace PVe
{
    partial class frmPrincipal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPrincipal));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.configuraçõesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.parametrosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gErarAssinaturaDigitalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnFechar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.btnSair = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnFecharCaixa = new System.Windows.Forms.Button();
            this.Dados = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.gdvFormaPgto = new System.Windows.Forms.DataGridView();
            this.colTipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colValor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label9 = new System.Windows.Forms.Label();
            this.gdvRelatorio = new System.Windows.Forms.DataGridView();
            this.txtSomaTotal = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnDepartamento = new System.Windows.Forms.Button();
            this.btnVendaCancelada = new System.Windows.Forms.Button();
            this.btnSetor = new System.Windows.Forms.Button();
            this.btnImprimirDep = new System.Windows.Forms.Button();
            this.btnImprimirVendaCanc = new System.Windows.Forms.Button();
            this.btnImprimirSetor = new System.Windows.Forms.Button();
            this.btnEstoque = new System.Windows.Forms.Button();
            this.btnImprimirEstoqueDep = new System.Windows.Forms.Button();
            this.cbDepartamento = new System.Windows.Forms.ComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.txtNumeroDep = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btmImprimirRelatorio = new System.Windows.Forms.Button();
            this.btnPesquisarEstoque = new System.Windows.Forms.Button();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.pb = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblQtdeDadosImportado = new System.Windows.Forms.ToolStripStatusLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.pnlFechamentoCaixa = new System.Windows.Forms.Panel();
            this.txtDespesas = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.lblDiferencaCaixa = new System.Windows.Forms.Label();
            this.btnCalcular = new System.Windows.Forms.Button();
            this.txtTotalRecebimento = new System.Windows.Forms.TextBox();
            this.txtTotalSangria = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtTotalConvenio = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.txtTotalCheque = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.txtTotalCartao = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.txtTotalDinehiro = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txtMoeda = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.Dados.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gdvFormaPgto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvRelatorio)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.pnlFechamentoCaixa.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configuraçõesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(987, 33);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // configuraçõesToolStripMenuItem
            // 
            this.configuraçõesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.parametrosToolStripMenuItem,
            this.gErarAssinaturaDigitalToolStripMenuItem});
            this.configuraçõesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.configuraçõesToolStripMenuItem.Name = "configuraçõesToolStripMenuItem";
            this.configuraçõesToolStripMenuItem.Size = new System.Drawing.Size(146, 29);
            this.configuraçõesToolStripMenuItem.Text = "Configurações";
            // 
            // parametrosToolStripMenuItem
            // 
            this.parametrosToolStripMenuItem.Name = "parametrosToolStripMenuItem";
            this.parametrosToolStripMenuItem.Size = new System.Drawing.Size(284, 30);
            this.parametrosToolStripMenuItem.Text = "Parametros";
            this.parametrosToolStripMenuItem.Click += new System.EventHandler(this.parametrosToolStripMenuItem_Click);
            // 
            // gErarAssinaturaDigitalToolStripMenuItem
            // 
            this.gErarAssinaturaDigitalToolStripMenuItem.Name = "gErarAssinaturaDigitalToolStripMenuItem";
            this.gErarAssinaturaDigitalToolStripMenuItem.Size = new System.Drawing.Size(284, 30);
            this.gErarAssinaturaDigitalToolStripMenuItem.Text = "Gerar Assinatura Digital";
            this.gErarAssinaturaDigitalToolStripMenuItem.Click += new System.EventHandler(this.GerarAssinaturaDigitalToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "F1: Tela Principal";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnFechar);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.btnSair);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 36);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(163, 289);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instruções";
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnFechar.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFechar.Location = new System.Drawing.Point(6, 196);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(150, 35);
            this.btnFechar.TabIndex = 118;
            this.btnFechar.Text = "Fechar Caixa";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(113, 16);
            this.label7.TabIndex = 69;
            this.label7.Text = "F7: 2ª. Via Cupom";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 16);
            this.label3.TabIndex = 68;
            this.label3.Text = "F12: Redução Z";
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.btnSair.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSair.Location = new System.Drawing.Point(5, 237);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(150, 35);
            this.btnSair.TabIndex = 67;
            this.btnSair.Text = "Sair";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click_1);
            this.btnSair.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnSair_KeyUp);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 170);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 16);
            this.label5.TabIndex = 8;
            this.label5.Text = "ESC: Sair";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "F3: Placa  Veiculo";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 122);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 16);
            this.label6.TabIndex = 5;
            this.label6.Text = "F11: Leitura X";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 74);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(140, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "F5: Pesquisar Produto";
            // 
            // btnFecharCaixa
            // 
            this.btnFecharCaixa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.btnFecharCaixa.Enabled = false;
            this.btnFecharCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFecharCaixa.Location = new System.Drawing.Point(364, 245);
            this.btnFecharCaixa.Name = "btnFecharCaixa";
            this.btnFecharCaixa.Size = new System.Drawing.Size(150, 35);
            this.btnFecharCaixa.TabIndex = 66;
            this.btnFecharCaixa.Text = "Finlizar Caixa";
            this.btnFecharCaixa.UseVisualStyleBackColor = false;
            this.btnFecharCaixa.Click += new System.EventHandler(this.btnFecharCaixa_Click);
            this.btnFecharCaixa.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnFecharCaixa_KeyDown);
            // 
            // Dados
            // 
            this.Dados.Controls.Add(this.groupBox2);
            this.Dados.Controls.Add(this.label9);
            this.Dados.Controls.Add(this.gdvRelatorio);
            this.Dados.Controls.Add(this.txtSomaTotal);
            this.Dados.Controls.Add(this.label8);
            this.Dados.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Dados.Location = new System.Drawing.Point(535, 36);
            this.Dados.Name = "Dados";
            this.Dados.Size = new System.Drawing.Size(444, 569);
            this.Dados.TabIndex = 66;
            this.Dados.TabStop = false;
            this.Dados.Text = "Relatórios";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.gdvFormaPgto);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 343);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(429, 157);
            this.groupBox2.TabIndex = 65;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Tipos de Pagamentos";
            // 
            // gdvFormaPgto
            // 
            this.gdvFormaPgto.AllowUserToAddRows = false;
            this.gdvFormaPgto.AllowUserToDeleteRows = false;
            this.gdvFormaPgto.BackgroundColor = System.Drawing.Color.White;
            this.gdvFormaPgto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvFormaPgto.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colTipo,
            this.colValor});
            this.gdvFormaPgto.Location = new System.Drawing.Point(6, 25);
            this.gdvFormaPgto.Name = "gdvFormaPgto";
            this.gdvFormaPgto.ReadOnly = true;
            this.gdvFormaPgto.Size = new System.Drawing.Size(417, 126);
            this.gdvFormaPgto.TabIndex = 0;
            this.gdvFormaPgto.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gdvFormaPgto_KeyUp);
            // 
            // colTipo
            // 
            this.colTipo.DataPropertyName = "TIPO";
            this.colTipo.HeaderText = "Tipo pagamento";
            this.colTipo.Name = "colTipo";
            this.colTipo.ReadOnly = true;
            this.colTipo.Width = 150;
            // 
            // colValor
            // 
            this.colValor.DataPropertyName = "VENDA";
            this.colValor.HeaderText = "Valor";
            this.colValor.Name = "colValor";
            this.colValor.ReadOnly = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Blue;
            this.label9.Location = new System.Drawing.Point(3, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(432, 17);
            this.label9.TabIndex = 69;
            this.label9.Text = "* Colabore com meio ambiente imprimir somente se for necessário. ";
            // 
            // gdvRelatorio
            // 
            this.gdvRelatorio.AllowUserToAddRows = false;
            this.gdvRelatorio.AllowUserToDeleteRows = false;
            this.gdvRelatorio.BackgroundColor = System.Drawing.Color.White;
            this.gdvRelatorio.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gdvRelatorio.Location = new System.Drawing.Point(6, 49);
            this.gdvRelatorio.Name = "gdvRelatorio";
            this.gdvRelatorio.ReadOnly = true;
            this.gdvRelatorio.Size = new System.Drawing.Size(429, 288);
            this.gdvRelatorio.TabIndex = 67;
            this.gdvRelatorio.KeyUp += new System.Windows.Forms.KeyEventHandler(this.gdvRelatorio_KeyUp);
            // 
            // txtSomaTotal
            // 
            this.txtSomaTotal.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSomaTotal.ForeColor = System.Drawing.Color.Blue;
            this.txtSomaTotal.Location = new System.Drawing.Point(158, 509);
            this.txtSomaTotal.Name = "txtSomaTotal";
            this.txtSomaTotal.Size = new System.Drawing.Size(277, 33);
            this.txtSomaTotal.TabIndex = 65;
            this.txtSomaTotal.Text = "R$ 0,00";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.label8.Location = new System.Drawing.Point(1, 512);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(149, 29);
            this.label8.TabIndex = 64;
            this.label8.Text = "Venda Total:";
            // 
            // btnDepartamento
            // 
            this.btnDepartamento.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDepartamento.Location = new System.Drawing.Point(6, 66);
            this.btnDepartamento.Name = "btnDepartamento";
            this.btnDepartamento.Size = new System.Drawing.Size(281, 35);
            this.btnDepartamento.TabIndex = 68;
            this.btnDepartamento.Text = "Departamentos";
            this.btnDepartamento.UseVisualStyleBackColor = true;
            this.btnDepartamento.Click += new System.EventHandler(this.btnDepartamento_Click);
            this.btnDepartamento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnDepartamento_KeyUp);
            // 
            // btnVendaCancelada
            // 
            this.btnVendaCancelada.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnVendaCancelada.Location = new System.Drawing.Point(6, 107);
            this.btnVendaCancelada.Name = "btnVendaCancelada";
            this.btnVendaCancelada.Size = new System.Drawing.Size(281, 35);
            this.btnVendaCancelada.TabIndex = 70;
            this.btnVendaCancelada.Text = "Vendas Canc.";
            this.btnVendaCancelada.UseVisualStyleBackColor = true;
            this.btnVendaCancelada.Click += new System.EventHandler(this.btnVendaCancelada_Click);
            this.btnVendaCancelada.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnVendaCancelada_KeyUp);
            // 
            // btnSetor
            // 
            this.btnSetor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSetor.Location = new System.Drawing.Point(6, 24);
            this.btnSetor.Name = "btnSetor";
            this.btnSetor.Size = new System.Drawing.Size(281, 35);
            this.btnSetor.TabIndex = 71;
            this.btnSetor.Text = "Setor";
            this.btnSetor.UseVisualStyleBackColor = true;
            this.btnSetor.Click += new System.EventHandler(this.btnSetor_Click);
            this.btnSetor.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnSetor_KeyUp);
            // 
            // btnImprimirDep
            // 
            this.btnImprimirDep.Enabled = false;
            this.btnImprimirDep.Image = global::PVe.Properties.Resources.impressora;
            this.btnImprimirDep.Location = new System.Drawing.Point(284, 66);
            this.btnImprimirDep.Name = "btnImprimirDep";
            this.btnImprimirDep.Size = new System.Drawing.Size(49, 35);
            this.btnImprimirDep.TabIndex = 73;
            this.btnImprimirDep.UseVisualStyleBackColor = true;
            this.btnImprimirDep.Click += new System.EventHandler(this.btnImprimirDep_Click);
            // 
            // btnImprimirVendaCanc
            // 
            this.btnImprimirVendaCanc.Enabled = false;
            this.btnImprimirVendaCanc.Image = global::PVe.Properties.Resources.impressora;
            this.btnImprimirVendaCanc.Location = new System.Drawing.Point(284, 107);
            this.btnImprimirVendaCanc.Name = "btnImprimirVendaCanc";
            this.btnImprimirVendaCanc.Size = new System.Drawing.Size(49, 35);
            this.btnImprimirVendaCanc.TabIndex = 74;
            this.btnImprimirVendaCanc.UseVisualStyleBackColor = true;
            this.btnImprimirVendaCanc.Click += new System.EventHandler(this.btnImprimirVendaCanc_Click);
            // 
            // btnImprimirSetor
            // 
            this.btnImprimirSetor.Enabled = false;
            this.btnImprimirSetor.Image = global::PVe.Properties.Resources.impressora;
            this.btnImprimirSetor.Location = new System.Drawing.Point(284, 24);
            this.btnImprimirSetor.Name = "btnImprimirSetor";
            this.btnImprimirSetor.Size = new System.Drawing.Size(49, 35);
            this.btnImprimirSetor.TabIndex = 75;
            this.btnImprimirSetor.UseVisualStyleBackColor = true;
            this.btnImprimirSetor.Click += new System.EventHandler(this.btnImprimirSetor_Click);
            // 
            // btnEstoque
            // 
            this.btnEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstoque.Location = new System.Drawing.Point(6, 244);
            this.btnEstoque.Name = "btnEstoque";
            this.btnEstoque.Size = new System.Drawing.Size(281, 35);
            this.btnEstoque.TabIndex = 76;
            this.btnEstoque.Text = "Pesquisa Estoque Dep.";
            this.btnEstoque.UseVisualStyleBackColor = true;
            this.btnEstoque.Click += new System.EventHandler(this.btnEstoque_Click);
            this.btnEstoque.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnEstoque_KeyUp);
            // 
            // btnImprimirEstoqueDep
            // 
            this.btnImprimirEstoqueDep.Enabled = false;
            this.btnImprimirEstoqueDep.Image = global::PVe.Properties.Resources.impressora;
            this.btnImprimirEstoqueDep.Location = new System.Drawing.Point(284, 244);
            this.btnImprimirEstoqueDep.Name = "btnImprimirEstoqueDep";
            this.btnImprimirEstoqueDep.Size = new System.Drawing.Size(49, 35);
            this.btnImprimirEstoqueDep.TabIndex = 77;
            this.btnImprimirEstoqueDep.UseVisualStyleBackColor = true;
            this.btnImprimirEstoqueDep.Click += new System.EventHandler(this.btnImprimirEstoqueDep_Click);
            // 
            // cbDepartamento
            // 
            this.cbDepartamento.FormattingEnabled = true;
            this.cbDepartamento.Location = new System.Drawing.Point(45, 209);
            this.cbDepartamento.Name = "cbDepartamento";
            this.cbDepartamento.Size = new System.Drawing.Size(288, 28);
            this.cbDepartamento.TabIndex = 78;
            this.cbDepartamento.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cbDepartamento_KeyUp);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.txtNumeroDep);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.btmImprimirRelatorio);
            this.groupBox3.Controls.Add(this.btnPesquisarEstoque);
            this.groupBox3.Controls.Add(this.cbDepartamento);
            this.groupBox3.Controls.Add(this.btnImprimirEstoqueDep);
            this.groupBox3.Controls.Add(this.btnEstoque);
            this.groupBox3.Controls.Add(this.btnImprimirSetor);
            this.groupBox3.Controls.Add(this.btnImprimirVendaCanc);
            this.groupBox3.Controls.Add(this.btnImprimirDep);
            this.groupBox3.Controls.Add(this.btnSetor);
            this.groupBox3.Controls.Add(this.btnVendaCancelada);
            this.groupBox3.Controls.Add(this.btnDepartamento);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(181, 36);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(348, 289);
            this.groupBox3.TabIndex = 66;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tipos de Pesquisas";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 186);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(26, 20);
            this.label12.TabIndex = 85;
            this.label12.Text = "Nº";
            // 
            // txtNumeroDep
            // 
            this.txtNumeroDep.Location = new System.Drawing.Point(6, 209);
            this.txtNumeroDep.Name = "txtNumeroDep";
            this.txtNumeroDep.ReadOnly = true;
            this.txtNumeroDep.Size = new System.Drawing.Size(33, 26);
            this.txtNumeroDep.TabIndex = 84;
            this.txtNumeroDep.Text = "0";
            this.txtNumeroDep.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(41, 186);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(262, 20);
            this.label10.TabIndex = 81;
            this.label10.Text = "Selecione Departamento Desejado:";
            // 
            // btmImprimirRelatorio
            // 
            this.btmImprimirRelatorio.Enabled = false;
            this.btmImprimirRelatorio.Image = global::PVe.Properties.Resources.impressora;
            this.btmImprimirRelatorio.Location = new System.Drawing.Point(284, 148);
            this.btmImprimirRelatorio.Name = "btmImprimirRelatorio";
            this.btmImprimirRelatorio.Size = new System.Drawing.Size(49, 35);
            this.btmImprimirRelatorio.TabIndex = 80;
            this.btmImprimirRelatorio.UseVisualStyleBackColor = true;
            this.btmImprimirRelatorio.Click += new System.EventHandler(this.btmImprimirRelatorio_Click);
            // 
            // btnPesquisarEstoque
            // 
            this.btnPesquisarEstoque.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPesquisarEstoque.Location = new System.Drawing.Point(6, 148);
            this.btnPesquisarEstoque.Name = "btnPesquisarEstoque";
            this.btnPesquisarEstoque.Size = new System.Drawing.Size(281, 35);
            this.btnPesquisarEstoque.TabIndex = 79;
            this.btnPesquisarEstoque.Text = "Relatório de Venda";
            this.btnPesquisarEstoque.UseVisualStyleBackColor = true;
            this.btnPesquisarEstoque.Click += new System.EventHandler(this.btnPesquisarEstoque_Click);
            this.btnPesquisarEstoque.KeyUp += new System.Windows.Forms.KeyEventHandler(this.btnPesquisarEstoque_KeyUp);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pb,
            this.toolStripStatusLabel1,
            this.lblQtdeDadosImportado});
            this.statusStrip1.Location = new System.Drawing.Point(0, 617);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(987, 22);
            this.statusStrip1.TabIndex = 67;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // pb
            // 
            this.pb.Name = "pb";
            this.pb.Size = new System.Drawing.Size(100, 16);
            this.pb.Visible = false;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(141, 17);
            this.toolStripStatusLabel1.Text = "Quant. Dados Importado:";
            // 
            // lblQtdeDadosImportado
            // 
            this.lblQtdeDadosImportado.Name = "lblQtdeDadosImportado";
            this.lblQtdeDadosImportado.Size = new System.Drawing.Size(13, 17);
            this.lblQtdeDadosImportado.Text = "0";
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // pnlFechamentoCaixa
            // 
            this.pnlFechamentoCaixa.Controls.Add(this.txtDespesas);
            this.pnlFechamentoCaixa.Controls.Add(this.label20);
            this.pnlFechamentoCaixa.Controls.Add(this.label19);
            this.pnlFechamentoCaixa.Controls.Add(this.lblDiferencaCaixa);
            this.pnlFechamentoCaixa.Controls.Add(this.btnCalcular);
            this.pnlFechamentoCaixa.Controls.Add(this.txtTotalRecebimento);
            this.pnlFechamentoCaixa.Controls.Add(this.btnFecharCaixa);
            this.pnlFechamentoCaixa.Controls.Add(this.txtTotalSangria);
            this.pnlFechamentoCaixa.Controls.Add(this.label11);
            this.pnlFechamentoCaixa.Controls.Add(this.label13);
            this.pnlFechamentoCaixa.Controls.Add(this.label14);
            this.pnlFechamentoCaixa.Controls.Add(this.txtTotalConvenio);
            this.pnlFechamentoCaixa.Controls.Add(this.label15);
            this.pnlFechamentoCaixa.Controls.Add(this.txtTotalCheque);
            this.pnlFechamentoCaixa.Controls.Add(this.label16);
            this.pnlFechamentoCaixa.Controls.Add(this.txtTotalCartao);
            this.pnlFechamentoCaixa.Controls.Add(this.label17);
            this.pnlFechamentoCaixa.Controls.Add(this.txtTotalDinehiro);
            this.pnlFechamentoCaixa.Controls.Add(this.label18);
            this.pnlFechamentoCaixa.Controls.Add(this.txtMoeda);
            this.pnlFechamentoCaixa.Location = new System.Drawing.Point(13, 331);
            this.pnlFechamentoCaixa.Name = "pnlFechamentoCaixa";
            this.pnlFechamentoCaixa.Size = new System.Drawing.Size(517, 283);
            this.pnlFechamentoCaixa.TabIndex = 69;
            this.pnlFechamentoCaixa.Visible = false;
            // 
            // txtDespesas
            // 
            this.txtDespesas.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDespesas.Location = new System.Drawing.Point(130, 169);
            this.txtDespesas.Name = "txtDespesas";
            this.txtDespesas.Size = new System.Drawing.Size(243, 26);
            this.txtDespesas.TabIndex = 6;
            this.txtDespesas.Text = "0,00";
            this.txtDespesas.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtDespesas_KeyDown);
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label20.ForeColor = System.Drawing.Color.Blue;
            this.label20.Location = new System.Drawing.Point(3, 170);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(115, 24);
            this.label20.TabIndex = 118;
            this.label20.Text = "DESPESAS:";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.ForeColor = System.Drawing.Color.Blue;
            this.label19.Location = new System.Drawing.Point(4, 250);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(123, 24);
            this.label19.TabIndex = 117;
            this.label19.Text = "DIFERENÇA:";
            // 
            // lblDiferencaCaixa
            // 
            this.lblDiferencaCaixa.AutoSize = true;
            this.lblDiferencaCaixa.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDiferencaCaixa.ForeColor = System.Drawing.Color.Blue;
            this.lblDiferencaCaixa.Location = new System.Drawing.Point(130, 250);
            this.lblDiferencaCaixa.Name = "lblDiferencaCaixa";
            this.lblDiferencaCaixa.Size = new System.Drawing.Size(45, 24);
            this.lblDiferencaCaixa.TabIndex = 116;
            this.lblDiferencaCaixa.Text = "0,00";
            // 
            // btnCalcular
            // 
            this.btnCalcular.Location = new System.Drawing.Point(379, 197);
            this.btnCalcular.Name = "btnCalcular";
            this.btnCalcular.Size = new System.Drawing.Size(31, 23);
            this.btnCalcular.TabIndex = 114;
            this.btnCalcular.Text = "...";
            this.btnCalcular.UseVisualStyleBackColor = true;
            this.btnCalcular.Click += new System.EventHandler(this.btnCalcular_Click);
            this.btnCalcular.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnCalcular_KeyDown);
            // 
            // txtTotalRecebimento
            // 
            this.txtTotalRecebimento.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalRecebimento.Location = new System.Drawing.Point(130, 195);
            this.txtTotalRecebimento.Name = "txtTotalRecebimento";
            this.txtTotalRecebimento.ReadOnly = true;
            this.txtTotalRecebimento.Size = new System.Drawing.Size(243, 26);
            this.txtTotalRecebimento.TabIndex = 113;
            this.txtTotalRecebimento.TabStop = false;
            this.txtTotalRecebimento.Text = "0,00";
            // 
            // txtTotalSangria
            // 
            this.txtTotalSangria.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalSangria.Location = new System.Drawing.Point(130, 143);
            this.txtTotalSangria.Name = "txtTotalSangria";
            this.txtTotalSangria.Size = new System.Drawing.Size(243, 26);
            this.txtTotalSangria.TabIndex = 5;
            this.txtTotalSangria.Text = "0,00";
            this.txtTotalSangria.TextChanged += new System.EventHandler(this.txtTotalSangria_TextChanged);
            this.txtTotalSangria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalSangria_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.ForeColor = System.Drawing.Color.Blue;
            this.label11.Location = new System.Drawing.Point(3, 144);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(98, 24);
            this.label11.TabIndex = 111;
            this.label11.Text = "SANGRIA:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.ForeColor = System.Drawing.Color.Blue;
            this.label13.Location = new System.Drawing.Point(3, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(97, 24);
            this.label13.TabIndex = 110;
            this.label13.Text = "MOEDAS:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.ForeColor = System.Drawing.Color.Blue;
            this.label14.Location = new System.Drawing.Point(3, 196);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(109, 24);
            this.label14.TabIndex = 109;
            this.label14.Text = "TOTAL CX:";
            // 
            // txtTotalConvenio
            // 
            this.txtTotalConvenio.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalConvenio.Location = new System.Drawing.Point(130, 117);
            this.txtTotalConvenio.Name = "txtTotalConvenio";
            this.txtTotalConvenio.Size = new System.Drawing.Size(243, 26);
            this.txtTotalConvenio.TabIndex = 4;
            this.txtTotalConvenio.Text = "0,00";
            this.txtTotalConvenio.TextChanged += new System.EventHandler(this.txtTotalConvenio_TextChanged);
            this.txtTotalConvenio.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalConvenio_KeyDown);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Blue;
            this.label15.Location = new System.Drawing.Point(3, 118);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(116, 24);
            this.label15.TabIndex = 107;
            this.label15.Text = "CONVENIO:";
            // 
            // txtTotalCheque
            // 
            this.txtTotalCheque.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCheque.Location = new System.Drawing.Point(130, 91);
            this.txtTotalCheque.Name = "txtTotalCheque";
            this.txtTotalCheque.Size = new System.Drawing.Size(243, 26);
            this.txtTotalCheque.TabIndex = 3;
            this.txtTotalCheque.Text = "0,00";
            this.txtTotalCheque.TextChanged += new System.EventHandler(this.txtTotalCheque_TextChanged);
            this.txtTotalCheque.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalCheque_KeyDown);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.ForeColor = System.Drawing.Color.Blue;
            this.label16.Location = new System.Drawing.Point(3, 92);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(96, 24);
            this.label16.TabIndex = 105;
            this.label16.Text = "CHEQUE:";
            // 
            // txtTotalCartao
            // 
            this.txtTotalCartao.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalCartao.Location = new System.Drawing.Point(130, 65);
            this.txtTotalCartao.Name = "txtTotalCartao";
            this.txtTotalCartao.Size = new System.Drawing.Size(243, 26);
            this.txtTotalCartao.TabIndex = 2;
            this.txtTotalCartao.Text = "0,00";
            this.txtTotalCartao.TextChanged += new System.EventHandler(this.txtTotalCartao_TextChanged);
            this.txtTotalCartao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalCartao_KeyDown);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.ForeColor = System.Drawing.Color.Blue;
            this.label17.Location = new System.Drawing.Point(3, 66);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(94, 24);
            this.label17.TabIndex = 103;
            this.label17.Text = "CARTAO:";
            // 
            // txtTotalDinehiro
            // 
            this.txtTotalDinehiro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTotalDinehiro.Location = new System.Drawing.Point(130, 39);
            this.txtTotalDinehiro.Name = "txtTotalDinehiro";
            this.txtTotalDinehiro.Size = new System.Drawing.Size(243, 26);
            this.txtTotalDinehiro.TabIndex = 1;
            this.txtTotalDinehiro.Text = "0,00";
            this.txtTotalDinehiro.TextChanged += new System.EventHandler(this.txtTotalDinehiro_TextChanged);
            this.txtTotalDinehiro.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTotalDinehiro_KeyDown);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.ForeColor = System.Drawing.Color.Blue;
            this.label18.Location = new System.Drawing.Point(3, 40);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(105, 24);
            this.label18.TabIndex = 101;
            this.label18.Text = "DINHEIRO:";
            // 
            // txtMoeda
            // 
            this.txtMoeda.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMoeda.Location = new System.Drawing.Point(130, 13);
            this.txtMoeda.Name = "txtMoeda";
            this.txtMoeda.Size = new System.Drawing.Size(243, 26);
            this.txtMoeda.TabIndex = 0;
            this.txtMoeda.Text = "0,00";
            this.txtMoeda.TextChanged += new System.EventHandler(this.txtMoeda_TextChanged);
            this.txtMoeda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMoeda_KeyDown);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(0, 315);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(11, 10);
            this.button1.TabIndex = 118;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_2);
            // 
            // frmPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(987, 639);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pnlFechamentoCaixa);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.Dados);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tela Principal";
            this.Load += new System.EventHandler(this.frmPrincipal_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.Dados.ResumeLayout(false);
            this.Dados.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gdvFormaPgto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gdvRelatorio)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.pnlFechamentoCaixa.ResumeLayout(false);
            this.pnlFechamentoCaixa.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem configuraçõesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametrosToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox Dados;
        private System.Windows.Forms.DataGridView gdvRelatorio;
        private System.Windows.Forms.Button btnFecharCaixa;
        private System.Windows.Forms.TextBox txtSomaTotal;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ComboBox cbDepartamento;
        private System.Windows.Forms.Button btnImprimirEstoqueDep;
        private System.Windows.Forms.Button btnEstoque;
        private System.Windows.Forms.Button btnImprimirSetor;
        private System.Windows.Forms.Button btnImprimirVendaCanc;
        private System.Windows.Forms.Button btnImprimirDep;
        private System.Windows.Forms.Button btnSetor;
        private System.Windows.Forms.Button btnVendaCancelada;
        private System.Windows.Forms.Button btnDepartamento;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView gdvFormaPgto;
        private System.Windows.Forms.Button btnSair;
        private System.Windows.Forms.Button btmImprimirRelatorio;
        private System.Windows.Forms.Button btnPesquisarEstoque;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtNumeroDep;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ToolStripMenuItem gErarAssinaturaDigitalToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar pb;
        private System.Windows.Forms.Timer timer1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel lblQtdeDadosImportado;
        private System.Windows.Forms.Panel pnlFechamentoCaixa;
        private System.Windows.Forms.TextBox txtTotalRecebimento;
        private System.Windows.Forms.TextBox txtTotalSangria;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txtTotalConvenio;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox txtTotalCheque;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox txtTotalCartao;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txtTotalDinehiro;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txtMoeda;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn colValor;
        private System.Windows.Forms.Button btnCalcular;
        private System.Windows.Forms.Label lblDiferencaCaixa;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtDespesas;
        private System.Windows.Forms.Label label20;
    }
}