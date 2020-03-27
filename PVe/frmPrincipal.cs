using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Collections;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing.Printing;

namespace PVe
{
    public partial class frmPrincipal : Form
    {
        RegraNegocio.VendaRegraNegocios novaVenda;
        RegraNegocio.UsuarioRegraNegocio novoUsuario;
        RegraNegocio.ParametroRegraNegocio novoParametro;
        RegraNegocio.FormaPagamentoRegraNegocio novaFormaPgto;
        RegraNegocio.ProdutoRegraNegocio novoProduto;
        RegraNegocio.DepartamentoRegraNegocio novoDepartamento;
        RegraNegocio.PagamentoVendaRegraNegocios novoPagamentoVenda;
        RegraNegocio.NumCaixaRegraNegocios novoCaixa;
        RegraNegocio.EscPos escPos;
        RegraNegocio.MateriaPrimaRegraNegocio novaMateriaPrima;
        RegraNegocio.SetorRegraNegocios novoSetor;
        RegraNegocio.SangriaRegraNegocios novaSangria;
        RegraNegocio.EscPos esc;
        RegraNegocio.TribRegraNegocio novaTrib;

        string pathBKP = @"C:\BACKUP\";

        //BKP

        private SqlConnection conn;
        private SqlCommand com;
        private SqlDataReader red;
        string sql = "";
        string connectionString = "";

        Button button = new Button();

        //DADOS FECHAMENTO DO CAIXA

        public decimal moeda, dinheiro, cheque, cartao, convenio, sangria, despesa, totalcaixa, resultado, diferencaCaixa = 0;
        string tipoPagamento = "";

        frmVenda frmVenda;
        string codRel, descRel, qtdeRel, totaRel, impressora;
        int idRetorno, qtdeCupom;
        int idUsuario = 0;
        DateTime data;
        decimal somaTotal = 0;
        string numeroDepartamento = "";
        string nomeImpressora = "";
        string printerName = "";

        //DADOS CLIENTE
        string nomeCliente = "";
        string enderecoCliente = "";
        string numeroCliente = "";
        string bairroCliente = "";
        string cidadeCliente = "";
        string ufCliente = "";
        string cepCliente = "";
        string cnpjCliente = "";
        string ieCliente = "";
        string dep, numDep = "";



        //DADOS RELATORIO
        string codigoBarras, descrica, qtde, total, estoque;
        string numCom = "";

        //------------------------------------------------------------------------------------------------------------------

        Boolean vendaNaoFechado = false;
        int codInternoprod, idvenda = 0;
        decimal qtdeVendida, estoqueAtual, novoEstoque = 0;
        string descricao_produto = "";
        //------------------------------------------------------------------------------------------------------------------

        //VARIAVEL LEITURA X.........................
        string aliquotaX = "";
        decimal valorX = 0;
        int idTipoPagamentoX = 0;
        decimal valorvendaX = 0;
        int qtdeVendaCancelX = 0;
        decimal valorVendaCanceladaX = 0;
        string descricaoTipoPgto = "";
        int qtdevenda = 0;
        string portaImpressora = "";

        string AX, BX, CX, DX, EX = "";

        string AB, BB, CB, DB, EB = "";

        //PRODUTO TABELA COMPOSTA
        decimal estoqueComposto, qtdeCoposto = 0;
        int idComposto = 0;
        decimal estoqueMateriaPrima = 0;


        public frmPrincipal(frmVenda fv)
        {
            InitializeComponent();
            this.frmVenda = fv;
        }


        private void produtosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOperador frmOperador = new frmOperador();
            frmOperador.ShowDialog();
        }

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCidade frmCidade = new frmCidade();
            frmCidade.ShowDialog();
        }

        private void cartãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCartao frmCartao = new frmCartao();
            frmCartao.ShowDialog();
        }

        private void parametrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginAcessoPrincipal frmAcessoPrincipal = new LoginAcessoPrincipal(frmVenda);
            frmAcessoPrincipal.ShowDialog();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void departamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDepartamento frmDepartamento = new frmDepartamento();
            frmDepartamento.ShowDialog();
        }

        private void fabricanteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFabricante frmFabricante = new frmFabricante();
            frmFabricante.ShowDialog();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            try
            {
                nomeImpressora = frmVenda.nomeImpressora;
                EstiloCoresLinha();
                PesquisarVendaTotalTurno();
                PesquisaFormaPgamento();
                CarregaCombo();
                cbDepartamento.Focus();
                qtdeCupom = frmVenda.qtdeCupom;
                PesquisarDadosFechamentoCaixa();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void Moeda(ref TextBox txt)
        {
            string n = string.Empty;
            decimal v = 0;

            try
            {
                n = txt.Text.Replace(",", "").Replace(".", "");

                if (n.Equals(""))
                    n = "";
                n = n.PadLeft(3, '0');
                if (n.Length > 3 & n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length - 1);
                v = Convert.ToDecimal(n) / 100;
                txt.Text = string.Format("{0:N}", v);
                txt.SelectionStart = txt.Text.Length;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registro do Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PesquisaFormaPgamento()
        {
            try
            {
                DateTime data = DateTime.Now.Date;
                novaFormaPgto = new RegraNegocio.FormaPagamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaFormaPgto.PesquisarFPgto(data, frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    gdvFormaPgto.DataSource = dadosTabela;
                    EstiloCoresLinha();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Pesquisa Forma Pgamento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void EstiloCoresLinha()
        {
            try
            {
                for (int i = 0; i < gdvRelatorio.Rows.Count; i += 2)
                {
                    gdvRelatorio.Rows[i].DefaultCellStyle.BackColor = Color.Honeydew;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método EstiloCoresLinha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnRelatorioTotal_Click(object sender, EventArgs e)
        {
            try
            {
                //METODO PARA GERAR RELATORIO TOTAL.............................................................
                decimal somtaTotal = 0;

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVendaTotalAll();

                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("COD        DESCRIÇÃO       TOTAL");

                if (dadosTabela.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        codRel = dadosTabela.Rows[i]["COD_BARRA"].ToString();
                        descRel = dadosTabela.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                        totaRel = dadosTabela.Rows[i]["TOTAL"].ToString();

                        if (codRel.Length > 0)
                        {
                            codRel = codRel.Substring(8, 5);
                        }

                        if (descRel.Length > 20)
                        {
                            descRel = descRel.Substring(0, 15);
                        }

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRecebimentoNaoFiscalMFD(codRel + "       " + descRel + "       " + totaRel);
                    }

                    MessageBox.Show("Relatório Total foi Relaizado com Sucesso", "Informação");

                    novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                    dadosTabela = new DataTable();
                    dadosTabela = novoUsuario.PesquisaUsuarioLogado(frmVenda.numcaixa);

                    if (dadosTabela.Rows.Count > 0)
                    {
                        novoUsuario.AlteraStatusUsuarioFechado(frmVenda.idUsuario);
                        MessageBox.Show("Turno foi Finalizado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        frmVenda.PesquisaUsuarioLogado();

                        this.Close();
                        frmVenda.Refresh();
                        frmVenda.AtualizarGridAberto();
                        EstiloCoresLinha();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void fechaTurnoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
            DataTable dadosTabela = new DataTable();
            dadosTabela = novoUsuario.PesquisaUsuarioLogado(frmVenda.numcaixa);

            if (dadosTabela.Rows.Count > 0)
            {
                novoUsuario.AlteraStatusUsuarioFechado(frmVenda.idUsuario);
                MessageBox.Show("Turno foi Finalizado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                frmVenda.PesquisaUsuarioLogado();

                this.Close();
                frmVenda.Refresh();
                frmVenda.AtualizarGridAberto();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
            DataTable dadosTabela = new DataTable();
            dadosTabela = novoUsuario.PesquisaUsuarioLogado(frmVenda.numcaixa);
            Dispose();

            if (dadosTabela.Rows.Count > 0)
            {
                frmVenda.PesquisaUsuarioLogado();
                frmVenda.AtualizarGridAberto();

                if (MessageBox.Show("Por Favor Confirmar o Fechamento de Turno.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    novoUsuario.AlteraStatusUsuarioFechado(frmVenda.idUsuario);
                    MessageBox.Show("Turno foi Finalizado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    frmVenda.Refresh();
                    this.Close();

                    dadosTabela = novoUsuario.PesquisaUsuarioLogado(frmVenda.numcaixa);

                    frmVenda.AtualizarGridAberto();
                    EstiloCoresLinha();
                }
            }
        }

        public void PesquisarVendaTotalTurno()
        {
            try
            {
                somaTotal = 0;
                idUsuario = frmVenda.idUsuario;
                data = DateTime.Now.Date;
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                dadosTabelaVenda = novaVenda.PesquisarVendaTotalTurno(frmVenda.idUsuarioLogado, data, frmVenda.numcaixa);

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    txtSomaTotal.Text = Convert.ToDecimal(somaTotal).ToString("C2");
                    gdvRelatorio.DataSource = dadosTabelaVenda;
                    EstiloCoresLinha();

                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        somaTotal += Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                    }

                    txtSomaTotal.Text = somaTotal.ToString("C2");
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Pesquisar Venda Total Turno.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnDepartamento_Click(object sender, EventArgs e)
        {
            novaVenda = new RegraNegocio.VendaRegraNegocios();
            DataTable dadosTabela = new DataTable();

            dadosTabela = novaVenda.FecharVendaDepartamentos(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

            decimal somaT = 0;

            btnImprimirDep.Enabled = true;

            if (btnImprimirDep.Enabled == true)
            {
                btnImprimirSetor.Enabled = false;
                btnImprimirVendaCanc.Enabled = false;
                btmImprimirRelatorio.Enabled = false;
                btnImprimirEstoqueDep.Enabled = false;
            }

            gdvRelatorio.DataSource = dadosTabela;

            if (gdvRelatorio.Rows.Count > 0)
            {
                for (int i = 0; i < gdvRelatorio.Rows.Count; i++)
                {
                    somaT += Convert.ToDecimal(gdvRelatorio.Rows[i].Cells["Total"].Value.ToString());
                }

                txtSomaTotal.Text = somaTotal.ToString("C2");
                EstiloCoresLinha();
            }

            if (somaT > 0)
            {
                txtSomaTotal.ForeColor = Color.Blue;
            }
            else
            {
                txtSomaTotal.ForeColor = Color.Red;
            }
        }

        public void FecharVendaDepartamentos()
        {
            try
            {
                idUsuario = frmVenda.idUsuario;
                data = DateTime.Now.Date;
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.FecharVendaDepartamentos(idUsuario, frmVenda.numcaixa);
                gdvRelatorio.DataSource = dadosTabela;

                decimal somaTotal = 0;

                if (dadosTabela.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        somaTotal += Convert.ToDecimal(gdvRelatorio.Rows[i].Cells["Total"].Value.ToString());
                    }

                    txtSomaTotal.Text = somaTotal.ToString();
                    EstiloCoresLinha();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Fechar Venda Departamentos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void FechaLogin()
        {
            try
            {
                idUsuario = frmVenda.idUsuarioLogado;
                novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                novoUsuario.AlteraStatusUsuarioFechado(frmVenda.idUsuarioLogado);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Fecha Login.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void AlteraVendaPagamentoFechado()
        {
            try
            {
                novaFormaPgto = new RegraNegocio.FormaPagamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();

                novaFormaPgto.AlteraVendaPagamentoFechado(frmVenda.idUsuarioLogado, frmVenda.numcaixa);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Altera Venda Pagamento Fechado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void AlterarBaixadoVenda()
        {
            try
            {
                int idUsuario = Convert.ToInt32(frmVenda.idUsuarioLogado);
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                novaVenda.AlterarBaixadoVenda(frmVenda.idUsuarioLogado, frmVenda.numcaixa);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Alterar Baixado Venda.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void PesquisarVendaCancelado()
        {
            try
            {
                int idUsuario = Convert.ToInt32(frmVenda.idUsuario);
                DateTime data = DateTime.Now.Date;
                decimal somaTot = 0;

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarVendaCancelado(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                gdvRelatorio.DataSource = dadosTabela;
                EstiloCoresLinha();

                for (int i = 0; i < gdvRelatorio.Rows.Count; i++)
                {
                    somaTot += Convert.ToDecimal(gdvRelatorio.Rows[i].Cells["Total"].Value.ToString());
                }

                txtSomaTotal.Text = somaTot.ToString("C2");

                if (somaTot < 0)
                {
                    txtSomaTotal.ForeColor = Color.Red;
                }
                else
                {
                    txtSomaTotal.ForeColor = Color.Blue;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Gerar Relatorio Venda Cancelado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnFecharCaixa_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Realmente Deseja Fecha Caixa Nº: " + frmVenda.numcaixa.ToString().Trim().PadLeft(3, '0'), "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    int qtdeVenda = frmVenda.qtdevenda;
                    int numVenda = frmVenda.numCupom;

                    if (qtdeVenda == 0)
                    {
                        FechamentoVendaBaixaEstoque();
                        PesquisarEstoqueMateriaPrimaBaixaEstoque();
                        MontarReducaoZ();

                    }
                    else if (qtdeVenda > 0)
                    {
                        if (MessageBox.Show("Exitem Venda em Aberto no Ponto de Venda.\nDeseja Cancelar Venda Nº " + numVenda + " em Aberto", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            frmLoginCancelarVenda frmCancelaVenda = new frmLoginCancelarVenda(frmVenda);
                            frmCancelaVenda.ShowDialog();
                        }
                        else
                        {
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void BKP_Banco()
        {
            try
            {
                //CONECTAR COM BANCO

                connectionString = "Data Source=CLAUDEMIR-PC\\SQLEXPRESS;Initial Catalog=CFe;Integrated Security=True";
                string instaciaBanco = "CFe";
                string enderecoBkp = "";
                conn = new SqlConnection(connectionString);
                conn.Open();

                sql = "EXEC sp_databases";

                com = new SqlCommand(sql, conn);
                red = com.ExecuteReader();
                red.Dispose();
                conn.Close();
                conn.Dispose();

                if (instaciaBanco != "")
                {
                    MessageBox.Show("Banco Conectado.");

                    //SELECIONE PATH 
                    enderecoBkp = pathBKP;

                    //BKP

                    conn = new SqlConnection(connectionString);
                    conn.Open();
                    sql = "BACKUP DATABASE " + instaciaBanco + " TO DISK = '" + enderecoBkp + instaciaBanco + ".bkp";
                    com = new SqlCommand(sql, conn);
                    com.ExecuteNonQuery();

                    MessageBox.Show("Backup foi Realizado com Sucesso.");
                }
                else
                {
                    MessageBox.Show("Banco Desconectado.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Atenção");
            }
        }

        #region RELATORIOS SAT E BEMATECH

        public void GerarRealtorioSetor1()
        {
            try
            {
                string estabelecimento, endereco, numero, cep, bairro = "";
                string usuario = frmVenda.NomeUsuario;
                string codBarra;
                int setor = 1;
                string desc;
                string total;
                string qtde;
                string espaco7 = "         ";
                string espaco5 = "     ";
                string espaco3 = "    ";
                decimal sTotal = 0;

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();

                dadosTabela = novaVenda.GerarRealtorioSetor1(frmVenda.idUsuarioLogado, setor, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco7 + "**** VENDA POR SETOR ****");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("SETOR: " + setor);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Cod     Descrição         Qtde     Total");

                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        codBarra = dadosTabela.Rows[i]["Cod"].ToString();
                        desc = dadosTabela.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabela.Rows[i]["qtde"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        sTotal += Convert.ToDecimal(dadosTabela.Rows[i]["Total"].ToString());

                        codBarra = codBarra.Substring(8, 5);
                        codBarra.PadLeft(0);

                        desc += espaco7;

                        if (desc.Length < 10)
                        {
                            desc.PadLeft(0);
                        }

                        if (desc.Length > 14)
                        {
                            desc = desc.Substring(0, 13);
                            desc.PadLeft(0);
                        }

                        if (total.Length > 6)
                        {
                            total = total.Substring(0, 6);
                        }

                        if (qtde.Length >= 5)
                        {
                            qtde = qtde.Substring(0, 5);
                        }

                        total.PadLeft(0);

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codBarra + espaco3 + desc + espaco3 + qtde + espaco5 + total);

                    }

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("VENDA TOTAL:" + sTotal.ToString("C2"));

                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                    EstiloCoresLinha();
                }

                else
                {
                    LimparCampos();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Gerar Relatorio Total Sat.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void GerarRelatorioSetor2()
        {
            try
            {
                string estabelecimento, endereco, numero, cep, bairro = "";
                string usuario = frmVenda.NomeUsuario;
                string codBarra;
                int setor = 2;
                string desc;
                string total;
                string qtde;
                string espaco7 = "         ";
                string espaco5 = "     ";
                string espaco3 = "    ";
                decimal sTotal = 0;

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();

                dadosTabela = novaVenda.GerarRealtorioSetor2(idUsuario, setor, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco7 + "**** VENDA POR SETOR ****");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("SETOR: " + setor);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Cod     Descrição         Qtde     Total");

                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        codBarra = dadosTabela.Rows[i]["Cod"].ToString();
                        desc = dadosTabela.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabela.Rows[i]["qtde"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        sTotal += Convert.ToDecimal(dadosTabela.Rows[i]["Total"].ToString());

                        codBarra = codBarra.Substring(8, 5);
                        codBarra.PadLeft(0);

                        desc += espaco7;

                        if (desc.Length < 10)
                        {
                            desc.PadLeft(0);
                        }

                        if (desc.Length > 14)
                        {
                            desc = desc.Substring(0, 13);
                            desc.PadLeft(0);
                        }

                        if (total.Length > 6)
                        {
                            total = total.Substring(0, 6);
                        }

                        if (qtde.Length >= 5)
                        {
                            qtde = qtde.Substring(0, 5);
                        }

                        total.PadLeft(0);

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codBarra + espaco3 + desc + espaco3 + qtde + espaco5 + total);

                    }
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("VENDA TOTAL:" + sTotal.ToString("C2"));

                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                    EstiloCoresLinha();
                }

                else
                {
                    LimparCampos();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método GerarRelatorioTotalSat.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void GerarRelatorioSetor()
        {
            try
            {
                string estabelecimento, endereco, numero, cep, bairro = "";
                string usuario = frmVenda.NomeUsuario;
                string setor;
                string total;
                string qtde;
                string espaco7 = "         ";
                string espaco5 = "     ";
                string espaco3 = "    ";
                decimal sTotal = 0;

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVendaSetor(idUsuario, data, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco7 + "**** VENDA(S) POR SETOR(ES) ****");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Setor          Total");

                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        setor = dadosTabela.Rows[i]["Setor"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        sTotal += Convert.ToDecimal(dadosTabela.Rows[i]["Total"].ToString());

                        if (setor.Length < 7)
                        {
                            setor += espaco7;
                        }

                        if (setor.Length > 7)
                        {
                            setor = setor.Substring(0, 7);
                        }

                        if (total.Length > 6)
                        {
                            total = total.Substring(0, 6);
                        }

                        total.PadLeft(0);

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(setor + espaco3 + espaco5 + total);

                    }
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("VENDA TOTAL:" + sTotal.ToString("C2"));

                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                    EstiloCoresLinha();
                }
                else
                {
                    LimparCampos();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método GerarRelatorioSetor.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void GerarRelatorioVendaCanceladaSat()
        {
            try
            {
                string estabelecimento, endereco, numero, cep, bairro = "";
                string usuario = frmVenda.NomeUsuario;
                string codBarra;
                string desc;
                string total;
                string qtde;
                string espaco7 = "       ";
                string espaco5 = "   ";
                string espaco3 = "  ";
                decimal sTotal = 0;

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarVendaCancelado(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco7 + "**** VENDA(S) CANCELADA(S) ****");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Cod   Descrição     Qtde      Total");

                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        codBarra = dadosTabela.Rows[i]["Cod"].ToString();
                        desc = dadosTabela.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabela.Rows[i]["qtde"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        sTotal += Convert.ToDecimal(dadosTabela.Rows[i]["Total"].ToString());

                        codBarra = codBarra.Substring(8, 5);
                        codBarra.PadLeft(0);

                        desc += espaco7;

                        if (desc.Length < 10)
                        {
                            desc.PadLeft(0);
                        }

                        if (desc.Length > 12)
                        {
                            desc = desc.Substring(0, 10);
                            desc.PadLeft(0);
                        }

                        if (total.Length >= 8)
                        {
                            total = total.Substring(0, 7);
                        }

                        if (qtde.Length < 7)
                        {
                            qtde += espaco5;
                        }

                        if (qtde.Length >= 8)
                        {
                            qtde = qtde.Substring(0, 7);
                        }

                        total.PadLeft(0);

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codBarra + espaco3 + desc + espaco3 + qtde + espaco5 + total);

                    }

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("TOTAL VENDA(S) CANCELADA(S):" + sTotal.ToString("C2"));

                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                    EstiloCoresLinha();
                }
                else
                {
                    LimparCampos();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método GerarRelatorioTotalSat.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void GerarRelatorioBemasatSetor1()
        {
            try
            {
                string estabelecimento, endereco, numero, cep, bairro = "";
                string usuario = frmVenda.NomeUsuario;
                string codBarra;
                int setor = 1;
                string desc;
                string total;
                string qtde;
                string espaco7 = "         ";
                string espaco5 = "     ";
                string espaco3 = "    ";
                decimal sTotal = 0;

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();

                dadosTabela = novaVenda.GerarRealtorioSetor1(idUsuario, setor, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.MP2032.IniciaPorta("COM11");
                    idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);

                    idRetorno = RegraNegocio.MP2032.BematechTX("**** VENDA POR SETOR ****");
                    //idRetorno = RegraNegocio.MP2032.BematechTX("SETOR: " + setor);
                    //idRetorno = RegraNegocio.MP2032.BematechTX("Cod     Descrição         Qtde     Total");

                    //for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    //{
                    //    codBarra = dadosTabela.Rows[i]["Cod"].ToString();
                    //    desc = dadosTabela.Rows[i]["Descricao"].ToString();
                    //    qtde = dadosTabela.Rows[i]["qtde"].ToString();
                    //    total = dadosTabela.Rows[i]["Total"].ToString();

                    //    sTotal += Convert.ToDecimal(dadosTabela.Rows[i]["Total"].ToString());

                    //    codBarra = codBarra.Substring(8, 5);
                    //    codBarra.PadLeft(0);

                    //    desc += espaco7;

                    //    if (desc.Length < 10)
                    //    {
                    //        desc.PadLeft(0);
                    //    }

                    //    if (desc.Length > 14)
                    //    {
                    //        desc = desc.Substring(0, 13);
                    //        desc.PadLeft(0);
                    //    }

                    //    if (total.Length > 6)
                    //    {
                    //        total = total.Substring(0, 6);
                    //    }

                    //    if (qtde.Length >= 5)
                    //    {
                    //        qtde = qtde.Substring(0, 5);
                    //    }

                    //    total.PadLeft(0);

                    //    idRetorno = RegraNegocio.MP2032.BematechTX(codBarra + espaco3 + desc + espaco3 + qtde + espaco5 + total);

                    //}

                    //idRetorno = RegraNegocio.MP2032.BematechTX("\n");
                    //idRetorno = RegraNegocio.MP2032.BematechTX("VENDA TOTAL:" + sTotal.ToString("C2"));

                    //novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    //DataTable dadosParametro = new DataTable();
                    //dadosParametro = novoParametro.PesquisaParametroE();

                    //estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    //endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    //numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    //cep = dadosParametro.Rows[0]["CEP"].ToString();
                    //bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    //idRetorno = RegraNegocio.MP2032.BematechTX("\n");
                    //idRetorno = RegraNegocio.MP2032.BematechTX(estabelecimento + " - Operador(a):" + usuario);
                    //idRetorno = RegraNegocio.MP2032.BematechTX(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.MP2032.FechaPorta();
                    EstiloCoresLinha();
                }

                else
                {
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarQtde_QtdeEstoqueGerarRelatorio()
        {
            try
            {
                //GERA RELATORIO VENDA TOTAL DO DIA MOSTRAR QTDE DE ESTOQUE E QTDE VENDIDA.................................................

                string estabelecimento, endereco, numero, cep, bairro = "";
                string usuario = frmVenda.NomeUsuario;
                string codBarra;
                string desc;
                string total;
                string qtde;
                string espaco7 = "                      ";
                string espaco6 = "          ";
                string espaco5 = "   ";
                string espaco3 = "  ";
                decimal sTotal = 0;
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVendaEstoque = new DataTable();
                dadosTabelaVendaEstoque = novaVenda.PesquisarQtde_QtdeEstoque(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabelaVendaEstoque.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco6 + "**** VENDA ESTOQUE ****");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Cod    Descrição               Qtde      Estoque");

                    for (int i = 0; i < dadosTabelaVendaEstoque.Rows.Count; i++)
                    {
                        codBarra = dadosTabelaVendaEstoque.Rows[i]["Cod"].ToString();
                        desc = dadosTabelaVendaEstoque.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabelaVendaEstoque.Rows[i]["qtde"].ToString();
                        total = dadosTabelaVendaEstoque.Rows[i]["Estoque"].ToString();

                        codBarra = codBarra.Substring(8, 5);
                        codBarra.PadLeft(0);

                        desc += espaco7;

                        if (desc.Length < 10)
                        {
                            desc.PadLeft(0);
                        }

                        if (desc.Length > 2)
                        {
                            desc = desc.Substring(0, 22);
                            desc.PadLeft(0);
                        }

                        if (total.Length >= 8)
                        {
                            total = total.Substring(0, 7);
                        }

                        if (qtde.Length < 7)
                        {
                            qtde += espaco5;
                        }

                        if (qtde.Length >= 8)
                        {
                            qtde = qtde.Substring(0, 7);
                        }

                        total.PadLeft(0);

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codBarra + espaco3 + desc + espaco3 + qtde + espaco5 + total);
                    }

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");

                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void GerarRelatorioDepartamentoSat()
        {
            //METODO PARA GERAR RELATORIO POR DEPARTAMENTO..............................................................
            try
            {
                string dep;
                string numDep;
                string total;
                string estabelecimento, endereco, numero, cep, bairro = "";
                string usuario = frmVenda.NomeUsuario;
                string espaco7 = "     ";
                string espaco10 = "            ";
                string espaco5 = "   ";
                string espaco3 = "  ";
                string espaco2 = " ";
                decimal sTotal = 0;

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.FecharVendaDepartamentos(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco7 + "**** VENDA POR DEPARTAMENTO ****");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("N Dep   Departamento        Total");

                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        dep = dadosTabela.Rows[i]["DEPARTAMENTO"].ToString();
                        numDep = dadosTabela.Rows[i]["DEP"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();
                        sTotal += Convert.ToDecimal(dadosTabela.Rows[i]["Total"].ToString());

                        dep += espaco7;

                        if (numDep.Length < 2)
                        {
                            numDep += espaco2;
                        }

                        if (numDep.Length < 5)
                        {
                            numDep += espaco7;
                        }

                        if (dep.Length < 15)
                        {
                            dep += espaco10;
                        }

                        if (dep.Length > 15)
                        {
                            dep = dep.Substring(0, 15);
                            dep.PadLeft(0);
                        }

                        if (total.Length >= 8)
                        {
                            total = total.Substring(0, 7);
                        }

                        total.PadRight(0);



                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(numDep + espaco3 + dep + espaco5 + total);
                    }

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("VENDA TOTAL:" + sTotal.ToString("C2"));


                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                    EstiloCoresLinha();
                }
                else
                {
                    MessageBox.Show("Não Contém Venda(s) para Geração de Relatório.", "Informação Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método GerarRelatorioDepartamentoSat.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void GerarRelatorioFormaPgtoSat()
        {
            try
            {
                string tipoPgto;
                decimal venda, sTotal = 0;
                string estabelecimento, endereco, numero, cep, bairro = "";
                string usuario = frmVenda.NomeUsuario;
                string espaco7 = "             ";
                string espaco5 = "   ";
                string espaco3 = "  ";

                if (gdvFormaPgto.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco5 + "**** FORMA DE PAGAMENTO ****");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Tipo        Venda");

                    for (int i = 0; i < gdvFormaPgto.Rows.Count; i++)
                    {
                        tipoPgto = gdvFormaPgto.Rows[i].Cells["colTipo"].Value.ToString();
                        venda = Convert.ToDecimal(gdvFormaPgto.Rows[i].Cells["colValor"].Value.ToString());
                        sTotal += Convert.ToDecimal(gdvFormaPgto.Rows[i].Cells["colValor"].Value.ToString());

                        tipoPgto += espaco7;
                        tipoPgto.PadLeft(0);

                        if (tipoPgto.Length > 10)
                        {
                            tipoPgto = tipoPgto.Substring(0, 10);
                        }

                        venda.ToString();
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(tipoPgto + espaco3 + venda);
                    }

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("VENDA TOTAL:" + sTotal.ToString("C2"));

                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                    EstiloCoresLinha();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método GerarRelatorioFormaPgto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void GerarRelatorioEstoqueDepartamento()
        {
            //GERAR RELATORIO DE ESTOQUE POR DEPARTAMENTO............................................................
            try
            {
                string tipoPgto;
                string estabelecimento, endereco, numero, cep, bairro = "";
                decimal venda, sTotal, somaTotal = 0;
                string codigo, descricao, estoque = "";
                string usuario = frmVenda.NomeUsuario;
                string espaco10 = "                    ";
                string espaco7 = "             ";
                string espaco6 = "           ";
                string espaco5 = "   ";
                string espaco3 = "  ";


                if (gdvRelatorio.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco7 + "**** ESTOQUE ****");

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Codigo Descricao                      Estoque");

                    for (int i = 0; i < gdvRelatorio.Rows.Count; i++)
                    {
                        codigo = gdvRelatorio.Rows[i].Cells["Codigo"].Value.ToString();
                        descricao = gdvRelatorio.Rows[i].Cells["Descricao"].Value.ToString();
                        estoque = gdvRelatorio.Rows[i].Cells["Estoque"].Value.ToString();
                        sTotal = Convert.ToDecimal(gdvRelatorio.Rows[i].Cells["Total"].Value);

                        if (codigo == "")
                        {
                            codigo = "0000000000000";
                        }

                        codigo = codigo.Substring(8, 5);
                        codigo.PadLeft(0);

                        descricao += espaco10;

                        if (descricao.Length < 30)
                        {
                            descricao.PadLeft(0);
                        }

                        if (descricao.Length > 20)
                        {
                            descricao = descricao.Substring(0, 26);
                            descricao.PadLeft(0);
                        }

                        estoque.PadRight(0);


                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codigo + espaco3 + descricao + espaco6 + estoque);
                        somaTotal += sTotal;

                        txtSomaTotal.Text = somaTotal.ToString("C2");
                    }

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("SOMA TOTAL: " + somaTotal.ToString("C2"));
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Departamento Nº:" + numeroDepartamento);

                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                    EstiloCoresLinha();

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void GerarRelatorioVendaTotal()
        {
            try
            {
                //GERA RELATORIO VENDA TOTAL DO DIA.........................................................
                string estabelecimento, endereco, numero, cep, bairro = "";
                string usuario = frmVenda.NomeUsuario;
                string codBarra;
                string desc;
                string total;
                string qtde;
                string espaco7 = "                      ";
                string espaco6 = "          ";
                string espaco5 = "   ";
                string espaco3 = "  ";
                decimal sTotal = 0;

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarVendaTotalTurno(frmVenda.idUsuarioLogado, data, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco6 + "**** VENDA TOTAL ****");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Cod    Descrição               Qtde      Total");

                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        codBarra = dadosTabela.Rows[i]["Cod"].ToString();
                        desc = dadosTabela.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabela.Rows[i]["qtde"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        sTotal += Convert.ToDecimal(dadosTabela.Rows[i]["Total"].ToString());

                        codBarra = codBarra.Substring(8, 5);
                        codBarra.PadLeft(0);

                        desc += espaco7;

                        if (desc.Length < 10)
                        {
                            desc.PadLeft(0);
                        }

                        if (desc.Length > 2)
                        {
                            desc = desc.Substring(0, 22);
                            desc.PadLeft(0);
                        }

                        if (total.Length >= 8)
                        {
                            total = total.Substring(0, 7);
                        }

                        if (qtde.Length < 7)
                        {
                            qtde += espaco5;
                        }

                        if (qtde.Length >= 8)
                        {
                            qtde = qtde.Substring(0, 7);
                        }

                        total.PadLeft(0);

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codBarra + espaco3 + desc + espaco3 + qtde + espaco5 + total);

                    }

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("TOTAL VENDA(S):" + sTotal.ToString("C2"));

                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                    EstiloCoresLinha();
                }
                else
                {
                    LimparCampos();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método GerarRelatorioTotalSat.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void GerarRelatorioEstoqueTotal()
        {
            try
            {
                string tipoPgto;
                string estabelecimento, endereco, numero, cep, bairro = "";
                decimal venda, sTotal, preco, somaTotal = 0;
                string codigo, descricao, estoque = "";
                string usuario = frmVenda.NomeUsuario;
                string espaco10 = "                    ";
                string espaco7 = "             ";
                string espaco5 = "   ";
                string espaco3 = "  ";

                if (gdvRelatorio.Rows.Count > 0)
                {
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco7 + "**** ESTOQUE TOTAL ****");

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Codigo Descricao                  Estoque");

                    for (int i = 0; i < gdvRelatorio.Rows.Count; i++)
                    {
                        codigo = gdvRelatorio.Rows[i].Cells["Codigo"].Value.ToString();
                        descricao = gdvRelatorio.Rows[i].Cells["Descricao"].Value.ToString();
                        estoque = gdvRelatorio.Rows[i].Cells["Estoque"].Value.ToString();
                        sTotal = Convert.ToDecimal(gdvRelatorio.Rows[i].Cells["Total"].Value);
                        preco = Convert.ToDecimal(gdvRelatorio.Rows[i].Cells["Preco"].Value);

                        if (codigo == "")
                        {
                            codigo = "0000000000000";
                        }

                        codigo = codigo.Substring(8, 5);
                        codigo.PadLeft(0);

                        descricao += espaco10;

                        if (descricao.Length < 30)
                        {
                            descricao.PadLeft(0);
                        }

                        if (descricao.Length > 20)
                        {
                            descricao = descricao.Substring(0, 26);
                            descricao.PadLeft(0);
                        }

                        estoque.PadRight(0);


                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codigo + espaco3 + descricao + espaco5 + estoque);
                        somaTotal += sTotal;
                        txtSomaTotal.Text = somaTotal.ToString("C2");
                    }

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("SOMA TOTAL: " + somaTotal.ToString("C2"));
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Departamento Nº " + numeroDepartamento);

                    novoParametro = new RegraNegocio.ParametroRegraNegocio();
                    DataTable dadosParametro = new DataTable();
                    dadosParametro = novoParametro.PesquisaParametroE();

                    estabelecimento = dadosParametro.Rows[0]["NOME_FANTASIA"].ToString();
                    endereco = dadosParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                    numero = dadosParametro.Rows[0]["NUMERO"].ToString();
                    cep = dadosParametro.Rows[0]["CEP"].ToString();
                    bairro = dadosParametro.Rows[0]["BAIRRO"].ToString();

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(estabelecimento + " - Operador(a):" + usuario);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(endereco + "" + numero + "\n" + bairro + " " + cep);

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                    EstiloCoresLinha();

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

        public void LimparCampos()
        {
            txtSomaTotal.Text = "0,00";
            cbDepartamento.Text = "";
        }

        private void btnVendaCancelada_Click(object sender, EventArgs e)
        {
            try
            {
                PesquisarVendaCancelado();

                btnImprimirVendaCanc.Enabled = true;

                if (btnVendaCancelada.Enabled == true)
                {
                    btnImprimirSetor.Enabled = false;
                    btmImprimirRelatorio.Enabled = false;
                    btnImprimirEstoqueDep.Enabled = false;
                    btnImprimirDep.Enabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btmSetor_Click(object sender, EventArgs e)
        {
            PesquisaVendaSetor();
        }

        public void PesquisaVendaSetor()
        {
            try
            {
                decimal somaT = 0;
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVendaSetor(frmVenda.idUsuarioLogado, data, frmVenda.numcaixa);
                gdvRelatorio.DataSource = dadosTabela;

                if (dadosTabela.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        somaT += Convert.ToDecimal(dadosTabela.Rows[i]["Total"].ToString());
                    }

                    if (somaT < 0)
                    {
                        txtSomaTotal.ForeColor = Color.Red;
                    }
                    else
                    {
                        txtSomaTotal.ForeColor = Color.Blue;
                    }

                    txtSomaTotal.Text = somaT.ToString("C2");
                    EstiloCoresLinha();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnSetor_Click(object sender, EventArgs e)
        {
            PesquisaVendaSetor();

            btnImprimirSetor.Enabled = true;

            if (btnImprimirSetor.Enabled == true)
            {
                btnImprimirDep.Enabled = false;
                btnImprimirVendaCanc.Enabled = false;
                btmImprimirRelatorio.Enabled = false;
                btnImprimirEstoqueDep.Enabled = false;
            }
        }

        public void PesquisarEstoqueNumeroDep()
        {
            try
            {
                int numDep = Convert.ToInt32(txtNumeroDep.Text);
                novoDepartamento = new RegraNegocio.DepartamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();

                dadosTabela = novoDepartamento.PesquisarDepartamento(numDep);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarEstoqueCombo()
        {
            try
            {
                string numDepartamento = cbDepartamento.SelectedValue.ToString();
                decimal somaTotal = 0;

                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadostabela = new DataTable();
                dadostabela = novoProduto.PesquisarProdutoDepartamento(numDepartamento);

                if (dadostabela.Rows.Count > 0)
                {
                    gdvRelatorio.DataSource = dadostabela;

                    EstiloCoresLinha();
                    for (int i = 0; i < dadostabela.Rows.Count; i++)
                    {
                        somaTotal += Convert.ToDecimal(dadostabela.Rows[i]["PRECO"].ToString());
                    }

                    txtSomaTotal.Text = somaTotal.ToString("C2");
                    txtNumeroDep.Text = numDepartamento.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisaSomaTotalEstoqueDep()
        {
            try
            {
                string numDepartamento = cbDepartamento.SelectedValue.ToString();
                numeroDepartamento = cbDepartamento.Text;

                decimal somatotalDep = 0;

                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.SomaTotalEstoqueDep(numDepartamento);

                somatotalDep = Convert.ToDecimal(dadosTabela.Rows[0]["TOTAL"].ToString());

                txtSomaTotal.Text = somatotalDep.ToString("C2");

                if (somatotalDep < 0)
                {
                    txtSomaTotal.ForeColor = Color.Red;
                }
                else
                {
                    txtSomaTotal.ForeColor = Color.Blue;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro na Operação.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnImprimirSetor_Click(object sender, EventArgs e)
        {
            try
            {
                LimparCampo();
                PesquisarDadosCliente();

                if (gdvRelatorio.Rows.Count > 0)
                {
                    if ((nomeImpressora == "BEMATECH") || (nomeImpressora == "SAT"))
                    {
                        GerarRealtorioSetor1();

                        MessageBox.Show("Relatório por Setor(es) foi Realizado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                        cbDepartamento.Focus();
                        CarregaCombo();
                        btnImprimirSetor.Enabled = false;
                    }
                    else if (nomeImpressora == "BEMASAT")
                    {
                        GerarRelaroioSetorBemasat();
                        GerarRelaroioSetorBemasat1();
                    }
                    else if (nomeImpressora == "ELGIN")
                    {
                        GerarRelarioSetorElgin();
                    }
                }
                else
                {
                    MessageBox.Show("Selecionar o Botão Setor para Concluir a Impressão Desejado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnSetor.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void PesquisarDadosCliente()
        {
            string data = DateTime.Now.ToShortDateString();

            novoParametro = new RegraNegocio.ParametroRegraNegocio();
            DataTable dadosTabelaParametro = new DataTable();
            dadosTabelaParametro = novoParametro.PesquisaParametroE();

            if (dadosTabelaParametro.Rows.Count > 0)
            {
                nomeCliente = dadosTabelaParametro.Rows[0]["NOME_FANTASIA"].ToString();
                enderecoCliente = dadosTabelaParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                numeroCliente = dadosTabelaParametro.Rows[0]["NUMERO"].ToString();
                bairroCliente = dadosTabelaParametro.Rows[0]["BAIRRO"].ToString();
                cidadeCliente = dadosTabelaParametro.Rows[0]["CIDADE"].ToString();
                ufCliente = dadosTabelaParametro.Rows[0]["UF"].ToString();
                cepCliente = dadosTabelaParametro.Rows[0]["CEP"].ToString();
                cnpjCliente = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                ieCliente = dadosTabelaParametro.Rows[0]["IE"].ToString();
            }
        }

        #region RELATORIOS BEMASAT

        public void GerarRelaroioSetorBemasat()
        {
            try
            {
                decimal totalvenda = 0;
                LimparCampo();

                AX = (nomeCliente + "\n" +
                 enderecoCliente + "," + numeroCliente + "\n" +
                 bairroCliente + " " + cidadeCliente + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                 "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + "\n" +
                 "-------------------------------------------------\n" +
                 "DATA: " + data + "\n" +
                 "-------------------------------------------------\n" +
                 "        ***** RELATORIO VENDA SETOR *****        \n" +
                 "-------------------------------------------------\n" +
                 "SETOR: 001" + "\n" +
                 "-------------------------------------------------\n" +
                 "COD    DESCRICAO                QTDE       TOTAL \n" +
                 "-------------------------------------------------");

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();

                dadosTabela = novaVenda.GerarRealtorioSetor1(frmVenda.idUsuarioLogado, 1, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        codigoBarras = dadosTabela.Rows[i]["Cod"].ToString();
                        descrica = dadosTabela.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabela.Rows[i]["qtde"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        totalvenda += Convert.ToDecimal(total);

                        //limpar variveis
                        codigoBarras = codigoBarras.Trim();

                        codigoBarras = codigoBarras.Substring(8, 5);

                        if (descrica.Length < 25)
                        {
                            descrica = descrica + "                               ";
                        }

                        if (descrica.Length > 25)
                        {
                            descrica = descrica.Substring(0, 25);
                        }

                        qtde = qtde.PadRight(8, ' ');
                        total = total.PadLeft(9, ' ');

                        BX += (codigoBarras + " " + descrica + " " + qtde + " " + total + "\n");
                    }

                    CX = ("TOTAL:                                 " + totalvenda.ToString("n2").PadLeft(11, ' ') + "\n" +
                          "--------------------------------------------------\n" +
                          "USUARIO:" + frmVenda.operadorAtuante +
                          " - CAIXA:" + frmVenda.numcaixa.ToString().PadLeft(3, '0') + "\n\n");

                    if (totalvenda > 0)
                    {
                        // PesquisarPortaImpressora();
                        frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                        idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                        idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);

                        string align = "" + (char)27 + (char)97 + (char)0;
                        RegraNegocio.MP2032.ComandoTX(align, align.Length);

                        idRetorno = RegraNegocio.MP2032.BematechTX(AX + "\n" + BX + "\n" + CX);

                        RegraNegocio.MP2032.AcionaGuilhotina(0);
                        RegraNegocio.MP2032.FechaPorta();
                    }
                    else
                    {
                        MessageBox.Show("Não Contém Venda(s) no Setor 001", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    EstiloCoresLinha();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarRelaroioSetorBemasat1()
        {
            try
            {
                decimal totalvenda = 0;
                LimparCampo();

                AX = (nomeCliente + "\n" +
                 enderecoCliente + "," + numeroCliente + "\n" +
                 bairroCliente + " " + cidadeCliente + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                 "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + "\n" +
                 "-------------------------------------------------\n" +
                 "DATA: " + data + "\n" +
                 "-------------------------------------------------\n" +
                 "        ***** RELATORIO VENDA SETOR *****        \n" +
                 "-------------------------------------------------\n" +
                 "SETOR: 002" + "\n" +
                 "-------------------------------------------------\n" +
                 "COD    DESCRICAO                QTDE       TOTAL \n" +
                 "-------------------------------------------------");

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();

                dadosTabela = novaVenda.GerarRealtorioSetor1(frmVenda.idUsuarioLogado, 2, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        codigoBarras = dadosTabela.Rows[i]["Cod"].ToString();
                        descrica = dadosTabela.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabela.Rows[i]["qtde"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        totalvenda += Convert.ToDecimal(total);

                        //limpar variveis
                        codigoBarras = codigoBarras.Trim();

                        codigoBarras = codigoBarras.Substring(8, 5);

                        if (descrica.Length < 25)
                        {
                            descrica = descrica + "                               ";
                        }

                        if (descrica.Length > 25)
                        {
                            descrica = descrica.Substring(0, 25);
                        }

                        qtde = qtde.PadRight(8, ' ');
                        total = total.PadLeft(9, ' ');

                        BX += (codigoBarras + " " + descrica + " " + qtde + " " + total + "\n");
                    }

                    CX = ("TOTAL:                                 " + totalvenda.ToString("n2").PadLeft(11, ' ') + "\n" +
                          "--------------------------------------------------\n" +
                          "USUARIO:" + frmVenda.operadorAtuante +
                          " - CAIXA:" + frmVenda.numcaixa.ToString().PadLeft(3, '0') + "\n\n");

                    if (totalvenda > 0)
                    {
                        // PesquisarPortaImpressora();
                        frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                        idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                        idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);

                        string align = "" + (char)27 + (char)97 + (char)0;
                        RegraNegocio.MP2032.ComandoTX(align, align.Length);

                        idRetorno = RegraNegocio.MP2032.BematechTX(AX + "\n" + BX + "\n" + CX);

                        RegraNegocio.MP2032.AcionaGuilhotina(0);
                        RegraNegocio.MP2032.FechaPorta();
                    }
                    else
                    {
                        MessageBox.Show("Não Contém Venda(s) no Setor 002", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    EstiloCoresLinha();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarRelatorioDepartamentoBemasat()
        {
            try
            {
                decimal totalvenda = 0;
                LimparCampo();

                AX = (nomeCliente + "\n" +
                enderecoCliente + "," + numeroCliente + "\n" +
                bairroCliente + " " + cidadeCliente + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + "\n" +
                "-------------------------------------------------\n" +
                "DATA: " + data + "\n" +
                "-------------------------------------------------\n" +
                "      ***** RELATORIO VENDA DEPARTAMENTO *****   \n" +
                "-------------------------------------------------\n" +
                "DEPARTAMENTO        N DEPARTAMENTO         TOTAL \n" +
                "-------------------------------------------------");

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.FecharVendaDepartamentos(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        dep = dadosTabela.Rows[i]["DEPARTAMENTO"].ToString();
                        numDep = dadosTabela.Rows[i]["DEP"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        totalvenda += Convert.ToDecimal(total);

                        if (dep.Length < 15)
                        {
                            dep = dep + "                       ";
                        }

                        if (dep.Length > 15)
                        {
                            dep = dep.Substring(0, 15);
                        }

                        numDep = numDep.PadLeft(5, ' ');

                        BX += (dep + "  " + numDep + "      " + total.PadLeft(21, ' ') + "\n");
                    }

                    CX = ("TOTAL:                                 " + totalvenda.ToString("n2").PadLeft(11, ' ') + "\n" +
                          "--------------------------------------------------\n" +
                          "USUARIO:" + frmVenda.operadorAtuante +
                          " - CAIXA:" + frmVenda.numcaixa.ToString().PadLeft(3, '0') + "\n\n");

                    if (totalvenda > 0)
                    {
                        // PesquisarPortaImpressora();
                        frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                        idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                        idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);

                        string align = "" + (char)27 + (char)97 + (char)0;
                        RegraNegocio.MP2032.ComandoTX(align, align.Length);

                        idRetorno = RegraNegocio.MP2032.BematechTX(AX + "\n" + BX + "\n" + CX);

                        RegraNegocio.MP2032.AcionaGuilhotina(0);
                        RegraNegocio.MP2032.FechaPorta();
                    }
                    else
                    {
                        MessageBox.Show("Não Contém Venda(s) no(s) Departamento(s)", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    EstiloCoresLinha();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarRelatorioVendaCanceladaBemasat()
        {
            try
            {
                LimparCampo();
                decimal totalvenda = 0;

                AX = (nomeCliente + "\n" +
                enderecoCliente + "," + numeroCliente + "\n" +
                bairroCliente + " " + cidadeCliente + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + "\n" +
                "-------------------------------------------------\n" +
                "DATA: " + data + "\n" +
                "-------------------------------------------------\n" +
                "   ***** RELATORIO VENDA(S) CANCELADA(S) *****   \n" +
                "-------------------------------------------------\n" +
                "COD  DESCRICAO                QTDE         TOTAL \n" +
                "-------------------------------------------------");

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarVendaCancelado(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        codigoBarras = dadosTabela.Rows[i]["Cod"].ToString();
                        descrica = dadosTabela.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabela.Rows[i]["qtde"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        totalvenda += Convert.ToDecimal(total);

                        //limpar variveis
                        codigoBarras = codigoBarras.Trim();

                        codigoBarras = codigoBarras.Substring(8, 5);

                        if (descrica.Length < 25)
                        {
                            descrica = descrica + "                               ";
                        }

                        if (descrica.Length > 25)
                        {
                            descrica = descrica.Substring(0, 25);
                        }

                        qtde = qtde.PadRight(7, ' ');
                        total = total.PadLeft(7, ' ');

                        BX += (codigoBarras + " " + descrica + " " + " " + qtde + " " + total + "\n");
                    }

                    CX = ("TOTAL:                                 " + totalvenda.ToString("n2").PadLeft(11, ' ') + "\n" +
                       "--------------------------------------------------\n" +
                       "USUARIO:" + frmVenda.operadorAtuante +
                       " - CAIXA:" + frmVenda.numcaixa.ToString().PadLeft(3, '0') + "\n\n");

                    // PesquisarPortaImpressora();
                    frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                    idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                    idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);

                    string align = "" + (char)27 + (char)97 + (char)0;
                    RegraNegocio.MP2032.ComandoTX(align, align.Length);

                    idRetorno = RegraNegocio.MP2032.BematechTX(AX + "\n" + BX + "\n" + CX);

                    RegraNegocio.MP2032.AcionaGuilhotina(0);
                    RegraNegocio.MP2032.FechaPorta();


                    EstiloCoresLinha();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarRelatorioVendasBemasat()
        {
            try
            {
                decimal totalvenda = 0;
                LimparCampo();

                AX = (nomeCliente + "\n" +
                enderecoCliente + "," + numeroCliente + "\n" +
                bairroCliente + " " + cidadeCliente + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + "\n" +
                "-------------------------------------------------\n" +
                "DATA: " + data + "\n" +
                "-------------------------------------------------\n" +
                "          ***** RELATORIO VENDA(S) *****         \n" +
                "-------------------------------------------------\n" +
                "COD  DESCRICAO                QTDE         TOTAL \n" +
                "-------------------------------------------------");

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.GerarRelatorioVendaTotal(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        codigoBarras = dadosTabela.Rows[i]["Cod"].ToString();
                        descrica = dadosTabela.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabela.Rows[i]["qtde"].ToString();
                        total = dadosTabela.Rows[i]["Total"].ToString();

                        totalvenda += Convert.ToDecimal(total);

                        //limpar variveis
                        codigoBarras = codigoBarras.Trim();

                        codigoBarras = codigoBarras.Substring(8, 5);

                        if (descrica.Length < 25)
                        {
                            descrica = descrica + "                               ";
                        }

                        if (descrica.Length > 25)
                        {
                            descrica = descrica.Substring(0, 25);
                        }

                        qtde = qtde.PadRight(7, ' ');
                        total = total.PadLeft(7, ' ');

                        BX += (codigoBarras + " " + descrica + " " + " " + qtde + " " + total + "\n");
                    }

                    CX = ("TOTAL:                                 " + totalvenda.ToString("n2").PadLeft(11, ' ') + "\n" +
                         "--------------------------------------------------\n" +
                         "USUARIO:" + frmVenda.operadorAtuante +
                         " -CAIXA:" + frmVenda.numcaixa.ToString().PadLeft(3, '0') + "\n\n");

                    if (totalvenda > 0)
                    {
                        // PesquisarPortaImpressora();
                        frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                        idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                        idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);

                        string align = "" + (char)27 + (char)97 + (char)0;
                        RegraNegocio.MP2032.ComandoTX(align, align.Length);

                        idRetorno = RegraNegocio.MP2032.BematechTX(AX + "\n" + BX + "\n" + CX);

                        RegraNegocio.MP2032.AcionaGuilhotina(0);
                        RegraNegocio.MP2032.FechaPorta();
                    }
                    else
                    {
                        MessageBox.Show("Não Contém Venda(s) Registada", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    EstiloCoresLinha();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarEstoqueProdutosEstoqueBemasat()
        {
            try
            {
                decimal totalvenda = 0;

                AX = (nomeCliente + "\n" +
                enderecoCliente + "," + numeroCliente + "\n" +
                bairroCliente + " " + cidadeCliente + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + "\n" +
                "-------------------------------------------------\n" +
                "DATA: " + data + "\n" +
                "-------------------------------------------------\n" +
                "           ***** RELATORIO ESTOQUE *****         \n" +
                "-------------------------------------------------\n" +
                "COD   DESCRICAO                          ESTOQUE \n" +
                "-------------------------------------------------\n");

                for (int i = 0; i < gdvRelatorio.Rows.Count; i++)
                {
                    codigoBarras = gdvRelatorio.Rows[i].Cells["Codigo"].Value.ToString();
                    descrica = gdvRelatorio.Rows[i].Cells["Descricao"].Value.ToString();
                    estoque = gdvRelatorio.Rows[i].Cells["Estoque"].Value.ToString();
                    total = gdvRelatorio.Rows[i].Cells["Total"].Value.ToString();

                    totalvenda += Convert.ToDecimal(total);

                    //limpar variveis
                    codigoBarras = codigoBarras.Trim();

                    codigoBarras = codigoBarras.Substring(8, 5);

                    if (descrica.Length < 25)
                    {
                        descrica = descrica + "                               ";
                    }

                    if (descrica.Length > 25)
                    {
                        descrica = descrica.Substring(0, 25);
                    }

                    estoque = estoque.PadLeft(13, ' ');

                    txtSomaTotal.Text = somaTotal.ToString("C2");

                    BX += (codigoBarras + "    " + descrica + "  " + estoque + "\n");
                }

                CX = ("TOTAL:                                 " + totalvenda.ToString("n2").PadLeft(11, ' ') + "\n" +
                        "--------------------------------------------------\n" +
                        "USUARIO:" + frmVenda.operadorAtuante +
                        " - CAIXA:" + frmVenda.numcaixa.ToString().PadLeft(3, '0') + "\n\n");


                // PesquisarPortaImpressora();
                frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);

                string align = "" + (char)27 + (char)97 + (char)0;
                RegraNegocio.MP2032.ComandoTX(align, align.Length);

                idRetorno = RegraNegocio.MP2032.BematechTX(AX + "\n" + BX + "\n" + CX + "\n");

                RegraNegocio.MP2032.AcionaGuilhotina(0);
                RegraNegocio.MP2032.FechaPorta();

                EstiloCoresLinha();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region RELATORIO ELGIN

        public void GerarRelarioSetorElgin()
        {
            try
            {
                var configImpressora = new PrinterSettings();
                printerName = configImpressora.PrinterName;

                PesquisarDadosCliente();
                frmVenda.AbreCupomSegViaBemasat();
                string cabecalhoElgin = frmVenda.A;

                esc = new RegraNegocio.EscPos();
                this.esc.normalModeText(printerName);
                this.esc.printText(printerName, cabecalhoElgin + "\n");


                decimal sTotal = 0;
                string vendaSetor = "";
                string total_ = "";

                novoSetor = new RegraNegocio.SetorRegraNegocios();
                DataTable dadosTabelaSetor = new DataTable();

                dadosTabelaSetor = novoSetor.PesquisarSetorNumero();

                if (dadosTabelaSetor.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaSetor.Rows.Count; i++)
                    {
                        int setor = Convert.ToInt32(dadosTabelaSetor.Rows[i]["ID"].ToString());
                        string descricaoSetor = dadosTabelaSetor.Rows[i]["DESCRICAO"].ToString();
                        descricaoSetor = descricaoSetor.Trim();

                        if (setor > 0)
                        {
                            //busca tabela venda
                            novaVenda = new RegraNegocio.VendaRegraNegocios();
                            DataTable dadosTabelaVenda = new DataTable();
                            dadosTabelaVenda = novaVenda.GerarRealtorioSetor1(frmVenda.idUsuarioLogado, setor, frmVenda.numcaixa);

                            if (dadosTabelaVenda.Rows.Count > 0)
                            {
                                string cabecalhoSetor = ("        **** VENDA POR SETOR ****   \n" +
                                                   descricaoSetor + ":" + setor + "      \n");

                                for (int y = 0; y < dadosTabelaVenda.Rows.Count; y++)
                                {
                                    string codBarra = dadosTabelaVenda.Rows[y]["Cod"].ToString();
                                    string desc = dadosTabelaVenda.Rows[y]["Descricao"].ToString();
                                    qtde = dadosTabelaVenda.Rows[y]["qtde"].ToString();
                                    total = dadosTabelaVenda.Rows[y]["Total"].ToString();

                                    sTotal += Convert.ToDecimal(total);

                                    codBarra = codBarra.Trim();
                                    codBarra = codBarra.Substring(8, 5);

                                    if (desc.Length < 20)
                                    {
                                        desc = desc + "                   ";
                                    }

                                    if (desc.Length > 20)
                                    {
                                        desc = desc.Substring(0, 20);
                                    }

                                    qtde = qtde.PadLeft(3, ' ');
                                    total = total.PadLeft(3, ' ');

                                    vendaSetor += (codBarra + " " + desc + "   " + qtde + "     " + total + "\n");
                                    total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(38, '.') + "\n");
                                }

                                esc = new RegraNegocio.EscPos();
                                this.esc.normalModeText(printerName);
                                this.esc.printText(printerName, cabecalhoSetor + "\n" + vendaSetor + "\n" + total_);

                                //LIMPA VARIAVEIS
                                vendaSetor = "";
                                sTotal = 0;
                                total_ = "";
                                descricaoSetor = "";

                            }

                        }
                    }
                }

                string dadosFinaisCupomElgin = "";

                dadosFinaisCupomElgin = ("------------------------------------------------\n" +
                                         "CAIXA: " + frmVenda.numcaixa.ToString().Trim().PadLeft(3, '0') + "\n" +
                                         "OPERADOR: " + frmVenda.operadorAtuante + "\n" +
                                         "DATA: " + DateTime.Now.Date.ToShortDateString().ToString());

                esc = new RegraNegocio.EscPos();
                this.esc.normalModeText(printerName);
                this.esc.printText(printerName, dadosFinaisCupomElgin + "\n");
                feedAndCutter(printerName, 5);

                dadosFinaisCupomElgin = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarRealtorioDepartamentoElgin()
        {
            try
            {
                var configImpressora = new PrinterSettings();
                printerName = configImpressora.PrinterName;

                PesquisarDadosCliente();
                frmVenda.AbreCupomSegViaBemasat();
                string cabecalhoElgin = frmVenda.A;

                esc = new RegraNegocio.EscPos();
                this.esc.normalModeText(printerName);
                this.esc.printText(printerName, cabecalhoElgin + "\n");


                //DADOS VENDA DEPARTAMENTO
                decimal sTotal = 0;
                string total_ = "";
                string cabecalhoDepartamento = "";
                string vendaDeparatmento = "";

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVendaDep = new DataTable();
                dadosTabelaVendaDep = novaVenda.FecharVendaDepartamentos(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabelaVendaDep.Rows.Count > 0)
                {
                    cabecalhoDepartamento = ("      **** VENDA POR DEPARTAMENTO ****   \n" +
                                             "N Dep    Departamento                 Total \n");

                    for (int d = 0; d < dadosTabelaVendaDep.Rows.Count; d++)
                    {
                        dep = dadosTabelaVendaDep.Rows[d]["DEPARTAMENTO"].ToString();
                        numDep = dadosTabelaVendaDep.Rows[d]["DEP"].ToString();
                        total = dadosTabelaVendaDep.Rows[d]["Total"].ToString();
                        sTotal += Convert.ToDecimal(dadosTabelaVendaDep.Rows[d]["Total"].ToString());

                        numDep = numDep.PadLeft(5, '0');

                        if (dep.Length < 20)
                        {
                            dep += "                ";
                        }

                        if (dep.Length > 20)
                        {
                            dep = dep.Substring(0, 20);
                        }

                        total = total.PadLeft(5, ' ');

                        vendaDeparatmento += (numDep + "    " + dep + "         " + total + "\n");
                        total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));
                    }

                    esc = new RegraNegocio.EscPos();
                    this.esc.normalModeText(printerName);
                    this.esc.printText(printerName, cabecalhoDepartamento + "\n" + vendaDeparatmento + "\n" + total_ + "\n");

                    //LIMPA VARIAVEIS
                    vendaDeparatmento = "";
                    sTotal = 0;
                    total_ = "";
                    idRetorno = 0;
                }

                string dadosFinaisCupomElgin = "";

                dadosFinaisCupomElgin = ("------------------------------------------------\n" +
                                         "CAIXA: " + frmVenda.numcaixa.ToString().Trim().PadLeft(3, '0') + "\n" +
                                         "OPERADOR: " + frmVenda.operadorAtuante + "\n" +
                                         "DATA: " + DateTime.Now.Date.ToShortDateString().ToString());

                esc = new RegraNegocio.EscPos();
                this.esc.normalModeText(printerName);
                this.esc.printText(printerName, dadosFinaisCupomElgin + "\n");
                feedAndCutter(printerName, 5);

                dadosFinaisCupomElgin = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarVendaCanceladasElgin()
        {
            try
            {
                var configImpressora = new PrinterSettings();
                printerName = configImpressora.PrinterName;

                PesquisarDadosCliente();
                frmVenda.AbreCupomSegViaBemasat();
                string cabecalhoElgin = frmVenda.A;

                esc = new RegraNegocio.EscPos();
                this.esc.normalModeText(printerName);
                this.esc.printText(printerName, cabecalhoElgin + "\n");

                string cabecalhoVendaCancelada = "";
                int qtdeVCancelada = 0;
                decimal sTotal = 0;
                string total_ = "";

                //DADOS TABELA VENDAS CANCELADA
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVendaCanc = new DataTable();
                dadosTabelaVendaCanc = novaVenda.PesquisarVendaCancelado(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabelaVendaCanc.Rows.Count > 0)
                {
                    cabecalhoVendaCancelada = ("      **** VENDA(S) CANCELADA(S) ****   \n");

                    for (int i = 0; i < dadosTabelaVendaCanc.Rows.Count; i++)
                    {
                        qtdeVCancelada += 1;
                        sTotal += Convert.ToDecimal(dadosTabelaVendaCanc.Rows[i]["TOTAL"].ToString());
                    }

                    total_ = ("QTDE VENDA: " + qtdeVCancelada.ToString().PadLeft(2, '0') + "\n" +
                              "TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));

                    esc = new RegraNegocio.EscPos();
                    this.esc.normalModeText(printerName);
                    this.esc.printText(printerName, cabecalhoVendaCancelada + "\n" + total_ + "\n");

                    //LIMPA VARIAVEIS
                    sTotal = 0;
                    total_ = "";
                    idRetorno = 0;
                }

                string dadosFinaisCupomElgin = "";

                dadosFinaisCupomElgin = ("------------------------------------------------\n" +
                                         "CAIXA: " + frmVenda.numcaixa.ToString().Trim().PadLeft(3, '0') + "\n" +
                                         "OPERADOR: " + frmVenda.operadorAtuante + "\n" +
                                         "DATA: " + DateTime.Now.Date.ToShortDateString().ToString());

                esc = new RegraNegocio.EscPos();
                this.esc.normalModeText(printerName);
                this.esc.printText(printerName, dadosFinaisCupomElgin + "\n");
                feedAndCutter(printerName, 5);

                dadosFinaisCupomElgin = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarVendaTotalElgin()
        {
            try
            {
                var configImpressora = new PrinterSettings();
                printerName = configImpressora.PrinterName;

                PesquisarDadosCliente();
                frmVenda.AbreCupomSegViaBemasat();
                string cabecalhoElgin = frmVenda.A;

                esc = new RegraNegocio.EscPos();
                this.esc.normalModeText(printerName);
                this.esc.printText(printerName, cabecalhoElgin + "\n");

                string cabecalhoVendaTotal = "";
                decimal sTotal = 0;
                string vendaTotaldia = "";
                string total_ = "";

                //DADOS TABELA VENDA TOTAL

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVendaTotal = new DataTable();

                dadosTabelaVendaTotal = novaVenda.GerarRelatorioVendaTotal(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabelaVendaTotal.Rows.Count > 0)
                {
                    cabecalhoVendaTotal = ("             **** VENDA TOTAL ***          \n" +
                                            "Cod   Descrição              Qtde     Total\n");

                    for (int i = 0; i < dadosTabelaVendaTotal.Rows.Count; i++)
                    {
                        string codBarra = dadosTabelaVendaTotal.Rows[i]["Cod"].ToString();
                        string desc = dadosTabelaVendaTotal.Rows[i]["Descricao"].ToString();
                        qtde = dadosTabelaVendaTotal.Rows[i]["qtde"].ToString();
                        total = dadosTabelaVendaTotal.Rows[i]["Total"].ToString();

                        sTotal += Convert.ToDecimal(total);

                        codBarra = codBarra.Trim();
                        codBarra = codBarra.Substring(8, 5);

                        if (desc.Length < 20)
                        {
                            desc = desc + "               ";
                        }

                        if (desc.Length > 20)
                        {
                            desc = desc.Substring(0, 20);
                        }

                        qtde = qtde.PadLeft(3, ' ');
                        total = total.PadLeft(3, ' ');

                        vendaTotaldia += (codBarra + " " + desc + "   " + qtde + "     " + total + "\n");
                        total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));
                    }

                    esc = new RegraNegocio.EscPos();
                    this.esc.normalModeText(printerName);
                    this.esc.printText(printerName, cabecalhoVendaTotal + "\n" + vendaTotaldia + "\n" + total_ + "\n");

                    //LIMPA VARIAVEIS
                    vendaTotaldia = "";
                    sTotal = 0;
                    total_ = "";
                    idRetorno = 0;
                }

                string dadosFinaisCupomElgin = "";

                dadosFinaisCupomElgin = ("------------------------------------------------\n" +
                                         "CAIXA: " + frmVenda.numcaixa.ToString().Trim().PadLeft(3, '0') + "\n" +
                                         "OPERADOR: " + frmVenda.operadorAtuante + "\n" +
                                         "DATA: " + DateTime.Now.Date.ToShortDateString().ToString());

                esc = new RegraNegocio.EscPos();
                this.esc.normalModeText(printerName);
                this.esc.printText(printerName, dadosFinaisCupomElgin + "\n");
                feedAndCutter(printerName, 5);

                dadosFinaisCupomElgin = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarRelatorioEstoqueElgin()
        {
            try
            {
                var configImpressora = new PrinterSettings();
                printerName = configImpressora.PrinterName;

                PesquisarDadosCliente();
                frmVenda.AbreCupomSegViaBemasat();
                string cabecalhoElgin = frmVenda.A;

                esc = new RegraNegocio.EscPos();
                this.esc.normalModeText(printerName);
                this.esc.printText(printerName, cabecalhoElgin + "\n");

                decimal sTotal = 0;
                string vendaEstoque = "";
                string cabecalhoEstoque = "";
                string total_ = "";

                if (gdvRelatorio.Rows.Count > 0)
                {
                    for (int y = 0; y < gdvRelatorio.Rows.Count; y++)
                    {
                        qtde = "";
                        string codBarra = gdvRelatorio.Rows[y].Cells[0].Value.ToString();
                        string desc = gdvRelatorio.Rows[y].Cells[1].Value.ToString();
                        decimal qtdeEstoque = Convert.ToDecimal(gdvRelatorio.Rows[y].Cells[2].Value.ToString());
                        qtde = gdvRelatorio.Rows[y].Cells[3].Value.ToString();
                        //   sTotal += Convert.ToDecimal(dadosTabelaVendaEstoque.Rows[y].["TOTAL"].ToString());

                        codBarra = codBarra.Trim();
                        codBarra = codBarra.Substring(8, 5);

                        if (desc.Length < 20)
                        {
                            desc = desc + "               ";
                        }

                        if (desc.Length > 20)
                        {
                            desc = desc.Substring(0, 20);
                        }

                        string qe = qtdeEstoque.ToString().Trim().PadLeft(3, ' ');
                        total = sTotal.ToString().PadLeft(11, ' ');

                        vendaEstoque += (codBarra + " " + desc + "  " + qtde + "   " + qe + "\n");
                        //    total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));
                    }

                    esc = new RegraNegocio.EscPos();
                    this.esc.normalModeText(printerName);
                    this.esc.printText(printerName, cabecalhoEstoque + "\n" + vendaEstoque + "\n");

                    //LIMPA VARIAVEIS
                    vendaEstoque = "";
                    cabecalhoEstoque = "";
                    sTotal = 0;
                    total_ = "";
                    idRetorno = 0;


                    string dadosFinaisCupomElgin = "";

                    dadosFinaisCupomElgin = ("------------------------------------------------\n" +
                                             "CAIXA: " + frmVenda.numcaixa.ToString().Trim().PadLeft(3, '0') + "\n" +
                                             "OPERADOR: " + frmVenda.operadorAtuante + "\n" +
                                             "DATA: " + DateTime.Now.Date.ToShortDateString().ToString());

                    esc = new RegraNegocio.EscPos();
                    this.esc.normalModeText(printerName);
                    this.esc.printText(printerName, dadosFinaisCupomElgin + "\n");
                    feedAndCutter(printerName, 5);

                    dadosFinaisCupomElgin = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        public void LimparCampo()
        {
            AX = "";
            BX = "";
            CX = "";
            DX = "";
            EX = "";
        }

        private void btnImprimirDep_Click(object sender, EventArgs e)
        {
            try
            {
                LimparCampo();

                PesquisarDadosCliente();

                if (gdvRelatorio.Rows.Count > 0)
                {
                    if ((nomeImpressora == "BEMATECH") || (nomeImpressora == "SAT"))
                    {
                        GerarRelatorioDepartamentoSat();

                        MessageBox.Show("Relatório por Departamento(s) foi Realizado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                        cbDepartamento.Focus();
                        CarregaCombo();
                        btnImprimirDep.Enabled = false;
                    }
                    else if (nomeImpressora == "BEMASAT")
                    {
                        GerarRelatorioDepartamentoBemasat();
                    }

                    else if (nomeImpressora == "ELGIN")
                    {
                        GerarRealtorioDepartamentoElgin();
                    }
                }
                else
                {
                    MessageBox.Show("Selecionar o Botão Departamento para Concluir a Impressão Desejado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnDepartamento.Focus();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void btnImprimirVendaCanc_Click(object sender, EventArgs e)
        {
            try
            {
                LimparCampo();

                PesquisarDadosCliente();

                if (gdvRelatorio.Rows.Count > 0)
                {
                    if ((nomeImpressora == "BEMATECH") || (nomeImpressora == "SAT"))
                    {
                        GerarRelatorioVendaCanceladaSat();

                        MessageBox.Show("Relatório Venda(s) Cancelda(s) foi Realizado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                        cbDepartamento.Focus();
                        CarregaCombo();
                        btnImprimirVendaCanc.Enabled = false;
                    }
                    else if (nomeImpressora == "BEMASAT")
                    {
                        GerarRelatorioVendaCanceladaBemasat();
                    }
                    else if (nomeImpressora == "ELGIN")
                    {
                        GerarVendaCanceladasElgin();
                    }
                }
                else
                {
                    MessageBox.Show("Selecionar o Botão Venda(s) Cancelada(s) para Concluir a Impressão Desejado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnVendaCancelada.Focus();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnEstoque_Click(object sender, EventArgs e)
        {
            PesquisarEstoqueCombo();

            PesquisaSomaTotalEstoqueDep();

            btnImprimirEstoqueDep.Enabled = true;

            if (btnImprimirEstoqueDep.Enabled == true)
            {
                btnImprimirSetor.Enabled = false;
                btnImprimirDep.Enabled = false;
                btmImprimirRelatorio.Enabled = false;
                btnImprimirVendaCanc.Enabled = false;
            }
        }

        private void CarregaCombo()
        {
            try
            {
                novoDepartamento = new RegraNegocio.DepartamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoDepartamento.PopularCb();


                for (int i = 0; i < dadosTabela.Rows.Count; i++)
                {
                    cbDepartamento.DataSource = dadosTabela;
                    cbDepartamento.ValueMember = "ID";
                    cbDepartamento.DisplayMember = "DESCRICAO";
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        private void btnImprimirEstoqueDep_Click(object sender, EventArgs e)
        {
            try
            {
                LimparCampo();

                PesquisarDadosCliente();

                if (gdvRelatorio.Rows.Count > 0)
                {
                    if ((nomeImpressora == "BEMATECH") || nomeImpressora == "SAT")
                    {
                        GerarRelatorioEstoqueDepartamento();

                        MessageBox.Show("Relatório de Estoque por Departamento foi Realizado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                        cbDepartamento.Focus();
                        btnImprimirEstoqueDep.Enabled = false;
                    }
                    else if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "MP4200"))
                    {
                        GerarEstoqueProdutosEstoqueBemasat();
                    }

                    else if (nomeImpressora == "ELGIN")
                    {
                        GerarRelatorioEstoqueElgin();
                    }
                }
                else
                {
                    MessageBox.Show("Selecione Numerro do Departamento Desejado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Não Foi Possível Completar a Operação.\n Verifique dado(s) do(s) Produto(s) ou entre em Contato com Administrador.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_CancelaCupom();
            }
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            AliquotaXBemasat();
            //  this.Close();
        }

        private void btnPesquisarEstoque_Click(object sender, EventArgs e)
        {
            PesquisarVendaTotalTurno();

            btmImprimirRelatorio.Enabled = true;

            if (btmImprimirRelatorio.Enabled == true)
            {
                btnImprimirSetor.Enabled = false;
                btnImprimirEstoqueDep.Enabled = false;
                btnImprimirDep.Enabled = false;
                btnImprimirVendaCanc.Enabled = false;
            }
        }

        private void PesquisarEstoqueTotal()
        {
            novoProduto = new RegraNegocio.ProdutoRegraNegocio();
            DataTable dadostabela = new DataTable();
            dadostabela = novoProduto.PesquisarEstoqueTotal();

            if (dadostabela.Rows.Count > 0)
            {
                gdvRelatorio.DataSource = dadostabela;
                EstiloCoresLinha();
            }

            if (Convert.ToDecimal(txtSomaTotal.Text) < 0)
            {
                txtSomaTotal.ForeColor = Color.Red;
            }
            else
            {
                txtSomaTotal.ForeColor = Color.Blue;
            }
        }

        private void btmImprimirRelatorio_Click(object sender, EventArgs e)
        {
            try
            {
                LimparCampo();

                PesquisarDadosCliente();

                if (gdvRelatorio.Rows.Count > 0)
                {
                    if ((nomeImpressora == "BEMATECH") || (nomeImpressora == "SAT"))
                    {
                        GerarRelatorioVendaTotal();
                        MessageBox.Show("Relatório de Venda Total foi Realizado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                        cbDepartamento.Focus();
                        btmImprimirRelatorio.Enabled = false;
                    }
                    else if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "MP4200"))
                    {
                        GerarRelatorioVendasBemasat();
                    }
                    else if (nomeImpressora == "ELGIN")
                    {
                        GerarVendaTotalElgin();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void gdvRelatorio_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnFecharCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSetor_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnDepartamento_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnVendaCancelada_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnPesquisarEstoque_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void cbDepartamento_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Enter)
            {
                PesquisarEstoqueCombo();
                PesquisaSomaTotalEstoqueDep();
                btnImprimirEstoqueDep.Enabled = true;

                if (btnImprimirEstoqueDep.Enabled == true)
                {
                    btnImprimirSetor.Enabled = false;
                    btnImprimirDep.Enabled = false;
                    btmImprimirRelatorio.Enabled = false;
                    btnImprimirVendaCanc.Enabled = false;
                }
            }
        }

        private void btnEstoque_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gdvFormaPgto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSair_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void FecharVenda()
        {
            //if (MessageBox.Show("Realmente Deseja Fechar o Caixa do Periodo da " + frmVenda.periodoAtuante_.ToString() + ".", "Fechamento do Caixa", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            //{
            //if (MessageBox.Show("Confirma Fechamento Venda", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            //{
            //    metodo pesquisa item vendidos e alterano estoque, vizualiza qtde de item ao usuario......


            //else
            //{
            //metodo altera estatus do baixado e fechado do pagamento venda............................

            AlteraVendaPagamentoFechado();

            //metodo altera status de baixado e fech da vendqa........................................
            AlterarBaixadoVenda();
            FechaLogin();
            FecharCaixaAtuante();
            FecharSangria();
            Application.Exit();
            //}
            //}
            //}
        }

        public void PesquisaItemVendido()
        {
            try
            {
                //contador de total de produtos vendidos....................................................................

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                dadosTabelaVenda = novaVenda.PesquisaVendaNaoFechado(vendaNaoFechado, frmVenda.numcaixa);

                int contador = 0;

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        codInternoprod = Convert.ToInt32(dadosTabelaVenda.Rows[i]["ID_PROD"].ToString());
                        qtdeVendida = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["QUANT"].ToString());
                        idvenda = Convert.ToInt32(dadosTabelaVenda.Rows[i]["ID"].ToString());

                        PesquisarEstoqueAtual();
                        contador = contador + 1;

                        //BAIXAR E FECHAR VENDA COM ID VENDA
                        novaVenda = new RegraNegocio.VendaRegraNegocios();
                        novaVenda.FecharVendaIdVenda(idvenda);

                        qtdeVendida = 0;
                        codInternoprod = 0;
                    }

                    //  PesquisarQtde_QtdeEstoqueGerarRelatorio();

                    lblQtdeDadosImportado.Text = contador.ToString();
                }
                else
                {
                    MessageBox.Show("Não há Dado(s) para ser(em) Baixado(s)", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void PesquisarEstoqueAtual()
        {
            try
            {
                //Metodo para pesquisar qtde Atual do produto vendido.....................................
                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabelaProduto = new DataTable();
                dadosTabelaProduto = novoProduto.PesquisaEstoqueAtual(codInternoprod);

                novaMateriaPrima = new RegraNegocio.MateriaPrimaRegraNegocio();
                DataTable dadosTabelaMateriaPrima = new DataTable();

                if (dadosTabelaProduto.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaProduto.Rows.Count; i++)
                    {
                        descricao_produto = dadosTabelaProduto.Rows[i]["DESCRICAO"].ToString();

                        try
                        {
                            estoqueAtual = Convert.ToDecimal(dadosTabelaProduto.Rows[i]["ESTOQUE"].ToString());
                        }
                        catch
                        {
                            estoqueAtual = 0;
                        }

                        novoEstoque = (estoqueAtual - qtdeVendida);

                        //metodo Atualizar estoque e baixar venda.....................................................
                        novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                        novoProduto.AtualizarEstoque(codInternoprod, novoEstoque);

                        estoqueAtual = 0;
                        novoEstoque = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarEstoqueMateriaPrimaBaixaEstoque()
        {
            try
            {
                //metodo para buscar produto composto
                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabelaProduto = new DataTable();
                dadosTabelaProduto = novoProduto.PesquisarProdutoComposto(codInternoprod);

                if (dadosTabelaProduto.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaProduto.Rows.Count; i++)
                    {
                        idComposto = Convert.ToInt32(dadosTabelaProduto.Rows[i]["ID_COMPOSTO"].ToString());
                        estoqueAtual = Convert.ToDecimal(dadosTabelaProduto.Rows[0]["ESTOQUE"].ToString());

                        DadosMateriaPrima();

                        //multiplica qtde vendida com qtde da materia prima utilizada
                        decimal qtdeCompostoVendido = ((qtdeVendida * estoqueAtual));
                        decimal qtdeCompostoVendido_ = ((qtdeVendida * estoqueComposto));

                        //baixa produto com id composto
                        novoEstoque = (qtdeCompostoVendido + qtdeCoposto);
                        //metodo Atualizar estoque e baixar venda.....................................................

                        novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                        novoProduto.AtualizarEstoque(idComposto, novoEstoque);

                        PesquisarMateriaPrimaBaixarQtde();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarMateriaPrimaBaixarQtde()
        {
            try
            {
                novaMateriaPrima = new RegraNegocio.MateriaPrimaRegraNegocio();
                DataTable dadosTabelaMateriaPrima = new DataTable();
                dadosTabelaMateriaPrima = novaMateriaPrima.PesquisarQtdeEstoqueMateriaPrima(idComposto);

                if (dadosTabelaMateriaPrima.Rows.Count > 0)
                {
                    novaMateriaPrima.BaixaEstoqueMateriaPrima(idComposto, novoEstoque);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DadosMateriaPrima()
        {
            novaMateriaPrima = new RegraNegocio.MateriaPrimaRegraNegocio();
            DataTable dadosMateriaPrima = new DataTable();

            dadosMateriaPrima = novaMateriaPrima.PesquisarQtdeEstoqueMateriaPrima(idComposto);

            if (dadosMateriaPrima.Rows.Count > 0)
            {
                qtdeCoposto = Convert.ToDecimal(dadosMateriaPrima.Rows[0]["QTDE"].ToString());
            }
        }

        public void FechamentoVendaBaixaEstoque()
        {
            impressora = frmVenda.nomeImpressora;

            impressora = impressora.Replace(" ", "");

            if ((impressora == "SAT") || (impressora == "BEMATECH"))
            {
                ImprimirFechamento();

                PesquisaItemVendido();
                FecharVenda();

            }
            else if ((impressora == "COMUM") || (impressora == "BEMASAT") || (impressora == "DARUMA") || (impressora == "ELGIN") || (impressora == "MP4200"))
            {
                if (MessageBox.Show("Realmente Deseja Realizar Fechamento do Caixa Nº 00" + frmVenda.numcaixa.ToString(), "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    if (MessageBox.Show("Por Favor, Confirmar o Fechamento do Caixa Nº 00" + frmVenda.numcaixa.ToString(), "Confirmação Fechamento", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        if ((impressora == "BEMASAT") || (impressora == "COMUM") || (impressora == "ELGIN") || (impressora == "MP4200"))
                        {
                            if (idRetorno > 0)
                            {
                                idRetorno = RegraNegocio.MP2032.FechaPorta();
                            }

                            novoParametro = new RegraNegocio.ParametroRegraNegocio();
                            DataTable dadosTabela = new DataTable();
                            dadosTabela = novoParametro.PesquisaParametroE();

                            if (dadosTabela.Rows.Count > 0)
                            {
                                timer1.Start();
                                qtdeCupom = Convert.ToInt32(dadosTabela.Rows[0]["QTDE_CUPOM"].ToString());

                                if (qtdeCupom > 0)
                                {
                                    // contador para duas vias de cada relatorio...............................................
                                    for (int i = 0; i < qtdeCupom; i++)
                                    {
                                        ImprimirRelatoriosFechamentoCaixa();
                                    }
                                }
                            }

                            PesquisaItemVendido();
                            FecharVenda();

                        }

                        else if ((impressora == "DARUMA"))
                        {
                            frmVenda.fecharCaixa = true;
                            frmVenda.AbreCupomLeituraXBemasat();
                            frmVenda.VendaXBemasat();
                            frmVenda.AliquotaXBemasat();
                            frmVenda.VendaCanceladaBemasat();
                            frmVenda.ObservacoesContruite();

                            string dadosElgin = (frmVenda.AX + "\n" + frmVenda.BX + "\n" + frmVenda.CX + "\n" + frmVenda.DX + "\n" + frmVenda.EX);
                            int idRetonro = RegraNegocio.DLLsDaruma.iImprimirTexto_DUAL_DarumaFramework(dadosElgin + "<c> Protocolo de Autorização.</c><sl>4</sl><gui></gui><l></l>", 0);

                            PesquisaItemVendido();
                            //metodo altera estatus do baixado e fechado do pagamento venda............................
                            AlteraVendaPagamentoFechado();
                            //metodo altera status de baixado e fech da vendqa........................................
                            // AlterarBaixadoVenda();
                            FechaLogin();
                            FecharCaixaAtuante();
                            FecharSangria();
                            frmVenda.Close();
                        }

                        else if (impressora == "ELGIN")
                        {
                            frmVenda.fecharCaixa = true;
                            frmVenda.AbreCupomLeituraXBemasat();
                            frmVenda.VendaXBemasat();
                            frmVenda.AliquotaXBemasat();
                            frmVenda.VendaCanceladaBemasat();
                            frmVenda.ObservacoesContruite();

                            string dadosElgin = (frmVenda.AX + "\n" + frmVenda.BX + "\n" + frmVenda.CX + "\n" + frmVenda.DX + "\n" + frmVenda.EX);
                            escPos = new RegraNegocio.EscPos();

                            this.escPos.printText(frmVenda.printerName, dadosElgin);
                            //this.esc.printBarcodeB(printerName, "{A012345678901234567", 73);
                            this.escPos.lineFeed(frmVenda.printerName, 2);

                            feedAndCutter(frmVenda.printerName, 5);

                            PesquisaItemVendido();
                            //metodo altera estatus do baixado e fechado do pagamento venda............................
                            AlteraVendaPagamentoFechado();
                            //metodo altera status de baixado e fech da vendqa........................................

                            ImprimirRelatoriosFechamentoCaixa();
                            //   AlterarBaixadoVenda();
                            FechaLogin();
                            FecharCaixaAtuante();
                            FecharSangria();
                            frmVenda.Close();
                        }


                        MessageBox.Show("Fechamento do Caixa Nº: " + frmVenda.numcaixa + " foi Realizado com Sucesso.", "Informação");
                        this.Close();
                    }
                }
            }
        }

        public void ImprimirFechamento()
        {
            novoParametro = new RegraNegocio.ParametroRegraNegocio();
            DataTable dadosTabela = new DataTable();
            dadosTabela = novoParametro.PesquisaParametroE();

            if (dadosTabela.Rows.Count > 0)
            {
                timer1.Start();
                qtdeCupom = Convert.ToInt32(dadosTabela.Rows[0]["QTDE_CUPOM"].ToString());

                if (qtdeCupom > 0)
                {
                    // contador para duas vias de cada relatorio...............................................
                    for (int i = 0; i < qtdeCupom; i++)
                    {
                        //GerarRelatorioDepartamentoSat();
                        //GerarRelatorioVendaTotal();
                        //GerarRelatorioFormaPgtoSat();
                        // GerarRelatorioVendaCanceladaSat();
                        ImprimirRelatoriosFechamentoCaixa();
                        //PesquisarQtde_QtdeEstoqueGerarRelatorio();
                    }
                }
            }

            if (MessageBox.Show("Caixa Fechado com Sucesso!!!.\n Deseja Reimprimir mais Cupons?", "Informação do Fechamento de Caixa", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
            {
                frmQtdeR frmQtde = new frmQtdeR(this, frmVenda);
                frmQtde.ShowDialog();
            }
        }

        public void FecharSangria()
        {
            try
            {
                novaSangria = new RegraNegocio.SangriaRegraNegocios();
                novaSangria.FecharSangria(frmVenda.idUsuarioLogado, frmVenda.numcaixa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FecharRelatorioBemasat()
        {

            try
            {
                frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);
                frmVenda.LerTextoCupom();
                RegraNegocio.MP2032.BematechTX("\n\n\n\n");
                RegraNegocio.MP2032.AcionaGuilhotina(0);

                RegraNegocio.MP2032.FechaPorta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void feedAndCutter(string printerName, int numLines)
        {
            escPos = new RegraNegocio.EscPos();
            System.Threading.Thread.Sleep(500);
            this.escPos.lineFeed(printerName, numLines);
            this.escPos.CutPaper(printerName);
        }

        public void FecharCaixaAtuante()
        {
            try
            {
                bool statusCaixa = false;
                novoCaixa = new RegraNegocio.NumCaixaRegraNegocios();
                novoCaixa.FecharStatusCaixa(statusCaixa, frmVenda.numcaixa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GerarAssinaturaDigitalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoginAssinaturaDigital frmLoginAssDigital = new LoginAssinaturaDigital();
            frmLoginAssDigital.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pb.Value < 100)
            {
                this.pb.Increment(2);
            }
            else
            {
                pb.Value = 0;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            GerarRelatorioBemasatSetor1();
        }

        public void MontarReducaoZ()
        {
            nomeImpressora = frmVenda.nomeImpressora;
            nomeImpressora = nomeImpressora.Replace(" ", "");

            if (nomeImpressora == "COMUM")
            {
                //AbreCupomLeituraX();
                //VendaX();
                //AliquotaX();
            }
            else if (nomeImpressora == "BEMASAT")
            {
                //AbreCupomLeituraXBemasat();
                //VendaXBemasat();
                //AliquotaXBemasat();
                //VendaCanceladaBemasat();
                //ObservacoesContruite();

                ////IMPRIMIR REDUCAO Z............................................................................

                //RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                //RegraNegocio.MP2032.IniciaPorta(portaImpressora);

                //RegraNegocio.MP2032.BematechTX(AX + "\n" + BX + "\n" + CX + "\n" + DX + "\n" + EX).ToString();

                //RegraNegocio.MP2032.BematechTX("\n\n\n\n");
                //RegraNegocio.MP2032.FechaPorta();

                //RegraNegocio.MP2032.AcionaGuilhotina(0);

            }
            else if (nomeImpressora == "BEMATECH")
            {
                RegraNegocio.CupomFiscal.Bematech_FI_ReducaoZ("", "");
            }
        }

        public void PesquisarPortaImpressora()
        {
            try
            {
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabelaParamentro = new DataTable();

                dadosTabelaParamentro = novoParametro.PesquisaParametroE();

                if (dadosTabelaParamentro.Rows.Count > 0)
                {
                    portaImpressora = dadosTabelaParamentro.Rows[0]["PORTA_COM_IMPRESSORA"].ToString();
                    portaImpressora = portaImpressora.Replace(" ", "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AbreCupomLeituraXBemasat()
        {
            try
            {
                string espaco1 = " ";
                string espaco2 = "  ";
                string pularlinha1 = "\n";
                string pontoFinal = "\r";
                string data = DateTime.Now.ToShortDateString();
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabelaParametro = new DataTable();
                dadosTabelaParametro = novoParametro.PesquisaParametroE();

                string nomeCliente = dadosTabelaParametro.Rows[0]["NOME_FANTASIA"].ToString();
                string enderecoCliente = dadosTabelaParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                string numeroCliente = dadosTabelaParametro.Rows[0]["NUMERO"].ToString();
                string bairroCliente = dadosTabelaParametro.Rows[0]["BAIRRO"].ToString();
                string cidadeCliente = dadosTabelaParametro.Rows[0]["CIDADE"].ToString();
                string ufCliente = dadosTabelaParametro.Rows[0]["UF"].ToString();
                string cepCliente = dadosTabelaParametro.Rows[0]["CEP"].ToString();
                string cnpjCliente = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                string ieCliente = dadosTabelaParametro.Rows[0]["IE"].ToString();

                AX = ("------------------------------------------\n" +
                     nomeCliente + "\n" +
                     enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                     bairroCliente + "\n" +
                     cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                     "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal + "\n" +
                     "----------------------------------------" +
                     "                REDUÇAO Z               \n" +
                     "----------------------------------------\n" +
                     "DATA: " + data + "                      \n" +
                     "----------------------------------------\n" +
                     "            ***** VENDAS *****          \n" +
                     "----------------------------------------\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void VendaXBemasat()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                string data = DateTime.Now.ToShortDateString();
                decimal total_x = 0;
                bool baixado = false;
                bool fechado = false;

                dadosTabelaVenda = novaVenda.VendaX(idUsuario, frmVenda.numcaixa, data);

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        valorvendaX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                        idTipoPagamentoX = Convert.ToInt32(dadosTabelaVenda.Rows[i]["TIPO_PAGAMENTO"].ToString());
                        total_x += Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());

                        if (idTipoPagamentoX == 1)
                        {
                            descricaoTipoPgto = "DINHEIRO......................" + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 2)
                        {
                            descricaoTipoPgto = "CARTAO........................" + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 3)
                        {
                            descricaoTipoPgto = "CHEQUE........................" + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 4)
                        {
                            descricaoTipoPgto = "ABERTO........................" + valorvendaX.ToString("C2");
                        }
                        else
                        {
                            descricaoTipoPgto = "OUTROS........................" + valorvendaX.ToString("C2");
                        }

                        AX += descricaoTipoPgto.ToString() + "\n\r";
                    }

                    BX = "\nTOTAL: ......................." + total_x.ToString("c2");
                }
                else
                {
                    MessageBox.Show("Não Contem Dados na Tabela Venda para Leitura X.", "Atenção");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AliquotaXBemasat()
        {
            try
            {
                novaTrib = new RegraNegocio.TribRegraNegocio();
                DataTable dadosTabelaTrib = new DataTable();

                string textoAliquota = "";
                string trib = "";
                string DT = DateTime.Now.Date.ToShortDateString();

                dadosTabelaTrib = novaTrib.PesquisarTribAll();

                if (dadosTabelaTrib.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaTrib.Rows.Count; i++)
                    {
                        novoPagamentoVenda = new RegraNegocio.PagamentoVendaRegraNegocios();
                        DataTable dadosTabelaVenda = new DataTable();

                        trib = dadosTabelaTrib.Rows[i]["DESCRICAO"].ToString().Trim();

                        dadosTabelaVenda = novoPagamentoVenda.LeituraX(frmVenda.numcaixa, idUsuario, DT, trib);

                        if (dadosTabelaVenda.Rows.Count > 0)
                        {
                            aliquotaX = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                            valorX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["VENDA"].ToString());

                            CX = ("-----------------------------------------\n" +
                                  "           ***** ALIQUOTAS *****         \n" +
                                  "-----------------------------------------\n" +
                                  "\n" +
                                   textoAliquota +
                                   "\r");
                        }
                        else
                        {
                            CX = ("-----------------------------------------\n" +
                                  "           ***** ALIQUOTAS *****         \n" +
                                  "-----------------------------------------\n" +
                                  "\n" +
                                   textoAliquota +
                                  "\r");
                        }

                        //Limpar variaveis............................................................
                        aliquotaX = aliquotaX.Replace(" ", "");

                        if (aliquotaX.Length < 2)
                        {
                            aliquotaX = aliquotaX.PadLeft(2, ' ');
                        }

                        if ((valorX == 0) || (valorX == null))
                        {
                            valorX = 0;
                            aliquotaX = trib;
                        }

                        textoAliquota += aliquotaX + "............................" + valorX.ToString("C2").ToString() + "\n";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VendaCanceladaBemasat()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelavenda = new DataTable();
                string data = DateTime.Now.Date.ToShortDateString();

                dadosTabelavenda = novaVenda.PesquisarVendaCancelada(idUsuario, frmVenda.numcaixa);

                if (dadosTabelavenda.Rows.Count > 0)
                {
                    try
                    {
                        qtdeVendaCancelX = Convert.ToInt32(dadosTabelavenda.Rows[0]["QTDE"].ToString());
                        valorVendaCanceladaX = Convert.ToDecimal(dadosTabelavenda.Rows[0]["TOTAL"].ToString());
                    }
                    catch
                    {
                        if (valorVendaCanceladaX == null)
                        {
                            valorVendaCanceladaX = 0;
                        }
                    }
                }
                else
                {
                    qtdevenda = 0;
                    valorVendaCanceladaX = 0;
                }

                DX = ("----------------------------------------\n" +
                      "  ****** VENDA(S) CANCELDA(S) ******    \n" +
                      "----------------------------------------\n" +
                      "QTDE VENDA CANCELADA:" + qtdeVendaCancelX.ToString() +
                      "\n" +
                      "TOTAL VENDA CANCELADA:" + valorVendaCanceladaX.ToString("C2") +
                      "\n" +
                      "------------------------------------------" +
                      "\n"
                      );
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ObservacoesContruite()
        {
            try
            {
                nomeImpressora = nomeImpressora.Replace(" ", "");
                DateTime data = DateTime.Now;

                if (nomeImpressora == "BEMASAT")
                {
                    #region BEMASAT
                    EX = ("DATA:" + data + "\n" +
                          "OPERADOR:" + frmVenda.NomeUsuario.ToString() +
                          "\n"
                            );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ImprimirRelatoriosFechamentoCaixa()
        {
            try
            {
                PesquisarDadosCliente();

                string iniciarCupom = "";
                string cabecalhoSetor = "";
                string cabecalhoDepartamento = "";
                string cabecalhoVendaCancelada = "";
                string cabecalhoSangria = "";
                string cabecalhoVendaTotal = "";
                string cabecalhoEstoque = "";
                string cabecalhoformaPagamento = "";
                string cabecalhoDirencacaixa = "";
                string dadosDiferencaCaia = "";
                string vendaFormaPagamento = "";
                string vendaEstoque = "";
                string vendaSetor = "";
                string vendaDeparatmento = "";
                string vendaTotaldia = "";
                string total_ = "";
                string usuario = frmVenda.NomeUsuario;
                string codBarra;
                int setor, qtdeVCancelada = 0;
                string desc;
                string total;
                string qtde;
                decimal sTotal = 0;
                string tipoPagamento = "";
                decimal qtdeEstoque = 0;

                if (nomeImpressora == "BEMASAT")
                {
                    iniciarCupom = ("----------------------------------------------\n" +
                                 nomeCliente + "\n" +
                                 enderecoCliente + ", " + numeroCliente + "\n" + cidadeCliente + "-" + ufCliente + "\n" +
                                 bairroCliente + " - " + cepCliente + "\n" +
                                 "CNPJ: " + cnpjCliente + "\n" +
                                 "IE:" + ieCliente + "\n" +
                                 "-------------------------------------------------");
                    #region SETOR

                    frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                    idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                    idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);


                    idRetorno = RegraNegocio.MP2032.BematechTX(iniciarCupom + "\n");

                    string align = "" + (char)27 + (char)97 + (char)0;
                    idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);


                    novoSetor = new RegraNegocio.SetorRegraNegocios();
                    DataTable dadosTabelaSetor = new DataTable();

                    dadosTabelaSetor = novoSetor.PesquisarSetorNumero();

                    if (dadosTabelaSetor.Rows.Count > 0)
                    {
                        for (int i = 0; i < dadosTabelaSetor.Rows.Count; i++)
                        {
                            setor = Convert.ToInt32(dadosTabelaSetor.Rows[i]["ID"].ToString());
                            string descricaoSetor = dadosTabelaSetor.Rows[i]["DESCRICAO"].ToString();

                            if (setor > 0)
                            {
                                //busca tabela venda
                                novaVenda = new RegraNegocio.VendaRegraNegocios();
                                DataTable dadosTabelaVenda = new DataTable();
                                dadosTabelaVenda = novaVenda.GerarRealtorioSetor1(frmVenda.idUsuarioLogado, setor, frmVenda.numcaixa);

                                if (dadosTabelaVenda.Rows.Count > 0)
                                {
                                    cabecalhoSetor = ("      **** VENDA POR SETOR ****     \n" +
                                                      descricaoSetor + ":" + setor + "      \n");

                                    for (int y = 0; y < dadosTabelaVenda.Rows.Count; y++)
                                    {
                                        codBarra = dadosTabelaVenda.Rows[y]["Cod"].ToString();
                                        desc = dadosTabelaVenda.Rows[y]["Descricao"].ToString();
                                        qtde = dadosTabelaVenda.Rows[y]["qtde"].ToString();
                                        total = dadosTabelaVenda.Rows[y]["Total"].ToString();

                                        sTotal += Convert.ToDecimal(total);

                                        codBarra = codBarra.Trim();
                                        codBarra = codBarra.Substring(8, 5);

                                        if (desc.Length < 20)
                                        {
                                            desc = desc + "                   ";
                                        }

                                        if (desc.Length > 20)
                                        {
                                            desc = desc.Substring(0, 20);
                                        }

                                        qtde = qtde.PadLeft(3, ' ');
                                        total = total.PadLeft(3, ' ');

                                        vendaSetor += (codBarra + " " + desc + "   " + qtde + "     " + total + "\n");
                                        total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));
                                    }

                                    idRetorno = RegraNegocio.MP2032.BematechTX(cabecalhoSetor + "\n" + total_ + "\n\n");

                                    //LIMPA VARIAVEIS
                                    vendaSetor = "";
                                    sTotal = 0;
                                    total_ = "";
                                    idRetorno = 0;
                                    descricaoSetor = "";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Setor não pode ser Zero", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    #endregion

                    //DADOS VENDA DEPARTAMENTO
                    #region DEPARTAMENTO

                    sTotal = 0;
                    total_ = "";

                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaDep = new DataTable();
                    dadosTabelaVendaDep = novaVenda.FecharVendaDepartamentos(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaDep.Rows.Count > 0)
                    {
                        cabecalhoDepartamento = ("      **** VENDA POR DEPARTAMENTO ****   \n" +
                                                 "N Dep    Departamento                 Total \n");

                        for (int d = 0; d < dadosTabelaVendaDep.Rows.Count; d++)
                        {
                            dep = dadosTabelaVendaDep.Rows[d]["DEPARTAMENTO"].ToString();
                            numDep = dadosTabelaVendaDep.Rows[d]["DEP"].ToString();
                            total = dadosTabelaVendaDep.Rows[d]["Total"].ToString();
                            sTotal += Convert.ToDecimal(dadosTabelaVendaDep.Rows[d]["Total"].ToString());

                            numDep = numDep.PadLeft(5, '0');

                            if (dep.Length < 20)
                            {
                                dep += "                ";
                            }

                            if (dep.Length > 20)
                            {
                                dep = dep.Substring(0, 20);
                            }

                            total = total.PadLeft(5, ' ');

                            vendaDeparatmento += (numDep + "    " + dep + "         " + total + "\n");
                            total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));
                        }

                        idRetorno = RegraNegocio.MP2032.BematechTX(cabecalhoDepartamento + "\n" + vendaDeparatmento + "\n" + total_ + "\n\n");

                        //LIMPA VARIAVEIS
                        vendaDeparatmento = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }

                    #endregion

                    //DADOS TABELA VENDAS CANCELADA
                    #region VENDAS CANCELADA

                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaCanc = new DataTable();
                    dadosTabelaVendaCanc = novaVenda.PesquisarVendaCancelado(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaCanc.Rows.Count > 0)
                    {
                        cabecalhoVendaCancelada = ("      **** VENDA(S) CANCELADA(S) ****   \n");

                        for (int i = 0; i < dadosTabelaVendaCanc.Rows.Count; i++)
                        {
                            qtdeVCancelada += 1;
                            sTotal += Convert.ToDecimal(dadosTabelaVendaCanc.Rows[i]["TOTAL"].ToString());
                        }

                        total_ = ("QTDE VENDA: " + qtdeVCancelada.ToString().PadLeft(2, '0') + "\n" +
                                  "TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));

                        idRetorno = RegraNegocio.MP2032.BematechTX(cabecalhoVendaCancelada + "\n" + total_ + "\n\n");

                        //LIMPA VARIAVEIS
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }
                    #endregion

                    //DADOS TABELA SANGRIA
                    #region SANGRIA

                    string dataDia = DateTime.Now.Date.ToShortDateString();
                    int count = 0;
                    novaSangria = new RegraNegocio.SangriaRegraNegocios();
                    DataTable dadosTabelaSangria = new DataTable();

                    dadosTabelaSangria = novaSangria.PesquisarSangriaCaixa(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaSangria.Rows.Count > 0)
                    {
                        cabecalhoSangria = ("              **** SANGRIA ****           \n");

                        for (int i = 0; i < dadosTabelaSangria.Rows.Count; i++)
                        {
                            sTotal += Convert.ToDecimal(dadosTabelaSangria.Rows[i]["TOTAL"].ToString());
                            count += 1;
                        }

                        total_ = ("TOTAL SANGRIA:  " + sTotal.ToString("C2").PadLeft(25, '.'));

                        idRetorno = RegraNegocio.MP2032.BematechTX(cabecalhoSangria + "\n" + total_ + "\n\n");

                        //LIMPA VARIAVEIS
                        cabecalhoSangria = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                        count = 0;
                    }
                    #endregion

                    //DADOS FORMA PAGAMENTO
                    #region FORMA PAGAMENTO

                    decimal tt = 0;

                    cabecalhoformaPagamento = ("           **** FORMA DE PAGAMENTO ****     \n" +
                                               "Tipo                 Venda(s)               \n");

                    novaFormaPgto = new RegraNegocio.FormaPagamentoRegraNegocio();
                    DataTable dadosTabelaFPagamento = new DataTable();
                    dadosTabelaFPagamento = novaFormaPgto.PesquisarFPgto(data, frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaFPagamento.Rows.Count > 0)
                    {
                        for (int i = 0; i < dadosTabelaFPagamento.Rows.Count; i++)
                        {
                            tipoPagamento = dadosTabelaFPagamento.Rows[i]["TIPO"].ToString();
                            sTotal = Convert.ToDecimal(dadosTabelaFPagamento.Rows[i]["VENDA"].ToString());

                            tt += Convert.ToDecimal(dadosTabelaFPagamento.Rows[i]["VENDA"].ToString());

                            if (tipoPagamento.Length >= 6)
                            {
                                tipoPagamento = tipoPagamento + "       ";
                            }

                            tipoPagamento = tipoPagamento.Substring(0, 9);

                            total_ = sTotal.ToString();
                            vendaFormaPagamento += (tipoPagamento + "       " + total_.ToString().PadLeft(3, ' ') + "\n");
                            total_ = "";
                            sTotal = 0;
                        }

                        total_ = ("TOTAL: " + tt.ToString().PadLeft(36, '.'));

                        idRetorno = RegraNegocio.MP2032.BematechTX(cabecalhoformaPagamento + "\n" + vendaFormaPagamento + "\n\n" + total_);

                        //LIMPA VARIAVEIS
                        vendaFormaPagamento = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;

                        //DADOS DIFERNÇA CAIXA

                        cabecalhoDirencacaixa = ("          *** FECHAMENTO CAIXA ***          \n");
                        dadosDiferencaCaia = ("MOEDA:     " + moeda + "\n" + "DINHEIRO:  " + dinheiro + "\n" + "CARTÃO:    " + cartao + "\n" + "CHEQUE:    " + cheque + "\n" + "CONVENIO:  " + convenio + "\n" + "SANGRIA:   " + sangria + "\n" + "DESPESA:   " + despesa + "\n\n" +
                                              "TOTAL: " + txtTotalRecebimento.Text.PadLeft(35, '.') + "\n\n" + "DIFERENCA CAIXA: " + lblDiferencaCaixa.Text.PadLeft(25, '.'));

                        idRetorno = RegraNegocio.MP2032.BematechTX(cabecalhoDirencacaixa + "\n" + dadosDiferencaCaia + "\n\n");
                        //LIMPAR VARIAVEIS
                        cabecalhoDirencacaixa = "";
                        dadosDiferencaCaia = "";
                    }
                    #endregion

                    //DADOS TABELA VENDA TOTAL
                    #region VENDA TOTAL

                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaTotal = new DataTable();

                    dadosTabelaVendaTotal = novaVenda.GerarRelatorioVendaTotal(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaTotal.Rows.Count > 0)
                    {
                        cabecalhoVendaTotal = ("             **** VENDA TOTAL ***          \n" +
                                                "Cod   Descrição              Qtde     Total\n");

                        for (int i = 0; i < dadosTabelaVendaTotal.Rows.Count; i++)
                        {
                            codBarra = dadosTabelaVendaTotal.Rows[i]["Cod"].ToString();
                            desc = dadosTabelaVendaTotal.Rows[i]["Descricao"].ToString();
                            qtde = dadosTabelaVendaTotal.Rows[i]["qtde"].ToString();
                            total = dadosTabelaVendaTotal.Rows[i]["Total"].ToString();

                            sTotal += Convert.ToDecimal(total);

                            codBarra = codBarra.Trim();

                            if (codBarra.Length < 13)
                            {
                                codBarra = codBarra + "XXXXXXXXXXXXXX";
                            }

                            codBarra = codBarra.Substring(8, 5);

                            if (desc.Length < 20)
                            {
                                desc = desc + "               ";
                            }

                            if (desc.Length > 20)
                            {
                                desc = desc.Substring(0, 20);
                            }

                            qtde = qtde.PadLeft(3, ' ');
                            total = total.PadLeft(3, ' ');

                            vendaTotaldia += (codBarra + " " + desc + "   " + qtde + "     " + total + "\n");
                            total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));
                        }

                        idRetorno = RegraNegocio.MP2032.BematechTX(cabecalhoVendaTotal + "\n" + vendaTotaldia + "\n" + total_ + "\n\n");

                        //LIMPA VARIAVEIS
                        vendaDeparatmento = "";
                        vendaTotaldia = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }
                    #endregion

                    //DADOS TABELA PRODUTO ESTOQUE
                    #region ESTOQUE PRODUTO

                    cabecalhoEstoque = ("            *** ESTOQUE PRODUTOS ***             \n" +
                                           "Codigo Descricao            Qtde  Estoque        \n");

                    //busca tabela venda


                    string dadosUsuario = "";
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaEstoque = new DataTable();
                    dadosTabelaVendaEstoque = novaVenda.GerarRealtorioEstoque(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaEstoque.Rows.Count > 0)
                    {
                        for (int y = 0; y < dadosTabelaVendaEstoque.Rows.Count; y++)
                        {
                            qtde = "";
                            codBarra = dadosTabelaVendaEstoque.Rows[y]["Cod"].ToString();
                            desc = dadosTabelaVendaEstoque.Rows[y]["Descricao"].ToString();
                            qtdeEstoque = Convert.ToDecimal(dadosTabelaVendaEstoque.Rows[y]["Estoque"].ToString());
                            qtde = dadosTabelaVendaEstoque.Rows[y]["Qtde"].ToString();
                            //   sTotal += Convert.ToDecimal(dadosTabelaVendaEstoque.Rows[y]["TOTAL"].ToString());

                            codBarra = codBarra.Trim();
                            codBarra = codBarra.Substring(8, 5);

                            if (desc.Length < 20)
                            {
                                desc = desc + "                          ";
                            }

                            if (desc.Length > 20)
                            {
                                desc = desc.Substring(0, 20);
                            }

                            string qe = qtdeEstoque.ToString().Trim().PadLeft(3, ' ');
                            total = sTotal.ToString().PadLeft(11, ' ');

                            vendaEstoque += (codBarra + " " + desc + "  " + qtde + "   " + qe + "\n");
                            // total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));


                            dadosUsuario = ("------------------------------------------------\n" +
                                            "CAIXA: " + frmVenda.numcaixa.ToString().Trim().PadLeft(3, '0') + "\n" +
                                            "OPERADOR: " + frmVenda.operadorAtuante + "\n" +
                                            "DATA: " + DateTime.Now.Date.ToShortDateString().ToString());
                        }

                        idRetorno = RegraNegocio.MP2032.BematechTX(cabecalhoEstoque + "\n" + vendaEstoque + "\n" + total_ + "\n" + dadosUsuario + "\n\n");

                        //LIMPA VARIAVEIS
                        vendaEstoque = "";
                        cabecalhoEstoque = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }
                    #endregion

                    //FECHAR CUPOM
                    idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(0);
                    idRetorno = RegraNegocio.MP2032.FechaPorta();

                    #endregion
                }
                else if (nomeImpressora == "BEMATECH")
                {
                    PesquisarDadosCliente();

                    #region sETOR
                    novoSetor = new RegraNegocio.SetorRegraNegocios();
                    DataTable dadosTabelaSetor = new DataTable();

                    dadosTabelaSetor = novoSetor.PesquisarSetorNumero();

                    if (dadosTabelaSetor.Rows.Count > 0)
                    {
                        for (int i = 0; i < dadosTabelaSetor.Rows.Count; i++)
                        {
                            setor = Convert.ToInt32(dadosTabelaSetor.Rows[i]["ID"].ToString());
                            string descricaoSetor = dadosTabelaSetor.Rows[i]["DESCRICAO"].ToString();

                            if (setor > 0)
                            {
                                //busca tabela venda
                                novaVenda = new RegraNegocio.VendaRegraNegocios();
                                DataTable dadosTabelaVenda = new DataTable();
                                dadosTabelaVenda = novaVenda.GerarRealtorioSetor1(frmVenda.idUsuarioLogado, setor, frmVenda.numcaixa);

                                if (dadosTabelaVenda.Rows.Count > 0)
                                {
                                    cabecalhoSetor = ("      **** VENDA POR SETOR ****     \n" +
                                                      descricaoSetor + ":" + setor + "      \n");

                                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(cabecalhoSetor);

                                    for (int y = 0; y < dadosTabelaVenda.Rows.Count; y++)
                                    {
                                        codBarra = dadosTabelaVenda.Rows[y]["Cod"].ToString();
                                        desc = dadosTabelaVenda.Rows[y]["Descricao"].ToString();
                                        qtde = dadosTabelaVenda.Rows[y]["qtde"].ToString();
                                        total = dadosTabelaVenda.Rows[y]["Total"].ToString();

                                        sTotal += Convert.ToDecimal(total);
                                        // idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codBarra + " " + desc + "   " + qtde + "     " + total);
                                    }

                                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("TOTAL: ........................" + sTotal.ToString("C2") + "\n\n");

                                    //LIMPA VARIAVEIS
                                    vendaSetor = "";
                                    sTotal = 0;
                                    total_ = "";
                                    idRetorno = 0;
                                    descricaoSetor = "";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Setor não pode ser Zero", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }


                    #endregion

                    //DADOS TABELA VENDAS CANCELADA
                    #region cANCEL
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaCanc = new DataTable();
                    dadosTabelaVendaCanc = novaVenda.PesquisarVendaCancelado(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaCanc.Rows.Count > 0)
                    {
                        cabecalhoVendaCancelada = ("      **** VENDA(S) CANCELADA(S) ****   \n");
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(cabecalhoVendaCancelada);

                        for (int i = 0; i < dadosTabelaVendaCanc.Rows.Count; i++)
                        {
                            qtdeVCancelada += 1;
                            sTotal += Convert.ToDecimal(dadosTabelaVendaCanc.Rows[i]["TOTAL"].ToString());
                        }

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("QTDE VENDA: " + qtdeVCancelada.ToString().PadLeft(2, '0'));
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("TOTAL: ........................" + sTotal.ToString("C2") + "\n");

                        //LIMPA VARIAVEIS
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }

                    //DADOS TABELA SANGRIA
                    string dataDia = DateTime.Now.Date.ToShortDateString();
                    int count = 0;
                    novaSangria = new RegraNegocio.SangriaRegraNegocios();
                    DataTable dadosTabelaSangria = new DataTable();

                    dadosTabelaSangria = novaSangria.PesquisarSangriaCaixa(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaSangria.Rows.Count > 0)
                    {
                        cabecalhoSangria = ("              **** SANGRIA ****           \n");

                        for (int i = 0; i < dadosTabelaSangria.Rows.Count; i++)
                        {
                            sTotal += Convert.ToDecimal(dadosTabelaSangria.Rows[i]["TOTAL"].ToString());
                            count += 1;
                        }

                        total_ = ("TOTAL SANGRIA:  " + sTotal.ToString("C2").PadLeft(25, '.'));
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(cabecalhoSangria + "\n" + total_ + "\n\n");

                        //LIMPA VARIAVEIS
                        cabecalhoSangria = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                        count = 0;
                    }

                    #endregion

                    decimal tt = 0;
                    //DADOS FORMA PAGAMENTO
                    #region fORMApAGTO
                    cabecalhoformaPagamento = ("           **** FORMA DE PAGAMENTO ****     \n" +
                                                          "Tipo                 Venda(s)               \n");

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(cabecalhoformaPagamento);

                    novaFormaPgto = new RegraNegocio.FormaPagamentoRegraNegocio();
                    DataTable dadosTabelaFPagamento = new DataTable();
                    dadosTabelaFPagamento = novaFormaPgto.PesquisarFPgto(data, frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaFPagamento.Rows.Count > 0)
                    {
                        for (int i = 0; i < dadosTabelaFPagamento.Rows.Count; i++)
                        {
                            tipoPagamento = dadosTabelaFPagamento.Rows[i]["TIPO"].ToString();
                            sTotal = Convert.ToDecimal(dadosTabelaFPagamento.Rows[i]["VENDA"].ToString());

                            tt += Convert.ToDecimal(dadosTabelaFPagamento.Rows[i]["VENDA"].ToString());

                            if (tipoPagamento.Length >= 6)
                            {
                                tipoPagamento = tipoPagamento + "       ";
                            }

                            tipoPagamento = tipoPagamento.Substring(0, 9);

                            total_ = sTotal.ToString();

                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(tipoPagamento + "       " + total_.ToString().PadLeft(3, ' '));
                        }

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("TOTAL: ........................" + tt.ToString("C2") + "\n\n");

                        //LIMPA VARIAVEIS
                        vendaFormaPagamento = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;


                    #endregion
                        //DADOS DIFERNÇA CAIXA

                        #region DiferencaCaixa
                        cabecalhoDirencacaixa = ("          *** FECHAMENTO CAIXA ***          \n");
                        dadosDiferencaCaia = ("MOEDA:     " + moeda + "\n" + "DINHEIRO:  " + dinheiro + "\n" + "CARTÃO:    " + cartao + "\n" + "CHEQUE:    " + cheque + "\n" + "CONVENIO:  " + convenio + "\n" + "SANGRIA:   " + sangria + "\n" + "DESPESA:   " + despesa + "\n\n" +
                                              "TOTAL: " + txtTotalRecebimento.Text.PadLeft(35, '.') + "\n\n" + "DIFERENCA CAIXA: " + lblDiferencaCaixa.Text.PadLeft(25, '.'));

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(cabecalhoDirencacaixa + "\n" + dadosDiferencaCaia + "\n");
                        //LIMPAR VARIAVEIS
                        cabecalhoDirencacaixa = "";
                        dadosDiferencaCaia = "";
                        #endregion

                        //DADOS VENDA DEPARTAMENTO
                        #region Departamento
                        sTotal = 0;
                        total_ = "";

                        novaVenda = new RegraNegocio.VendaRegraNegocios();
                        DataTable dadosTabelaVendaDep = new DataTable();
                        dadosTabelaVendaDep = novaVenda.FecharVendaDepartamentos(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                        if (dadosTabelaVendaDep.Rows.Count > 0)
                        {
                            cabecalhoDepartamento = ("      **** VENDA POR DEPARTAMENTO ****   \n" +
                                                     "N Dep    Departamento                 Total \n");

                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(cabecalhoDepartamento);

                            for (int d = 0; d < dadosTabelaVendaDep.Rows.Count; d++)
                            {
                                dep = dadosTabelaVendaDep.Rows[d]["DEPARTAMENTO"].ToString();
                                numDep = dadosTabelaVendaDep.Rows[d]["DEP"].ToString();
                                total = dadosTabelaVendaDep.Rows[d]["Total"].ToString();
                                sTotal += Convert.ToDecimal(dadosTabelaVendaDep.Rows[d]["Total"].ToString());

                                numDep = numDep.PadLeft(5, '0');

                                if (dep.Length < 20)
                                {
                                    dep += "                ";
                                }

                                if (dep.Length > 20)
                                {
                                    dep = dep.Substring(0, 20);
                                }

                                total = total.PadLeft(5, ' ');

                                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(numDep + "    " + dep + "         " + total);
                            }

                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("TOTAL: ........................" + sTotal.ToString("C2") + "\n\n");

                            //LIMPA VARIAVEIS
                            vendaDeparatmento = "";
                            sTotal = 0;
                            total_ = "";
                            idRetorno = 0;
                        }
                        #endregion

                        //DADOS TABELA VENDA TOTAL

                        #region VendaTotal
                        novaVenda = new RegraNegocio.VendaRegraNegocios();
                        DataTable dadosTabelaVendaTotal = new DataTable();

                        dadosTabelaVendaTotal = novaVenda.GerarRelatorioVendaTotal(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                        if (dadosTabelaVendaTotal.Rows.Count > 0)
                        {
                            cabecalhoVendaTotal = ("             **** VENDA TOTAL ***          \n" +
                                                    "Cod   Descrição              Qtde     Total\n");

                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(cabecalhoVendaTotal);

                            for (int i = 0; i < dadosTabelaVendaTotal.Rows.Count; i++)
                            {
                                codBarra = dadosTabelaVendaTotal.Rows[i]["Cod"].ToString();
                                desc = dadosTabelaVendaTotal.Rows[i]["Descricao"].ToString();
                                qtde = dadosTabelaVendaTotal.Rows[i]["qtde"].ToString();
                                total = dadosTabelaVendaTotal.Rows[i]["Total"].ToString();

                                sTotal += Convert.ToDecimal(total);

                                codBarra = codBarra.Trim();

                                if (codBarra.Length < 13)
                                {
                                    codBarra = codBarra + "XXXXXXXXXXXXXX";
                                }
                                codBarra = codBarra.Substring(8, 5);

                                if (desc.Length < 20)
                                {
                                    desc = desc + "               ";
                                }

                                if (desc.Length > 20)
                                {
                                    desc = desc.Substring(0, 20);
                                }

                                qtde = qtde.PadLeft(3, ' ');
                                total = total.PadLeft(3, ' ');

                                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codBarra + " " + desc + "   " + qtde + "     " + total);
                            }

                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("TOTAL: ........................" + sTotal.ToString("C2") + "\n\n");

                            //LIMPA VARIAVEIS
                            vendaDeparatmento = "";
                            vendaTotaldia = "";
                            sTotal = 0;
                            total_ = "";
                            idRetorno = 0;
                        }


                        #endregion
                    }

                    //DADOS TABELA PRODUTO ESTOQUE
                    #region Estoque
                    cabecalhoEstoque = ("            *** ESTOQUE PRODUTOS ***             \n" +
                                                   "Codigo Descricao            Qtde  Estoque        \n");

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(cabecalhoEstoque);

                    //busca tabela venda

                    string dadosUsuario = "";
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaEstoque = new DataTable();
                    dadosTabelaVendaEstoque = novaVenda.GerarRealtorioEstoque(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaEstoque.Rows.Count > 0)
                    {
                        for (int y = 0; y < dadosTabelaVendaEstoque.Rows.Count; y++)
                        {
                            qtde = "";
                            codBarra = dadosTabelaVendaEstoque.Rows[y]["Cod"].ToString();
                            desc = dadosTabelaVendaEstoque.Rows[y]["Descricao"].ToString();
                            qtdeEstoque = Convert.ToDecimal(dadosTabelaVendaEstoque.Rows[y]["Estoque"].ToString());
                            qtde = dadosTabelaVendaEstoque.Rows[y]["Qtde"].ToString();
                            //   sTotal += Convert.ToDecimal(dadosTabelaVendaEstoque.Rows[y]["TOTAL"].ToString());

                            codBarra = codBarra.Trim();
                            codBarra = codBarra.Substring(8, 5);

                            if (desc.Length < 20)
                            {
                                desc = desc + "               ";
                            }

                            if (desc.Length > 20)
                            {
                                desc = desc.Substring(0, 20);
                            }

                            string qe = qtdeEstoque.ToString().Trim().PadLeft(3, ' ');
                            total = sTotal.ToString().PadLeft(11, ' ');

                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codBarra + " " + desc + "  " + qtde + "   " + qe);
                        }

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("TOTAL: ........................" + sTotal.ToString("C2") + "\n\n");


                        dadosUsuario = ("------------------------------------------------\n" +
                               "CAIXA: " + frmVenda.numcaixa.ToString().Trim().PadLeft(3, '0') + "\n" +
                                "OPERADOR: " + frmVenda.operadorAtuante + "\n" +
                                "DATA: " + DateTime.Now.Date.ToShortDateString().ToString());

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(dadosUsuario);

                        //LIMPA VARIAVEIS
                        vendaEstoque = "";
                        cabecalhoEstoque = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }

                    //FECHAR CUPOM
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                    #endregion

                }
                else if (nomeImpressora == "ELGIN")
                {
                    var configImpressora = new PrinterSettings();
                    printerName = configImpressora.PrinterName;

                    PesquisarDadosCliente();
                    frmVenda.AbreCupomSegViaBemasat();
                    string cabecalhoElgin = frmVenda.A;

                    esc = new RegraNegocio.EscPos();
                    this.esc.normalModeText(printerName);
                    this.esc.printText(printerName, cabecalhoElgin + "\n");


                    #region SETOR
                    novoSetor = new RegraNegocio.SetorRegraNegocios();
                    DataTable dadosTabelaSetor = new DataTable();

                    dadosTabelaSetor = novoSetor.PesquisarSetorNumero();

                    if (dadosTabelaSetor.Rows.Count > 0)
                    {
                        for (int i = 0; i < dadosTabelaSetor.Rows.Count; i++)
                        {
                            setor = Convert.ToInt32(dadosTabelaSetor.Rows[i]["ID"].ToString());
                            string descricaoSetor = dadosTabelaSetor.Rows[i]["DESCRICAO"].ToString();
                            descricaoSetor = descricaoSetor.Trim();

                            if (setor > 0)
                            {
                                //busca tabela venda
                                novaVenda = new RegraNegocio.VendaRegraNegocios();
                                DataTable dadosTabelaVenda = new DataTable();
                                dadosTabelaVenda = novaVenda.GerarRealtorioSetor1(frmVenda.idUsuarioLogado, setor, frmVenda.numcaixa);

                                if (dadosTabelaVenda.Rows.Count > 0)
                                {
                                    cabecalhoSetor = ("        **** VENDA POR SETOR ****   \n" +
                                                      descricaoSetor + ":" + setor + "      \n");

                                    for (int y = 0; y < dadosTabelaVenda.Rows.Count; y++)
                                    {
                                        codBarra = dadosTabelaVenda.Rows[y]["Cod"].ToString();
                                        desc = dadosTabelaVenda.Rows[y]["Descricao"].ToString();
                                        qtde = dadosTabelaVenda.Rows[y]["qtde"].ToString();
                                        total = dadosTabelaVenda.Rows[y]["Total"].ToString();

                                        sTotal += Convert.ToDecimal(total);

                                        codBarra = codBarra.Trim();
                                        codBarra = codBarra.Substring(8, 5);

                                        if (desc.Length < 20)
                                        {
                                            desc = desc + "                   ";
                                        }

                                        if (desc.Length > 20)
                                        {
                                            desc = desc.Substring(0, 20);
                                        }

                                        qtde = qtde.PadLeft(3, ' ');
                                        total = total.PadLeft(3, ' ');

                                        vendaSetor += (codBarra + " " + desc + "   " + qtde + "     " + total + "\n");
                                        total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(38, '.') + "\n");
                                    }

                                    esc = new RegraNegocio.EscPos();
                                    this.esc.normalModeText(printerName);
                                    this.esc.printText(printerName, cabecalhoSetor + "\n" + total_);

                                    //LIMPA VARIAVEIS
                                    vendaSetor = "";
                                    sTotal = 0;
                                    total_ = "";
                                    idRetorno = 0;
                                    descricaoSetor = "";
                                }
                            }
                            else
                            {
                                MessageBox.Show("Setor não pode ser Zero", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }

                    #endregion

                    //DADOS VENDA DEPARTAMENTO
                    #region DEPARTAMENTO
                    sTotal = 0;
                    total_ = "";

                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaDep = new DataTable();
                    dadosTabelaVendaDep = novaVenda.FecharVendaDepartamentos(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaDep.Rows.Count > 0)
                    {
                        cabecalhoDepartamento = ("      **** VENDA POR DEPARTAMENTO ****   \n" +
                                                 "N Dep    Departamento                 Total \n");

                        for (int d = 0; d < dadosTabelaVendaDep.Rows.Count; d++)
                        {
                            dep = dadosTabelaVendaDep.Rows[d]["DEPARTAMENTO"].ToString();
                            numDep = dadosTabelaVendaDep.Rows[d]["DEP"].ToString();
                            total = dadosTabelaVendaDep.Rows[d]["Total"].ToString();
                            sTotal += Convert.ToDecimal(dadosTabelaVendaDep.Rows[d]["Total"].ToString());

                            numDep = numDep.PadLeft(5, '0');

                            if (dep.Length < 20)
                            {
                                dep += "                ";
                            }

                            if (dep.Length > 20)
                            {
                                dep = dep.Substring(0, 20);
                            }

                            total = total.PadLeft(5, ' ');

                            vendaDeparatmento += (numDep + "    " + dep + "         " + total + "\n");
                            total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));
                        }

                        esc = new RegraNegocio.EscPos();
                        this.esc.normalModeText(printerName);
                        this.esc.printText(printerName, cabecalhoDepartamento + "\n" + vendaDeparatmento + "\n" + total_ + "\n\n");

                        //LIMPA VARIAVEIS
                        vendaDeparatmento = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }

                    #endregion

                    //DADOS TABELA VENDAS CANCELADA
                    #region cANCEL
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaCanc = new DataTable();
                    dadosTabelaVendaCanc = novaVenda.PesquisarVendaCancelado(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaCanc.Rows.Count > 0)
                    {
                        cabecalhoVendaCancelada = ("      **** VENDA(S) CANCELADA(S) ****   \n");

                        for (int i = 0; i < dadosTabelaVendaCanc.Rows.Count; i++)
                        {
                            qtdeVCancelada += 1;
                            sTotal += Convert.ToDecimal(dadosTabelaVendaCanc.Rows[i]["TOTAL"].ToString());
                        }

                        total_ = ("QTDE VENDA: " + qtdeVCancelada.ToString().PadLeft(2, '0') + "\n" +
                                  "TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));

                        esc = new RegraNegocio.EscPos();
                        this.esc.normalModeText(printerName);
                        this.esc.printText(printerName, cabecalhoVendaCancelada + "\n" + total_ + "\n\n");

                        //LIMPA VARIAVEIS
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }

                    #endregion

                    //DADOS TABELA SANGRIA
                    #region SANGRIA
                    string dataDia = DateTime.Now.Date.ToShortDateString();
                    int count = 0;
                    novaSangria = new RegraNegocio.SangriaRegraNegocios();
                    DataTable dadosTabelaSangria = new DataTable();

                    dadosTabelaSangria = novaSangria.PesquisarSangriaCaixa(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaSangria.Rows.Count > 0)
                    {
                        cabecalhoSangria = ("              **** SANGRIA ****           \n");

                        for (int i = 0; i < dadosTabelaSangria.Rows.Count; i++)
                        {
                            sTotal += Convert.ToDecimal(dadosTabelaSangria.Rows[i]["TOTAL"].ToString());
                            count += 1;
                        }

                        total_ = ("TOTAL SANGRIA:  " + sTotal.ToString("C2").PadLeft(25, '.'));

                        esc = new RegraNegocio.EscPos();
                        this.esc.normalModeText(printerName);
                        this.esc.printText(printerName, cabecalhoSangria + "\n" + total_ + "\n\n");

                        //LIMPA VARIAVEIS
                        cabecalhoSangria = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                        count = 0;
                    }
                    #endregion

                    //DADOS TABELA VENDA TOTAL
                    #region VENDA TOTAL
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaTotal = new DataTable();

                    dadosTabelaVendaTotal = novaVenda.GerarRelatorioVendaTotal(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaTotal.Rows.Count > 0)
                    {
                        cabecalhoVendaTotal = ("             **** VENDA TOTAL ***          \n" +
                                                "Cod   Descrição              Qtde     Total\n");

                        for (int i = 0; i < dadosTabelaVendaTotal.Rows.Count; i++)
                        {
                            codBarra = dadosTabelaVendaTotal.Rows[i]["Cod"].ToString();
                            desc = dadosTabelaVendaTotal.Rows[i]["Descricao"].ToString();
                            qtde = dadosTabelaVendaTotal.Rows[i]["qtde"].ToString();
                            total = dadosTabelaVendaTotal.Rows[i]["Total"].ToString();

                            sTotal += Convert.ToDecimal(total);

                            codBarra = codBarra.Trim();

                            if (codBarra.Length < 13)
                            {
                                codBarra = codBarra + "XXXXXXXXXXXXXX";
                            }

                            codBarra = codBarra.Substring(8, 5);

                            if (desc.Length < 20)
                            {
                                desc = desc + "               ";
                            }

                            if (desc.Length > 20)
                            {
                                desc = desc.Substring(0, 20);
                            }

                            qtde = qtde.PadLeft(3, ' ');
                            total = total.PadLeft(3, ' ');

                            vendaTotaldia += (codBarra + " " + desc + "   " + qtde + "     " + total + "\n");
                            total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));
                        }

                        esc = new RegraNegocio.EscPos();
                        this.esc.normalModeText(printerName);
                        this.esc.printText(printerName, cabecalhoVendaTotal + "\n" + vendaTotaldia + "\n" + total_ + "\n\n");

                        //LIMPA VARIAVEIS
                        vendaDeparatmento = "";
                        vendaTotaldia = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }
                    decimal tt = 0;
                    #endregion

                    //DADOS FORMA PAGAMENTO
                    #region FORMA PAGTO
                    cabecalhoformaPagamento = ("           **** FORMA DE PAGAMENTO ****     \n" +
                                                           "Tipo                 Venda(s)               \n");

                    novaFormaPgto = new RegraNegocio.FormaPagamentoRegraNegocio();
                    DataTable dadosTabelaFPagamento = new DataTable();
                    dadosTabelaFPagamento = novaFormaPgto.PesquisarFPgto(data, frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaFPagamento.Rows.Count > 0)
                    {
                        for (int i = 0; i < dadosTabelaFPagamento.Rows.Count; i++)
                        {
                            tipoPagamento = dadosTabelaFPagamento.Rows[i]["TIPO"].ToString();
                            sTotal = Convert.ToDecimal(dadosTabelaFPagamento.Rows[i]["VENDA"].ToString());

                            tt += Convert.ToDecimal(dadosTabelaFPagamento.Rows[i]["VENDA"].ToString());

                            if (tipoPagamento.Length >= 6)
                            {
                                tipoPagamento = tipoPagamento + "       ";
                            }

                            tipoPagamento = tipoPagamento.Substring(0, 9);

                            total_ = sTotal.ToString();
                            vendaFormaPagamento += (tipoPagamento + "       " + total_.ToString().PadLeft(3, ' ') + "\n");
                            total_ = "";
                            sTotal = 0;
                        }

                        total_ = ("TOTAL: " + tt.ToString().PadLeft(36, '.'));

                        //LIMPA VARIAVEIS
                        vendaFormaPagamento = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;

                        //DADOS DIFERNÇA CAIXA

                        cabecalhoDirencacaixa = ("          *** FECHAMENTO CAIXA ***          \n");
                        dadosDiferencaCaia = ("MOEDA:     " + moeda + "\n" + "DINHEIRO:  " + dinheiro + "\n" + "CARTÃO:    " + cartao + "\n" + "CHEQUE:    " + cheque + "\n" + "CONVENIO:  " + convenio + "\n" + "SANGRIA:   " + sangria + "\n" + "DESPESA:   " + despesa + "\n\n" +
                                              "TOTAL: " + txtTotalRecebimento.Text.PadLeft(35, '.') + "\n\n" + "DIFERENCA CAIXA: " + lblDiferencaCaixa.Text.PadLeft(25, '.'));

                        esc = new RegraNegocio.EscPos();
                        this.esc.normalModeText(printerName);
                        this.esc.printText(printerName, cabecalhoDirencacaixa + "\n" + dadosDiferencaCaia + "\n\n");

                        //LIMPAR VARIAVEIS
                        cabecalhoDirencacaixa = "";
                        dadosDiferencaCaia = "";
                    }

                    #endregion

                    //DADOS TABELA PRODUTO ESTOQUE
                    cabecalhoEstoque = ("            *** ESTOQUE PRODUTOS ***             \n" +
                                        "Codigo Descricao            Qtde  Estoque        \n");


                    //busca tabela venda
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVendaEstoque = new DataTable();
                    dadosTabelaVendaEstoque = novaVenda.GerarRealtorioEstoque(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                    if (dadosTabelaVendaEstoque.Rows.Count > 0)
                    {
                        for (int y = 0; y < dadosTabelaVendaEstoque.Rows.Count; y++)
                        {
                            qtde = "";
                            codBarra = dadosTabelaVendaEstoque.Rows[y]["Cod"].ToString();
                            desc = dadosTabelaVendaEstoque.Rows[y]["Descricao"].ToString();
                            qtdeEstoque = Convert.ToDecimal(dadosTabelaVendaEstoque.Rows[y]["Estoque"].ToString());
                            qtde = dadosTabelaVendaEstoque.Rows[y]["Qtde"].ToString();
                            //   sTotal += Convert.ToDecimal(dadosTabelaVendaEstoque.Rows[y]["TOTAL"].ToString());

                            codBarra = codBarra.Trim();
                            codBarra = codBarra.Substring(8, 5);

                            if (desc.Length < 20)
                            {
                                desc = desc + "               ";
                            }

                            if (desc.Length > 20)
                            {
                                desc = desc.Substring(0, 20);
                            }

                            string qe = qtdeEstoque.ToString().Trim().PadLeft(3, ' ');
                            total = sTotal.ToString().PadLeft(11, ' ');

                            vendaEstoque += (codBarra + " " + desc + "  " + qtde + "   " + qe + "\n");
                            //    total_ = ("TOTAL: " + sTotal.ToString("C2").PadLeft(36, '.'));
                        }

                        esc = new RegraNegocio.EscPos();
                        this.esc.normalModeText(printerName);
                        this.esc.printText(printerName, cabecalhoEstoque + "\n" + vendaEstoque + "\n\n");

                        //LIMPA VARIAVEIS
                        vendaEstoque = "";
                        cabecalhoEstoque = "";
                        sTotal = 0;
                        total_ = "";
                        idRetorno = 0;
                    }

                    string dadosFinaisCupomElgin = "";

                    dadosFinaisCupomElgin = ("------------------------------------------------\n" +
                                             "CAIXA: " + frmVenda.numcaixa.ToString().Trim().PadLeft(3, '0') + "\n" +
                                             "OPERADOR: " + frmVenda.operadorAtuante + "\n" +
                                             "DATA: " + DateTime.Now.Date.ToShortDateString().ToString());

                    esc = new RegraNegocio.EscPos();
                    this.esc.normalModeText(printerName);
                    this.esc.printText(printerName, dadosFinaisCupomElgin + "\n");
                    feedAndCutter(printerName, 5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarDadosFechamentoCaixa()
        {
            try
            {
                for (int i = 0; i < gdvFormaPgto.Rows.Count; i++)
                {
                    tipoPagamento = gdvFormaPgto.Rows[i].Cells["colTipo"].Value.ToString();

                    if ((tipoPagamento == "DINHEIRO") || (tipoPagamento == "Dinheiro"))
                    {
                        dinheiro = Convert.ToDecimal(gdvFormaPgto.Rows[i].Cells["colValor"].Value.ToString());
                        txtTotalDinehiro.Text = dinheiro.ToString();
                    }

                    if ((tipoPagamento == "CARTAO") || (tipoPagamento == "Cartao"))
                    {
                        cartao = Convert.ToDecimal(gdvFormaPgto.Rows[i].Cells["colValor"].Value.ToString());
                        txtTotalCartao.Text = cartao.ToString();
                    }

                    if ((tipoPagamento == "CHEQUE") || (tipoPagamento == "Cheque"))
                    {
                        cheque = Convert.ToDecimal(gdvFormaPgto.Rows[i].Cells["colValor"].Value.ToString());
                        txtTotalCheque.Text = cheque.ToString();
                    }

                    if ((tipoPagamento == "ABERTO") || (tipoPagamento == "Aberto"))
                    {
                        convenio = Convert.ToDecimal(gdvFormaPgto.Rows[i].Cells["colValor"].Value.ToString());
                        txtTotalConvenio.Text = convenio.ToString();
                    }
                }

                PesquisarSangria();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarSangria()
        {
            try
            {
                //DADOS TABELA SANGRIA
                string dataDia = DateTime.Now.Date.ToShortDateString();

                novaSangria = new RegraNegocio.SangriaRegraNegocios();
                DataTable dadosTabelaSangria = new DataTable();

                dadosTabelaSangria = novaSangria.PesquisarSangriaCaixa(frmVenda.idUsuarioLogado, frmVenda.numcaixa);

                if (dadosTabelaSangria.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaSangria.Rows.Count; i++)
                    {
                        bool tipo = Convert.ToBoolean(dadosTabelaSangria.Rows[i]["TIPO"].ToString());

                        if (tipo == true)
                        {
                            sangria = Convert.ToDecimal(dadosTabelaSangria.Rows[i]["TOTAL"].ToString());
                        }
                        else if (tipo == false)
                        {
                            despesa = Convert.ToDecimal(dadosTabelaSangria.Rows[i]["TOTAL"].ToString());
                        }
                    }

                    txtTotalSangria.Text = sangria.ToString();
                    txtDespesas.Text = despesa.ToString();
                }
                else
                {
                    txtTotalSangria.Text = "0,00";
                    txtDespesas.Text = "0,00";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            try
            {
                moeda = Convert.ToDecimal(txtMoeda.Text);
                dinheiro = Convert.ToDecimal(txtTotalDinehiro.Text);
                cartao = Convert.ToDecimal(txtTotalCartao.Text);
                cheque = Convert.ToDecimal(txtTotalCheque.Text);
                convenio = Convert.ToDecimal(txtTotalConvenio.Text);
                sangria = Convert.ToDecimal(txtTotalSangria.Text);
                despesa = Convert.ToDecimal(txtDespesas.Text);
                string st = txtSomaTotal.Text;

                st = st.Replace("R$", "");
                somaTotal = Convert.ToDecimal(st);
                resultado = (moeda + dinheiro + cartao + cheque + convenio + sangria + despesa);
                txtTotalRecebimento.Text = resultado.ToString("C2");
                diferencaCaixa = (resultado - somaTotal);
                lblDiferencaCaixa.Text = diferencaCaixa.ToString();
                diferencaCaixa = 0;
                PesquisarSangria();
                btnFecharCaixa.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            pnlFechamentoCaixa.Visible = true;
            txtMoeda.Focus();
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            if (idRetorno > 0)
            {
                idRetorno = RegraNegocio.MP2032.FechaPorta();
            }

            ImprimirRelatoriosFechamentoCaixa();
        }

        private void txtMoeda_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtMoeda);
        }

        private void txtTotalDinehiro_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtTotalDinehiro);
        }

        private void txtTotalCartao_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtTotalCartao);
        }

        private void txtTotalCheque_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtTotalCheque);
        }

        private void txtTotalConvenio_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtTotalConvenio);
        }

        private void txtTotalSangria_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtTotalSangria);
        }

        private void txtMoeda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtTotalDinehiro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtTotalCartao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtTotalCheque_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtTotalConvenio_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtTotalSangria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtDespesas_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCalcular_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BKP_Banco();
        }
    }
}
