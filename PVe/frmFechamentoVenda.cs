using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Globalization;
using System.Runtime.InteropServices;
using iTextSharp.text.pdf;
using BarcodeLib;

using System.IO.Ports;

using MessagingToolkit.QRCode.Codec;
using MessagingToolkit.QRCode.Codec.Data;
using MessagingToolkit.QRCode.Helper;

using RegraNegocio;
using System.Xml.Linq;

using System.Diagnostics;
using System.Drawing.Printing;

namespace PVe
{
    public partial class frmFechamentoVenda : Form
    {
        #region DLLs

        [DllImport("MP2032.dll")]
        public static extern int BematechTX(String texto);

        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("MP2032.dll")]
        public static extern int ConfiguraCodigoBarras(int Altura, int Largura, int Posicao, int Fonte, int Margem);

        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("MP2032.dll")]
        public static extern int ImprimeCodigoBarrasCODE128(String texto);

        [DllImport("mp2032.dll")]
        public static extern int ImprimeCodigoQRCODE(int errorCorrectionLevel, int moduleSize, int codeType, int QRCodeVersion, int encodingModes, String codeQr);

        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("MP2032.dll")]
        public static extern int ConfiguraModeloImpressora(int model);

        [DllImport("MP2032.dll")]
        public static extern int IniciaPorta(String porta);

        [DllImport("MP2032.dll")]
        public static extern int FormataTX(String texto, int TipoLetra, int italico, int sublinhado, int expandido, int enfatizado);

        /// <returns>INTEIRO - Indica se a função conseguiu enviar o comando para impressora.</returns>
        [DllImport("MP2032.dll")]
        public static extern int FechaPorta();
        #endregion

        #region VARIAVEIS

        //CONTAS RECEBER

        int idPagamentoVenda, idUsuario, diaVencimento = 0;
        decimal valorReceber, valoRecebido, multa, juros = 0;
        DateTime dataVencimento, dtRecebido, inicio;
        Boolean baixado;

        //variavel do cupom cabecalho
        string informacaoSat = "";
        string cnpjEmit, nomeEmit, nomeFantasiaEmit, logradouroEmit, razaoSocialEmit, numImit, cnpjEmite, ieEmi, imEmi, cidadeEmit, bairroEmit, cepEmit, cpfDest, nCfeEmit, numSerieSat, retornoVendaSat, dataEmit, horaEmit = "";

        string tab1 = " ";
        string tab2 = "     ";

        //varivel produtos cupom
        string itemProd, codProd, descProd, qtdeProd, unidProd, valorUnProd, valorTrProd, valorItemProd, totalProd, trocoProd, tipoPgtoProd, valorEtregaProd, infCompleProd, qrCodProd, totalProduto = "";

        string un = "";
        Boolean cupomImagem;

        //vaiveis do dados Complementares.......
        string nCupomComp, ncaixaComp, operadoComp, peiodoComp, placaComp, msgComp, aliqComp, infComp, kmComp = "";

        string textoCupom = "";
        string A_, B_, C_, D_, E_, F_, G_, H_, I_, J_ = "";
        string impressora = "";
        string portaComImpressora = "";
        string cupomComumAberto = "";
        decimal somaTotalFech, aliquotaFec = 0;
        bool placaAutorizar;

        string dadosCupoDaruma = "";

        //----------------------------------------------------------------------------------------------------------------------------

        //ELGIN
        public string printerName = "ELGIN i9";

        //---------------------------------------------------------------------------------------------------------------------------- 
        #endregion


        #region CLASSES

        RegraNegocio.VendaRegraNegocios novaVenda;
        RegraNegocio.TipoPagamentoRegraNegocio novoTipo;
        RegraNegocio.FechamentoVendaReegraNegocio novoFechamento;
        RegraNegocio.ParametroRegraNegocio novoParametro;
        RegraNegocio.OperadorRegraNegocio novoOperador;
        RegraNegocio.TempRegraNegocios novoTemp;
        RegraNegocio.FormaPagamentoRegraNegocio novaFormaPgto;
        RegraNegocio.PlacaRegraNegocio novaPlaca;
        RegraNegocio.ClienteRegraNegocio novoCliente;
        RegraNegocio.CsosnRegraNegocio novoCsosn;
        RegraNegocio.CST_PIS_RegraNegocios novoPis;
        RegraNegocio.NumCaixaRegraNegocios novoNumCupom;
        RegraNegocio.EscPos esc;
        RegraNegocio.ConexaoRegraNegocios novaConexao;
        RegraNegocio.SetorRegraNegocios novoSetor;
        RegraNegocio.EstoqueInicialRegraNegocios novoEstoqueIncial;
        RegraNegocio.COFINS_RN novoCofins;
        RegraNegocio.ContaReceberRegraNegocios novaContaReceber;
        #endregion

        #region PATH
        string pathFormaPgto = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\FORMA_PGTO\\";
        string pathVendaXML = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\VENDA\\";
        string pathCustodia = @"C:\CFe\Custodia\";
        string pathLogo = @"C:\CFe\Custodia\Logo\Logo.jpg";
        string pathQRCode = @"C:\CFe\Custodia\QRCode\";
        string pathCodBarra = @"C:\CFe\Custodia\CodigoBarra\";
        string pathVendaCancelada = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\uVENDA_CANCELADA\\";
        string pathAssiDitital = Path.GetDirectoryName(Application.ExecutablePath) + "\\Banco\\ASS_DIG.xml";
        string pathDadosVendaAutorizada = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\VENDA_TEXTO\\";
        string pathUltimaVenda = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\ULTIMA_VENDA.xml";
        string pathACodAtivacao = Path.GetDirectoryName(Application.ExecutablePath) + "\\Banco\\COD_ATIVACAO.xml";
        #endregion

        #region OBJETOS

        private string paramentroDocumentos;

        //---------------------------------------------------------------------------------------------
        //ESTOQUE INICIAL MES
        decimal qtdeEstoqueInicial = 0;

        string A, B, C, D, F, E, G, H, I, J, L, M, N;
        private StringReader leitor;
        bool impressaoAutomatica;
        string retornoSat = "";
        string align = "";

        //CUPOM PORTA COM

        string xitem, cnpj, xNome, xLgr, xFant, xBairro, nro, xMun, CEP, IE, IM, cProd, xProd, assinaturaQRCODE;
        string uCom, qCom, vProd, vItem, cMP, vMP, vCFe, vTroco, nserieSAT, nCFe;
        int idRetornoMp = 0;

        //BEMASAT............................................................
        int numvenda;
        string nomeArquivo;
        String arqu;
        public int sessao;
        public string AutVenda = "00000";
        int tipo = 0;
        string nomeCliente = "";
        string idCfe = "";

        string numeroAssinaturaDigital = "";
        string numeroCnpjAssinatura = "";
        string NomeArquCancelVenda;
        string Ultimachave = "";

        //variaveis cancelamento de venda
        string chaveVenda;
        string codAtivacao = "";

        public bool autorizarVendaAberto = false;

        XmlDocument arquivoXML;
        XmlElement nodeCFe;
        XmlElement nodeCFeCanc;
        XmlElement nodeInfCFe;
        XmlElement nodeInfCFeCanc;
        XmlElement nodeIDE;
        XmlElement nodeIDECanc;
        XmlElement nodeEmitente;
        XmlElement nodeDestinatario;
        XmlElement nodeTotal;
        XmlElement nodePagto;
        XmlElement nodeInfAdic;
        XmlElement MP;

        //
        public int idCliente;
        public string valorCofins;
        public string cstPis;
        public string _valorPis;
        public string _cstPis;
        public string _cstCofins;
        public string _cfop;
        public string _ncm;
        public string _icmCst;
        public string _origemProduto;
        decimal somaCompra = 0;
        public decimal somaTotalCompra, valorAberto = 0;
        public Boolean retornoVendaId;
        string porta = "";
        frmVenda frmVenda;
        frmPrincipal frmPrincipal;

        public int abrirFechamentoCaixa = 0;
        #endregion


        public frmFechamentoVenda(frmVenda venda, frmPrincipal fPrincipal)
        {
            InitializeComponent();
            this.frmVenda = venda;
            this.frmPrincipal = fPrincipal;
            impressora = venda.nomeImpressora.ToString();
            lblOperador.Text = venda.NomeUsuario;
        }

        int _numVenda;
        string cpfTemp = "";
        private int idRetorno;
        private int IdRetImpressora;
        string item, codigoB, descri, preco, alicota, tipoQtde, tipoDesconto, desconto, Recebido, qtde, cpf_cnpj, h_, csosn, cest, cstProduto, crt, tipoCSOSN, aliquota;
        decimal _soma = 0;
        string desc;
        string codTipoPagamento, descricaoTipoPagamento;
        string RetECF;
        decimal recebido = 0;
        decimal total = 0;
        decimal troco = 0;
        decimal _valorTotal = 0;
        bool autorizarPlaca;
        string PLACA = "PLACA:";
        string KM = "KM:";
        string dadosCupomElgin;

        //VARIAVEIS CPF CNPJ
        string cpfCnpjClinente = "";
        string cnpjEstabelecimento = "";
        decimal aliqDia = 0;

        //string da pasta para exporta dados venda................. 
        private SqlConnection conn;
        private SqlDataAdapter daVenda;

        private const string tabela = "VENDA";
        public string nomeImpressora = "";

        string descTipoPagamento;

        public frmFechamentoVenda(string documentos)
        {
            this.paramentroDocumentos = documentos;
        }

        public void ExportarPagamentoVendas()
        {
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PVe.Properties.Settings.CFeConnectionString"].ToString());

                string numeroPastaPgtoVenda = _numVenda.ToString();
                string pathF_Pgto = (pathFormaPgto + numeroPastaPgtoVenda + ".XML");
                DataSet dsVenda = new DataSet();
                daVenda = new SqlDataAdapter("SELECT NUM_CUPOM, TIPO_PAGAMENTO, VALOR, DT, TROCO, CNPJ FROM PAGAMENTO_VENDA where PAGAMENTO_VENDA.NUM_CUPOM =" + _numVenda, conn);
                daVenda.Fill(dsVenda, tabela);
                dsVenda.WriteXml(pathF_Pgto);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método ExportarPagamentoVendas.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ExportarItemVenda()
        {
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["PVe.Properties.Settings.CFeConnectionString"].ToString());

                string numeroPastaVenda = _numVenda.ToString();
                string pathVenda = (pathVendaXML + numeroPastaVenda + ".XML");
                DataSet dsVenda = new DataSet();
                daVenda = new SqlDataAdapter("select ID,ID_PROD,COD_BARRA, QUANT, PRECO, TOTAL, ITEM, DATA, NUM_VENDA from VENDA where VENDA.NUM_VENDA = " + _numVenda, conn);
                daVenda.Fill(dsVenda, tabela);
                dsVenda.WriteXml(pathVenda);

            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método ExportarItemVenda.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SetFormatting()
        {
            gdvTipoPgto.Columns["Valor"].DefaultCellStyle.Format = "C2";
            //  this.gdvTipoPgto.Columns["PRECO"].DefaultCellStyle.Format = "C";
        }

        private void frmFechamentoVenda_Load(object sender, EventArgs e)
        {
           
            nomeImpressora = frmVenda.nomeImpressora;
            lblOperador.Text = frmVenda.operadorAtuante.ToString();

            PesquisarTipo();

            lblNumCaixa.Text = frmVenda.numcaixa.ToString();

            if ((nomeImpressora == "BEMATECH") || nomeImpressora == "SAT" || (nomeImpressora == "BEMATECH"))
            {
                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_VerificaImpressoraLigada();
                SetFormatting();

                if (idRetorno <= 0)
                {
                    frmVenda.ProgressBarEtart();
                }

                lblOperador.Text = frmVenda.operadorAtuante;
                //Metodo automatico para verificar a Redução Z do Dia foi Ralizada...................................................
                int ACK, ST1, ST2, ST3;
                ACK = ST1 = ST2 = ST3 = 0;

                RegraNegocio.CupomFiscal.Bematech_FI_RetornoImpressoraMFD(ref ACK, ref ST1, ref ST2, ref ST3);

                if (ST3 == 66)
                {
                    MessageBox.Show("Está sendo realizado Redução " + "Z" + " do Sistema, aguarde alguns segundos para retirar cupom na Impressora.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RegraNegocio.CupomFiscal.Bematech_FI_ReducaoZ("", "");
                }

                if (idRetorno > 0)
                {
                    PesquisarAssinaturaDigital();
                    PrencherCupomFiscal();
                    PesquisarPlaca();
                    UltimaVenda();
                    SomaTotal();
                }
                else
                {
                    this.Close();
                }
            }
            else if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "COMUM") || (nomeImpressora == "DARUMA") || (nomeImpressora == "ELGIN") || (nomeImpressora == "LPT") || (nomeImpressora == "MP2500") || (nomeImpressora == "MP4200"))
            {
                PesquisarAssinaturaDigital();
                PrencherCupomFiscal();
                PesquisarPlaca();
                UltimaVenda();
                SomaTotal();
            }
        }

        public void PesquisarPlaca()
        {
            try
            {
                novaPlaca = new RegraNegocio.PlacaRegraNegocio();
                DataTable dadosTabelaPlaca = new DataTable();
                dadosTabelaPlaca = novaPlaca.PesquisarPlaca(lblEntrada.Text);

                if (dadosTabelaPlaca.Rows.Count > 0)
                {
                    lblNomePlaca.Text = dadosTabelaPlaca.Rows[0]["PLACA"].ToString();
                    lblKmPlaca.Text = dadosTabelaPlaca.Rows[0]["KM"].ToString();

                    placaComp = dadosTabelaPlaca.Rows[0]["PLACA"].ToString();
                    kmComp = dadosTabelaPlaca.Rows[0]["KM"].ToString();

                    placaComp = placaComp.Trim();
                    kmComp = kmComp.Trim();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void SomaTotal()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.SomaTotalEntrada(_numVenda, Convert.ToInt32(frmVenda.numcaixa));

                if (dadosTabela.Rows.Count > 0)
                {
                    somaCompra = Convert.ToDecimal(dadosTabela.Rows[0][0].ToString());

                    txtTotal.Text = somaCompra.ToString();

                    lblSomaCupom.Text = somaCompra.ToString("C2");
                    txtTroco.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método SomaTotal.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void UltimaVenda()
        {
            try
            {
                _numVenda = Convert.ToInt32(frmVenda.numCupom);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método UltimaVenda.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void AlterarNumeroEntrada()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                int altNum = _numVenda = _numVenda + 1;
                novaVenda.AlterarNumEntrada(altNum);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método AlterarNumeroEntrada.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtRecebimento_KeyUp(object sender, KeyEventArgs e)
        {
            novoParametro = new RegraNegocio.ParametroRegraNegocio();
            DataTable dadosTabelaParametro = new DataTable();
            dadosTabelaParametro = novoParametro.PesquisaParametroE();

            autorizarPlaca = Convert.ToBoolean(dadosTabelaParametro.Rows[0]["PLACA"].ToString());
            
            aliqDia = Convert.ToDecimal(dadosTabelaParametro.Rows[0]["ALIQUOTA_DIA"].ToString());

            if (e.KeyCode == Keys.Space)
            {
                if (autorizarPlaca == true)
                {
                    frmPlacaVeiculo fpv = new frmPlacaVeiculo(frmVenda);
                    fpv.ShowDialog();

                    txtRecebimento.Focus();
                    txtRecebimento.Text = "0,00";
                }
            }

            if (e.KeyCode == Keys.End)
            {
                FecharVendaAberto();
            }

            if (e.KeyCode == Keys.F8)
            {
                frmCpf_Cnpj frmCpfCnpj = new frmCpf_Cnpj(frmVenda);
                frmCpfCnpj.ShowDialog();
            }

            if (e.KeyCode == Keys.F5)
            {
                frmTipoPagamento frmTipoPgto = new frmTipoPagamento(this);
                frmTipoPgto.ShowDialog();
            }

            if (e.KeyCode == Keys.PageUp)
            {
                FecharVendaCheque();
            }

            //if (e.KeyCode == Keys.Escape)
            //{
            //    frmVenda.PesquisarNumCaixa_NumVenda();
            //    this.Close();
            //}

            if (e.KeyCode == Keys.Home)
            {
                FecharVendaCartao();
            }

            if (e.KeyCode == Keys.PageDown)
            {
                FecharVendaDinheiro();
            }

            if (e.KeyCode == Keys.Back)
            {
                txtRecebimento.Text = "";
                txtRecebimento.Focus();
            }
        }

        private void btnDinheiro_Click(object sender, EventArgs e)
        {
            FecharVendaDinheiro();
        }

        public void FecharVendaDinheiro()
        {
            try
            {
                codTipoPagamento = "1";
                decimal somaRecebimento = 0;
                decimal valorVenda = 0;
                decimal somax = 0;

                novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoTipo.PesquisaTipoPagamento(codTipoPagamento);

                if (dadosTabela.Rows.Count > 0)
                {
                    descTipoPagamento = dadosTabela.Rows[0]["TIPO_PAGTO"].ToString();

                    codTipoPagamento = "1";
                    HabilitarCampos();

                    txtTroco.Text = "0,00";

                    decimal recebimento = 0;
                    recebimento = Convert.ToDecimal(txtRecebimento.Text);

                    if (txtRecebimento.Text == "0,00")
                    {
                        gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtTotal.Text);
                        LiberarBotoesFecharCaixa();
                        txtRecebimento.ReadOnly = true;
                        btnFecharVenda.Focus();
                        SetFormatting();
                        retornoVendaId = true;
                    }

                    if (recebimento > 0)
                    {
                        gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtRecebimento.Text);

                        for (int i = 0; i < gdvTipoPgto.Rows.Count; i++)
                        {
                            somaRecebimento += Convert.ToDecimal(gdvTipoPgto.Rows[i].Cells[1].Value);
                        }

                        valorVenda = Convert.ToDecimal(txtTotal.Text);

                        txtTroco.Text = (somaRecebimento - valorVenda).ToString();
                        //txtRecebimento.Text = "";
                        txtRecebimento.Focus();

                        somax = Convert.ToDecimal(txtTroco.Text);

                        if (somax >= 0)
                        {
                            LiberarBotoesFecharCaixa();
                            btnFecharVenda.Focus();
                        }
                        else
                        {
                            somax = (somax * -1);
                            txtRecebimento.Select();
                            txtRecebimento.Text = somax.ToString();
                        }

                        SetFormatting();
                        retornoVendaId = true;
                    }
                }
                else
                {
                    MessageBox.Show("Não Contém Dados da Tabela Tipo de Pagamento Inserido no Banco", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro no Método Fechar Venda Dinheiro. \nDetalhes: " + ex.Message);
                txtRecebimento.Text = "0,00";
                txtRecebimento.Focus();
            }
        }

        private void LiberarBotoesFecharCaixa()
        {
            try
            {
                lblFecharVenda.Enabled = true;
                lblCancelarVenda.Enabled = true;

                btnFecharVenda.Enabled = true;
                btnCancelarVenda.Enabled = true;
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método LiberarBotoesFecharCaixa.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void liberarbotoesfecharCaixa()
        {
            try
            {
                lblFecharVenda.Enabled = true;
                lblCancelarVenda.Enabled = true;
                btnFecharVenda.Enabled = true;
                btnCancelarVenda.Enabled = true;
                btnFecharVenda.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método liberarbotoesfecharCaixa.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void HabilitarCampos()
        {
            try
            {
                txtRecebimento.Enabled = true;
                txtTotal.Enabled = true;
                txtTroco.Enabled = true;
                txtRecebimento.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Erro no Método HabilitarCampos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnFecharVenda_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                // SalvarFechamentoVenda_();
                LimparCampos();
                this.Close();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCheque_Click(object sender, EventArgs e)
        {
            FecharVendaCartao();
        }

        public void FecharVendaCheque()
        {
            try
            {
                codTipoPagamento = "3";

                decimal somaRecebimento = 0;
                decimal valorVenda = 0;
                decimal somax = 0;

                novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                descTipoPagamento = dadosTabela.Rows[0]["TIPO_PAGTO"].ToString();

                if (txtRecebimento.Text == "0,00")
                {
                    HabilitarCampos();
                    txtTroco.Text = "0,00";
                    txtRecebimento.ReadOnly = true;
                    gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtTotal.Text);
                    LiberarBotoesFecharCaixa();
                    btnFecharVenda.Focus();
                    retornoVendaId = true;
                }

                if (Convert.ToDecimal(txtRecebimento.Text) > 0)
                {
                    gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtRecebimento.Text);

                    for (int i = 0; i < gdvTipoPgto.Rows.Count; i++)
                    {
                        somaRecebimento += Convert.ToDecimal(gdvTipoPgto.Rows[i].Cells[1].Value);
                    }

                    valorVenda = Convert.ToDecimal(txtTotal.Text);
                    SetFormatting();
                    txtTroco.Text = (somaRecebimento - valorVenda).ToString();
                    //txtRecebimento.Text = "";
                    txtRecebimento.Focus();

                    somax = Convert.ToDecimal(txtTroco.Text);

                    if (somax >= 0)
                    {
                        LiberarBotoesFecharCaixa();
                        btnFecharVenda.Focus();
                    }

                    somax = (somax * -1);
                    txtRecebimento.Text = somax.ToString();
                    SetFormatting();
                }

                //else
                //{
                //    HabilitarCampos();
                //    txtTroco.Text = "0,00";
                //    gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtRecebimento.Text);
                //    LiberarBotoesFecharCaixa();
                //    txtRecebimento.Text = "0,00";
                //    btnFecharVenda.Focus();
                //}

                SetFormatting();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CancelarVenda()
        {
            frmVenda.CancelaVenda();
            this.Close();
            frmVenda.Refresh();
            frmVenda.LimpaCampos();
        }

        //metodos das funcoes do S@T.......................................................................................................

        public void AbreCupomSat()
        {
            try
            {
                novoTemp = new RegraNegocio.TempRegraNegocios();
                DataTable dadosTabela_ = new DataTable();
                dadosTabela_ = novoTemp.PesquisarCpfTemp();

                cpfTemp = "";
                cpfTemp = dadosTabela_.Rows[0]["CPF_CNPJ"].ToString();
                cpfTemp = cpfTemp.Replace(" ", "");

                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_AbreCupomMFD(cpfTemp, "", "");
                // RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                //RegraNegocio.CupomFiscal.Analisa_RetornoImpressora();

                if (idRetorno != 1)
                {
                    MessageBox.Show("Erro ao Abrir Cupom. \nCupom foi cancelado.", "Informação Importanete.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    RegraNegocio.CupomFiscal.Bematech_FI_CancelaCupom();

                    this.Close();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro de Comunicação com Impressora.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void VendeItemCompletoSat()
        {
            try
            {
                int numCupom = frmVenda.numVenda;

                //parametros Impressoras..................................................................................
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaTabelaVenda(numCupom, frmVenda.numcaixa);

                for (int i = 0; i < dadosTabela.Rows.Count; i++)
                {
                    descri = dadosTabela.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                    item = dadosTabela.Rows[i]["ITEM"].ToString();
                    codigoB = dadosTabela.Rows[i]["COD_BARRA"].ToString();
                    qtde = Convert.ToDecimal(dadosTabela.Rows[i]["QUANT"]).ToString();
                    preco = Convert.ToDecimal(dadosTabela.Rows[i]["PRECO"]).ToString();
                    alicota = dadosTabela.Rows[i]["ALIQUOTA"].ToString();
                    total = Convert.ToDecimal(dadosTabela.Rows[i]["TOTAL"]);
                    _valorPis = dadosTabela.Rows[i]["VALOR_PIS"].ToString();
                    cstPis = _cfop = dadosTabela.Rows[i]["CST_PIS"].ToString();
                    valorCofins = dadosTabela.Rows[i]["VALOR_COFINS"].ToString();
                    _cstCofins = dadosTabela.Rows[i]["CST_COFINS"].ToString();
                    _cfop = dadosTabela.Rows[i]["CFOP"].ToString();
                    _ncm = dadosTabela.Rows[i]["NCM"].ToString();
                    _icmCst = dadosTabela.Rows[i]["ICM_CST"].ToString();
                    _origemProduto = dadosTabela.Rows[i]["ORIGEM_PRODUTO"].ToString();
                    cest = dadosTabela.Rows[i]["CEST"].ToString();

                    alicota = alicota.Replace(" ", "");
                    cstPis = cstPis.Replace(" ", "");
                    valorCofins = valorCofins.Replace(" ", "");
                    _cstCofins = _cstCofins.Replace(" ", "");
                    _cfop = _cfop.Replace(" ", "");
                    _ncm = _ncm.Replace(" ", "");
                    _icmCst = _icmCst.Replace(" ", "");
                    _origemProduto = _origemProduto.Replace(" ", "");
                    cest = cest.Replace(" ", "");

                    PesquisarCsosnProduto();

                    string Codigo = codigoB;
                    string EAN13 = "";
                    string Descricao = descri;
                    string IndiceDepartamento = "00";
                    string Aliquota = alicota;
                    string UnidadeMedida = "UN";
                    string TipoQuantidade = "D";
                    string CasasDecimaisQtde = "3";
                    decimal Quantidade = Convert.ToDecimal(qtde);
                    string CasasDecimaisValor = "3";
                    string ValorUnitario = preco.ToString();
                    string TipoDesconto = "$";
                    string ValorAcrescimo = "0,00";
                    string ValorDesconto = "0,00";
                    string ArredondaTrunca = "A";
                    string NCM = _ncm;
                    string CFOP = _cfop;
                    string InformacaoAdicional = "INFORMAÇÕES";
                    string OrigemProduto = _origemProduto;
                    string CST_ICMS = _icmCst;
                    string CodigoIBGE = "";
                    string CodigoISS = "";
                    string NaturezaOperacaoISS = "";
                    string IndicadorIncentivoFiscal = "";
                    string ItemListaServico = item;
                    string CSOSN = csosn; //--> Simples Nacional
                    string ValorBaseCalculoSimples = "0";
                    string ValorICMSRetidoSimples = "0";
                    string ModalidadeBaseCalculo = "0";
                    string PercentualReducaoBase = "0";
                    string ModalidadeBC = "0";
                    string PercentualMargemICMS = "0";
                    string PercentualBCICMS = "0";
                    string ValorReducaoBCICMS = "0";
                    string ValorAliquotaICMS = "0";
                    string ValorICMS = "0";
                    string ValorICMSDesonerado = "0";
                    string MotivoDesoneracaoICMS = "0";
                    string AliquotaCalculoCredito = "0";
                    string ValorCreditoICMS = "0";
                    string ValorTotalTributos = ""; //tributos
                    string CSTPIS = cstPis;
                    string BaseCalculoPIS = "";
                    string AliquotaPIS = "";
                    string ValorPIS = "";
                    string QuantVendidaPIS = "";
                    string ValorAliquotaPIS = "";
                    string CSTCOFINS = _cstCofins;
                    string BaseCalculoCOFINS = "";
                    string AliquotaCOFINS = "";
                    string ValorCOFINS = valorCofins;
                    string QunatVendidaCOFINS = "";
                    string ValorAliquotaCOFINS = "";
                    string CEST = cest;
                    string Reservado01 = "";
                    string Reservado02 = "";
                    string Reservado03 = "";
                    string Reservado04 = "";
                    string Reservado05 = "";
                    string Reservado06 = "";
                    string Reservado07 = "";
                    string Reservado08 = "";
                    string Reservado09 = "";

                    try
                    {
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_VendeItemCompleto(Codigo, EAN13, Descricao, IndiceDepartamento, Aliquota, UnidadeMedida, TipoQuantidade, CasasDecimaisQtde, Quantidade.ToString("N3"), CasasDecimaisValor, ValorUnitario, TipoDesconto, ValorAcrescimo, ValorDesconto, ArredondaTrunca, NCM, CFOP, InformacaoAdicional, CST_ICMS, OrigemProduto, ItemListaServico, CodigoISS, NaturezaOperacaoISS, IndicadorIncentivoFiscal, CodigoIBGE, CSOSN, ValorBaseCalculoSimples, ValorICMSRetidoSimples, ModalidadeBaseCalculo, PercentualReducaoBase, ModalidadeBC, PercentualMargemICMS, PercentualBCICMS, ValorReducaoBCICMS, ValorAliquotaICMS, ValorICMS, ValorICMSDesonerado, MotivoDesoneracaoICMS, AliquotaCalculoCredito, ValorCreditoICMS, ValorTotalTributos, CSTPIS, BaseCalculoPIS, AliquotaPIS, ValorPIS, QuantVendidaPIS, ValorAliquotaPIS, CSTCOFINS, BaseCalculoCOFINS, AliquotaCOFINS, ValorCOFINS, QunatVendidaCOFINS, ValorAliquotaCOFINS, CEST, Reservado01, Reservado02, Reservado03, Reservado04, Reservado05, Reservado06, Reservado07, Reservado08, Reservado09);
                        //  RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                        // RegraNegocio.CupomFiscal.Analisa_RetornoImpressora();
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        public void IniciaFechamentoSat()
        {
            try
            {
                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_IniciaFechamentoCupom("D", "%", "0");
                RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Iniciar FechamentoSat.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void PesquisarCsosnProduto()
        {
            try
            {
                novoCsosn = new RegraNegocio.CsosnRegraNegocio();
                DataTable dadosTabelaCsosn = new DataTable();
                dadosTabelaCsosn = novoCsosn.PesquisarCsosnProduto();

                string cst;

                if (dadosTabelaCsosn.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaCsosn.Rows.Count; i++)
                    {
                        cst = dadosTabelaCsosn.Rows[i]["CST"].ToString();
                        cst = cst.Replace(" ", "");

                        if (_icmCst == cst)
                        {
                            csosn = dadosTabelaCsosn.Rows[i]["CSOSN"].ToString();
                            csosn = csosn.Replace(" ", "");
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void FormaPgtoSat()
        {
            try
            {
                for (int i = 0; i < gdvTipoPgto.Rows.Count; i++)
                {
                    string valor = "";
                    int codigo = 0;


                    descTipoPagamento = gdvTipoPgto.Rows[i].Cells[0].Value.ToString();
                    valor = gdvTipoPgto.Rows[i].Cells[1].Value.ToString();

                    if (descTipoPagamento == "Aberto")
                    {
                        valorAberto = Convert.ToDecimal(valor);
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_EfetuaFormaPagamento(descTipoPagamento.ToString(), valorAberto.ToString());
                        RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                        RegraNegocio.CupomFiscal.Analisa_RetornoImpressora();

                    }
                    else
                    {
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_EfetuaFormaPagamento(descTipoPagamento.ToString(), valor);
                        RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                        //RegraNegocio.CupomFiscal.Analisa_RetornoImpressora();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método FormaPgtoFI.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void PesquisarAssinaturaDigital()
        {
            try
            {
                //CONSTROI O DATASET
                DataSet dsProduct = new DataSet();

                //PREENCHER DATASET 
                dsProduct.ReadXml(pathAssiDitital);

                //POPULAR DATASET NO GRIDVIEW
                gdvAssinatuDig.DataSource = dsProduct.Tables[0];

                if (gdvAssinatuDig.Rows.Count <= 0)
                {
                    MessageBox.Show("Não contém dados no Arquivo Leitura Digital.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PesquisarAssinaturaDigital.\nDeseja Cancela a Venda?.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void DadosSoftHouseSat()
        {
            try
            {
                int numCupom = frmVenda.numVenda;

                numeroAssinaturaDigital = gdvAssinatuDig.Rows[0].Cells[0].Value.ToString();
                numeroCnpjAssinatura = gdvAssinatuDig.Rows[0].Cells[1].Value.ToString();

                //string cnpjAssinatura = "16716114000172";
                //string assinaturaSW = "SGR-SAT SISTEMA DE GESTAO E RETAGUARDA DO SAT";

                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_DadosSoftwareHouseSAT(numeroCnpjAssinatura, numeroAssinaturaDigital);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método DadosSoftHouseSat.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void FechaCupomSat()
        {
            try
            {
                frmVenda.operadorAtuante = lblOperador.Text;
                //Aritimetica dos Atributos Aproximado................................................................
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabela = new DataTable();
                int numCupom = frmVenda.numVenda;
                novaPlaca = new RegraNegocio.PlacaRegraNegocio();

                DataTable dadosTabelaPlaca = new DataTable();
                dadosTabelaPlaca = novaPlaca.PesquisarPlaca(lblEntrada.Text);

                dadosTabela = novoParametro.PesquisaParametroE();
                decimal aliquota = 0;
                decimal somaAliquotaTotal = 0;

                string placa = "";
                string km = "";
                string numcaixa = "";

                aliquota = Convert.ToDecimal(dadosTabela.Rows[0]["ALIQUOTA_DIA"]);
                autorizarPlaca = Convert.ToBoolean(dadosTabela.Rows[0]["PLACA"].ToString());
                numcaixa = dadosTabela.Rows[0]["NUM_CAIXA"].ToString();

                if (autorizarPlaca == true)
                {
                    if (dadosTabelaPlaca.Rows.Count > 0)
                    {
                        placa = dadosTabelaPlaca.Rows[0]["PLACA"].ToString();
                        km = dadosTabelaPlaca.Rows[0]["KM"].ToString();
                    }
                    else
                    {
                        placa = "";
                        km = "";
                    }
                }
                else
                {
                    PLACA = "";
                    KM = "";
                }
                totalCupom = Convert.ToDecimal(txtTotal.Text);
                somaAliquotaTotal = ((totalCupom * aliquota) / 100);

                //valor dos atributos..................................................................................
                string mensagem = "OBRIGADO, VOLTE SEMPRE!!!. \t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\n" + PLACA + placa.ToString() + KM + "\t\t" + km.ToString() + "\t\t\n" +
                "Operador(a): " + lblOperador.Text + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\n" +
                "NºVenda: " + lblEntrada.Text + " \t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\n" +
                "Fonte IBPT - Valor Aprox. Tributos:" + somaAliquotaTotal.ToString("C") + "\n." +
                "Nº Caixa: " + numcaixa + ".";

                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_TerminaFechamentoCupom(mensagem);

                //FUNCAO PARA TRATAMENTO RETORNO DE IMPRESSORA........................................................
                //RegraNegocio.CupomFiscal.Analisa_RetornoImpressora();
                //RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);

                if (idRetorno == 1)
                {
                    SalvarFechamentoVenda_();
                    AlterarStatusFechar();
                    AlterarParametroNumFecharVenda();
                    LimparCampos();
                    this.Close();

                    //----------------------------------------------------------------------------------------------
                    //ExportarItemVenda();
                    //ExportarPagamentoVendas();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            //METODOS-------------------------------------------------------------------------------------------------
            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_AcionaGaveta();
            totalCupom = 0;
        }


        //metodos das funcoes do SIMP_FISCALT.......................................................................................................

        public void AbreCupomIF()
        {
            try
            {
                novoTemp = new RegraNegocio.TempRegraNegocios();
                DataTable dadosTabela_ = new DataTable();
                dadosTabela_ = novoTemp.PesquisarCpfTemp();

                cpfTemp = dadosTabela_.Rows[0]["CPF_CNPJ"].ToString();

                cpfTemp = cpfTemp.Replace(" ", "");

                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_AbreCupomMFD(cpfTemp, "", "");
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método AbreCupomIF.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        decimal totalCupom = 0;
        public void VendeItemIF()
        {
            try
            {
                int numCupom = frmVenda.numCupom;
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaTabelaVenda(numCupom, frmVenda.numcaixa);

                for (int i = 0; i < dadosTabela.Rows.Count; i++)
                {
                    descri = dadosTabela.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                    item = dadosTabela.Rows[i]["ITEM"].ToString();
                    codigoB = dadosTabela.Rows[i]["COD_BARRA"].ToString();
                    qtde = Convert.ToDecimal(dadosTabela.Rows[i]["QUANT"]).ToString();
                    preco = Convert.ToDecimal(dadosTabela.Rows[i]["PRECO"]).ToString();
                    alicota = dadosTabela.Rows[i]["ALIQUOTA"].ToString();
                    total = Convert.ToDecimal(dadosTabela.Rows[i]["TOTAL"]);
                    _valorPis = dadosTabela.Rows[i]["VALOR_PIS"].ToString();
                    cstPis = _cfop = dadosTabela.Rows[i]["CST_PIS"].ToString();
                    valorCofins = dadosTabela.Rows[i]["VALOR_COFINS"].ToString();
                    _cstCofins = dadosTabela.Rows[i]["CST_COFINS"].ToString();
                    _cfop = dadosTabela.Rows[i]["CFOP"].ToString();
                    _ncm = dadosTabela.Rows[i]["NCM"].ToString();
                    _icmCst = dadosTabela.Rows[i]["ICM_CST"].ToString();
                    _origemProduto = dadosTabela.Rows[i]["ORIGEM_PRODUTO"].ToString();

                    alicota = alicota.Replace(" ", "");
                    cstPis = cstPis.Replace(" ", "");
                    valorCofins = valorCofins.Replace(" ", "");
                    _cstCofins = _cstCofins.Replace(" ", "");
                    _cfop = _cfop.Replace(" ", "");
                    _ncm = _ncm.Replace(" ", "");
                    _icmCst = _icmCst.Replace(" ", "");
                    _origemProduto = _origemProduto.Replace(" ", "");

                    string tt = total.ToString();
                    tt = tt.Replace(".", ",");


                    string Codigo = codigoB;
                    string EAN13 = "";
                    string Descricao = descri;
                    string IndiceDepartamento = "00";
                    string Aliquota = alicota;
                    string UnidadeMedida = "UN";
                    string TipoQuantidade = "F";
                    string CasasDecimaisQtde = "2";
                    string Quantidade = qtde;
                    string CasasDecimaisValor = "2";
                    string ValorUnitario = preco.ToString();
                    string TipoDesconto = "$";
                    string ValorAcrescimo = "0,00";
                    string ValorDesconto = "0,00";
                    string ArredondaTrunca = "A";
                    string NCM = _ncm;
                    string CFOP = _cfop;
                    string InformacaoAdicional = "INFORMAÇÕES";
                    string OrigemProduto = _origemProduto;
                    string CST_ICMS = _icmCst;
                    string CodigoIBGE = "";
                    string CodigoISS = "";
                    string NaturezaOperacaoISS = "";
                    string IndicadorIncentivoFiscal = "";
                    string ItemListaServico = item;
                    string CSOSN = csosn; //--> Simples Nacional
                    string ValorBaseCalculoSimples = "0";
                    string ValorICMSRetidoSimples = "0";
                    string ModalidadeBaseCalculo = "0";
                    string PercentualReducaoBase = "0";
                    string ModalidadeBC = "0";
                    string PercentualMargemICMS = "0";
                    string PercentualBCICMS = "0";
                    string ValorReducaoBCICMS = "0";
                    string ValorAliquotaICMS = "0";
                    string ValorICMS = "0";
                    string ValorICMSDesonerado = "0";
                    string MotivoDesoneracaoICMS = "0";
                    string AliquotaCalculoCredito = "0";
                    string ValorCreditoICMS = "0";
                    string ValorTotalTributos = ""; //tributos
                    string CSTPIS = cstPis;
                    string BaseCalculoPIS = "";
                    string AliquotaPIS = "";
                    string ValorPIS = "";
                    string QuantVendidaPIS = "";
                    string ValorAliquotaPIS = "";
                    string CSTCOFINS = _cstCofins;
                    string BaseCalculoCOFINS = "";
                    string AliquotaCOFINS = "";
                    string ValorCOFINS = valorCofins;
                    string QunatVendidaCOFINS = "";
                    string ValorAliquotaCOFINS = "";
                    string Reservado01 = "";
                    string Reservado02 = "";
                    string Reservado03 = "";
                    string Reservado04 = "";
                    string Reservado05 = "";
                    string Reservado06 = "";
                    string Reservado07 = "";
                    string Reservado08 = "";
                    string Reservado09 = "";
                    string Reservado10 = "";


                    //totalCupom += (total);
                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_VendeItemCompleto(Codigo, EAN13, Descricao, IndiceDepartamento, Aliquota, UnidadeMedida, TipoQuantidade, CasasDecimaisQtde, Quantidade, CasasDecimaisValor, ValorUnitario, TipoDesconto, ValorAcrescimo, ValorDesconto, ArredondaTrunca, NCM, CFOP, InformacaoAdicional, CST_ICMS, OrigemProduto, ItemListaServico, CodigoISS, NaturezaOperacaoISS, IndicadorIncentivoFiscal, CodigoIBGE, CSOSN, ValorBaseCalculoSimples, ValorICMSRetidoSimples, ModalidadeBaseCalculo, PercentualReducaoBase, ModalidadeBC, PercentualMargemICMS, PercentualBCICMS, ValorReducaoBCICMS, ValorAliquotaICMS, ValorICMS, ValorICMSDesonerado, MotivoDesoneracaoICMS, AliquotaCalculoCredito, ValorCreditoICMS, ValorTotalTributos, CSTPIS, BaseCalculoPIS, AliquotaPIS, ValorPIS, QuantVendidaPIS, ValorAliquotaPIS, CSTCOFINS, BaseCalculoCOFINS, AliquotaCOFINS, ValorCOFINS, QunatVendidaCOFINS, ValorAliquotaCOFINS, Reservado01, Reservado02, Reservado03, Reservado04, Reservado05, Reservado06, Reservado07, Reservado08, Reservado09, Reservado10);
                    //RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);

                }
            }
            catch (Exception)
            {
                MessageBox.Show("Parametro Inválido.\nErro " + idRetorno + " ", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public void IniciaFecharCumpomIF()
        {
            try
            {
                // idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_IniciaFechamentoCupom("D", "%", "0");
                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_IniciaFechamentoCupomMFD("D", "%", "0", "0");
                RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método IniciaFecharCumpomIF.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void FormaPgtoFI()
        {
            try
            {
                for (int i = 0; i < gdvTipoPgto.Rows.Count; i++)
                {
                    string valor = "";
                    int codigo = 0;

                    descTipoPagamento = gdvTipoPgto.Rows[i].Cells[0].Value.ToString();
                    valor = gdvTipoPgto.Rows[i].Cells[1].Value.ToString();
                    decimal v = Convert.ToDecimal(valor);
                    valor = v.ToString("N2");

                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_EfetuaFormaPagamento(descTipoPagamento.ToString(), valor);
                    RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método FormaPgtoFI.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void FechaCumpomIF()
        {
            try
            {
                frmVenda.operadorAtuante = lblOperador.Text;
                //Aritimetica dos Atributos Aproximado................................................................
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabela = new DataTable();
                int numCupom = frmVenda.numVenda;
                novaPlaca = new RegraNegocio.PlacaRegraNegocio();

                DataTable dadosTabelaPlaca = new DataTable();
                dadosTabelaPlaca = novaPlaca.PesquisarPlaca(lblEntrada.Text);

                dadosTabela = novoParametro.PesquisaParametroE();
                decimal aliquota = 0;
                decimal somaAliquotaTotal = 0;

                string placa = "";
                string km = "";

                aliquota = Convert.ToDecimal(dadosTabela.Rows[0]["ALIQUOTA_DIA"]);
                autorizarPlaca = Convert.ToBoolean(dadosTabela.Rows[0]["PLACA"].ToString());

                if (autorizarPlaca == true)
                {
                    if (dadosTabelaPlaca.Rows.Count > 0)
                    {
                        placa = dadosTabelaPlaca.Rows[0]["PLACA"].ToString();
                        km = dadosTabelaPlaca.Rows[0]["KM"].ToString();
                    }
                    else
                    {
                        placa = "";
                        km = "";
                    }
                }
                else
                {
                    PLACA = "";
                    KM = "";
                }

                totalCupom = Convert.ToDecimal(txtTotal.Text);
                somaAliquotaTotal = ((totalCupom * aliquota) / 100);

                //valor dos atributos..................................................................................
                string mensagem = "OBRIGADO, VOLTE SEMPRE!!!. \t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\n" + PLACA + placa.ToString() + KM + "\t\t" + km.ToString() + "\t\t\n" +
                "Operador(a): " + lblOperador.Text + "\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\n" +
                "Nº CAIXA: " + frmVenda.numcaixa + " - " +
                "NºVenda: " + lblEntrada.Text + " \t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\t\n" +
                "Fonte IBPT - Valor Aprox. Tributos:" + somaAliquotaTotal.ToString("C") + ".";

                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_TerminaFechamentoCupom(mensagem);
                RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);

                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_AcionaGaveta();

                SalvarFechamentoVenda_();
                AlterarStatusFechar();
                // AlterarParametroNumFecharVenda();
                frmVenda.AlterarNumVendaNumCaixa();
                frmVenda.AtualizarGridAberto();
                LimparCampos();


            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método FechaCumpomIF.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            totalCupom = 0;
        }

        public static byte[] FromHex(string hex)
        {
            byte[] data = FromHex("47-61-74-65-77-61-79-53-65-72-76-65-72");
            string s = Encoding.ASCII.GetString(data); // GatewayServer

            hex = hex.Replace("-", "");
            byte[] raw = new byte[hex.Length / 2];
            for (int i = 0; i < raw.Length; i++)
            {
                raw[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            }
            return raw;
        }

        //metodo das funcoes BemaSat..............................................................................................................

        public void CriarArquivo()
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(pathVendaCancelada + _numVenda + ".xml", null);

                //inicia o documento xml
                writer.WriteStartDocument();
                //escreve o elmento raiz
                writer.WriteStartElement("Teste");
                writer.WriteEndElement();
                ////Escreve o XML para o arquivo e fecha o objeto escritor
                //writer.Close();
                //MessageBox.Show("Arquivo XML gerado com sucesso.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void IniciarXML()
        {
            try
            {
                arquivoXML = new XmlDocument();
                nodeCFe = arquivoXML.CreateElement("CFe");
                arquivoXML.AppendChild(nodeCFe);
                XmlProcessingInstruction encoding = arquivoXML.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                arquivoXML.InsertBefore(encoding, nodeCFe);

                nodeInfCFe = arquivoXML.CreateElement("infCFe");
                XmlAttribute attr = arquivoXML.CreateAttribute("versaoDadosEnt");
                attr.Value = "0.07";

                nodeInfCFe.SetAttributeNode(attr);
                nodeCFe.AppendChild(nodeInfCFe);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PreencherIDE()
        {
            try
            {
                //dados Software House......................................................................

                numeroAssinaturaDigital = gdvAssinatuDig.Rows[0].Cells[0].Value.ToString();
                numeroCnpjAssinatura = gdvAssinatuDig.Rows[0].Cells[1].Value.ToString();

                nodeIDE = arquivoXML.CreateElement("ide");

                XmlElement cnpj = arquivoXML.CreateElement("CNPJ");
                nodeIDE.AppendChild(cnpj);
                cnpj.InnerText = numeroCnpjAssinatura;

                XmlElement signAC = arquivoXML.CreateElement("signAC");
                nodeIDE.AppendChild(signAC);
                signAC.InnerText = numeroAssinaturaDigital;

                XmlElement numeroCaixa = arquivoXML.CreateElement("numeroCaixa");
                nodeIDE.AppendChild(numeroCaixa);
                numeroCaixa.InnerText = "00" + frmVenda.numcaixa.ToString();

                nodeInfCFe.AppendChild(nodeIDE);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PreencherEmitente()
        {
            try
            {
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabelaParametro = new DataTable();
                dadosTabelaParametro = novoParametro.PesquisaParametroE();

                if (dadosTabelaParametro.Rows.Count > 0)
                {
                    string cnpjEmitente = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                    string ieEmitente = dadosTabelaParametro.Rows[0]["IE"].ToString();
                    string imMunicipal = dadosTabelaParametro.Rows[0]["IM"].ToString();

                    nodeEmitente = arquivoXML.CreateElement("emit");

                    XmlElement CNPJ = arquivoXML.CreateElement("CNPJ");
                    nodeEmitente.AppendChild(CNPJ);

                    string cpfCnpj = lblCnpjCpf.Text;

                    cnpjEmitente = cnpjEmitente.Replace("/", "");
                    cnpjEmitente = cnpjEmitente.Replace("-", "");
                    cnpjEmitente = cnpjEmitente.Replace(" ", "");
                    CNPJ.InnerText = cnpjEmitente;

                    XmlElement IE = arquivoXML.CreateElement("IE");
                    nodeEmitente.AppendChild(IE);
                    ieEmitente = ieEmitente.Replace(" ", "");
                    IE.InnerText = ieEmitente;

                    XmlElement IM = arquivoXML.CreateElement("IM");
                    nodeEmitente.AppendChild(IM);
                    imMunicipal = imMunicipal.Replace(" ", "");
                    IM.InnerText = imMunicipal;

                    XmlElement indRatISSQN = arquivoXML.CreateElement("indRatISSQN");
                    nodeEmitente.AppendChild(indRatISSQN);
                    indRatISSQN.InnerText = "N";

                    nodeInfCFe.AppendChild(nodeEmitente);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarTipo()
        {
            try
            {
                novoTemp = new TempRegraNegocios();
                DataTable dadosTabelaTemp = new DataTable();

                dadosTabelaTemp = novoTemp.PesquisarCpfTemp();

                if (dadosTabelaTemp.Rows.Count > 0)
                {
                    tipo = Convert.ToInt32(dadosTabelaTemp.Rows[0]["TIPO"].ToString());
                    nomeCliente = dadosTabelaTemp.Rows[0]["NOME"].ToString();
                    cpf_cnpj = dadosTabelaTemp.Rows[0]["CPF_CNPJ"].ToString();

                    if (tipo == 1)
                    {
                        lblCpfCnpj.Text = "CPF: ";
                    }
                    else if (tipo == 2)
                    {
                        lblCpfCnpj.Text = "CNPJ: ";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void preenchedestinario()
        {
            if (tipo == 1)
            {
                if ((nomeCliente != "") && cpf_cnpj != "")
                {
                    nodeDestinatario = arquivoXML.CreateElement("dest");

                    XmlElement CNPJ = arquivoXML.CreateElement("CPF");
                    nodeDestinatario.AppendChild(CNPJ);

                    nomeCliente = nomeCliente.Replace(" ", "");
                    CNPJ.InnerText = cpf_cnpj;

                    XmlElement xNome = arquivoXML.CreateElement("xNome");
                    nodeDestinatario.AppendChild(xNome);
                    xNome.InnerText = nomeCliente;

                    nodeInfCFe.AppendChild(nodeDestinatario);
                }
                else if ((nomeCliente == "") && (cpf_cnpj == ""))
                {
                    nodeDestinatario = arquivoXML.CreateElement("dest");

                    //XmlElement CNPJ = arquivoXML.CreateElement("CPF");
                    //nodeDestinatario.AppendChild(CNPJ);
                    //string cpfDestinatario = lblCnpjCpf.Text;
                    //cpfDestinatario = cpfDestinatario.Replace(" ", "");
                    //CNPJ.InnerText = "22318972885";

                    //XmlElement xNome = arquivoXML.CreateElement("xNome");
                    //nodeDestinatario.AppendChild(xNome);
                    //xNome.InnerText = "CLAUDEMIR";

                    nodeInfCFe.AppendChild(nodeDestinatario);
                }
                else if ((nomeCliente == "") && (cpf_cnpj != ""))
                {
                    nodeDestinatario = arquivoXML.CreateElement("dest");

                    XmlElement CNPJ = arquivoXML.CreateElement("CPF");
                    nodeDestinatario.AppendChild(CNPJ);
                    string cpfDestinatario = cpf_cnpj;
                    cpf_cnpj = cpf_cnpj.Replace(" ", "");
                    CNPJ.InnerText = cpf_cnpj;

                    //XmlElement xNome = arquivoXML.CreateElement("xNome");
                    //nodeDestinatario.AppendChild(xNome);
                    //xNome.InnerText = "CLAUDEMIR";

                    nodeInfCFe.AppendChild(nodeDestinatario);
                }
            }
            else if (tipo == 2)
            {
                if ((nomeCliente != "") && cpf_cnpj != "")
                {
                    nodeDestinatario = arquivoXML.CreateElement("dest");

                    XmlElement CNPJ = arquivoXML.CreateElement("CNPJ");
                    nodeDestinatario.AppendChild(CNPJ);

                    cpf_cnpj = cpf_cnpj.Replace(" ", "");
                    CNPJ.InnerText = cpf_cnpj;

                    XmlElement xNome = arquivoXML.CreateElement("xNome");
                    nodeDestinatario.AppendChild(xNome);
                    xNome.InnerText = nomeCliente;

                    nodeInfCFe.AppendChild(nodeDestinatario);
                }
                else if ((nomeCliente == "") && (cpf_cnpj == ""))
                {
                    nodeDestinatario = arquivoXML.CreateElement("dest");

                    //XmlElement CNPJ = arquivoXML.CreateElement("CPF");
                    //nodeDestinatario.AppendChild(CNPJ);
                    //string cpfDestinatario = lblCnpjCpf.Text;
                    //cpfDestinatario = cpfDestinatario.Replace(" ", "");
                    //CNPJ.InnerText = "22318972885";

                    //XmlElement xNome = arquivoXML.CreateElement("xNome");
                    //nodeDestinatario.AppendChild(xNome);
                    //xNome.InnerText = "CLAUDEMIR";

                    nodeInfCFe.AppendChild(nodeDestinatario);
                }
                else if ((nomeCliente == "") && (cpf_cnpj != ""))
                {
                    nodeDestinatario = arquivoXML.CreateElement("dest");

                    XmlElement CNPJ = arquivoXML.CreateElement("CNPJ");
                    nodeDestinatario.AppendChild(CNPJ);

                    cpf_cnpj = cpf_cnpj.Replace(" ", "");
                    CNPJ.InnerText = cpf_cnpj;

                    //XmlElement xNome = arquivoXML.CreateElement("xNome");
                    //nodeDestinatario.AppendChild(xNome);
                    //xNome.InnerText = "CLAUDEMIR";

                    nodeInfCFe.AppendChild(nodeDestinatario);
                }
            }
        }

        private void preencheProduto()
        {
            novaVenda = new RegraNegocio.VendaRegraNegocios();
            DataTable dadosTabelaVenda = new DataTable();
            dadosTabelaVenda = novaVenda.ListarEntrada(_numVenda, false, frmVenda.numcaixa);

            string idRegra = "A";
            string trib = "";

            for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
            {
                int item = Convert.ToInt32(dadosTabelaVenda.Rows[i]["ITEM"].ToString());
                int idProduto = Convert.ToInt32(dadosTabelaVenda.Rows[i]["ID"].ToString());
                string produto = dadosTabelaVenda.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                string cfopProd = dadosTabelaVenda.Rows[i]["CFOP"].ToString();
                string unid = dadosTabelaVenda.Rows[i]["UNIDADE"].ToString();
                decimal qtdeVenda = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["QUANT"].ToString());
                string precoUnitario = dadosTabelaVenda.Rows[i]["PRECO"].ToString();
                string total = dadosTabelaVenda.Rows[i]["TOTAL"].ToString();
                string codigoBarra = dadosTabelaVenda.Rows[i]["COD_BARRA"].ToString();
                string origemProd = dadosTabelaVenda.Rows[i]["ORIGEM_PRODUTO"].ToString();
                string cestProd = dadosTabelaVenda.Rows[i]["CEST"].ToString();
                string ncmProd = dadosTabelaVenda.Rows[i]["NCM"].ToString();
                cstProduto = dadosTabelaVenda.Rows[i]["ICMS_CST"].ToString();
                aliquota = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                string cstPis = dadosTabelaVenda.Rows[i]["CST_PIS"].ToString();
                string cest = dadosTabelaVenda.Rows[i]["CEST"].ToString();
                decimal desconto = 0;
                decimal outros = 0;

                //limpar e padroniza variavel

                unid = unid.Replace(" ", "");
                cestProd = cestProd.Replace(" ", "");
                cstProduto = cstProduto.Replace(" ", "");
                origemProd = origemProd.Replace(" ", "");
                cstPis = cstPis.Replace(" ", "");
                total = total.Replace(" ", "");
                total = total.Replace(",", ".");

                aliquota = aliquota.Replace(" ", "");
                aliquota = aliquota.Replace(",", ".");

                if (unid == "LT")
                {
                    idRegra = "T";
                }

                XmlElement nodeDET = arquivoXML.CreateElement("det");
                XmlAttribute attr = arquivoXML.CreateAttribute("nItem");
                attr.Value = item.ToString();
                nodeDET.SetAttributeNode(attr);
                nodeInfCFe.AppendChild(nodeDET);

                XmlElement nodeProduto = arquivoXML.CreateElement("prod");

                XmlElement cProd = arquivoXML.CreateElement("cProd");
                nodeProduto.AppendChild(cProd);
                codigoBarra = codigoBarra.Replace(" ", "");
                cProd.InnerText = codigoBarra.ToString();

                XmlElement xProd = arquivoXML.CreateElement("xProd");
                nodeProduto.AppendChild(xProd);
                produto = produto.Replace(" ", "");
                xProd.InnerText = produto.ToString();

                XmlElement ncm = arquivoXML.CreateElement("NCM");
                nodeProduto.AppendChild(ncm);
                ncmProd = ncmProd.Replace(" ", "");
                ncm.InnerText = ncmProd;

                XmlElement cfop = arquivoXML.CreateElement("CFOP");
                nodeProduto.AppendChild(cfop);
                cfopProd = cfopProd.Replace(" ", "");
                cfop.InnerText = cfopProd;

                XmlElement uCom = arquivoXML.CreateElement("uCom");
                nodeProduto.AppendChild(uCom);
                unid = unid.Replace(" ", "");
                uCom.InnerText = unid;

                XmlElement qCom = arquivoXML.CreateElement("qCom");
                nodeProduto.AppendChild(qCom);
                //double quantidade = 3;
                qCom.InnerText = qtdeVenda.ToString("0.0000", new CultureInfo("en-US"));

                XmlElement vUnCom = arquivoXML.CreateElement("vUnCom");
                nodeProduto.AppendChild(vUnCom);

                double valor = Convert.ToDouble(precoUnitario);
                vUnCom.InnerText = valor.ToString("0.000", new CultureInfo("en-US"));

                XmlElement indRegra = arquivoXML.CreateElement("indRegra");
                nodeProduto.AppendChild(indRegra);
                indRegra.InnerText = idRegra;

                XmlElement obscest = arquivoXML.CreateElement("obsFiscoDet");
                obscest.SetAttribute("xCampoDet", "Cod.CEST");

                XmlElement infObsCest = arquivoXML.CreateElement("xTextoDet");
                cest = cest.Replace(" ", "");
                infObsCest.InnerText = (cest);
                obscest.AppendChild(infObsCest);

                nodeProduto.AppendChild(obscest);
                nodeDET.AppendChild(nodeProduto);

                {
                    PesquiarCrt();
                    PesquisarCSOSN();

                    crt = crt.Replace(" ", "");

                    if (crt == "1")
                    {
                        #region ICMS CRT 1

                        //node imposto
                        XmlElement imposto = arquivoXML.CreateElement("imposto");
                        nodeDET.AppendChild(imposto);

                        //Valor aproximanto dos impostos
                        double valorAprox = 0.00;

                        XmlElement vItem12741 = arquivoXML.CreateElement("vItem12741");

                        vItem12741.InnerText = valorAprox.ToString("0.00", new CultureInfo("en-US"));
                        imposto.AppendChild(vItem12741);


                        //ICMS
                        XmlElement icms = arquivoXML.CreateElement("ICMS");

                        string tipoICMS = cstProduto;             //-> define  DR["tipoICMS"].toString();
                        string descICMS = "ICMS";        //-> default to 102
                        string descICMS40 = "ICMS40";
                        string desCST = "CST";


                        //condicao para compara cst do produto....................................................
                        #region ICMS40_102
                        if ((cstProduto == "40") || (cstProduto == "41") || (cstProduto == "50") || (cstProduto == "60"))
                        {
                            //Regime tributario Simples Nacional
                            if (crt == "1")
                            {
                                if ((tipoCSOSN == "102") || (tipoCSOSN == "300") || (tipoCSOSN == "500")) descICMS40 = "ICMSSN102";

                                if (tipoCSOSN == "900") descICMS40 = "ICMSSN900";
                                // ... more types of tipoICMS

                                cstProduto = tipoCSOSN;
                                desCST = "CSOSN";
                            }

                            XmlElement nodetipoICMS = arquivoXML.CreateElement(descICMS);

                            XmlElement nodeIcms40 = arquivoXML.CreateElement(descICMS40);
                            nodetipoICMS.AppendChild(nodeIcms40);

                            XmlElement origem = arquivoXML.CreateElement("Orig");

                            origemProd = origemProd.Replace(" ", "");
                            origem.InnerText = origemProd.ToString();

                            XmlElement cst = arquivoXML.CreateElement(desCST);

                            //ICMS
                            cst.InnerText = cstProduto;
                            nodeIcms40.AppendChild(origem);
                            nodeIcms40.AppendChild(cst);

                            imposto.AppendChild(nodetipoICMS);

                        }

                        //Condicao no tratamento de cst.....................................................................

                        //tratamento para gerar tag Aliquota...............................................................

                        #region ICMS00_900
                        else if ((cstProduto == "00") || (cstProduto == "20") || (cstProduto == "90"))
                        {
                            descICMS40 = "ICMS00";
                            desCST = "CSOSN";

                            if ((tipoCSOSN == "102") || (tipoCSOSN == "300") || (tipoCSOSN == "500")) descICMS40 = "ICMSSN102";

                            if (tipoCSOSN == "900") descICMS40 = "ICMSSN900";
                            // ... more types of tipoICMS

                            cstProduto = tipoCSOSN;

                            XmlElement nodetipoICMS = arquivoXML.CreateElement(descICMS);

                            XmlElement nodeIcms00 = arquivoXML.CreateElement(descICMS40);
                            nodetipoICMS.AppendChild(nodeIcms00);

                            XmlElement origem = arquivoXML.CreateElement("Orig");
                            origemProd = origemProd.Replace(" ", "");
                            origem.InnerText = origemProd.ToString();

                            XmlElement cst = arquivoXML.CreateElement(desCST);
                            cst.InnerText = cstProduto;

                            //XmlElement pIcms = arquivoXML.CreateElement("pICMS");
                            //aliquota = aliquota.Replace(" ", "");
                            //pIcms.InnerText = aliquota;


                            nodeIcms00.AppendChild(origem);
                            nodeIcms00.AppendChild(cst);
                            //nodeIcms00.AppendChild(pIcms);

                            imposto.AppendChild(nodetipoICMS);
                        }

                        //tratamento para gerar tag Aliquota.. 
                        #endregion.............................................................

                        #endregion

                        //PIS

                        if ((cstPis == "02") || (cstPis == "01"))
                        {
                            XmlElement pis = arquivoXML.CreateElement("PIS");
                            XmlElement nodeTipoPis = arquivoXML.CreateElement("PISAliq");

                            XmlElement cstPIS = arquivoXML.CreateElement("CST");
                            cstPIS.InnerText = cstPis;
                            nodeTipoPis.AppendChild(cstPIS);

                            //gera aliquota
                            XmlElement vBC = arquivoXML.CreateElement("vBC");
                            nodeTipoPis.AppendChild(vBC);

                            //aritimetica para somar BC-PIS

                            valor = Convert.ToDouble(precoUnitario);
                            double pppis_ = (valor * Convert.ToDouble(qtdeVenda));
                            vBC.InnerText = pppis_.ToString("0.00", new CultureInfo("en-US"));

                            //busca tabela pPis

                            novoPis = new CST_PIS_RegraNegocios();
                            DataTable dadosTabelaPis = new DataTable();
                            dadosTabelaPis = novoPis.PesquisarAliquotaPis(cstPis);

                            string ppis = dadosTabelaPis.Rows[0]["ALIQ"].ToString();
                            decimal vAliquota = (Convert.ToDecimal(ppis) * Convert.ToDecimal(pppis_));

                            //tag Ppis
                            XmlElement pPIS = arquivoXML.CreateElement("pPIS");
                            nodeTipoPis.AppendChild(pPIS);
                            ppis = ppis.Replace(",", ".");
                            pPIS.InnerText = ppis.ToString();

                            pis.AppendChild(nodeTipoPis);
                            imposto.AppendChild(pis);

                            //PISST
                            XmlElement pisSt = arquivoXML.CreateElement("PISST");

                            XmlElement vBCp = arquivoXML.CreateElement("vBC");
                            string pbc = precoUnitario.ToString();
                            valor = Convert.ToDouble(precoUnitario);
                            vBCp.InnerText = valor.ToString("0.00", new CultureInfo("en-US"));
                            pisSt.AppendChild(vBCp);

                            XmlElement pPISSt = arquivoXML.CreateElement("pPIS");
                            pisSt.AppendChild(pPISSt);
                            pPISSt.InnerText = ppis.ToString();

                            imposto.AppendChild(pisSt);

                            //-----------------------------------------------------------------------------------------------------

                            //COFINS
                            XmlElement cofins = arquivoXML.CreateElement("COFINS");
                            XmlElement nodeTipoCOFINS = arquivoXML.CreateElement("COFINSAliq");

                            XmlElement cstCOFINS = arquivoXML.CreateElement("CST");
                            cstCOFINS.InnerText = cstPis;
                            nodeTipoCOFINS.AppendChild(cstCOFINS);

                            XmlElement avBC = arquivoXML.CreateElement("vBC");
                            nodeTipoCOFINS.AppendChild(avBC);
                            avBC.InnerText = valor.ToString("0.00", new CultureInfo("en-US"));

                            XmlElement ppPIS = arquivoXML.CreateElement("pCOFINS");
                            nodeTipoCOFINS.AppendChild(ppPIS);
                            ppPIS.InnerText = ppis.ToString();

                            cofins.AppendChild(nodeTipoCOFINS);
                            imposto.AppendChild(cofins);
                        }
                        else if (cstPis == "04")
                        {
                            //tratamento para  nao gerar tag Aliquota..................................................

                            XmlElement pis = arquivoXML.CreateElement("PIS");
                            XmlElement nodeTipoPis = arquivoXML.CreateElement("PISNT");


                            XmlElement cstPIS = arquivoXML.CreateElement("CST");
                            cstPIS.InnerText = cstPis;
                            nodeTipoPis.AppendChild(cstPIS);

                            pis.AppendChild(nodeTipoPis);
                            imposto.AppendChild(pis);


                            imposto.AppendChild(pis);


                            //-----------------------------------------------------------------------------------------------------

                            //COFINS
                            XmlElement cofins = arquivoXML.CreateElement("COFINS");
                            XmlElement nodeTipoCOFINS = arquivoXML.CreateElement("COFINSNT");

                            XmlElement cstCOFINS = arquivoXML.CreateElement("CST");
                            cstCOFINS.InnerText = cstPis;
                            nodeTipoCOFINS.AppendChild(cstCOFINS);


                            cofins.AppendChild(nodeTipoCOFINS);
                            imposto.AppendChild(cofins);

                        }
                        #endregion
                    }

                    else
                    {
                        #region ICMS CRT 3

                        double _vItem = 0.00;
                        //node imposto
                        XmlElement imposto = arquivoXML.CreateElement("imposto");
                        nodeDET.AppendChild(imposto);

                        //Valor aproximanto dos impostos
                        double valorAprox = 0.00;

                        XmlElement vItem12741 = arquivoXML.CreateElement("vItem12741");

                        if ((aliquota == "II") || (aliquota == "FF") || (aliquota == "NN"))
                        {
                            vItem12741.InnerText = valorAprox.ToString("0.00", new CultureInfo("en-US"));
                            imposto.AppendChild(vItem12741);
                        }
                        else
                        {
                            double alic = Convert.ToDouble(aliquota);
                            double vp = Convert.ToDouble(precoUnitario);

                            _vItem = Convert.ToDouble((alic * vp) / 100);

                            vItem12741.InnerText = _vItem.ToString("0.00", new CultureInfo("en-US"));
                            imposto.AppendChild(vItem12741);
                        }

                        //ICMS
                        XmlElement icms = arquivoXML.CreateElement("ICMS");

                        string tipoICMS = cstProduto;             //-> define  DR["tipoICMS"].toString();
                        string descICMS = "ICMS";        //-> default to 102
                        string descICMS40 = "ICMS40";
                        string desCST = "CST";


                        //condicao para compara cst do produto....................................................
                        if ((cstProduto == "40") || (cstProduto == "41") || (cstProduto == "50") || (cstProduto == "60"))
                        {
                            //Regime tributario Simples Nacional
                            if (crt != "3")
                            {
                                if ((tipoCSOSN == "102") || (tipoCSOSN == "300") || (tipoCSOSN == "500")) descICMS40 = "ICMSSN102";

                                if (tipoCSOSN == "900") descICMS40 = "ICMSSN900";
                                // ... more types of tipoICMS

                                cstProduto = tipoCSOSN;
                            }

                            XmlElement nodetipoICMS = arquivoXML.CreateElement(descICMS);

                            XmlElement nodeIcms40 = arquivoXML.CreateElement(descICMS40);
                            nodetipoICMS.AppendChild(nodeIcms40);

                            XmlElement origem = arquivoXML.CreateElement("Orig");

                            origemProd = origemProd.Replace(" ", "");
                            origem.InnerText = origemProd.ToString();

                            XmlElement cst = arquivoXML.CreateElement(desCST);

                            //ICMS
                            cst.InnerText = cstProduto;
                            nodeIcms40.AppendChild(origem);
                            nodeIcms40.AppendChild(cst);

                            imposto.AppendChild(nodetipoICMS);

                        }

                        else if ((cstProduto == "00") || (cstProduto == "20") || (cstProduto == "90"))
                        {
                            descICMS40 = "ICMS00";

                            if ((tipoCSOSN == "102") || (tipoCSOSN == "300") || (tipoCSOSN == "500")) descICMS40 = "ICMS00";

                            if (tipoCSOSN == "900") descICMS40 = "ICMS00";

                            XmlElement nodetipoICMS = arquivoXML.CreateElement(descICMS);

                            XmlElement nodeIcms00 = arquivoXML.CreateElement(descICMS40);
                            nodetipoICMS.AppendChild(nodeIcms00);

                            XmlElement origem = arquivoXML.CreateElement("Orig");
                            origemProd = origemProd.Replace(" ", "");
                            origem.InnerText = origemProd.ToString();

                            XmlElement cst = arquivoXML.CreateElement(desCST);
                            cst.InnerText = cstProduto;

                            XmlElement pIcms = arquivoXML.CreateElement("pICMS");
                            aliquota = aliquota.Replace(" ", "");
                            string a = aliquota.ToString();
                            a = (a + ".00");
                            pIcms.InnerText = a;

                            nodeIcms00.AppendChild(origem);
                            nodeIcms00.AppendChild(cst);
                            nodeIcms00.AppendChild(pIcms);

                            imposto.AppendChild(nodetipoICMS);
                        }

                        //tratamento para gerar tag Aliquota...............................................................

                        else if ((cstProduto == "00") || (cstProduto == "20") || (cstProduto == "90"))
                        {
                            XmlElement nodetipoICMS = arquivoXML.CreateElement(descICMS);

                            XmlElement nodeIcms40 = arquivoXML.CreateElement("ICMS00");
                            nodetipoICMS.AppendChild(nodeIcms40);

                            XmlElement origem = arquivoXML.CreateElement("Orig");

                            origemProd = origemProd.Replace(" ", "");
                            origem.InnerText = origemProd.ToString();

                            if (tipoICMS == "102") descICMS = "ICMSSN102";

                            if (tipoICMS == "500") descICMS = "ICMSSN500";
                            // ... more types of tipoICMS

                            XmlElement cst = arquivoXML.CreateElement("CST");

                            //ICMS
                            cst.InnerText = tipoICMS;
                            nodeIcms40.AppendChild(origem);
                            nodeIcms40.AppendChild(cst);

                            imposto.AppendChild(nodetipoICMS);
                        }

                        //PIS

                        if ((cstPis == "02") || (cstPis == "01") || (cstPis == "05"))
                        {
                            XmlElement pis = arquivoXML.CreateElement("PIS");
                            XmlElement nodeTipoPis = arquivoXML.CreateElement("PISAliq");

                            XmlElement cstPIS = arquivoXML.CreateElement("CST");
                            cstPIS.InnerText = cstPis;
                            nodeTipoPis.AppendChild(cstPIS);

                            //gera aliquota
                            XmlElement vBC = arquivoXML.CreateElement("vBC");
                            nodeTipoPis.AppendChild(vBC);

                            //aritimetica para somar BC-PIS

                            valor = Convert.ToDouble(precoUnitario);
                            double pppis_ = (valor * Convert.ToDouble(qtdeVenda));
                            vBC.InnerText = pppis_.ToString("0.00", new CultureInfo("en-US"));

                            //busca tabela pPis

                            novoPis = new CST_PIS_RegraNegocios();
                            DataTable dadosTabelaPis = new DataTable();
                            dadosTabelaPis = novoPis.PesquisarAliquotaPis(cstPis);

                            if (dadosTabelaPis.Rows.Count > 0)
                            {
                                string ppis = dadosTabelaPis.Rows[0]["ALIQ"].ToString();
                                decimal vAliquota = (Convert.ToDecimal(ppis) * Convert.ToDecimal(pppis_));

                                ppis = ppis.Trim();

                                //tag Ppis
                                XmlElement pPIS = arquivoXML.CreateElement("pPIS");
                                nodeTipoPis.AppendChild(pPIS);
                                ppis = ppis.Replace(",", ".");
                                pPIS.InnerText = ppis.ToString();

                                pis.AppendChild(nodeTipoPis);
                                imposto.AppendChild(pis);

                                //PISST
                                XmlElement pisSt = arquivoXML.CreateElement("PISST");

                                XmlElement vBCp = arquivoXML.CreateElement("vBC");
                                string pbc = precoUnitario.ToString();
                                valor = Convert.ToDouble(precoUnitario);
                                vBCp.InnerText = valor.ToString("0.00", new CultureInfo("en-US"));
                                pisSt.AppendChild(vBCp);

                                XmlElement pPISSt = arquivoXML.CreateElement("pPIS");
                                pisSt.AppendChild(pPISSt);
                                pPISSt.InnerText = ppis.ToString();

                                imposto.AppendChild(pisSt);

                                //-----------------------------------------------------------------------------------------------------

                                novoCofins = new COFINS_RN();
                                DataTable dadosTabelaCofins = new DataTable();
                                dadosTabelaCofins = novoCofins.PesquisarCofins(cstPis);

                                if (dadosTabelaCofins.Rows.Count > 0)
                                {
                                    string pcofins = dadosTabelaCofins.Rows[0]["ALIQ"].ToString();
                                    decimal vAliquota_ = (Convert.ToDecimal(ppis) * Convert.ToDecimal(pppis_));

                                    pcofins = pcofins.Trim();
                                    pcofins = pcofins.Replace(",", ".");

                                    //COFINS
                                    XmlElement cofins = arquivoXML.CreateElement("COFINS");
                                    XmlElement nodeTipoCOFINS = arquivoXML.CreateElement("COFINSAliq");

                                    XmlElement cstCOFINS = arquivoXML.CreateElement("CST");
                                    cstCOFINS.InnerText = cstPis;
                                    nodeTipoCOFINS.AppendChild(cstCOFINS);

                                    XmlElement avBC = arquivoXML.CreateElement("vBC");
                                    nodeTipoCOFINS.AppendChild(avBC);
                                    avBC.InnerText = valor.ToString("0.00", new CultureInfo("en-US"));

                                    XmlElement ppPIS = arquivoXML.CreateElement("pCOFINS");
                                    nodeTipoCOFINS.AppendChild(ppPIS);
                                    ppPIS.InnerText = pcofins.ToString();

                                    cofins.AppendChild(nodeTipoCOFINS);
                                    imposto.AppendChild(cofins);
                                }
                            }
                        }
                        else if ((cstPis == "04") || (cstPis == "06") || (cstPis == "49"))
                        {
                            //tratamento para  nao gerar tag Aliquota..................................................

                            XmlElement pis = arquivoXML.CreateElement("PIS");
                            XmlElement nodeTipoPis = arquivoXML.CreateElement("PISNT");

                            XmlElement cstPIS = arquivoXML.CreateElement("CST");
                            cstPIS.InnerText = cstPis;
                            nodeTipoPis.AppendChild(cstPIS);

                            pis.AppendChild(nodeTipoPis);
                            imposto.AppendChild(pis);

                            imposto.AppendChild(pis);

                            //-----------------------------------------------------------------------------------------------------

                            //COFINS
                            XmlElement cofins = arquivoXML.CreateElement("COFINS");
                            XmlElement nodeTipoCOFINS = arquivoXML.CreateElement("COFINSNT");

                            XmlElement cstCOFINS = arquivoXML.CreateElement("CST");
                            cstCOFINS.InnerText = cstPis;
                            nodeTipoCOFINS.AppendChild(cstCOFINS);


                            cofins.AppendChild(nodeTipoCOFINS);
                            imposto.AppendChild(cofins);
                        }

                        #endregion
                    }
                }
            }
        }

        private void PesquisarCSOSN()
        {
            try
            {
                novoCsosn = new CsosnRegraNegocio();
                DataTable dadosTabelaCsosn = new DataTable();
                cstProduto = cstProduto.Replace(" ", "");
                dadosTabelaCsosn = novoCsosn.PesquisarCsosnProduto(cstProduto);

                if (dadosTabelaCsosn.Rows.Count > 0)
                {
                    tipoCSOSN = dadosTabelaCsosn.Rows[0]["CSOSN"].ToString();
                    tipoCSOSN = tipoCSOSN.Replace(" ", "");
                }
                else
                {
                    MessageBox.Show("Produto" + descProd + " sem tributo CST.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void PesquiarCrt()
        {
            try
            {
                novoParametro = new ParametroRegraNegocio();
                DataTable dadosTabelaParametro = new DataTable();

                dadosTabelaParametro = novoParametro.PesquisaParametroE();

                if (dadosTabelaParametro.Rows.Count > 0)
                {
                    crt = dadosTabelaParametro.Rows[0]["CRT"].ToString();
                    crt = crt.Replace(" ", "");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void preenchePagamento()
        {
            string vOutros = "";
            int codigo = 0;
            decimal vDinheiro = 0;

            nodeTotal = arquivoXML.CreateElement("total");
            nodeInfCFe.AppendChild(nodeTotal);

            nodePagto = arquivoXML.CreateElement("pgto");

            XmlElement MP = arquivoXML.CreateElement("MP");
            XmlElement cMP = arquivoXML.CreateElement("cMP");
            XmlElement vMP = arquivoXML.CreateElement("vMP");
            XmlElement vTroco = arquivoXML.CreateElement("vTroco");

            for (int i = 0; i < gdvTipoPgto.Rows.Count; i++)
            {
                descTipoPagamento = gdvTipoPgto.Rows[i].Cells[0].Value.ToString();
                vDinheiro = Convert.ToDecimal(gdvTipoPgto.Rows[i].Cells[1].Value.ToString());

                if ((descTipoPagamento == "Dinheiro") || (descTipoPagamento == "DINHEIRO"))
                {
                    codigo = 1;
                }
                else if ((descTipoPagamento == "Cheque") || (descTipoPagamento == "CHEQUE"))
                {
                    codigo = 2;
                }
                else if ((descTipoPagamento == "Cartao") || (descTipoPagamento == "CARTAO") || (descTipoPagamento == "CARTÃO") || (descTipoPagamento == "CARTAO"))
                {
                    codigo = 3;
                }
                else if ((descTipoPagamento == "Aberto") || (descTipoPagamento == "ABERTO"))
                {
                    codigo = 4;
                }

                //MP.AppendChild(vMP);
                MP = arquivoXML.CreateElement("MP");
                vMP = arquivoXML.CreateElement("vMP");
                string v = Convert.ToString(vDinheiro.ToString("N2"));
                v = v.Replace(".", "");
                v = v.Replace(",", ".");
                vMP.InnerText = v;

                MP = arquivoXML.CreateElement("MP");
                cMP = arquivoXML.CreateElement("cMP");
                cMP.InnerText = "0" + codigo.ToString();
                MP.AppendChild(cMP);
                nodePagto.AppendChild(MP);
                MP.AppendChild(vMP);

                nodeInfCFe.AppendChild(nodePagto);
            }
        }

        private void preencheInfo()
        {
            string mesg = "";
            bool ativarPlaca;
            string placa = "";
            string km = "";
            decimal aliquota = 0;
            decimal somaAliquotaTotal = 0;
            string operador = "";
            string periodo = frmVenda.periodoAtuante_;
            string versao = frmVenda.versaoCfe;

            versao = versao.Replace("ã", "a");
            operador = frmVenda.operadorAtuante;
            operador = operador.Replace(" ", "");

            novoParametro = new RegraNegocio.ParametroRegraNegocio();
            DataTable dadosTabelaParametro = new DataTable();
            dadosTabelaParametro = novoParametro.PesquisaParametroE();

            if (dadosTabelaParametro.Rows.Count > 0)
            {
                mesg = dadosTabelaParametro.Rows[0]["MSG"].ToString();
                ativarPlaca = Convert.ToBoolean(dadosTabelaParametro.Rows[0]["PLACA"].ToString());
                aliquota = Convert.ToDecimal(dadosTabelaParametro.Rows[0]["ALIQUOTA_DIA"]);

                aliquota = Convert.ToDecimal(dadosTabelaParametro.Rows[0]["ALIQUOTA_DIA"].ToString());
                total = Convert.ToDecimal(txtTotal.Text);

                if (ativarPlaca == true)
                {
                    placa = "PLACA: ";
                }
                else
                {
                    placa = "";
                }

                somaAliquotaTotal = ((total * aliquota) / 100);
            }

            nodeInfAdic = arquivoXML.CreateElement("infAdic");

            XmlElement infCpl = arquivoXML.CreateElement("infCpl");
            // infCpl.InnerText = ("Volte Sempre Obrigado !-.:preferencia");

            string operador_ = frmVenda.operadorAtuante;
            operador_ = operador_.Replace(" ", "");
            string periodo_ = frmVenda.periodoAtuante_;
            periodo_ = periodo_.Replace(" ", "");

            infCpl.InnerText = ("N Cupom: " + _numVenda.ToString() + " | " + "N Caixa: " + lblNumCaixa.Text + " | " + "Operador: " + operador_ + " | " + "\n" + "Perido: " + periodo_ + " | " + placa + " | " + mesg + "\n" +
             " | " + "Perc. Aliquota: " + somaAliquotaTotal.ToString("C2") + " | " + "CFE: " + versao + " | " + "Todos Direitos TI-AutoCom");

            nodeInfAdic.AppendChild(infCpl);
            nodeInfCFe.AppendChild(nodeInfAdic);
        }

        //------------------------------------------------------------------------------------------------------------------------------------------



        public void imprimirCupomComum()
        {
            try
            {
                frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                idRetorno = ConfiguraModeloImpressora(7);
                idRetorno = IniciaPorta("COM7"); //inicia a porta com o IP digitado

                align = "" + (char)27 + (char)97 + (char)1;
                idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);
                textoCupom = (frmVenda.periodoAtuante_).ToString();

                FormataTX("OLA MUNDO" + (char)10 + (char)13, 2, 0, 0, 0, 0);
                idRetorno = RegraNegocio.MP2032.FechaPorta();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void FecharVenda()
        {
            try
            {
                //metodopara buscar tipo de impressora......................................................
                impressora = "";
                impressora = frmVenda.nomeImpressora;
                int numCupom = frmVenda.numCupom;
                int erro;
                PesquisarPlaca();

                impressora = impressora.Replace(" ", "");

                #region sat
                if (impressora == "SAT")
                {
                    if (idRetorno == 1)
                    {
                        AbreCupomSat();
                    }

                    if (idRetorno == 1)
                    {
                        VendeItemCompletoSat();
                    }
                    if (idRetorno == 1)
                    {
                        IniciaFechamentoSat();
                    }

                    if (idRetorno == 1)
                    {
                        FormaPgtoSat();
                    }

                    if (idRetorno == 1)
                    {
                        DadosSoftHouseSat();
                    }

                    if (idRetorno == 1)
                    {
                        FechaCupomSat();
                    }
                    else
                    {
                        if (MessageBox.Show("Erro ao Tentar Realizar essa Venda.\nDeseja Cancelar a Venda?.", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                        {
                            novoTemp = new RegraNegocio.TempRegraNegocios();
                            novoTemp.AlterarCpfCliente("", 0, "");

                            novoParametro = new RegraNegocio.ParametroRegraNegocio();
                            novoParametro.AlterarStatusFechar(Convert.ToInt32(numCupom), frmVenda.numcaixa);

                            frmVenda.CancelaVenda();
                            frmVenda.AlterarNumVenda();
                            frmVenda.AtualizarGridAberto();
                            this.Close();
                            frmVenda.LimpaCampos();

                            MessageBox.Show("Venda e Cupom " + numCupom + " foram Cancelados com Sucesso.", "Informação");
                            RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);
                        }
                    }
                }
                #endregion

                #region Bematech / Elgin

                #region Bematech
                else if (impressora == "BEMATECH")
                {
                    if (idRetorno == 1)
                    {
                        AbreCupomIF();
                    }

                    if (idRetorno == 1)
                    {
                        VendeItemIF();
                    }

                    if (idRetorno == 1)
                    {
                        IniciaFecharCumpomIF();
                    }

                    if (idRetorno == 1)
                    {
                        FormaPgtoFI();
                    }

                    if (idRetorno == 1)
                    {
                        FechaCumpomIF();
                    }

                    else if (MessageBox.Show("Erro no Parametro do(s) Produto(s).\nDeseja Cancela a Venda?.", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.None) == DialogResult.OK)
                    {

                        novoTemp = new RegraNegocio.TempRegraNegocios();
                        novoTemp.AlterarCpfCliente("", 0, "");

                        novoParametro = new RegraNegocio.ParametroRegraNegocio();
                        novoParametro.AlterarStatusFechar(Convert.ToInt32(numCupom), frmVenda.numcaixa);

                        frmVenda.CancelaVenda();
                        frmVenda.AlterarNumVendaNumCaixa();
                        frmVenda.AtualizarGridAberto();
                        this.Close();
                        frmVenda.LimpaCampos();

                        MessageBox.Show("Venda e Cupom " + numCupom + " foram Cancelados com Sucesso.", "Informação");
                        idCliente = RegraNegocio.CupomFiscal.Bematech_FI_CancelaCupom();
                    }
                }
                #endregion

                #region Elgin
                else if (nomeImpressora == "ELGIN")
                {
                    var configImpressora = new PrinterSettings();
                    printerName = configImpressora.PrinterName;

                    PesquisarCupomImagem();
                    PesquisarPlaca();

                    if (cupomImagem == true)
                    {
                        AbreCupomComum();
                        VendeItemComum();
                        FormaPagamentoComum();

                        dadosCupomElgin = (A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + "\n" + F);

                        esc = new EscPos();

                        this.esc.printText(printerName, dadosCupomElgin);
                        feedAndCutter(printerName, 5);

                        //ExportarItemVenda();
                        //ExportarPagamentoVendas();

                        SalvarFechamentoVenda_();
                        AlterarStatusFechar();
                        frmVenda.AlterarNumVendaNumCaixa();
                        frmVenda.AtualizarGridAberto();
                        LimparCampos();
                    }
                    else
                    {
                        //variavel do caminho para salvar arquivo xml antes de ir para SAT
                        string caminho = "";
                        string ret = "";
                        string mes, ano = "";

                        //funcoes para gerar arquivo xml..................................
                        saveFileDialog1.FileName = caminho;
                        IniciarXML();
                        PreencherIDE();
                        PreencherEmitente();
                        preenchedestinario();
                        preencheProduto();
                        preenchePagamento();
                        preencheInfo();

                        arqu = (pathVendaXML);

                        arquivoXML.Save(arqu + _numVenda + "Cx" + frmVenda.numcaixa + ".xml");

                        //lear caminho do arquivo xml...................................
                        LerArqTxt(arqu + _numVenda + "Cx" + frmVenda.numcaixa + ".xml");

                        //gera numero rondomico........................................
                        getNumberRandom();

                        PesquisarCodAticavaoXml();

                        codAtivacao = codAtivacao.Replace(" ", "");

                        //si arquivo for diferente de nulo entra no metodo............
                        if (arqu != null)
                        {
                            //recupera o retorno do SAT...............................
                            ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.EnviarDadosVenda(sessao, codAtivacao, arqu)));

                            //separa os retorno por pig...............................
                            string ret_sessao = (Sep_Delimitador('|', 0, ret));
                            string ret_sat = (Sep_Delimitador('|', 1, ret));
                            string ret_erro = (Sep_Delimitador('|', 2, ret));
                            string ret_err_ = (Sep_Delimitador('|', 3, ret));
                            string ret_nulo1_ = (Sep_Delimitador('|', 4, ret));
                            string ret_nulo2 = (Sep_Delimitador('|', 5, ret));
                            string ret_desc = (Sep_Delimitador('|', 6, ret));
                            string ret_dataVenda = (Sep_Delimitador('|', 7, ret));
                            string ret_id = (Sep_Delimitador('|', 8, ret));
                            string ret_valorvenda = (Sep_Delimitador('|', 9, ret));
                            string ret_nulo3 = (Sep_Delimitador('|', 10, ret));
                            string ret_qrCod = (Sep_Delimitador('|', 11, ret));
                            string ret_inf = (Sep_Delimitador('|', 12, ret));
                            string ret_inf1 = (Sep_Delimitador('|', 13, ret));

                            idCfe = ret_id;
                            Ultimachave = ret_id;
                            retornoVendaSat = ret_qrCod;
                            retornoSat = ret_sat;

                            // MessageBox.Show(ret);

                            // si restorno do sat for igual 0600 para aprovacao da venda
                            if (ret_sat == "06000")
                            {
                                byte[] senhaBinaria = new byte[256];

                                //Descriptografa....................................................................................................
                                senhaBinaria = Convert.FromBase64String(ret_desc);

                                string senhaDescripto = ASCIIEncoding.ASCII.GetString(senhaBinaria);

                                //gera arquivo em txt aprovado......................................................................................
                                string strPathFile = pathDadosVendaAutorizada + "00" + lblNumCaixa.Text + ".txt";

                                using (FileStream fs = File.Create(strPathFile))
                                {
                                    using (StreamWriter sw = new StreamWriter(fs))
                                    {
                                        sw.Write(senhaDescripto);
                                    }
                                }

                                mes = DateTime.Now.Month.ToString();
                                ano = DateTime.Now.Year.ToString();

                                esc = new EscPos();

                                string enderecoExitente = (pathCustodia + ano + "\\" + mes + "\\" + "Cx" + frmVenda.numcaixa);

                                if (Directory.Exists(enderecoExitente))
                                {
                                    //Converte TXTpara XML e Salva em Arquivo XML............................................................................................
                                    FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                                    byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                                    sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                                    FileStream srXml = new FileStream(enderecoExitente + "\\" + ret_id + ".xml", FileMode.Create, FileAccess.Write);
                                    StreamWriter wr = new StreamWriter(srXml);
                                    srXml.Write(bytes, 0, bytes.Length);
                                    sr.Close();
                                    srXml.Close();

                                    GerarUltimaVendaXml();

                                    //  MontarCupomElgin();

                                    this.esc.normalModeText(printerName);

                                    AbreCupomComum();
                                    VendeItemComum();
                                    FormaPagamentoComum();

                                    //string date = DateTime.Now.Date.ToShortDateString();

                                    //this.esc.normalModeText(printerName);

                                    string data = "-----------------------------------------------\n";

                                    //imtprimir dados venda
                                    dadosCupomElgin = (A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + data);


                                    ////Centering
                                    //this.esc.SelectJustification(printerName, 1);

                                    string nSat = "              SAT Nr:" + nserieSAT + "\n             " + DateTime.Now.ToString() + "\n";


                                    this.esc.printText(printerName, dadosCupomElgin + nSat);

                                    this.esc.barcode_height(printerName, 50);
                                    this.esc.barcode_width(printerName, 2);
                                    this.esc.barcodeHRI_chars(printerName, 1);
                                    this.esc.barcodeHRIPostion(printerName);
                                  

                                    string parte_um = idCfe;
                                    parte_um = parte_um.Replace("CFe", "");
                                    parte_um = parte_um.Substring(0, 22);

                                    string parte_dois = idCfe;
                                    parte_dois = parte_dois.Replace("CFe", "").Trim();
                                    parte_dois = parte_dois.Substring(22, 22);



                                    this.esc.printBarcodeB(printerName, "{A" + parte_um, 73);
                                    this.esc.printBarcodeB(printerName, "{A" + parte_dois, 73);

                                    //this.esc.printBarcodeB(printerName, "{B          " + parte_um, 72);
                                    //this.esc.printBarcodeB(printerName, "{B          " + parte_dois, 72);



                                    //string parte_um = idCfe;
                                    //parte_um = parte_um.Replace("CFe", "");
                                    //parte_um = parte_um.Substring(0, 22);

                                    //this.esc.barcode_height(printerName, 50);
                                    //this.esc.barcode_width(printerName, 2);
                                    //this.esc.barcodeHRI_chars(printerName, 1);
                                    //this.esc.barcodeHRIPostion(printerName);

                                    // this.esc.printBarcodeB(printerName, parte_um, 72);

                                    //string parte_dois = idCfe;
                                    //parte_dois = parte_dois.Replace("CFe", "");
                                    //parte_dois.Trim();
                                    //parte_dois = parte_dois.Substring(22, 22);
                                    //this.esc.printBarcodeB(printerName, parte_dois, 73);

                                    this.esc.printQrcode(retornoVendaSat, printerName);


                                    ////Make sure to specify start character. The start character must be code set selection character (any of CODE
                                    ////A, CODE B, or CODE C) which selects the first code set

                                    ////this.esc.printBarcodeB(printerName, "{B012345678901234567", 73);
                                    //this.esc.printBarcodeB(printerName, "{B" + parte_um, 73);
                                    //this.esc.printBarcodeB(printerName, "{B" + parte_dois, 73);

                                    ////this.esc.printBarcodeB(printerName, "{B012345678901234567", 73);
                                    //idCfe = idCfe.Replace("CFe", " ");
                                    //this.esc.printBarcodeB(printerName, "{C" + idCfe, 73);

                                    //this.esc.lineFeed(printerName, 1);

                                    ////dados qrcode
                                    //this.esc.printQrcode(assinaturaQRCODE, printerName);
                                    //this.esc.lineFeed(printerName, 1);

                                    //corte
                                    feedAndCutter(printerName, 5);
                                }
                                else
                                {
                                    Directory.CreateDirectory(enderecoExitente);
                                    //Converte TXTpara XML e Salva em Arquivo XML............................................................................................
                                    FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                                    byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                                    sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                                    FileStream srXml = new FileStream(enderecoExitente + "\\" + ret_id + ".xml", FileMode.Create, FileAccess.Write);
                                    StreamWriter wr = new StreamWriter(srXml);
                                    srXml.Write(bytes, 0, bytes.Length);
                                    sr.Close();
                                    srXml.Close();

                                    GerarUltimaVendaXml();

                                    // MontarCupom();

                                    //string date = DateTime.Now.Date.ToShortDateString();

                                    this.esc.normalModeText(printerName);

                                    //imtprimir dados venda
                                    dadosCupomElgin = (A + "\n" + B + "\n" + C + "\n" + D + "\n" + E);

                                    this.esc.printText(printerName, dadosCupomElgin);

                                    //Right justification
                                    string data = "--------------------------------------------\n";
                                    this.esc.SelectJustification(printerName, 0);
                                    this.esc.printText(printerName, data);

                                    this.esc.normalModeText(printerName);

                                    //Centering
                                    this.esc.SelectJustification(printerName, 1);

                                    this.esc.printText(printerName, "SAT Nr:" + nserieSAT);
                                    this.esc.lineFeed(printerName, 1);

                                    this.esc.SelectJustification(printerName, 1);

                                    this.esc.printText(printerName, DateTime.Now.Date.ToShortDateString());
                                    this.esc.lineFeed(printerName, 1);

                                    string parte_um = idCfe;
                                    parte_um = parte_um.Replace("CFe", "");
                                    parte_um = parte_um.Substring(0, 22);

                                    this.esc.barcode_height(printerName, 30);
                                    this.esc.barcode_width(printerName, 2);
                                    this.esc.barcodeHRI_chars(printerName, 1);
                                    this.esc.barcodeHRIPostion(printerName);

                                    string parte_dois = idCfe;
                                    parte_dois = parte_dois.Replace("CFe", "");
                                    parte_dois.Trim();
                                    parte_dois = parte_dois.Substring(22, 22);


                                    //Make sure to specify start character. The start character must be code set selection character (any of CODE
                                    //A, CODE B, or CODE C) which selects the first code set

                                    //this.esc.printBarcodeB(printerName, "{B012345678901234567", 73);
                                    this.esc.printBarcodeB(printerName, "{B" + parte_um, 73);
                                    this.esc.printBarcodeB(printerName, "{B" + parte_dois, 73);
                                    this.esc.lineFeed(printerName, 1);

                                    //dados qrcode
                                    this.esc.printQrcode(assinaturaQRCODE, printerName);
                                    this.esc.lineFeed(printerName, 1);

                                    //corte
                                    feedAndCutter(printerName, 5);
                                    this.Close();

                                    int mes_ = DateTime.Now.Month;
                                    MessageBox.Show("Por Favor Retirar Ralatório Gerencial Venda Total do Mês: " + (mes_ - 1) + ".", "Relatório Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    frmVenda.MontarRelatorioTotalMes();
                                }

                                SalvarFechamentoVenda_();
                                AlterarStatusFechar();
                                frmVenda.AlterarNumVendaNumCaixa();
                                frmVenda.AtualizarGridAberto();
                                LimparCampos();
                            }
                            else
                            {
                                MessageBox.Show("Erro no S@T: " + ret, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                DeltarArquivoXml();
                                this.Close();
                            }
                        }
                    }
                }
                #endregion

                #endregion

                #region Daruma
                else if (nomeImpressora == "DARUMA")
                {
                    PesquisarCupomImagem();

                    if (cupomImagem == false)
                    {
                        //variavel do caminho para salvar arquivo xml antes de ir para SAT
                        string caminho = "";
                        string ret = "";
                        string mes, ano = "";

                        //funcoes para gerar arquivo xml..................................
                        saveFileDialog1.FileName = caminho;
                        IniciarXML();
                        PreencherIDE();
                        PreencherEmitente();
                        preenchedestinario();
                        preencheProduto();
                        preenchePagamento();
                        preencheInfo();

                        arqu = (pathVendaXML);

                        arquivoXML.Save(arqu + _numVenda + "Cx" + frmVenda.numcaixa + ".xml");

                        //lear caminho do arquivo xml...................................
                        LerArqTxt(arqu + _numVenda + "Cx" + frmVenda.numcaixa + ".xml");

                        //gera numero rondomico........................................
                        getNumberRandom();

                        PesquisarCodAticavaoXml();

                        codAtivacao = codAtivacao.Replace(" ", "");

                        //si arquivo for diferente de nulo entra no metodo............
                        if (arqu != null)
                        {
                            //recupera o retorno do SAT...............................
                            ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.EnviarDadosVenda(sessao, codAtivacao, arqu)));

                            //separa os retorno por pig...............................
                            string ret_sessao = (Sep_Delimitador('|', 0, ret));
                            string ret_sat = (Sep_Delimitador('|', 1, ret));
                            string ret_erro = (Sep_Delimitador('|', 2, ret));
                            string ret_err_ = (Sep_Delimitador('|', 3, ret));
                            string ret_nulo1_ = (Sep_Delimitador('|', 4, ret));
                            string ret_nulo2 = (Sep_Delimitador('|', 5, ret));
                            string ret_desc = (Sep_Delimitador('|', 6, ret));
                            string ret_dataVenda = (Sep_Delimitador('|', 7, ret));
                            string ret_id = (Sep_Delimitador('|', 8, ret));
                            string ret_valorvenda = (Sep_Delimitador('|', 9, ret));
                            string ret_nulo3 = (Sep_Delimitador('|', 10, ret));
                            string ret_qrCod = (Sep_Delimitador('|', 11, ret));
                            string ret_inf = (Sep_Delimitador('|', 12, ret));
                            string ret_inf1 = (Sep_Delimitador('|', 13, ret));

                            idCfe = ret_id;
                            Ultimachave = ret_id;
                            retornoVendaSat = ret_qrCod;
                            retornoSat = ret_sat;

                            // MessageBox.Show(ret);

                            // si restorno do sat for igual 0600 para aprovacao da venda
                            if (ret_sat == "06000")
                            {
                                byte[] senhaBinaria = new byte[256];

                                //Descriptografa....................................................................................................
                                senhaBinaria = Convert.FromBase64String(ret_desc);

                                string senhaDescripto = ASCIIEncoding.ASCII.GetString(senhaBinaria);

                                //gera arquivo em txt aprovado......................................................................................
                                string strPathFile = pathDadosVendaAutorizada + "00" + lblNumCaixa.Text + ".txt";

                                using (FileStream fs = File.Create(strPathFile))
                                {
                                    using (StreamWriter sw = new StreamWriter(fs))
                                    {
                                        sw.Write(senhaDescripto);
                                    }
                                }

                                mes = DateTime.Now.Month.ToString();
                                ano = DateTime.Now.Year.ToString();

                                string enderecoExitente = (pathCustodia + ano + "\\" + mes + "\\" + "Cx" + frmVenda.numcaixa);

                                if (Directory.Exists(enderecoExitente))
                                {
                                    //Converte TXTpara XML e Salva em Arquivo XML............................................................................................
                                    FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                                    byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                                    sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                                    FileStream srXml = new FileStream(enderecoExitente + "\\" + ret_id + ".xml", FileMode.Create, FileAccess.Write);
                                    StreamWriter wr = new StreamWriter(srXml);
                                    srXml.Write(bytes, 0, bytes.Length);
                                    sr.Close();
                                    srXml.Close();

                                    dadosCupoDaruma = (enderecoExitente + "\\" + idCfe + ".xml");

                                    GerarUltimaVendaXml();

                                    MontarCupomDaruma();
                                }
                                else
                                {
                                    Directory.CreateDirectory(enderecoExitente);
                                    //Converte TXTpara XML e Salva em Arquivo XML............................................................................................
                                    FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                                    byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                                    sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                                    FileStream srXml = new FileStream(enderecoExitente + "\\" + ret_id + ".xml", FileMode.Create, FileAccess.Write);
                                    StreamWriter wr = new StreamWriter(srXml);
                                    srXml.Write(bytes, 0, bytes.Length);
                                    sr.Close();
                                    srXml.Close();

                                    GerarUltimaVendaXml();

                                    MontarCupomDaruma();

                                    this.Close();

                                    int mes_ = DateTime.Now.Month;
                                    if (mes_ == 1)
                                    {
                                        mes_ = 13;
                                    }
                                    MessageBox.Show("Por Favor Retirar Ralatório Gerencial Venda Total do Mês: " + (mes_ - 1) + ".", "Relatório Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    frmVenda.MontarRelatorioTotalMes();
                                }

                                SalvarFechamentoVenda_();
                                AlterarStatusFechar();
                                // AlterarParametroNumFecharVenda();
                                frmVenda.AlterarNumVendaNumCaixa();
                                frmVenda.AtualizarGridAberto();
                                LimparCampos();
                            }
                            else
                            {
                                MessageBox.Show("Erro: " + ret, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                DeltarArquivoXml();
                                this.Close();
                            }
                        }
                    }
                    else
                    {
                        AbreCupomComum();
                        VendeItemComum();
                        FormaPagamentoComum();

                        SalvarFechamentoVenda_();
                        AlterarStatusFechar();
                        // AlterarParametroNumFecharVenda();
                        frmVenda.AlterarNumVendaNumCaixa();
                        frmVenda.AtualizarGridAberto();
                        LimparCampos();

                        PesquisarImpressaoAutomatica();

                        if (impressaoAutomatica == true)
                        {
                            int iRetorno;
                            iRetorno = DLLsDaruma.iImprimirTexto_DUAL_DarumaFramework(A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + "</c><sl>4</sl><gui></gui><l></l>", 0);
                        }

                        this.Close();
                    }

                }
                else if (nomeImpressora == "LPT")
                {
                    AbreCupomComum();
                    VendeItemComum();
                    FormaPagamentoComum();

                    PesquisarImpressaoAutomatica();

                    if (impressaoAutomatica == true)
                    {
                        ImprimiCupomNaoFiscal();
                    }

                    //ExportarItemVenda();
                    //ExportarPagamentoVendas();

                    SalvarFechamentoVenda_();
                    AlterarStatusFechar();
                    // AlterarParametroNumFecharVenda();
                    frmVenda.AlterarNumVendaNumCaixa();
                    frmVenda.AtualizarGridAberto();
                    LimparCampos();
                    this.Close();
                }
                else if (nomeImpressora == "MP2500")
                {
                    AbreCupomComum();
                    VendeItemComum();
                    FormaPagamentoComum();

                    PesquisarImpressaoAutomatica();

                    if (impressaoAutomatica == true)
                    {
                        frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();

                        idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);
                        idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);

                        string mensagem = "";

                        mensagem = (A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + "\n" + F).ToString();

                        idRetorno = RegraNegocio.MP2032.BematechTX(mensagem);
                        idRetorno = RegraNegocio.MP2032.BematechTX("\n\n\n\n");


                        RegraNegocio.MP2032.AcionaGuilhotina(0);
                        idRetorno = RegraNegocio.MP2032.FechaPorta();
                    }

                    //ExportarItemVenda();
                    //ExportarPagamentoVendas();

                    SalvarFechamentoVenda_();
                    AlterarStatusFechar();
                    frmVenda.AlterarNumVendaNumCaixa();
                    frmVenda.AtualizarGridAberto();
                    LimparCampos();
                    this.Close();
                }

                else if (nomeImpressora == "COMUM")
                {
                    AbreCupomComum();
                    VendeItemComum();
                    FormaPagamentoComum();

                    PesquisarImpressaoAutomatica();

                    if (impressaoAutomatica == true)
                    {
                        frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();

                        idRetorno = ConfiguraModeloImpressora(7);
                        idRetorno = IniciaPorta(frmVenda.numComimp); //inicia a porta com o IP digitado

                        align = "" + (char)27 + (char)97 + (char)1;
                        idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);
                        textoCupom = (frmVenda.periodoAtuante_).ToString();

                        FormataTX((A + B + C + D + E + F + G + H) + (char)10 + (char)13, 1, 1, 0, 0, 0);

                        RegraNegocio.MP2032.AcionaGuilhotina(0);
                        idRetorno = RegraNegocio.MP2032.FechaPorta();
                    }

                    //ExportarItemVenda();
                    //ExportarPagamentoVendas();

                    SalvarFechamentoVenda_();
                    AlterarStatusFechar();
                    frmVenda.AlterarNumVendaNumCaixa();
                    frmVenda.AtualizarGridAberto();
                    LimparCampos();
                    this.Close();
                }
                #region BEMASAT
                else if (impressora == "BEMASAT")
                {
                    PesquisarCupomImagem();

                    if (cupomImagem == true)
                    {
                        //variavel do caminho para salvar arquivo xml antes de ir para SAT
                        string caminho = "";
                        string ret = "";

                        //funcoes para gerar arquivo xml..................................
                        saveFileDialog1.FileName = caminho;
                        IniciarXML();
                        PreencherIDE();
                        PreencherEmitente();
                        preenchedestinario();
                        preencheProduto();
                        preenchePagamento();
                        preencheInfo();

                        arqu = (pathVendaXML);

                        arquivoXML.Save(arqu + _numVenda + ".xml");

                        //lear caminho do arquivo xml...................................
                        LerArqTxt(arqu + _numVenda + ".xml");

                        //gera numero rondomico........................................
                        getNumberRandom();

                        PesquisarCodAticavaoXml();

                        codAtivacao = codAtivacao.Replace(" ", "");

                        //si arquivo for diferente de nulo entra no metodo............
                        if (arqu != null)
                        {
                            //recupera o retorno do SAT...............................
                            ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.EnviarDadosVenda(sessao, codAtivacao, arqu)));

                            //separa os retorno por pig...............................
                            string ret_sessao = (Sep_Delimitador('|', 0, ret));
                            string ret_sat = (Sep_Delimitador('|', 1, ret));
                            string ret_erro = (Sep_Delimitador('|', 2, ret));
                            string ret_err_ = (Sep_Delimitador('|', 3, ret));
                            string ret_nulo1_ = (Sep_Delimitador('|', 4, ret));
                            string ret_nulo2 = (Sep_Delimitador('|', 5, ret));
                            string ret_desc = (Sep_Delimitador('|', 6, ret));
                            string ret_dataVenda = (Sep_Delimitador('|', 7, ret));
                            string ret_id = (Sep_Delimitador('|', 8, ret));
                            string ret_valorvenda = (Sep_Delimitador('|', 9, ret));
                            string ret_nulo3 = (Sep_Delimitador('|', 10, ret));
                            string ret_qrCod = (Sep_Delimitador('|', 11, ret));
                            string ret_inf = (Sep_Delimitador('|', 12, ret));
                            string ret_inf1 = (Sep_Delimitador('|', 13, ret));

                            idCfe = ret_id;
                            Ultimachave = ret_id;
                            retornoVendaSat = ret_qrCod;

                            // MessageBox.Show(ret);

                            // si restorno do sat for igual 0600 para aprovacao da venda
                            if (ret_sat == "06000")
                            {
                                byte[] senhaBinaria = new byte[256];

                                //Descriptografa....................................................................................................
                                senhaBinaria = Convert.FromBase64String(ret_desc);

                                string senhaDescripto = ASCIIEncoding.ASCII.GetString(senhaBinaria);

                                //gera arquivo em txt aprovado......................................................................................
                                string strPathFile = pathDadosVendaAutorizada + "00" + lblNumCaixa.Text + ".txt";

                                using (FileStream fs = File.Create(strPathFile))
                                {
                                    using (StreamWriter sw = new StreamWriter(fs))
                                    {
                                        sw.Write(senhaDescripto);
                                    }
                                }

                                //Converte TXTpara XML e Salva em Arquivo XML............................................................................................
                                FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                                byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                                sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                                FileStream srXml = new FileStream(pathCustodia + ret_id + ".xml", FileMode.Create, FileAccess.Write);
                                StreamWriter wr = new StreamWriter(srXml);
                                srXml.Write(bytes, 0, bytes.Length);
                                sr.Close();
                                srXml.Close();

                                //gerar arquivo contem chave da venda.............................................................................
                                GerarUltimaVendaXml();
                                GerarPDFRedu(pathCustodia + idCfe + ".pdf");

                                //METODOS-------------------------------------------------------------------------------------------------
                                SalvarFechamentoVenda_();
                                AlterarStatusFechar();
                                AlterarParametroNumFecharVenda();
                                LimparCampos();

                                //====================================================================================
                                PesquisarImpressaoAutomatica();

                                if (impressaoAutomatica == true)
                                {
                                    PrintDocument pd = new PrintDocument();
                                    pd.PrintPage += new PrintPageEventHandler(imprimirCod);
                                    pd.Print();

                                    PrintDocument pd_ = new PrintDocument();
                                    pd_.PrintPage += new PrintPageEventHandler(imprimirQrCode);
                                    pd_.Print();

                                    PrintDocument pdEspaco = new PrintDocument();
                                    pdEspaco.PrintPage += new PrintPageEventHandler(imprimirEspaco);
                                    pdEspaco.Print();

                                    //Pesquisar Parametro da porta da Impressora

                                    novoParametro = new ParametroRegraNegocio();
                                    DataTable dadosTabelaParametro = new DataTable();

                                    dadosTabelaParametro = novoParametro.PesquisaParametroE();

                                    if (dadosTabelaParametro.Rows.Count > 0)
                                    {
                                        porta = dadosTabelaParametro.Rows[0]["PORTA_COM_IMPRESSORA"].ToString();
                                        porta = porta.Replace(" ", "");
                                    }
                                    else
                                    {
                                        MessageBox.Show("Porta COM não Encontrado.");
                                    }

                                    idRetorno = RegraNegocio.MP2032.IniciaPorta(porta);

                                    int contador = 0;

                                    while (idRetorno == 0)
                                    {
                                        idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(1);

                                        if (idRetorno == 1)
                                        {
                                            this.Close();
                                        }
                                        else if (idRetorno == 0)
                                        {
                                            contador++;

                                            if (contador == 5)
                                            {
                                                break;
                                                //MessageBox.Show("Venda foi Aprovada com Sucesso.\nErro no Corte do Papel, Verifique se Impressora Está Ligada ou Entre em Contado com seu Administrado.", "Erro ao Imprimir Cupom", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                                this.Close();
                                            }
                                        }
                                    }

                                    idRetorno = RegraNegocio.MP2032.FechaPorta();
                                    idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(0);
                                    ImprimiCupomNaoFiscal();
                                }
                            }
                            else
                            {
                                if (MessageBox.Show("Venda " + _numVenda + " não Aprovada.\nErro: : " + ret + ".\nDeseja Cancelar Venda???", "Error", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                                {
                                    frmVenda.CancelaVenda();
                                    frmVenda.AlterarNumVenda();
                                    // statusStrip1.Refresh();
                                    MessageBox.Show("Venda Cancelado com Sucesso.", "Informação");

                                    frmVenda.AtualizarGridAberto();
                                    frmVenda.LimpaCampos();
                                    this.Close();
                                }
                                else
                                {
                                    this.Refresh();
                                }
                            }
                        }
                    }
                    else
                    {
                        //variavel do caminho para salvar arquivo xml antes de ir para SAT
                        string caminho = "";
                        string ret = "";
                        string mes, ano = "";

                        //funcoes para gerar arquivo xml..................................
                        saveFileDialog1.FileName = caminho;
                        IniciarXML();
                        PreencherIDE();
                        PreencherEmitente();
                        preenchedestinario();
                        preencheProduto();
                        preenchePagamento();
                        preencheInfo();

                        arqu = (pathVendaXML);

                        arquivoXML.Save(arqu + _numVenda + "Cx" + frmVenda.numcaixa + ".xml");

                        //lear caminho do arquivo xml...................................
                        LerArqTxt(arqu + _numVenda + "Cx" + frmVenda.numcaixa + ".xml");

                        //gera numero rondomico........................................
                        getNumberRandom();

                        PesquisarCodAticavaoXml();

                        codAtivacao = codAtivacao.Replace(" ", "");

                        //si arquivo for diferente de nulo entra no metodo............
                        if (arqu != null)
                        {
                            //recupera o retorno do SAT...............................
                            ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.EnviarDadosVenda(sessao, codAtivacao, arqu)));

                            //separa os retorno por pig...............................
                            string ret_sessao = (Sep_Delimitador('|', 0, ret));
                            string ret_sat = (Sep_Delimitador('|', 1, ret));
                            string ret_erro = (Sep_Delimitador('|', 2, ret));
                            string ret_err_ = (Sep_Delimitador('|', 3, ret));
                            string ret_nulo1_ = (Sep_Delimitador('|', 4, ret));
                            string ret_nulo2 = (Sep_Delimitador('|', 5, ret));
                            string ret_desc = (Sep_Delimitador('|', 6, ret));
                            string ret_dataVenda = (Sep_Delimitador('|', 7, ret));
                            string ret_id = (Sep_Delimitador('|', 8, ret));
                            string ret_valorvenda = (Sep_Delimitador('|', 9, ret));
                            string ret_nulo3 = (Sep_Delimitador('|', 10, ret));
                            string ret_qrCod = (Sep_Delimitador('|', 11, ret));
                            string ret_inf = (Sep_Delimitador('|', 12, ret));
                            string ret_inf1 = (Sep_Delimitador('|', 13, ret));

                            idCfe = ret_id;
                            Ultimachave = ret_id;
                            retornoVendaSat = ret_qrCod;
                            retornoSat = ret_sat;

                            // MessageBox.Show(ret);

                            // si restorno do sat for igual 0600 para aprovacao da venda
                            if (ret_sat == "06000")
                            {
                                byte[] senhaBinaria = new byte[256];

                                //Descriptografa....................................................................................................
                                senhaBinaria = Convert.FromBase64String(ret_desc);

                                string senhaDescripto = ASCIIEncoding.ASCII.GetString(senhaBinaria);

                                //gera arquivo em txt aprovado......................................................................................
                                string strPathFile = pathDadosVendaAutorizada + "00" + lblNumCaixa.Text + ".txt";

                                using (FileStream fs = File.Create(strPathFile))
                                {
                                    using (StreamWriter sw = new StreamWriter(fs))
                                    {
                                        sw.Write(senhaDescripto);
                                    }
                                }

                                mes = DateTime.Now.Month.ToString();
                                ano = DateTime.Now.Year.ToString();

                                string enderecoExitente = (pathCustodia + ano + "\\" + mes + "\\" + "Cx" + frmVenda.numcaixa);

                                if (Directory.Exists(enderecoExitente))
                                {
                                    //Converte TXT para XML e Salva em Arquivo XML............................................................................................
                                    FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                                    byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                                    sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                                    FileStream srXml = new FileStream(enderecoExitente + "\\" + ret_id + ".xml", FileMode.Create, FileAccess.Write);
                                    StreamWriter wr = new StreamWriter(srXml);
                                    srXml.Write(bytes, 0, bytes.Length);
                                    sr.Close();
                                    srXml.Close();

                                    GerarUltimaVendaXml();
                                    MontarCupom();
                                }
                                else
                                {
                                    Directory.CreateDirectory(enderecoExitente);
                                    //Converte TXTpara XML e Salva em Arquivo XML............................................................................................
                                    FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                                    byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                                    sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                                    FileStream srXml = new FileStream(enderecoExitente + "\\" + ret_id + ".xml", FileMode.Create, FileAccess.Write);
                                    StreamWriter wr = new StreamWriter(srXml);
                                    srXml.Write(bytes, 0, bytes.Length);
                                    sr.Close();
                                    srXml.Close();

                                    GerarUltimaVendaXml();
                                    MontarCupom();
                                    this.Close();

                                    int mes_ = DateTime.Now.Month;
                                    MessageBox.Show("Por Favor Retirar Ralatório Gerencial Venda Total do Mês: " + (mes_ - 1) + ".", "Relatório Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    frmVenda.ImprimirRelatorioMes();
                                }

                                //ExportarItemVenda();
                                //ExportarPagamentoVendas();

                                SalvarFechamentoVenda_();
                                AlterarStatusFechar();
                                frmVenda.AlterarNumVendaNumCaixa();
                                frmVenda.AtualizarGridAberto();
                                LimparCampos();

                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Erro no S@T: " + ret, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                DeltarArquivoXml();
                                this.Close();
                            }
                        }
                    }
                }
                #endregion

                #region MP4200


                else if (impressora == "MP4200")
                {
                    //variavel do caminho para salvar arquivo xml antes de ir para SAT
                    string caminho = "";
                    string ret = "";
                    string mes, ano = "";

                    //funcoes para gerar arquivo xml..................................
                    saveFileDialog1.FileName = caminho;
                    IniciarXML();
                    PreencherIDE();
                    PreencherEmitente();
                    preenchedestinario();
                    preencheProduto();
                    preenchePagamento();
                    preencheInfo();

                    arqu = (pathVendaXML);

                    arquivoXML.Save(arqu + _numVenda + "Cx" + frmVenda.numcaixa + ".xml");

                    //lear caminho do arquivo xml...................................
                    LerArqTxt(arqu + _numVenda + "Cx" + frmVenda.numcaixa + ".xml");

                    //gera numero rondomico........................................
                    getNumberRandom();

                    PesquisarCodAticavaoXml();

                    codAtivacao = codAtivacao.Replace(" ", "");

                    //si arquivo for diferente de nulo entra no metodo............
                    if (arqu != null)
                    {
                        //recupera o retorno do SAT...............................
                        //ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.EnviarDadosVenda(sessao, codAtivacao, arqu)));

                        //separa os retorno por pig...............................
                        string ret_sessao = (Sep_Delimitador('|', 0, ret));
                        string ret_sat = "06000";
                        string ret_erro = (Sep_Delimitador('|', 2, ret));
                        string ret_err_ = (Sep_Delimitador('|', 3, ret));
                        string ret_nulo1_ = (Sep_Delimitador('|', 4, ret));
                        string ret_nulo2 = (Sep_Delimitador('|', 5, ret));
                        string ret_desc = (Sep_Delimitador('|', 6, ret));
                        string ret_dataVenda = (Sep_Delimitador('|', 7, ret));
                        string ret_id = (Sep_Delimitador('|', 8, ret));
                        string ret_valorvenda = (Sep_Delimitador('|', 9, ret));
                        string ret_nulo3 = (Sep_Delimitador('|', 10, ret));
                        string ret_qrCod = (Sep_Delimitador('|', 11, ret));
                        string ret_inf = (Sep_Delimitador('|', 12, ret));
                        string ret_inf1 = (Sep_Delimitador('|', 13, ret));

                        idCfe = ret_id;
                        Ultimachave = ret_id;
                        retornoVendaSat = ret_qrCod;
                        retornoSat = ret_sat;

                        // MessageBox.Show(ret);

                        // si restorno do sat for igual 0600 para aprovacao da venda
                        if (ret_sat == "06000")
                        {
                            byte[] senhaBinaria = new byte[256];

                            //Descriptografa....................................................................................................
                            //senhaBinaria = Convert.FromBase64String(ret_desc);

                            string senhaDescripto = ASCIIEncoding.ASCII.GetString(senhaBinaria);

                            //gera arquivo em txt aprovado......................................................................................
                            string strPathFile = pathDadosVendaAutorizada + "00" + lblNumCaixa.Text + ".txt";

                            using (FileStream fs = File.Create(strPathFile))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    sw.Write(senhaDescripto);
                                }
                            }

                            mes = DateTime.Now.Month.ToString();
                            ano = DateTime.Now.Year.ToString();

                            string enderecoExitente = (pathCustodia + ano + "\\" + mes + "\\" + "Cx" + frmVenda.numcaixa);

                            if (Directory.Exists(enderecoExitente))
                            {
                                //Converte TXT para XML e Salva em Arquivo XML............................................................................................
                                FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                                byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                                sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                                FileStream srXml = new FileStream(enderecoExitente + "\\" + ret_id + ".xml", FileMode.Create, FileAccess.Write);
                                StreamWriter wr = new StreamWriter(srXml);
                                srXml.Write(bytes, 0, bytes.Length);
                                sr.Close();
                                srXml.Close();

                                //GerarUltimaVendaXml();
                                //MontarCupom();
                            }
                            else
                            {
                                Directory.CreateDirectory(enderecoExitente);
                                //Converte TXTpara XML e Salva em Arquivo XML............................................................................................
                                FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                                byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                                sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                                FileStream srXml = new FileStream(enderecoExitente + "\\" + ret_id + ".xml", FileMode.Create, FileAccess.Write);
                                StreamWriter wr = new StreamWriter(srXml);
                                srXml.Write(bytes, 0, bytes.Length);
                                sr.Close();
                                srXml.Close();

                                GerarUltimaVendaXml();
                                MontarCupom();
                                this.Close();

                                int mes_ = DateTime.Now.Month;
                                MessageBox.Show("Por Favor Retirar Ralatório Gerencial Venda Total do Mês: " + (mes_ - 1) + ".", "Relatório Gerencial", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                frmVenda.ImprimirRelatorioMes();
                            }

                            //ExportarItemVenda();
                            //ExportarPagamentoVendas();

                            SalvarFechamentoVenda_();
                            AlterarStatusFechar();
                            frmVenda.AlterarNumVendaNumCaixa();
                            frmVenda.AtualizarGridAberto();
                            LimparCampos();

                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Erro no S@T: " + ret, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            DeltarArquivoXml();
                            this.Close();
                        }
                    }

                }
                #endregion

                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //public void SalvarEstoqueInicialMes()
        //{
        //    try
        //    {
        //        string data_atual = DateTime.Now.Month.ToString();
        //        int numeroSetor = 0;
        //        novaVenda = new VendaRegraNegocios();
        //        DataTable dadosTabelaVenda = new DataTable();

        //        dadosTabelaVenda = novaVenda.PesquisarUltimaVendaNumCaixa(frmVenda.numcaixa);

        //        if (dadosTabelaVenda.Rows.Count > 0)
        //        {
        //            string dataUlVenda = dadosTabelaVenda.Rows[0]["DATA"].ToString();
        //            dataUlVenda = DateTime.Now.Month.ToString();

        //            if (dataUlVenda != data_atual)
        //            {
        //                //pesquisar setores
        //                novoSetor = new SetorRegraNegocios();
        //                DataTable dadosTabelaSetor = new DataTable();
        //                dadosTabelaSetor = novoSetor.PesquisarSetorNumero();

        //                if (dadosTabelaSetor.Rows.Count > 0)
        //                {
        //                    for (int i = 0; i < dadosTabelaSetor.Rows.Count; i++)
        //                    {
        //                        //PESQUISAR NUMERO SETOR
        //                        numeroSetor = Convert.ToInt32(dadosTabelaSetor.Rows[i]["ID"].ToString());

        //                        //PESQUISAR A QTDE DO ESTOQUE INCIAL DO MES
        //                        novaVenda = new VendaRegraNegocios();
        //                        dadosTabelaVenda = new DataTable();
        //                        dadosTabelaVenda = novaVenda.PesquisarEstoqueIncialMes(numeroSetor);

        //                        if (dadosTabelaVenda.Rows.Count > 0)
        //                        {
        //                            qtdeEstoqueInicial = Convert.ToDecimal(dadosTabelaVenda.Rows[0]["ESTOQUE_INICIAL"].ToString());

        //                            novoEstoqueIncial = new EstoqueInicialRegraNegocios();
        //                            novoEstoqueIncial.CadastraEstoqueIncialMes(qtdeEstoqueInicial, DateTime.Now, frmVenda.idUsuarioLogado, numeroSetor);
        //                        }
        //                    }
        //                }

        //                CargaTabelaProdutoXml();
        //            }
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        //public void CargaTabelaProdutoXml()
        //{
        //    try
        //    {
        //        string mes = DateTime.Now.Month.ToString();
        //        novaConexao = new RegraNegocio.ConexaoRegraNegocios();

        //        SqlConnection conn = new SqlConnection(novaConexao.stringConexao);
        //        DataSet dsProduto = new DataSet();
        //        SqlDataAdapter daProduto = new SqlDataAdapter("SELECT COD_BARRA, NUM_DEPAR, DEPARTAM, DESCRICAO, PRECO, UNID, TRIB, ESTOQUE, GRANEL, SETOR, CUSTO FROM PRODUTO", conn);
        //        daProduto.Fill(dsProduto, "PRODUTO_INI");
        //        dsProduto.WriteXml(pathCargaProduto + "Mes-" + mes);
        //    }
        //    catch (Exception EX)
        //    {
        //        MessageBox.Show(EX.Message);
        //    }
        //}

        //linefeed and paper cutter
        public void feedAndCutter(string printerName, int numLines)
        {
            System.Threading.Thread.Sleep(500);
            this.esc.lineFeed(printerName, numLines);
            this.esc.CutPaper(printerName);
        }

        private void MontarCupomElgin()
        {
            throw new NotImplementedException();
        }

        private void MontarCupomDaruma()
        {
            try
            {
                int iRetorno;
                iRetorno = RegraNegocio.DLLsDaruma.iImprimirCFe_SAT_Daruma(dadosCupoDaruma, "1");

                if (iRetorno < 0)
                {
                    MessageBox.Show("Retorno do método: " + iRetorno, "DarumaFramework - NFCe", MessageBoxButtons.OK);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PesquisarImpressaoAutomatica()
        {
            try
            {
                novoParametro = new ParametroRegraNegocio();
                DataTable dadosTabelaParametro = new DataTable();

                dadosTabelaParametro = novoParametro.PesquisarImpressaoAutmoatica();

                if (dadosTabelaParametro.Rows.Count > 0)
                {
                    impressaoAutomatica = Convert.ToBoolean(dadosTabelaParametro.Rows[0]["IMPRESSAO_DIGITAL"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void FormaPagamentoComum()
        {
            try
            {
                if ((nomeImpressora == "DARUMA") || (nomeImpressora == "BEMASAT") || (nomeImpressora == "COMUM"))
                {
                    string dadosFormaPagto = "";
                    decimal vCFe = 0;

                    for (int t = 0; t < gdvTipoPgto.Rows.Count; t++)
                    {
                        string valor = "";

                        descTipoPagamento = gdvTipoPgto.Rows[t].Cells[0].Value.ToString();
                        valor = gdvTipoPgto.Rows[t].Cells[1].Value.ToString();

                        vCFe += Convert.ToDecimal(valor);

                        string traco = "";

                        if ((descTipoPagamento == "Dinheiro") || (descTipoPagamento == "DINHEIRO"))
                        {
                            if ((valor.Length == 2) || (valor.Length == 3) || (valor.Length == 4))
                            {
                                decimal v = Convert.ToDecimal(valor);
                                valor = v.ToString("N2");
                            }

                            if (valor.Length == 4)
                            {
                                traco = ".........................R$ ";
                            }
                            else if (valor.Length == 5)
                            {
                                traco = "........................R$ ";
                            }
                            else if (valor.Length == 6)
                            {
                                traco = ".......................R$ ";
                            }
                            else if (valor.Length == 7)
                            {
                                traco = "......................R$ ";
                            }

                            else if (valor.Length == 8)
                            {
                                traco = "......................R$ ";
                            }
                        }
                        else if ((descTipoPagamento == "Aberto") || (descTipoPagamento == "ABERTO"))
                        {
                            if (valor.Length == 4)
                            {
                                traco = "..............................R$ ";
                            }
                            else if (valor.Length == 5)
                            {
                                traco = ".............................R$ ";
                            }
                            else if (valor.Length == 6)
                            {
                                traco = "............................R$ ";
                            }
                            else if (valor.Length == 7)
                            {
                                traco = "..........................R$ ";
                            }
                        }
                        else
                        {
                            if (valor.Length == 4)
                            {
                                traco = "..............................R$ ";
                            }
                            else if (valor.Length == 5)
                            {
                                traco = ".............................R$ ";
                            }
                            else if (valor.Length == 6)
                            {
                                traco = "...........................R$ ";
                            }
                            else if (valor.Length == 7)
                            {
                                traco = "..........................R$ ";
                            }
                        }

                        dadosFormaPagto += ("\n" + descTipoPagamento + traco + valor.ToString()).ToString() + "\n";
                        D = dadosFormaPagto.ToString();
                    }

                    //observacao do contribuinte..............................................................................
                    string vTroco = txtTroco.Text;

                    vTroco = vTroco.Replace(" ", "");

                    string traco_ = "Troco: ........................R$ ";

                    if (vTroco.Trim().Length == 4)
                    {
                        traco_ = "Troco: ..........................R$ ";
                    }
                    else if (vTroco.Trim().Length == 5)
                    {
                        traco_ = "Troco: .........................R$ ";
                    }
                    else if (vTroco.Trim().Length == 6)
                    {
                        traco_ = "Troco: ........................R$ ";
                    }
                    else if (vTroco.Trim().Length == 7)
                    {
                        traco_ = "Troco: ........................R$ ";
                    }

                    string total_ = "";
                    string t_ = txtTotal.Text;

                    if (t_.Length == 4)
                    {
                        total_ = "TOTAL: ..........................R$ ";
                    }
                    else if (t_.Length == 5)
                    {
                        total_ = "TOTAL: .........................R$ ";
                    }
                    else if (t_.Length == 6)
                    {
                        total_ = "TOTAL: ........................R$ ";
                    }
                    else if (t_.Length == 7)
                    {
                        total_ = "TOTAL: .......................R$ ";
                    }

                    string operadoComp = frmVenda.operadorAtuante;
                    operadoComp = operadoComp.Replace(" ", "");

                    decimal somaAliq = ((aliqDia * Convert.ToDecimal(t_)) / 100);

                    string obsContribuinte = (total_ + txtTotal.Text + "\n" +
                                              traco_.ToString() + txtTroco.Text + "\n" +
                                              "------------------------------------------\n" +
                                              "OBSERVAÇOES DO CONTRIBUINTE                  \n" +
                                              "Nº Caixa:" + lblNumCaixa + ". CUPOM Nº" + frmVenda.numCupom + "\n" +
                                              "OP:" + operadoComp + ". - TURNO:" + frmVenda.periodoAtuante + "\n" +
                                              "Data:" + DateTime.Now.Date.ToShortDateString() + "  Hora:" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "\n" +
                                              "TROCA DE PRODUTO(S) SOMENTE COM O CUPOM \n" +
                                              "OBRIGADO...VOLTE SEMPRE!!!.\n" +
                                              "IBPT: Valor Aprox. trib: " + somaAliq +"\n\n");
                    E = obsContribuinte.ToString() + "\n\n\n";
                }
                else if (nomeImpressora == "ELGIN")
                {
                    string valor = "";
                    string dadosFormaPagto = "";
                    string dadosCupom = "";
                    decimal vCFe = 0;
                    string traco = "";
                    decimal tt_ = Convert.ToDecimal(somaCompra);

                    tt_ = Convert.ToDecimal(somaCompra);
                    string t_ = "";

                    t_ = tt_.ToString("N2");
                    t_ = t_.PadLeft(8, ' ');

                    // string total_ = "TOTAL..................................." + t_;
                    string total_ = "TOTAL:                                  " + t_;

                    for (int t = 0; t < gdvTipoPgto.Rows.Count; t++)
                    {
                        valor = "";
                        descTipoPagamento = gdvTipoPgto.Rows[t].Cells[0].Value.ToString();
                        valor = gdvTipoPgto.Rows[t].Cells[1].Value.ToString();

                        vCFe += Convert.ToDecimal(valor);

                        if ((descTipoPagamento == "DINHEIRO") || (descTipoPagamento == "Dinheiro"))
                        {
                            traco = traco.Trim();
                            traco = traco.PadLeft(35, ' ');
                        }
                        else
                        {
                            traco = traco.Trim();
                            traco = traco.PadLeft(36, ' ');
                        }

                        dadosFormaPagto += (descTipoPagamento + traco + Convert.ToDecimal(valor).ToString("N2")) + "\n";
                    }

                    dadosCupom = (total_ + "\n" + dadosFormaPagto);

                    D = dadosCupom.ToString();

                    //observacao do contribuinte..............................................................................
                    string vTroco = txtTroco.Text;
                    DateTime date;
                    vTroco = vTroco.Replace(" ", "");
                    vTroco = vTroco.PadLeft(8, ' ');

                    //  string traco_ = "Troco:...................................."+ vTroco;
                    string traco_ = "TROCO:                                  " + vTroco;
                  
                    string operadoComp = frmVenda.operadorAtuante;
                    operadoComp = operadoComp.Replace(" ", "");
                    decimal somaAliq = ((aliqDia * Convert.ToDecimal(t_)) / 100);

                    string obsContribuinte = (
                                              traco_.ToString() + "\n" +
                                              "------------------------------------------------\n" +
                                              "OBSERVAÇOES DO CONTRIBUINTE                  \n" +
                                              "Nº Caixa:" + lblNumCaixa + ". CUPOM Nº" + frmVenda.numCupom + "\n" +
                                              "OP:" + operadoComp + ". - TURNO:" + frmVenda.periodoAtuante + "\n" +
                                              "Data:" + DateTime.Now.Date.ToShortDateString() + "  Hora:" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "\n" +
                                              "PLACA:" + placaComp + "KM:" + kmComp + "\n" +
                                              "TROCA DE PRODUTO(S) SOMENTE COM O CUPOM \n" +
                                              "OBRIGADO...VOLTE SEMPRE!!!.\n" + "IBPT: Valor Aprox. trib: " + somaAliq.ToString("C2") +"\n\n");

                    E = obsContribuinte.ToString();
                }
                else if (nomeImpressora == "MP2500")
                {
                    string dadosFormaPagto = "";
                    decimal vCFe = 0;
                    string traco = ".................................";
                    decimal tt_ = Convert.ToDecimal(somaCompra);

                    tt_ = Convert.ToDecimal(somaCompra);
                    string t_ = "";

                    t_ = tt_.ToString("N2");
                    t_ = t_.PadLeft(8, ' ');

                    // string total_ = "TOTAL..................................." + t_;
                    string total_ = "TOTAL:                                  " + t_;

                    for (int t = 0; t < gdvTipoPgto.Rows.Count; t++)
                    {
                        string valor = "";

                        descTipoPagamento = gdvTipoPgto.Rows[t].Cells[0].Value.ToString();
                        valor = gdvTipoPgto.Rows[t].Cells[1].Value.ToString();

                        vCFe += Convert.ToDecimal(valor);

                        if ((descTipoPagamento == "DINHEIRO") || (descTipoPagamento == "Dinheiro"))
                        {
                            //  traco = ".................................";
                            traco = "                                 ";
                        }
                        else
                        {
                            //  traco = "...................................";
                            traco = "                                  ";
                        }

                        valor = valor.PadLeft(8, ' ');

                        dadosFormaPagto += (total_ + "\n" + descTipoPagamento + traco + valor) + "\n";
                        D = dadosFormaPagto.ToString();
                    }

                    //observacao do contribuinte..............................................................................
                    string vTroco = txtTroco.Text;
                    DateTime date;
                    vTroco = vTroco.Replace(" ", "");
                    vTroco = vTroco.PadLeft(8, ' ');

                    //  string traco_ = "Troco:...................................."+ vTroco;
                    string traco_ = "TROCO:                                  " + vTroco;

                    string operadoComp = frmVenda.operadorAtuante;
                    operadoComp = operadoComp.Replace(" ", "");

                    string obsContribuinte = (
                                              traco_.ToString() + "\n" +
                                              "------------------------------------------------\n" +
                                              "OBSERVAÇOES DO CONTRIBUINTE                  \n" +
                                              "Nº Caixa:" + lblNumCaixa + ". CUPOM Nº" + frmVenda.numCupom + "\n" +
                                              "OP:" + operadoComp + ". - TURNO:" + frmVenda.periodoAtuante + "\n" +
                                              "Data:" + DateTime.Now.Date.ToShortDateString() + "  Hora:" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "\n" +
                                              "TROCA DE PRODUTO(S) SOMENTE COM O CUPOM \n" +
                                              "OBRIGADO...VOLTE SEMPRE!!!.\n");

                    E = obsContribuinte.ToString();
                }
                else if (nomeImpressora == "LPT")
                {
                    string dadosFormaPagto = "";
                    decimal vCFe = 0;
                    string traco = ".................................";
                    decimal tt_ = Convert.ToDecimal(somaCompra);

                    tt_ = Convert.ToDecimal(somaCompra);
                    string t_ = "";

                    t_ = tt_.ToString("N2");
                    t_ = t_.PadLeft(8, ' ');

                    // string total_ = "TOTAL..................................." + t_;
                    string total_ = "TOTAL:                                                 " + t_;

                    for (int t = 0; t < gdvTipoPgto.Rows.Count; t++)
                    {
                        string valor = "";

                        descTipoPagamento = gdvTipoPgto.Rows[t].Cells[0].Value.ToString();
                        valor = gdvTipoPgto.Rows[t].Cells[1].Value.ToString();

                        vCFe += Convert.ToDecimal(valor);

                        if ((descTipoPagamento == "DINHEIRO") || (descTipoPagamento == "Dinheiro"))
                        {
                            //  traco = ".................................";
                            traco = "                                               ";
                        }
                        else
                        {
                            //  traco = "...................................";
                            traco = "                                                  ";
                        }

                        valor = valor.PadLeft(8, ' ');

                        dadosFormaPagto += (total_ + "\n" + descTipoPagamento + traco + valor) + "\n";
                        D = dadosFormaPagto.ToString();
                    }

                    //observacao do contribuinte..............................................................................
                    string vTroco = txtTroco.Text;
                    DateTime date;
                    vTroco = vTroco.Replace(" ", "");
                    vTroco = vTroco.PadLeft(8, ' ');

                    //  string traco_ = "Troco:...................................."+ vTroco;
                    string traco_ = "TROCO:                                                  " + vTroco;

                    string operadoComp = frmVenda.operadorAtuante;
                    operadoComp = operadoComp.Replace(" ", "");

                    string obsContribuinte = (
                                              traco_.ToString() + "\n" +
                                              "---------------------------------------------------------------\n" +
                                              "OBSERVAÇOES DO CONTRIBUINTE                  \n" +
                                              "Nº Caixa:" + lblNumCaixa + ". CUPOM Nº" + frmVenda.numCupom + "\n" +
                                              "OP:" + operadoComp + ". - TURNO:" + frmVenda.periodoAtuante + "\n" +
                                              "Data:" + DateTime.Now.Date.ToShortDateString() + "  Hora:" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + "\n" +
                                              "TROCA DE PRODUTO(S) SOMENTE COM O CUPOM \n" +
                                              "OBRIGADO...VOLTE SEMPRE!!!.\n");

                    E = obsContribuinte.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VendeItemComum()
        {
            try
            {
                string textoVendeItemCupomNaoFiscal = "";
                string descricaoItemCupomNaoFiscal = "";
                string espaco1 = " ";
                string espaco2 = "  ";
                string espaco7 = "                                       ";
                string espaco5 = "     ";
                string espaco3 = "    ";
                string pularlinha1 = "\n";
                string pontoFinal = "\r";

                somaCompra = 0;
                int numCupom = frmVenda.numVenda;
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                dadosTabelaVenda = novaVenda.PesquisaTabelaVenda(frmVenda.numCupom, frmVenda.numcaixa);
                nomeImpressora = nomeImpressora.Replace(" ", "");

                B = "";
                C = "";

                //modelovisual do cupom nao fiscal............................................................................
                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    if (nomeImpressora == "COMUM")
                    {
                        B = ("------------------------------------------\n" +
                             "CPF/CNPJ:" + lblCnpjCpf.Text + "\n" +
                             "------------------------------------------\n" +
                             "            EXTRATO No." + frmVenda.numCupom + "\n" +
                             "              PRÉ-VENDA                   \n" +
                             "------------------------------------------\n" +
                             " Item Codigo Descricao Un Qtde Preco Total\n" +
                             "------------------------------------------\n"
                             );
                    }
                    else if (nomeImpressora == "MP2500")
                    {
                        B = ("------------------------------------------------\n" +
                             "EXTRATO No." + frmVenda.numCupom + "\n" +
                             "PRÉ-VENDA                                       \n" +
                             "----------------------------------------------- \n" +
                              "ITEM COD DESCRICAO         UN  QTD PRECO  TOTAL\n" +
                             "-----------------------------------------------");
                    }
                    else if (nomeImpressora == "LPT")
                    {
                        B = ("---------------------------------------------------------------\n" +
                             "                           EXTRATO No." + frmVenda.numCupom + "\n" +
                             "                                   PRÉ-VENDA                   \n" +
                             "---------------------------------------------------------------\n" +
                             "# Item  Codigo    Descricao    Un     Qtde   Preco  Total      \n" +
                             "---------------------------------------------------------------");
                    }
                    else if (nomeImpressora == "ELGIN")
                    {
                        if (cupomImagem == true)
                        {
                            B = ("------------------------------------------------------\n" +
                                 "                   EXTRATO No." + frmVenda.numCupom + "\n" +
                                 "                       PRÉ-VENDA                       \n" +
                                 "-------------------------------------------------------\n" +
                                 "ITEM COD DESCRICAO            UN  QTD  PRECO      TOTAL\n" +
                                 "---------------------------------------------------------"
                                 );
                        }
                        else
                        {
                            LerXmlCustodia();

                            B = ("------------------------------------------------\n" +
                                 "CPF/CNPJ:" + lblCnpjCpf.Text + "\n" +
                                 "------------------------------------------------\n" +
                                 "                EXTRATO No." + nCFe + "\n" +
                                 "           CUPOM FISCAL ELETRONICO - SAT        \n" +
                                 "------------------------------------------------\n" +
                                 "ITEM COD DESCR        UN    QTD    PRECO  TOTAL \n" +
                                 "------------------------------------------------"
                                 );
                        }
                    }

                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        descri = dadosTabelaVenda.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                        item = dadosTabelaVenda.Rows[i]["ITEM"].ToString();
                        codigoB = dadosTabelaVenda.Rows[i]["COD_BARRA"].ToString();
                        qtde = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["QUANT"]).ToString();
                        preco = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["PRECO"]).ToString();
                        alicota = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                        string total = dadosTabelaVenda.Rows[i]["TOTAL"].ToString();
                        string un = dadosTabelaVenda.Rows[i]["UNID"].ToString();

                        somaCompra += Convert.ToDecimal(total);

                        //replace retira espaco nulo e vazio....................................
                        codigoB = codigoB.Replace(" ", "");
                        alicota = alicota.Replace(" ", "");
                        un = un.Replace(" ", "");

                        //tratamento de tamanhos dos abjetos....................................

                        if ((nomeImpressora == "COMUM"))
                        {
                            item = item.PadLeft(3, '0');

                            codigoB = codigoB.Substring(8, 5);

                            descri += espaco7;

                            if (descri.Length < 3)
                            {
                                descri += espaco7;
                            }

                            if (descri.Length > 10)
                            {
                                descri = descri.Substring(0, 10);
                            }

                            decimal qtde_ = Convert.ToDecimal(qtde);

                            string pre_p = Convert.ToString(preco);

                            if (pre_p.Length == 4)
                            {
                                pre_p = pre_p.PadLeft(1, ' ');
                            }

                            if (total.Length == 4)
                            {
                                total = total.PadLeft(1, ' ');
                            }

                            textoVendeItemCupomNaoFiscal = (item + " " + codigoB + " " + descri + " " + un + " " + qtde_.ToString("N3") + " " + pre_p + " " + total + "\r\n");
                            C += textoVendeItemCupomNaoFiscal + "\n";
                        }

                        else if (nomeImpressora == "LPT")
                        {
                            item = item.PadLeft(3, '0');

                            codigoB = codigoB.Substring(8, 5);

                            descri = descri.PadRight(50, ' ');

                            if (descri.Length >= 12)
                            {
                                descri = descri.Substring(0, 12);
                            }

                            decimal qtde_ = Convert.ToDecimal(qtde);
                            qtde = qtde_.ToString("N3");

                            string preco_ = Convert.ToString(preco);

                            preco_ = preco_.PadLeft(7, ' ');
                            total = total.PadRight(7, ' ');
                            qtde = qtde.PadLeft(7, ' ');

                            textoVendeItemCupomNaoFiscal = (item + "" + codigoB + "" + descri + "" + un + " " + qtde + "" + preco_ + "" + total + "\n");
                            C += textoVendeItemCupomNaoFiscal;
                        }

                        else if (nomeImpressora == "MP2500")
                        {
                            item = item.PadLeft(3, '0');

                            codigoB = codigoB.Substring(8, 5);

                            descri += espaco7;

                            if (descri.Length < 3)
                            {
                                descri += espaco7;
                            }

                            if (descri.Length > 11)
                            {
                                descri = descri.Substring(0, 11);
                            }

                            decimal qtde_ = Convert.ToDecimal(qtde);
                            qtde_ = Convert.ToDecimal(qtde_.ToString("N2"));
                            string q = Convert.ToString(qtde_);
                            q = q.PadLeft(7, ' ');

                            string pre_p = Convert.ToString(preco);

                            pre_p = pre_p.PadLeft(7, ' ');
                            total = total.PadLeft(7, ' ');

                            textoVendeItemCupomNaoFiscal = (item + " " + codigoB + " " + descri + " " + un + " " + q + " " + pre_p + " " + total + "\r");
                            C += textoVendeItemCupomNaoFiscal + "\n";
                        }

                        else if (nomeImpressora == "ELGIN")
                        {
                            item = item.PadLeft(3, '0');

                            codigoB = codigoB.Substring(8, 5);

                            descri += espaco7;

                            if (descri.Length < 3)
                            {
                                descri += "                   ";
                            }

                            if (descri.Length > 11)
                            {
                                descri = descri.Substring(0, 11);
                            }

                            decimal qtde_ = Convert.ToDecimal(qtde);
                            qtde_ = Convert.ToDecimal(qtde_.ToString("N2"));
                            string q = Convert.ToString(qtde_);
                            q = q.PadLeft(7, ' ');

                            string pre_p = Convert.ToString(preco);

                            pre_p = pre_p.PadLeft(7, ' ');
                            total = total.PadLeft(7, ' ');

                            textoVendeItemCupomNaoFiscal = (item + " " + codigoB + " " + descri + " " + un + " " + q + " " + pre_p + " " + total + "\r");
                            C += textoVendeItemCupomNaoFiscal + "\n";

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AbreCupomComum()
        {
            try
            {
                string espaco1 = " ";
                string espaco2 = "  ";
                string pularlinha1 = "\n";
                string pontoFinal = "\r";

                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabelaParametro = new DataTable();
                dadosTabelaParametro = novoParametro.PesquisaParametroE();

                nomeImpressora = nomeImpressora.Replace(" ", "");

                string nomeCliente = dadosTabelaParametro.Rows[0]["NOME_FANTASIA"].ToString();
                string enderecoCliente = dadosTabelaParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                string numeroCliente = dadosTabelaParametro.Rows[0]["NUMERO"].ToString();
                string bairroCliente = dadosTabelaParametro.Rows[0]["BAIRRO"].ToString();
                string cidadeCliente = dadosTabelaParametro.Rows[0]["CIDADE"].ToString();
                string ufCliente = dadosTabelaParametro.Rows[0]["UF"].ToString();
                string cepCliente = dadosTabelaParametro.Rows[0]["CEP"].ToString();
                string cnpjCliente = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                string ieCliente = dadosTabelaParametro.Rows[0]["IE"].ToString();

                if (nomeImpressora == "COMUM")
                {
                    A = "";
                    A = ("-------------------------------------\n" +
                         nomeCliente + "\n" +
                         enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                         bairroCliente + "\n" +
                         cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                         "CNPJ:" + cnpjCliente + " - " + "IE:" + ieCliente + pontoFinal + "\n");
                }
                else if (nomeImpressora == "MP2500")
                {
                    A = ("-------------------------------------------------\n" +
                         nomeCliente + "\n" +
                         enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                         bairroCliente + "\n" +
                         cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                         "CNPJ:" + cnpjCliente + " - " + "IE:" + ieCliente + pontoFinal);
                }
                else if (nomeImpressora == "LPT")
                {
                    A = ("--------------------------------------------------------------------------\n" +
                      nomeCliente + "\n" +
                      enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                      bairroCliente + "\n" +
                      cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                      "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal);
                }
                else if (nomeImpressora == "ELGIN")
                {
                    A = ("-----------------------------------------------\n" +
                     nomeCliente + "\n" +
                     enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                     bairroCliente + "\n" +
                     cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                     "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region CUPOM PORTA

        public void LerXmlCustodia()
        {
            string ano, mes = "";

            ano = DateTime.Now.Year.ToString();
            mes = DateTime.Now.Month.ToString();

            string endereco = pathCustodia + ano + "\\" + mes + "\\" + "Cx" + frmVenda.numcaixa;

            //pegar valor ultimo idCfe da venda aprovada................................................................
            XmlTextReader x = new XmlTextReader(endereco + "\\" + idCfe + ".xml");

            while (x.Read())
            {
                if (x.NodeType == XmlNodeType.Element && x.Name == "xNome")
                    xNome = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "xFant")
                    xFant = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "xLgr")
                    xLgr = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "xBairro")
                    xBairro = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "nro")
                    nro = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "xMun")
                    xMun = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "CEP")
                    CEP = (x.ReadString());

                //if (x.NodeType == XmlNodeType.Element && x.Name == "CNPJ")
                //    cnpj = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "IE")
                    IE = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "IM")
                    IM = (x.ReadString());

                //descricao

                if (x.NodeType == XmlNodeType.Element && x.Name == "det nItem")
                    item = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "cProd")
                    cProd = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "xProd")
                    xProd = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "uCom")
                    uCom = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "qCom")
                    qCom = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "vProd")
                    vProd = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "vCFe")
                    vCFe = (x.ReadString());


                //descricao pagamento

                if (x.NodeType == XmlNodeType.Element && x.Name == "cMP")
                    cMP = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "vMP")
                    vMP = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "vTroco")
                    vTroco = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "nCFe")
                    nCFe = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "CPF")
                    cpfDest = (x.ReadString());


                //observacoes contribuintes....................................................................

                //if (x.NodeType == XmlNodeType.Element && x.Name == "numeroCaixa")
                //   string numcaixa = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "nserieSAT")
                    nserieSAT = (x.ReadString());

                //IMPRIMIR QRcODE
                if (x.NodeType == XmlNodeType.Element && x.Name == "assinaturaQRCODE")
                    assinaturaQRCODE = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "nCFe")
                    nCFe = (x.ReadString());
            }

            x.Close();
        }

        public void LerXmlQrCode()
        {
            try
            {
                //pegar valor ultimo idCfe da venda aprovada................................................................

                string ano, mes = "";
                ano = DateTime.Now.Year.ToString();
                mes = DateTime.Now.Month.ToString();

                string endereco = (pathCustodia + "\\" + ano + "\\" + mes + "\\" + "Cx" + frmVenda.numcaixa);

                XmlTextReader x = new XmlTextReader(endereco + "\\" + idCfe + ".xml");

                while (x.Read())
                {
                    if (x.NodeType == XmlNodeType.Element && x.Name == "assinaturaQRCODE")
                        qrCodProd = (x.ReadString());

                    if (x.NodeType == XmlNodeType.Element && x.Name == "nserieSAT")
                        numSerieSat = (x.ReadString());

                    codProd = idCfe;
                }

                x.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #endregion

        public void MontarCupom()
        {
            string dadosTipoPagamento = "";
            int idRetorno_ = 0;
            novoParametro = new ParametroRegraNegocio();
            DataTable dadosTabelaParametro = new DataTable();

            dadosTabelaParametro = novoParametro.PesquisaParametroE();

            if (dadosTabelaParametro.Rows.Count > 0)
            {
                aliquotaFec = Convert.ToDecimal(dadosTabelaParametro.Rows[0]["ALIQUOTA_DIA"].ToString());
                cnpj = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                placaAutorizar = Convert.ToBoolean(dadosTabelaParametro.Rows[0]["PLACA"].ToString());
                //portaComImpressora = portaComImpressora.Replace(" ", "");
                //portaComImpressora = portaComImpressora;

                frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                portaComImpressora = frmVenda.numComimp.ToString();

                string mensagemCupom = dadosTabelaParametro.Rows[0]["MSG"].ToString();

                idRetorno_ = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                idRetorno_ = RegraNegocio.MP2032.IniciaPorta(portaComImpressora);

                LerXmlCustodia();

                string cabecalho = ("------------------------------------------\n" +
                                    xFant + "\n" +
                                    xNome + "\n" +
                                    xLgr + ", " + nro + "\n" + xMun + "-" + CEP + "\n" +
                                    "CNPJ: " + cnpj + "\n" +
                                    "IE:" + IE + "-" + "IM:" + IM + "\n");

                //cabecalho................................................................................................
                //iRetorno = FormataTX(cabecalho, 2, 0, 0, 1, 1);

                //idRetorno_ = RegraNegocio.MP2032.BematechTX(cabecalho);



                //alinahr esquerda
                align = "" + (char)27 + (char)97 + (char)0;
                idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);

                A_ = cabecalho.ToString();

                string dadosCupom = ("------------------------------------------\n" +
                                     " CPF/CNPJ:" + lblCnpjCpf.Text.Trim() + "\n" +
                                     "------------------------------------------\n" +
                                     "         EXTRATO No." + nCFe + "\n" +
                                     "      CUPOM FISCAL ELETRONICO - SAT       \n" +
                                     "------------------------------------------\n" +
                                     "#|COD|DESC|QT|UN|VUNR$|(VLR$)*DESC|VLIT R$\n" +
                                     "------------------------------------------\r\n");

                //idRetorno_ = RegraNegocio.MP2032.BematechTX(dadosCupom);
                B_ = dadosCupom.ToString();

                novaVenda = new VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();

                dadosTabelaVenda = novaVenda.PesquisaVendas(frmVenda.numCupom, frmVenda.numcaixa);

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    decimal totalVenda = 0;
                    string unid_prod = "";
                    decimal valorP = 0;

                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        xitem = dadosTabelaVenda.Rows[i]["ITEM"].ToString();
                        cProd = dadosTabelaVenda.Rows[i]["COD_BARRA"].ToString();
                        xProd = dadosTabelaVenda.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                        uCom = dadosTabelaVenda.Rows[i]["UNID"].ToString();
                        qCom = dadosTabelaVenda.Rows[i]["QUANT"].ToString();
                        vProd = dadosTabelaVenda.Rows[i]["PRECO"].ToString();
                        vCFe = dadosTabelaVenda.Rows[i]["TOTAL"].ToString();
                        unid_prod = dadosTabelaVenda.Rows[i]["UNID"].ToString();

                        cProd = cProd.Replace(" ", "");
                        uCom = uCom.Replace(" ", "");
                        qCom = qCom.Replace(" ", "");
                        vProd = vProd.Replace(" ", "");
                        vCFe = vCFe.Replace(" ", "");
                        unid_prod = unid_prod.Replace(" ", "");

                        totalVenda += Convert.ToDecimal(vCFe);

                        //taratmento de variaveis
                        xitem = xitem.PadLeft(3, '0');
                        cProd = cProd.Substring(8, 5);

                        if (xProd.Length < 11)
                        {
                            xProd = xProd + "                   ";
                        }

                        xProd = xProd.Substring(0, 11);

                        valorP.ToString("N2");

                        C_ += (xitem + " " + cProd + " " + xProd + " " + uCom + " " + Convert.ToDecimal(qCom).ToString("N3") + " " + Convert.ToDecimal(vProd).ToString("N3") + " " + Convert.ToDecimal(vCFe).ToString("N2") + "\r\n");
                        //idRetorno_ = 0;
                        //idRetorno_ = BematechTX(dadosVenda + "\n");
                    }


                    for (int t = 0; t < gdvTipoPgto.Rows.Count; t++)
                    {
                        string valor = "";

                        descTipoPagamento = gdvTipoPgto.Rows[t].Cells[0].Value.ToString();
                        valor = gdvTipoPgto.Rows[t].Cells[1].Value.ToString();
                        decimal v = Convert.ToDecimal(valor);

                        string traco = "..........................R$";

                        if ((descTipoPagamento.Length == 8) && (valor.Length == 6))
                        {
                            traco = ".........................R$";
                        }

                        if ((descTipoPagamento.Length == 8) && (valor.Length == 3))
                        {
                            traco = ".........................R$";
                        }

                        if ((descTipoPagamento.Length == 8) && (valor.Length == 5))
                        {
                            traco = ".........................R$";
                        }

                        if ((descTipoPagamento.Length == 8) && (valor.Length == 4))
                        {
                            traco = "...........................R$";
                        }

                        if ((descTipoPagamento.Length == 6) && (valor.Length == 6))
                        {
                            traco = "...........................R$";
                        }

                        if ((descTipoPagamento.Length == 6) && (valor.Length == 5))
                        {
                            traco = ".............................R$";
                        }

                        if ((descTipoPagamento.Length == 6) && (valor.Length == 4))
                        {
                            traco = ".............................R$";
                        }

                        dadosTipoPagamento += (descTipoPagamento + traco + v.ToString("N2")).ToString();
                        //idRetornoMp = BematechTX(dadosTipoPagamento);
                        //idRetorno_ = RegraNegocio.MP2032.BematechTX(dadosTipoPagamento);

                        F_ = vCFe.ToString();
                        D_ = dadosTipoPagamento.ToString();
                    }


                    //observacao do contribuinte..............................................................................

                    string traco_ = "Troco: ..........................R$";

                    if (vTroco.Length == 4)
                    {
                        traco_ = "Troco: ............................R$";
                    }
                    else if (vTroco.Length == 5)
                    {
                        traco_ = "Troco: ...........................R$";
                    }
                    else if (vTroco.Length == 6)
                    {
                        traco_ = "Troco: ..........................R$";
                    }
                    else if (vTroco.Length == 7)
                    {
                        traco_ = "Troco:.........................R$";
                    }

                    string total_ = "TOTAL.............................";

                    if (vCFe.Length == 4)
                    {
                        total_ = "TOTAL: ............................R$";
                    }
                    else if (vCFe.Length == 5)
                    {
                        total_ = "TOTAL: ...........................R$";
                    }
                    else if (vCFe.Length == 6)
                    {
                        total_ = "TOTAL: ..........................R$";
                    }
                    else if (vCFe.Length == 7)
                    {
                        total_ = "TOTAL: ...........................R$";
                    }

                    //verificar se a placa deve sair impresso no cupom.............................

                    string placa = "";
                    string km = "";

                    if (autorizarPlaca == true)
                    {
                        placa = "PLACA: ";
                        km = " - KM: ";
                    }
                    else
                    {
                        placa = "";
                        km = "";
                    }

                    //Soma do valor de tributo dos valor da venda
                    decimal somaAliquotaTotal = ((totalVenda * aliquotaFec) / 100);

                    string op = frmVenda.operadorAtuante.ToString();
                    op = op.Replace(" ", "");

                    string obsContribuinte = ("\n" + total_ + totalVenda + "\r\n"
                                               + dadosTipoPagamento + "\n" +
                                              traco_ + vTroco.ToString() + "\n" +
                                              "-----------------------------------------\n" +
                                               "Consulte QrCode Cupom no app DeOlhoNaNota" +
                                              "\n------------------------------------------\n" +
                                              "OBSERVAÇOES DO CONTRIBUINTE             \n" +
                                              "Fonte IBPT R$ Aprox Conforme Lei 12.741/12\n" +
                                              "Valor Aproximado Tributos:" + somaAliquotaTotal.ToString("C2") + "\n" +
                                              "Nº Caixa:" + lblNumCaixa + ". CUPOM Nº" + frmVenda.numCupom + "\n" +
                                              "OP:" + op + " - TURNO:" + frmVenda.periodoAtuante + "\n" +
                                               placa + lblNomePlaca.Text + km + lblKmPlaca.Text + "\n" +
                                               mensagemCupom + "\n"
                                               );

                    //idRetorno_ = RegraNegocio.MP2032.BematechTX(obsContribuinte);

                    E_ = obsContribuinte.ToString();

                    //dados do Sat............................................................................................

                    LerXmlQrCode();

                    string dadosSat = ("------------------------------------------" +
                                       "        Numero SAT: " + numSerieSat + "\n" +
                                       "         " + DateTime.Now.ToString() + "\n" +
                                       "------------------------------------------");

                    //idRetorno_ = RegraNegocio.MP2032.BematechTX(dadosSat);
                    F_ = dadosSat.ToString();

                    textoCupom = (A_ + B_ + C_ + E_ + F_).ToString();

                    idRetorno_ = BematechTX(textoCupom);

                    if (idRetorno_ > 0)
                    {
                        //Montar e  Imprimir QrCode e codigo de Barra................................................

                        LerXmlQrCode();

                        idRetorno = ConfiguraModeloImpressora(7);
                        idRetorno = IniciaPorta(portaComImpressora); //inicia a porta com o IP digitado

                        //idRetorno = ConfiguraCodigoBarras(50, 0, 0, 1, 0);
                        //idRetorno = ImprimeCodigoBarrasCODE128(idCfe);

                        //alinhar ao centro

                        idCfe = idCfe.Replace("CFe", "");
                        idCfe = idCfe.Replace(" ", "");

                        align = "" + (char)27 + (char)97 + (char)1;
                        idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);
                        ConfiguraCodigoBarras(50, 0, 1, 1, 0);

                        align = "" + (char)27 + (char)97 + (char)1;
                        idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);
                        ImprimeCodigoBarrasCODE128(idCfe);

                        align = "" + (char)27 + (char)97 + (char)1;
                        idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);
                        idRetorno = ImprimeCodigoQRCODE(1, 5, 0, 6, 1, qrCodProd);

                        idRetorno = RegraNegocio.MP2032.BematechTX("\n\n\n\n");
                        idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(0);

                        idRetorno = RegraNegocio.MP2032.FechaPorta();
                    }
                    else
                    {
                        MessageBox.Show("Venda Realizado com Sucesso, Erro ao Imprimir Cupom.", "Atenção");
                        idRetorno = RegraNegocio.MP2032.FechaPorta();
                    }
                }
            }
        }

        public void ImprimirCabecalhoBemasat()
        {

            idRetorno = BematechTX("------------------------------------------");
            idRetorno = BematechTX("            " + xFant + "                 ");
            idRetorno = BematechTX("            " + xNome + "                 ");
            idRetorno = BematechTX(xLgr + ", " + nro + "-" + xMun + "-" + CEP);
            idRetorno = BematechTX("CNPJ:" + cnpj + "                          ");
            idRetorno = BematechTX("IE:" + IE + "-" + "IM:" + IM + "           ");
        }

        public void ImprimirDadosVenda()
        {
            FormataTX("------------------------------------------", 0, 0, 0, 0, 0);
            FormataTX("CPF/CNPJ:" + cpfDest, 0, 0, 0, 0, 0);
            FormataTX("EXTRATO No." + nCfeEmit, 0, 0, 0, 0, 0);
            FormataTX("     CUPOM FISCAL ELETRONICO - SAT        ", 0, 0, 0, 0, 0);
            FormataTX("------------------------------------------", 0, 0, 0, 0, 0);
            FormataTX("#|COD|DESC|QT|UN|VUNR$|(VLR$)*DESC|VLIT R$", 0, 0, 0, 0, 0);
            FormataTX("------------------------------------------", 0, 0, 0, 0, 0);
            FormataTX("------------------------------------------", 0, 0, 0, 0, 0);
        }

        public void ImprimirDadosProduto()
        {
            novaVenda = new VendaRegraNegocios();
            DataTable dadosTabelaVenda = new DataTable();

            dadosTabelaVenda = novaVenda.PesquisaVendas(frmVenda.numCupom, frmVenda.numcaixa);
            if (dadosTabelaVenda.Rows.Count > 0)
            {
                for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                {
                    xitem = dadosTabelaVenda.Rows[i]["ITEM"].ToString();
                    cProd = dadosTabelaVenda.Rows[i]["COD_BARRA"].ToString();
                    xProd = dadosTabelaVenda.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                    uCom = dadosTabelaVenda.Rows[i]["UNID"].ToString();
                    qCom = dadosTabelaVenda.Rows[i]["QUANT"].ToString();
                    vProd = dadosTabelaVenda.Rows[i]["PRECO"].ToString();
                    vCFe = dadosTabelaVenda.Rows[i]["TOTAL"].ToString();

                    cProd = cProd.Replace(" ", "");
                    uCom = uCom.Replace(" ", "");
                    qCom = qCom.Replace(" ", "");
                    vProd = vProd.Replace(" ", "");

                    //taratmento de variaveis
                    xitem = xitem.PadLeft(3, '0');
                    cProd = cProd.Substring(8, 5);

                    if (xProd.Length < 13)
                    {
                        xProd = xProd + "                   ";
                    }

                    xProd = xProd.Substring(0, 13);

                    FormataTX("------------------------------------------", 0, 0, 0, 0, 0);
                    FormataTX(xitem + " " + cProd + " " + xProd + " " + uCom + " " + Convert.ToDecimal(qCom).ToString("N3") + " " + Convert.ToDecimal(vProd).ToString("n2") + " " + Convert.ToDecimal(vCFe).ToString("N2"), 0, 0, 0, 0, 0);

                }
            }
        }

        public void ImprimirTipoPagamento()
        {
            //tipo pagamento
            for (int t = 0; t < gdvTipoPgto.Rows.Count; t++)
            {
                string valor = "";

                descTipoPagamento = gdvTipoPgto.Rows[t].Cells[0].Value.ToString();
                valor = gdvTipoPgto.Rows[t].Cells[1].Value.ToString();

                string traco = "........................R$";

                if (valor.Length == 4)
                {
                    traco = "........................R$";
                }
                else if (valor.Length == 5)
                {
                    traco = ".......................R$";
                }
                else if (valor.Length == 6)
                {
                    traco = "......................R$";
                }
                else if (valor.Length == 7)
                {
                    traco = "....................R$";
                }

                //  string dadosTipoPagamento = ();
                idRetornoMp = BematechTX(descTipoPagamento + traco + valor.ToString().ToString());
            }
        }

        public void PesquisarCupomImagem()
        {
            try
            {
                novoParametro = new ParametroRegraNegocio();
                DataTable dadosTabelaParametro = new DataTable();
                dadosTabelaParametro = novoParametro.PesquisaParametroE();

                if (dadosTabelaParametro.Rows.Count > 0)
                {
                    cupomImagem = Convert.ToBoolean(dadosTabelaParametro.Rows[0]["CUPOM_IMAGEM"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarCodAticavaoXml()
        {
            try
            {
                //pegar valor ultimo idCfe da venda aprovada................................................................
                XmlTextReader x = new XmlTextReader(pathACodAtivacao);

                while (x.Read())
                {

                    if (x.NodeType == XmlNodeType.Element && x.Name == "COD_ATIVACAO")
                        codAtivacao = (x.ReadString());
                }

                x.Close();
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void GerarUltimaVendaXml()
        {
            try
            {
                XmlTextWriter writer = new XmlTextWriter(pathUltimaVenda, null);

                //inicia o documento xml
                writer.WriteStartDocument();
                //escreve o elmento raiz
                writer.WriteStartElement("Venda");
                //Escreve os sub-elementos
                writer.WriteElementString("id", Ultimachave);
                // encerra o elemento raiz
                writer.WriteEndElement();
                //Escreve o XML para o arquivo e fecha o objeto escritor
                writer.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CancelarUltimaVenda()
        {
            try
            {
                //variaveis.................................................................................................
                string caminho = "";
                string assinatura = "";
                string cnpjAssinatua = "";

                saveFileDialog1.FileName = caminho;

                //pegar valor ultimo idCfe da venda aprovada................................................................
                XmlTextReader x = new XmlTextReader(pathUltimaVenda);
                while (x.Read())
                {
                    if (x.NodeType == XmlNodeType.Element && x.Name == "id")
                        chaveVenda = (x.ReadString());
                }

                chaveVenda = chaveVenda.Replace(" ", "");
                x.Close();

                //pegar valores da assinatura digital.......................................................................

                XmlTextReader y = new XmlTextReader(pathAssiDitital);

                while (y.Read())
                {
                    if (y.NodeType == XmlNodeType.Element && y.Name == "numAssinatura")
                        assinatura = (y.ReadString());

                    if (y.NodeType == XmlNodeType.Element && y.Name == "numCnpj")
                        cnpjAssinatua = (y.ReadString());
                }

                y.Close();

                //------------------------------------------------------------------------------------------------------------
                //criar arquivo xml Venda Cancelada...........................................................................

                arquivoXML = new XmlDocument();
                nodeCFeCanc = arquivoXML.CreateElement("CFeCanc");

                arquivoXML.AppendChild(nodeCFeCanc);
                XmlProcessingInstruction encoding = arquivoXML.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"utf-8\"");
                arquivoXML.InsertBefore(encoding, nodeCFeCanc);

                nodeInfCFeCanc = arquivoXML.CreateElement("infCFe");
                XmlAttribute attr = arquivoXML.CreateAttribute("chCanc");
                attr.Value = chaveVenda.ToString();

                nodeInfCFeCanc.SetAttributeNode(attr);
                nodeCFeCanc.AppendChild(nodeInfCFeCanc);

                nodeIDECanc = arquivoXML.CreateElement("ide");

                XmlElement cnpj = arquivoXML.CreateElement("CNPJ");

                nodeIDECanc.AppendChild(cnpj);
                cnpj.InnerText = cnpjAssinatua;

                XmlElement signAC = arquivoXML.CreateElement("signAC");
                nodeIDECanc.AppendChild(signAC);
                signAC.InnerText = assinatura;

                XmlElement numeroCaixa = arquivoXML.CreateElement("numeroCaixa");
                nodeIDECanc.AppendChild(numeroCaixa);
                numeroCaixa.InnerText = "00" + frmVenda.numcaixa.ToString();
                nodeInfCFeCanc.AppendChild(nodeIDECanc);
                XmlElement emitente = arquivoXML.CreateElement("emit");
                nodeInfCFeCanc.AppendChild(emitente);
                emitente.InnerText = " ";
                XmlElement destinatario = arquivoXML.CreateElement("dest");
                nodeInfCFeCanc.AppendChild(destinatario);
                destinatario.InnerText = " ";
                XmlElement total = arquivoXML.CreateElement("total");
                nodeInfCFeCanc.AppendChild(total);
                total.InnerText = " ";
                //XmlElement infAdc = arquivoXML.CreateElement("infAdic");
                //nodeInfCFeCanc.AppendChild(infAdc);


                //encontrar aruivo especifico.............
                arqu = (pathVendaCancelada);
                //salvar no proprio arquivo renomeando para cancelado
                arquivoXML.Save(arqu + chaveVenda + "-canc.xml");

                //buscar arquivo renomeado para cancelamento...............................
                string arquivoUltimaVenda = pathVendaCancelada + chaveVenda + "-canc.xml";

                if (arquivoUltimaVenda != "")
                {
                    //gera numero rondomico........................................
                    getNumberRandom();

                    //pega cod.....................................................
                    string cod = codAtivacao;

                    //recupera o retorno do SAT...............................
                    string ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.CancelarUltimaVenda(sessao, cod, chaveVenda, arquivoUltimaVenda)));

                    MessageBox.Show(ret);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void IniciarXMLCancel()
        {
            arquivoXML = new XmlDocument();
            nodeCFeCanc = arquivoXML.CreateElement("CFeCanc");

            arquivoXML.AppendChild(nodeCFeCanc);
            XmlProcessingInstruction encoding = arquivoXML.CreateProcessingInstruction("xml", "version=\"1.0\"");
            arquivoXML.InsertBefore(encoding, nodeCFeCanc);

            nodeInfCFeCanc = arquivoXML.CreateElement("infCFe");
            XmlAttribute attr = arquivoXML.CreateAttribute("chCanc");
            attr.Value = idCfe.ToString();

            nodeInfCFeCanc.SetAttributeNode(attr);
            nodeCFeCanc.AppendChild(nodeInfCFeCanc);
        }

        private void PreencherIDECancel()
        {
            try
            {
                nodeIDECanc = arquivoXML.CreateElement("ide");

                XmlElement cnpj = arquivoXML.CreateElement("CNPJ");

                nodeIDECanc.AppendChild(cnpj);
                cnpj.InnerText = numeroCnpjAssinatura;

                XmlElement signAC = arquivoXML.CreateElement("signAC");
                nodeIDECanc.AppendChild(signAC);
                signAC.InnerText = numeroAssinaturaDigital;

                XmlElement numeroCaixa = arquivoXML.CreateElement("numeroCaixa");
                nodeIDECanc.AppendChild(numeroCaixa);
                numeroCaixa.InnerText = "00" + frmVenda.numcaixa.ToString();

                nodeInfCFeCanc.AppendChild(nodeIDECanc);

                XmlElement emitente = arquivoXML.CreateElement("emit");
                nodeInfCFeCanc.AppendChild(emitente);
                emitente.InnerText = " ";

                XmlElement destinatario = arquivoXML.CreateElement("dest");
                nodeInfCFeCanc.AppendChild(destinatario);
                destinatario.InnerText = " ";

                XmlElement total = arquivoXML.CreateElement("total");
                nodeInfCFeCanc.AppendChild(total);
                total.InnerText = " ";
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string Sep_Delimitador(char sep, int posicao, string dados)
        {
            try
            {
                string[] ret_dados = dados.Split(sep);
                return ret_dados[posicao];
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public int getNumberRandom()
        {
            Random number = new Random();
            int retorno = number.Next(999999);
            sessao = retorno;
            return retorno;
        }

        private string LerArqTxt(string NomeArqui)
        {
            try
            {
                StreamReader strarq = new StreamReader(NomeArqui);

                NomeArqui = strarq.ReadToEnd();
                this.arqu = NomeArqui;
                strarq.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERRO: " + ex.ToString(), "Erro");
                return "";
            }

            return ConverterToUTF8(NomeArqui);
        }

        private string ConverterToUTF8(string dados)  // sempre mandar os dados para o sat em UT8
        {
            byte[] utf16Bytes = Encoding.Unicode.GetBytes(dados);
            byte[] utf8Bytes = Encoding.Convert(Encoding.Unicode, Encoding.UTF8, utf16Bytes);

            return Encoding.Default.GetString(utf8Bytes);
        }

        public void AlterarStatusFechar()
        {
            try
            {
                int numC = Convert.ToInt32(frmVenda.numcaixa);
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                novoParametro.AlterarStatusFechar(_numVenda, numC);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ImprimiCupomAberto()
        {
            string valorA = "";
            descTipoPagamento = gdvTipoPgto.Rows[0].Cells[0].Value.ToString();
            valorA = (gdvTipoPgto.Rows[0].Cells[1].Value.ToString()).ToString();

            if ((descTipoPagamento == "Aberto") || (descTipoPagamento == "ABERTO"))
            {
                valorAberto = Convert.ToDecimal(valorA);
                VendaAberto();
            }
        }

        public void PrencherCupomFiscal()
        {
            try
            {
                int numVenda = frmVenda.numCupom;
                lblEntrada.Text = numVenda.ToString();

                DataTable dadosTabela = new DataTable();
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                dadosTabela = novaVenda.PesquisaTabelaVenda(frmVenda.numCupom, frmVenda.numcaixa);

                if (dadosTabela.Rows.Count > 0)
                {
                    //preencher cupom fiscal......................................................................................
                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        descri = dadosTabela.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                        item = dadosTabela.Rows[i]["ITEM"].ToString();
                        codigoB = dadosTabela.Rows[i]["COD_BARRA"].ToString();
                        qtde = Convert.ToDecimal(dadosTabela.Rows[i]["QUANT"]).ToString();
                        preco = dadosTabela.Rows[i]["PRECO"].ToString();

                        lbCupom.Items.Add(item + " " + codigoB + " " + descri + " " + qtde + " " + preco + "");
                        dadosCupomElgin += (item + " " + codigoB + " " + descri + " " + qtde + " " + preco);
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PrencherCupomFiscal.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            //metodopara preecher cpf no cupom.......................................

            novoTemp = new RegraNegocio.TempRegraNegocios();
            DataTable dadosTabela_ = new DataTable();
            dadosTabela_ = novoTemp.PesquisarCpfTemp();

            if (dadosTabela_.Rows.Count > 0)
            {
                string cpfTemp = "";
                cpfTemp = dadosTabela_.Rows[0]["CPF_CNPJ"].ToString();

                cpf_cnpj = cpfTemp.Replace(" ", "");
                lblCnpjCpf.Text = cpfTemp.ToString();
            }
        }

        private void AlterarParametroNumFecharVenda()
        {
            try
            {

                bool FecharCupom = true;

                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                int novoNum = (_numVenda + 1);
                novoParametro.AlterarNumVendaFecharVenda(novoNum, FecharCupom, frmVenda.numcaixa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registro do Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LimparCampos()
        {
            txtTotal.Text = "";
        }

        public void SalvarFechamentoVenda_()
        {
            try
            {
                int _numVenda = frmVenda.numCupom;
                int cod;
                decimal valor = 0;

                novoFechamento = new RegraNegocio.FechamentoVendaReegraNegocio();

                novoTemp = new RegraNegocio.TempRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoTemp.PesquisarCpfTemp();

                if (dadosTabela.Rows.Count > 0)
                {
                    cpf_cnpj = dadosTabela.Rows[0]["CPF_CNPJ"].ToString();
                }
                else
                {
                    cpf_cnpj = "";
                }
                for (int i = 0; i < gdvTipoPgto.Rows.Count; i++)
                {
                    valor = Convert.ToDecimal(gdvTipoPgto.Rows[i].Cells[1].Value.ToString());
                    descTipoPagamento = gdvTipoPgto.Rows[i].Cells[0].Value.ToString();

                    if ((descTipoPagamento == "Dinheiro") || (descTipoPagamento == "DINHEIRO"))
                    {
                        novoFechamento.SalvarFechamentoVenda(0, 1, valor, cpf_cnpj, _numVenda, false, DateTime.Now, Convert.ToDecimal(txtTroco.Text), frmVenda.idUsuarioLogado, false, frmVenda.numcaixa);
                    }

                    if ((descTipoPagamento == "Cheque") || (descTipoPagamento == "CHEQUE"))
                    {
                        novoFechamento.SalvarFechamentoVenda(0, 3, valor, cpf_cnpj, _numVenda, false, DateTime.Now, Convert.ToDecimal(txtTroco.Text), frmVenda.idUsuarioLogado, false, frmVenda.numcaixa);
                    }

                    if ((descTipoPagamento == "Cartao") || (descTipoPagamento == "Cartão") || (descTipoPagamento == "CARTAO") || (descTipoPagamento == "CARTÃO"))
                    {
                        novoFechamento.SalvarFechamentoVenda(0, 2, valor, cpf_cnpj, _numVenda, false, DateTime.Now, Convert.ToDecimal(txtTroco.Text), frmVenda.idUsuarioLogado, false, frmVenda.numcaixa);
                    }

                    if ((descTipoPagamento == "Aberto") || descTipoPagamento == "ABERTO")
                    {
                        novoFechamento.SalvarFechamentoVenda(idCliente, 4, valor, cpf_cnpj, _numVenda, false, DateTime.Now, troco, frmVenda.idUsuarioLogado, false, frmVenda.numcaixa);
                        PesquisarUltimoPagamentoVenda();
                    }
                }

                AbrirCpfCnpj();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registro do Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PesquisarUltimoPagamentoVenda()
        {
            try
            {
                novoFechamento = new FechamentoVendaReegraNegocio();
                DataTable dadosTabelaPagamentoVenda = new DataTable();

                dadosTabelaPagamentoVenda = novoFechamento.PesquisarUltimoPagamentoVenda(frmVenda.numcaixa, frmVenda.idUsuarioLogado);

                if (dadosTabelaPagamentoVenda.Rows.Count > 0)
                {
                    idPagamentoVenda = Convert.ToInt32(dadosTabelaPagamentoVenda.Rows[0]["ID"].ToString());
                    valorReceber = Convert.ToDecimal(dadosTabelaPagamentoVenda.Rows[0]["VALOR"].ToString());
                    idCliente = Convert.ToInt32(dadosTabelaPagamentoVenda.Rows[0]["ID_CLIENTE"].ToString());

                    CadastrarContaReceber();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registro do Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AbrirCpfCnpj()
        {
            try
            {
                string cpf = "";
                int tipo = 0;
                string nome = "";
                novoTemp = new RegraNegocio.TempRegraNegocios();
                novoTemp.AlterarCpfCliente(cpf, tipo, nome);
                this.Close();
            }
            catch (Exception)
            {
                throw;
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

        private void txtRecebimento_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtRecebimento);
        }

        private void btnRgGerencial_Click(object sender, EventArgs e)
        {
            FecharVendaAberto();
        }

        public void FecharVendaAberto()
        {
            try
            {
                codTipoPagamento = "4";

                decimal somaRecebimento = 0;
                decimal valorVenda = 0;
                decimal somax = 0;

                novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                descTipoPagamento = dadosTabela.Rows[0]["TIPO_PAGTO"].ToString();

                if (txtRecebimento.Text == "0,00")
                {
                    HabilitarCampos();
                    txtTroco.Text = "0,00";
                    txtRecebimento.ReadOnly = true;
                    gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtTotal.Text);
                    LiberarBotoesFecharCaixa();
                    btnFecharVenda.Focus();

                    somaTotalCompra = Convert.ToDecimal(txtTotal.Text);

                    frmCliente frmCliente = new frmCliente(this);
                    frmCliente.ShowDialog();
                }
                if (Convert.ToDecimal(txtRecebimento.Text) > 0)
                {
                    gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtRecebimento.Text);

                    if (descTipoPagamento == "Aberto")
                    {
                        somaTotalCompra += Convert.ToDecimal(gdvTipoPgto.Rows[0].Cells[1].Value);
                    }

                    valorVenda = Convert.ToDecimal(somaTotalCompra);
                    SetFormatting();
                    txtTroco.Text = (somaTotalCompra - valorVenda).ToString();
                    //txtRecebimento.Text = "";
                    txtRecebimento.Focus();

                    somax = Convert.ToDecimal(txtTroco.Text);

                    if (somax >= 0)
                    {
                        LiberarBotoesFecharCaixa();
                        btnFecharVenda.Focus();

                        frmCliente frmCliente = new frmCliente(this);
                        frmCliente.ShowDialog();
                    }
                }

                somax = (somax * -1);
                txtRecebimento.Text = somax.ToString();
                SetFormatting();

            }
            catch (Exception)
            {
                MessageBox.Show("Verifique se Exite o Tipo de Pagamento em Aberto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void VendaAberto()
        {
            try
            {
                if ((impressora == "BEMATECH") || (impressora == "SAT"))
                {
                    if (idCliente > 0)
                    {
                        //  _valorTotal = 0;
                        codTipoPagamento = "4";

                        // frmVenda = new frmVenda(frmPrincipal);

                        string numeroCupom = lblEntrada.Text;
                        string operador = lblOperador.Text;
                        string spaco13 = "        ";
                        string nomeCliente = "";

                        novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                        DataTable dadosTabelaTp = new DataTable();
                        dadosTabelaTp = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                        descTipoPagamento = dadosTabelaTp.Rows[0]["TIPO_PAGTO"].ToString();

                        novoCliente = new RegraNegocio.ClienteRegraNegocio();
                        DataTable dadosTabelaCliente = new DataTable();
                        dadosTabelaCliente = novoCliente.PesquisarCliente(idCliente);

                        nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                        if ((dadosTabelaTp.Rows.Count > 0) && (dadosTabelaCliente.Rows.Count > 0))
                        {
                            //metodo somaTotal da Tabela....................................................................
                            novaVenda = new RegraNegocio.VendaRegraNegocios();
                            DataTable dadosTabela = new DataTable();

                            dadosTabela = novaVenda.PesquisaVendas(_numVenda, frmVenda.numcaixa);

                            // valorAberto = Convert.ToDecimal(dadosTabela.Rows[0]["TOTAL"]);

                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(spaco13 + "*** COMPROVANTE DE COMPRA ***");
                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("VALOR TOTAL...................." + valorAberto.ToString("C2"));
                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada no Valor de " + valorAberto.ToString("C2") + "\t\t\t\t\t");
                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("ASS:______________________________________\n");
                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Nº CAIXA:" + frmVenda.numcaixa + " - Nº CUPOM: " + numeroCupom);
                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Operador: " + operador);
                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Informe o Cliente para Venda do Tipo em Aberto!!!. \nDeseja Selecionar o Cliente", "Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                        {
                            _valorTotal = 0;
                            codTipoPagamento = "4";

                            string responsavelCompra = frmVenda.operadorAtuante.ToString();
                            string numeroCupom = frmVenda.numCupom.ToString();
                            string operador = frmVenda.operadorAtuante;
                            string spaco13 = "        ";
                            string nomeCliente = "";

                            novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                            DataTable dadosTabelaTp = new DataTable();
                            dadosTabelaTp = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                            descTipoPagamento = dadosTabelaTp.Rows[0]["TIPO_PAGTO"].ToString();

                            novoCliente = new RegraNegocio.ClienteRegraNegocio();
                            DataTable dadosTabelaCliente = new DataTable();
                            dadosTabelaCliente = novoCliente.PesquisarCliente(idCliente);

                            nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                            if ((dadosTabelaTp.Rows.Count > 0) && (dadosTabelaCliente.Rows.Count > 0))
                            {
                                //metodo somaTotal da Tabela....................................................................
                                novaVenda = new RegraNegocio.VendaRegraNegocios();
                                DataTable dadosTabela = new DataTable();

                                dadosTabela = novaVenda.PesquisaVendas(_numVenda, frmVenda.numcaixa);

                                for (int i = 0; i < dadosTabela.Rows.Count; i++)
                                {
                                    _valorTotal += Convert.ToDecimal(dadosTabela.Rows[i]["TOTAL"]);
                                }

                                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(spaco13 + "*** COMPROVANTE DE COMPRA ***");
                                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("VALOR TOTAL...................." + _valorTotal.ToString("C2"));
                                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada no Valor de " + _valorTotal.ToString("C2") + "\t\t\t\t\t");
                                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("ASS:______________________________________\n");
                                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Nº CUPOM: " + numeroCupom);
                                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Operador: " + operador);
                                idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                            }
                        }
                    }
                }
                else if (impressora == "BEMASAT")
                {
                    //  LerXmlCustodia();
                    if (idCliente > 0)
                    {
                        //  _valorTotal = 0;
                        codTipoPagamento = "4";

                        frmVenda = new frmVenda(frmPrincipal);

                        string numeroCupom = lblEntrada.Text;
                        string operador = lblOperador.Text;
                        string spaco13 = "        ";
                        string nomeCliente = "";

                        novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                        DataTable dadosTabelaTp = new DataTable();
                        dadosTabelaTp = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                        descTipoPagamento = dadosTabelaTp.Rows[0]["TIPO_PAGTO"].ToString();

                        novoCliente = new RegraNegocio.ClienteRegraNegocio();
                        DataTable dadosTabelaCliente = new DataTable();
                        dadosTabelaCliente = novoCliente.PesquisarCliente(idCliente);

                        nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                        gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtRecebimento.Text);

                        if (descTipoPagamento == "Aberto")
                        {
                            somaTotalCompra += Convert.ToDecimal(gdvTipoPgto.Rows[0].Cells[1].Value);
                        }

                        somaTotalCompra = Convert.ToDecimal(somaTotalCompra);

                        idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                        idRetorno = RegraNegocio.MP2032.IniciaPorta(portaComImpressora);

                        string cabecalho = ("------------------------------------------" +
                                             xFant + "\n" +
                                             xNome + "\n" +
                                             xLgr + ", " + nro + "-" + xMun + "-" + CEP + "\n" +
                                             "CNPJ: " + cnpj + "\n" +
                                             "IE:" + IE + "-" + "IM:" + IM + "\n" +
                                             "------------------------------------------");


                        string cab = ("     *** COMPROVANTE DE COMPRA ***         " +
                                      "\n" +
                                      "VALOR TOTAL....................." + valorAberto.ToString("C2") + "\r\n" +
                                      "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada " +
                                      "no Valor de: \n" + valorAberto.ToString("C2") + "." + "\r\n" +
                                      "ASS:_________________________________\n" +
                                      "Nº CUPOM:" + numeroCupom + " - Operador: " + operador +
                                      "\n\n\n\n"
                                       );

                        idRetorno = BematechTX(cabecalho + "\n" + cab);

                        string s_cmdTX = "\r\n";
                        //Comando para salto de linha
                        idRetorno = RegraNegocio.MP2032.ComandoTX(s_cmdTX, s_cmdTX.Length);
                        idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(0);

                        idRetorno = MP2032.FechaPorta();
                        this.Close();
                    }
                }
                else if (nomeImpressora == "COMUM")
                {
                    //AbreCupomComum();
                    //VendeItemComum();
                    //FormaPagamentoComum();


                    ImprimiCupomNaoFiscal();
                    ImprimirCupomAbertoComum();
                    this.Close();
                }
                else if (nomeImpressora == "DARUMA")
                {
                    //  LerXmlCustodia();
                    if (idCliente > 0)
                    {
                        //  _valorTotal = 0;
                        codTipoPagamento = "4";

                        frmVenda = new frmVenda(frmPrincipal);

                        string numeroCupom = lblEntrada.Text;
                        string operador = lblOperador.Text;
                        string spaco13 = "        ";
                        string nomeCliente = "";
                        string data = DateTime.Now.Date.ToShortDateString();

                        novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                        DataTable dadosTabelaTp = new DataTable();
                        dadosTabelaTp = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                        descTipoPagamento = dadosTabelaTp.Rows[0]["TIPO_PAGTO"].ToString();

                        novoCliente = new RegraNegocio.ClienteRegraNegocio();
                        DataTable dadosTabelaCliente = new DataTable();
                        dadosTabelaCliente = novoCliente.PesquisarCliente(idCliente);

                        nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                        gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtRecebimento.Text);

                        //Dados cliente local

                        novoParametro = new RegraNegocio.ParametroRegraNegocio();
                        DataTable dadosTabelaParametro = new DataTable();
                        dadosTabelaParametro = novoParametro.PesquisaParametroE();

                        string nomeCliente_ = dadosTabelaParametro.Rows[0]["NOME_FANTASIA"].ToString();
                        string enderecoCliente = dadosTabelaParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                        string numeroCliente = dadosTabelaParametro.Rows[0]["NUMERO"].ToString();
                        string bairroCliente = dadosTabelaParametro.Rows[0]["BAIRRO"].ToString();
                        string cidadeCliente = dadosTabelaParametro.Rows[0]["CIDADE"].ToString();
                        string ufCliente = dadosTabelaParametro.Rows[0]["UF"].ToString();
                        string cepCliente = dadosTabelaParametro.Rows[0]["CEP"].ToString();
                        string cnpjCliente = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                        string ieCliente = dadosTabelaParametro.Rows[0]["IE"].ToString();
                        string imCliente = dadosTabelaParametro.Rows[0]["IM"].ToString();

                        numeroCliente = numeroCliente.Replace(" ", "");
                        cnpjCliente = cnpjCliente.Replace(" ", "");
                        ieCliente = ieCliente.Replace(" ", "");
                        imCliente = imCliente.Replace(" ", "");

                        if (descTipoPagamento == "Aberto")
                        {
                            somaTotalCompra += Convert.ToDecimal(gdvTipoPgto.Rows[0].Cells[1].Value);
                        }

                        somaTotalCompra = Convert.ToDecimal(somaTotalCompra);



                        string cabecalho = ("------------------------------------------------" +
                                             nomeCliente_ + "\n" +
                                             enderecoCliente + ", " + numeroCliente + "-" + cidadeCliente + "-" + cepCliente + "\n" +
                                             "CNPJ: " + cnpjCliente + "\n" +
                                             "IE:" + ieCliente + "-" + "IM:" + imCliente + "\n" +
                                             "------------------------------------------------");


                        string cab = ("     *** COMPROVANTE DE COMPRA ***         " +
                                      "\n" +
                                      "VALOR TOTAL....................." + valorAberto.ToString("C2") + "\r\n" +
                                      "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada " +
                                      "no Valor de: " + valorAberto.ToString("C2") + "." + "\r\n" +
                                      "ASS:___________________________________________\n" +
                                      "Nº CUPOM:" + numeroCupom + " - Operador: " + operador +
                                      "\r\n\n\n");

                        int iRetorno;
                        iRetorno = RegraNegocio.DLLsDaruma.iImprimirTexto_DUAL_DarumaFramework(cabecalho + cab + "<c>" + data + ":</c><sl>4</sl><gui></gui><l></l>", 0);

                        this.Close();
                    }
                }
                else if (nomeImpressora == "MP2500")
                {
                    if (idCliente > 0)
                    {
                        //  _valorTotal = 0;
                        codTipoPagamento = "4";

                        frmVenda = new frmVenda(frmPrincipal);

                        string numeroCupom = lblEntrada.Text;
                        string operador = lblOperador.Text;
                        string spaco13 = "        ";
                        string nomeCliente = "";
                        string data = DateTime.Now.Date.ToShortDateString();

                        novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                        DataTable dadosTabelaTp = new DataTable();
                        dadosTabelaTp = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                        descTipoPagamento = dadosTabelaTp.Rows[0]["TIPO_PAGTO"].ToString();

                        novoCliente = new RegraNegocio.ClienteRegraNegocio();
                        DataTable dadosTabelaCliente = new DataTable();
                        dadosTabelaCliente = novoCliente.PesquisarCliente(idCliente);

                        nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                        gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtRecebimento.Text);

                        //Dados cliente local

                        novoParametro = new RegraNegocio.ParametroRegraNegocio();
                        DataTable dadosTabelaParametro = new DataTable();
                        dadosTabelaParametro = novoParametro.PesquisaParametroE();

                        string nomeCliente_ = dadosTabelaParametro.Rows[0]["NOME_FANTASIA"].ToString();
                        string enderecoCliente = dadosTabelaParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                        string numeroCliente = dadosTabelaParametro.Rows[0]["NUMERO"].ToString();
                        string bairroCliente = dadosTabelaParametro.Rows[0]["BAIRRO"].ToString();
                        string cidadeCliente = dadosTabelaParametro.Rows[0]["CIDADE"].ToString();
                        string ufCliente = dadosTabelaParametro.Rows[0]["UF"].ToString();
                        string cepCliente = dadosTabelaParametro.Rows[0]["CEP"].ToString();
                        string cnpjCliente = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                        string ieCliente = dadosTabelaParametro.Rows[0]["IE"].ToString();
                        string imCliente = dadosTabelaParametro.Rows[0]["IM"].ToString();

                        numeroCliente = numeroCliente.Replace(" ", "");
                        cnpjCliente = cnpjCliente.Replace(" ", "");
                        ieCliente = ieCliente.Replace(" ", "");
                        imCliente = imCliente.Replace(" ", "");

                        somaTotalCompra = Convert.ToDecimal(somaTotalCompra);

                        frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();

                        idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                        idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);



                        string cabecalho = ("--------------------------------------------------" +
                                             nomeCliente_ + "\n" +
                                             enderecoCliente + ", " + numeroCliente + "-" + cidadeCliente + "-" + cepCliente + "\n" +
                                             "CNPJ: " + cnpjCliente + "\n" +
                                             "IE:" + ieCliente + "-" + "IM:" + imCliente + "\n" +
                                             "-------------------------------------------------");


                        string cab = ("         *** COMPROVANTE DE COMPRA ***         " +
                                      "\n" +
                                      "VALOR TOTAL....................." + valorAberto.ToString("C2") + "\r\n" +
                                      "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada " +
                                      "no Valor de: " + valorAberto.ToString("C2") + "." + "\r\n" +
                                      "ASS:___________________________________________\n" +
                                      "Nº CUPOM:" + numeroCupom + " - Operador: " + operador +
                                      "\r\n\n\n");

                        string s_cmdTX = "\r\n";
                        idRetorno = BematechTX(cabecalho + "\n" + cab + "\n\n");

                        //Comando para salto de linha
                        idRetorno = RegraNegocio.MP2032.ComandoTX(s_cmdTX, s_cmdTX.Length);
                        idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(0);

                        idRetorno = MP2032.FechaPorta();
                        this.Close();
                    }
                }

                else if (nomeImpressora == "ELGIN")
                {
                    //  LerXmlCustodia();
                    if (idCliente > 0)
                    {
                        //  _valorTotal = 0;
                        codTipoPagamento = "4";

                        frmVenda = new frmVenda(frmPrincipal);

                        string numeroCupom = lblEntrada.Text;
                        string operador = lblOperador.Text;
                        string spaco13 = "        ";
                        string nomeCliente = "";
                        string data = DateTime.Now.Date.ToShortDateString();

                        novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                        DataTable dadosTabelaTp = new DataTable();
                        dadosTabelaTp = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                        descTipoPagamento = dadosTabelaTp.Rows[0]["TIPO_PAGTO"].ToString();

                        novoCliente = new RegraNegocio.ClienteRegraNegocio();
                        DataTable dadosTabelaCliente = new DataTable();
                        dadosTabelaCliente = novoCliente.PesquisarCliente(idCliente);

                        nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                        gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtRecebimento.Text);

                        //Dados cliente local

                        novoParametro = new RegraNegocio.ParametroRegraNegocio();
                        DataTable dadosTabelaParametro = new DataTable();
                        dadosTabelaParametro = novoParametro.PesquisaParametroE();

                        string nomeCliente_ = dadosTabelaParametro.Rows[0]["NOME_FANTASIA"].ToString();
                        string enderecoCliente = dadosTabelaParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                        string numeroCliente = dadosTabelaParametro.Rows[0]["NUMERO"].ToString();
                        string bairroCliente = dadosTabelaParametro.Rows[0]["BAIRRO"].ToString();
                        string cidadeCliente = dadosTabelaParametro.Rows[0]["CIDADE"].ToString();
                        string ufCliente = dadosTabelaParametro.Rows[0]["UF"].ToString();
                        string cepCliente = dadosTabelaParametro.Rows[0]["CEP"].ToString();
                        string cnpjCliente = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                        string ieCliente = dadosTabelaParametro.Rows[0]["IE"].ToString();
                        string imCliente = dadosTabelaParametro.Rows[0]["IM"].ToString();

                        numeroCliente = numeroCliente.Replace(" ", "");
                        cnpjCliente = cnpjCliente.Replace(" ", "");
                        ieCliente = ieCliente.Replace(" ", "");
                        imCliente = imCliente.Replace(" ", "");

                        if (descTipoPagamento == "Aberto")
                        {
                            somaTotalCompra += Convert.ToDecimal(gdvTipoPgto.Rows[0].Cells[1].Value);
                        }

                        somaTotalCompra = Convert.ToDecimal(somaTotalCompra);



                        string cabecalho = ("------------------------------------------------" +
                                             nomeCliente_ + "\n" +
                                             enderecoCliente + ", " + numeroCliente + "-" + cidadeCliente + "-" + cepCliente + "\n" +
                                             "CNPJ: " + cnpjCliente + "\n" +
                                             "IE:" + ieCliente + "-" + "IM:" + imCliente + "\n" +
                                             "------------------------------------------------");


                        string cab = ("     *** COMPROVANTE DE COMPRA ***         " +
                                      "\n" +
                                      "VALOR TOTAL....................." + valorAberto.ToString("C2") + "\r\n" +
                                      "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada " +
                                      "no Valor de: " + valorAberto.ToString("C2") + "." + "\r\n" +
                                      "ASS:___________________________________________\n" +
                                      "Nº CUPOM:" + numeroCupom + " - Operador: " + operador +
                                      "\r\n\n\n");

                        string dadosElgin = (cabecalho + cab + "<c>" + data);

                        esc = new RegraNegocio.EscPos();

                        this.esc.printText(printerName, dadosElgin);
                        this.esc.lineFeed(printerName, 2);

                        feedAndCutter(printerName, 5);

                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Informe Porta COm da impressora.", "Atenção");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Registro do Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ImprimirCupomAbertoComum()
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                string texto = "";

                leitor = new StringReader(texto);

                this.printDocument1.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtRecebimento_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            //{
            //    e.Handled = true;
            //}
        }

        private void txtTotal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void txtTroco_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            //Aqui você faz a sua leitura
            //Aqui você desabilita a leitura ou o textBox
            txtTroco.Enabled = false;
            txtTroco.ReadOnly = true;
        }

        private void btnCartao_Click(object sender, EventArgs e)
        {
            FecharVendaCheque();
        }

        public void FecharVendaCartao()
        {
            try
            {
                codTipoPagamento = "2";

                decimal somaRecebimento = 0;
                decimal valorVenda = 0;
                decimal somax = 0;

                novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                descTipoPagamento = dadosTabela.Rows[0]["TIPO_PAGTO"].ToString();

                if (txtRecebimento.Text == "0,00")
                {
                    HabilitarCampos();
                    txtTroco.Text = "0,00";
                    txtRecebimento.ReadOnly = true;
                    gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtTotal.Text);
                    LiberarBotoesFecharCaixa();
                    btnFecharVenda.Focus();
                    retornoVendaId = true;
                }

                if (Convert.ToDecimal(txtRecebimento.Text) > 0)
                {
                    gdvTipoPgto.Rows.Insert(0, descTipoPagamento.ToString(), txtRecebimento.Text);

                    for (int i = 0; i < gdvTipoPgto.Rows.Count; i++)
                    {
                        somaRecebimento += Convert.ToDecimal(gdvTipoPgto.Rows[i].Cells[1].Value);
                    }

                    valorVenda = Convert.ToDecimal(txtTotal.Text);
                    SetFormatting();
                    txtTroco.Text = (somaRecebimento - valorVenda).ToString();
                    //txtRecebimento.Text = "";
                    txtRecebimento.Focus();

                    somax = Convert.ToDecimal(txtTroco.Text);

                    if (somax >= 0)
                    {
                        LiberarBotoesFecharCaixa();
                        btnFecharVenda.Focus();
                    }

                    somax = (somax * -1);
                    txtRecebimento.Text = somax.ToString();
                }

                SetFormatting();
            }
            catch (Exception)
            {
                MessageBox.Show("Error Metodo Fechar Venda Cartão", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gdvTipoPgto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            SetFormatting();
        }

        private void gdvTipoPgto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtRecebimento_TextChanged_1(object sender, EventArgs e)
        {
            //Moeda(ref txtRecebimento);
        }

        public void CancelaVendaBemasat()
        {
            try
            {
                GerarXmlVendaCancelda();
                string NomeArquCancelVenda = pathVendaCancelada + _numVenda + "canc.xml";

                getNumberRandom();
                LerArqTxt(NomeArquCancelVenda);
                LerChave();

                PesquisarCodAticavaoXml();

                string cod = codAtivacao;

                if (NomeArquCancelVenda != null)
                {
                    string ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.CancelarUltimaVenda(sessao, cod, chaveVenda, NomeArquCancelVenda)));

                    for (int i = 0; i < 6; i++)
                    {
                        lbVenda.Items.Add(Sep_Delimitador('|', i, ret));
                    }

                    MessageBox.Show(ret);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LerChave()
        {
            //Cria uma instância de um documento XML
            XmlDocument oXML = new XmlDocument();

            //Define o caminho do arquivo XML 
            string ArquivoXML = pathVendaCancelada + _numVenda + "canc.xml";
            //carrega o arquivo XML
            oXML.Load(ArquivoXML);

            //Lê o filho de um Nó Pai específico 
            chaveVenda = oXML.SelectSingleNode("VENDA_CANCELADA").ChildNodes[0].InnerText;
        }

        public void GerarXmlVendaCancelda()
        {
            try
            {
                string caminhoCancelaVenda = "";

                saveFileDialog1.FileName = caminhoCancelaVenda;
                IniciarXML();
                PreencherIDE();
                PreencherEmitente();
                preenchedestinario();
                preencheProduto();
                preenchePagamento();
                preencheInfo();
                arqu = (pathVendaCancelada);

                arquivoXML.Save(arqu + _numVenda + "canc.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            CancelarUltimaVenda();
        }

        public void GerarPDFRedu(string caminhoPdf)
        {
            try
            {
                string autorzarTest = "";
                novoParametro = new ParametroRegraNegocio();
                DataTable dadosTabelaParametro = new DataTable();
                dadosTabelaParametro = novoParametro.PesquisaParametroE();

                iTextSharp.text.Document document = new iTextSharp.text.Document();
                var fileStream = PdfWriter.GetInstance(document, new FileStream(caminhoPdf, FileMode.Create));
                //ler xml sat venda Aprovado

                if (dadosTabelaParametro.Rows.Count > 0)
                {
                    XmlTextReader x = new XmlTextReader(pathCustodia + idCfe + ".xml");

                    while (x.Read())
                    {
                        //cabecalho
                        if (x.NodeType == XmlNodeType.Element && x.Name == "xFant")
                            nomeFantasiaEmit = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "CNPJ")
                            cnpjEmit = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "xNome")
                            nomeEmit = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "xLgr")
                            logradouroEmit = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "nro")
                            numImit = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "xMun")
                            cidadeEmit = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "xBairro")
                            bairroEmit = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "CEP")
                            cepEmit = (x.ReadString());


                        if (x.NodeType == XmlNodeType.Element && x.Name == "IE")
                            ieEmi = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "IM")
                            imEmi = (x.ReadString());

                        //cpf Consumidor
                        if (x.NodeType == XmlNodeType.Element && x.Name == "CPF")
                            cpfDest = (x.ReadString());

                        //datat e hora

                        if (x.NodeType == XmlNodeType.Element && x.Name == "dEmi")
                            dataEmit = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "hEmi")
                            horaEmit = (x.ReadString());
                        //corpo

                        if (x.NodeType == XmlNodeType.Element && x.Name == "vCFe")
                            totalProduto = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "vTroco")
                            trocoProd = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "cMP")
                            tipoPgtoProd = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "vMP")
                            valorEtregaProd = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "infCpl")
                            infCompleProd = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "assinaturaQRCODE")
                            qrCodProd = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "nCFe")
                            nCfeEmit = (x.ReadString());

                        if (x.NodeType == XmlNodeType.Element && x.Name == "nserieSAT")
                            nserieSAT = (x.ReadString());
                    }

                    x.Close();

                    //abre pdf
                    document.Open();

                    // Figuras geométricas.
                    var contentByte = fileStream.DirectContent;

                    //Basicamente, temos que registrar o diretório de fontes do Windows no iTextSharp aí ele passa a reconhecer todas as fontes instaladas no seu computador:
                    iTextSharp.text.FontFactory.RegisterDirectory(@"C:\WINDOWS\\Fonts\");
                    var font = iTextSharp.text.FontFactory.GetFont("Arial", 10);

                    // Textos.
                    //criar variavel paragraph
                    var paragraph = new iTextSharp.text.Paragraph(nomeEmit, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph(razaoSocialEmit, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph(logradouroEmit + ", " + numImit, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph(cidadeEmit + ", " + bairroEmit + " CEP: " + cepEmit, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph("CNPJ: " + cnpjEmit + "IE: " + ieEmi, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph("IM: " + imEmi, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    AbreCupomNaoFiscal();

                    paragraph = new iTextSharp.text.Paragraph("________________________________________________________", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);
                    //corpo
                    paragraph = new iTextSharp.text.Paragraph("EXTRATO Nº: " + nCfeEmit, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph("CUPOM FISCAL ELETRONICO - SAT", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph(autorzarTest, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph("________________________________________________________", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    if (cpfDest == null)
                    {
                        paragraph = new iTextSharp.text.Paragraph("CPF/CNPJ do Consumidor: Não Informado", font);
                        paragraph.Alignment = iTextSharp.text.Element.BODY;
                        document.Add(paragraph);
                    }
                    else
                    {
                        paragraph = new iTextSharp.text.Paragraph("CPF/CNPJ do Consumidor: " + cpfDest, font);
                        paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        document.Add(paragraph);
                    }

                    paragraph = new iTextSharp.text.Paragraph("________________________________________________________", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph("#|COD|DESC|QTDE|UNI|VLU UN R$|(VL TR R$ )*DESC|VL ITEM R$|  ", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph("________________________________________________________", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    VendeItemNaoFiscal();

                    novaVenda = new VendaRegraNegocios();
                    DataTable dadosTabelaVenda = new DataTable();
                    dadosTabelaVenda = novaVenda.PesquisarVenda(Convert.ToInt32(frmVenda.numCupom));

                    if (dadosTabelaVenda.Rows.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        //preencher venda cupom fiscal......................................................................................
                        foreach (DataRow linha in dadosTabelaVenda.Rows)
                        {
                            descri = linha["DESCRICAO_PRODUTO"].ToString();
                            item = linha["ITEM"].ToString();
                            codigoB = linha["COD_BARRA"].ToString();
                            qtde = linha["QUANT"].ToString();
                            preco = linha["PRECO"].ToString();
                            totalProd = linha["TOTAL"].ToString();
                            unidProd = linha["UNID"].ToString();

                            //tratamento de variaveis
                            //item 
                            item = item.PadLeft(3, '0');

                            //codigo de barra
                            codigoB = codigoB.Substring(8, 5);

                            //unidade
                            unidProd = unidProd.Replace(" ", "");

                            ////desccricao

                            if (descri.Length > 25)
                            {
                                descri = descri.Substring(0, 25);
                            }

                            //lbCupom.Items.Add(item + "," + codigoB + "," + descri + "," + unidProd);
                            sb = new StringBuilder();
                            sb.AppendFormat("{0,-4}{1,-6}{2,-10}{3,-10}{4,-1}",
                            item.ToString(),
                            codigoB, descri.Trim(), qtde, Environment.NewLine);

                            paragraph = new iTextSharp.text.Paragraph(sb.ToString(), font);
                            paragraph.Alignment = iTextSharp.text.Element.ALIGN_UNDEFINED;
                            document.Add(paragraph);
                        }

                        paragraph = new iTextSharp.text.Paragraph("TOTAL R$: " + totalProduto, font);
                        paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        document.Add(paragraph);

                        C = "TOTAL: .........................................................................." + totalProduto;

                        paragraph = new iTextSharp.text.Paragraph("\n", font);
                        paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        document.Add(paragraph);

                        string tipoPto = "";

                        if (tipoPgtoProd == "01")
                        {
                            tipoPto = "DINHEIRO: ";
                        }
                        else if (tipoPgtoProd == "02")
                        {
                            tipoPto = "CARTÃO: ";
                        }

                        else if (tipoPgtoProd == "03")
                        {
                            tipoPto = "CHEQUE: ";
                        }

                        else if (tipoPgtoProd == "04")
                        {
                            tipoPto = "ABERTO: ";
                        }

                        paragraph = new iTextSharp.text.Paragraph(tipoPto + valorEtregaProd, font);
                        paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        document.Add(paragraph);

                        //   C = "......................TOTAL: " + totalProd + "\n";
                        D = tipoPto + "..................................................................." + valorEtregaProd;

                        paragraph = new iTextSharp.text.Paragraph("TROCO R$: " + trocoProd, font);
                        paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        document.Add(paragraph);
                        E = "TROCO R$: ..................................................................." + trocoProd;
                    }

                    paragraph = new iTextSharp.text.Paragraph("________________________________________________________", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);


                    paragraph = new iTextSharp.text.Paragraph("Consulte o QRCode desse Cupom pelo App DeOlhoNaNota.", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    paragraph = new iTextSharp.text.Paragraph("________________________________________________________", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    G = " -----------------------------------------------------------------------------------\n" +
                        "Consulte QRCode desse Cupom pelo App DeOlhoNaNota                                   \n" +
                        " -----------------------------------------------------------------------------------";

                    paragraph = new iTextSharp.text.Paragraph("OBSERVAÇÕES DO CONTRIBUINTE", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    H = ("OBSERVAÇÕES DO CONTRIBUINTE                                           ");

                    paragraph = new iTextSharp.text.Paragraph("Fonte: IBPT Trib Aprox. Conforme a Lei Fed. 12.741/2012", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    I = ("Fonte: IBPT Trib Aprox. Conforme a Lei Fed. 12.741/2012               ");

                    paragraph = new iTextSharp.text.Paragraph("* Valor Aprox Tributos:  ", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    J = ("* Valor Aprox Tributos:  ");

                    nCupomComp = dadosTabelaParametro.Rows[0]["NUM_CUPOM"].ToString();
                    ncaixaComp = dadosTabelaParametro.Rows[0]["NUM_CAIXA"].ToString();

                    L = ("Nº CAIXA: " + ncaixaComp + ". - " + "Nº CUPOM: " + nCupomComp);

                    paragraph = new iTextSharp.text.Paragraph("Nº CUPOM: " + nCupomComp + " - Nº CAIXA: " + ncaixaComp, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    operadoComp = frmVenda.operadorAtuante;
                    peiodoComp = frmVenda.periodoAtuante;

                    M = ("OPERADOR: " + operadoComp + "." + "PERIODO: " + peiodoComp + "." + "\n" +
                         "PLACA: " + placaComp + ". - KM: " + kmComp + "\n" +
                        " -----------------------------------------------------------------------------------\n");

                    paragraph = new iTextSharp.text.Paragraph("OPERADOR(A): " + operadoComp + peiodoComp, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    infComp = dadosTabelaParametro.Rows[0]["MSG"].ToString();

                    paragraph = new iTextSharp.text.Paragraph(infComp, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    //PLACA
                    bool placaAutorizada = Convert.ToBoolean(dadosTabelaParametro.Rows[0]["PLACA"].ToString());

                    if (placaAutorizada == true)
                    {
                        novaPlaca = new PlacaRegraNegocio();
                        DataTable dadosTabelaPlaca = new DataTable();

                        dadosTabelaPlaca = novaPlaca.PesquisarPlaca(frmVenda.numCupom.ToString());

                        if (dadosTabelaPlaca.Rows.Count > 0)
                        {
                            placaComp = dadosTabelaPlaca.Rows[0]["PLACA"].ToString();
                            kmComp = dadosTabelaPlaca.Rows[0]["KM"].ToString();
                        }
                        else
                        {
                            placaComp = "";
                            kmComp = "";
                        }

                        paragraph = new iTextSharp.text.Paragraph("PLACA: " + placaComp + ". - KM: " + kmComp, font);
                        paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                        document.Add(paragraph);
                    }

                    paragraph = new iTextSharp.text.Paragraph("________________________________________________________", font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    paragraph = new iTextSharp.text.Paragraph("NUMERO SAT: " + numSerieSat, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    //hora SO
                    DateTime data = (DateTime.Now);
                    string dataHora = Convert.ToString(data);

                    paragraph = new iTextSharp.text.Paragraph(dataHora, font);
                    paragraph.Alignment = iTextSharp.text.Element.ALIGN_LEFT;
                    document.Add(paragraph);

                    idCfe = idCfe.Replace("CFe", "");

                    // separando espaco da chave 
                    string I_ = idCfe.Substring(0, 4);
                    string II_ = idCfe.Substring(4, 4);
                    string III_ = idCfe.Substring(8, 4);
                    string IV_ = idCfe.Substring(12, 4);
                    string V_ = idCfe.Substring(16, 4);
                    string VI_ = idCfe.Substring(20, 4);
                    string VII_ = idCfe.Substring(24, 4);
                    string VIII_ = idCfe.Substring(28, 4);
                    string IX_ = idCfe.Substring(32, 4);
                    string X_ = idCfe.Substring(36, 4);
                    string XI_ = idCfe.Substring(40, 4);


                    N = ("NUMERO SAT: " + numSerieSat + "\n" +
                           dataHora + "                                              \n\n" +
                           "  " + I_ + " " + II_ + " " + III_ + " " + IV_ + " " + V_ + " " + VI_ + " " + VII_ + " " + VIII_ + " " + IX_ + " " + X_ + " " + XI_);

                    //GERAR IMAGEM QRCODE
                    QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                    qrCodeEncoder.QRCodeBackgroundColor = System.Drawing.Color.White;
                    qrCodeEncoder.QRCodeForegroundColor = System.Drawing.Color.Black;
                    qrCodeEncoder.CharacterSet = "UTF-8";
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    qrCodeEncoder.QRCodeScale = 1;
                    qrCodeEncoder.QRCodeVersion = 0;
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;

                    string descricaoQrcode = idCfe + "|" + cnpjEmit + "|" + dataEmit + horaEmit + "||" + totalProduto + "|" + retornoVendaSat;

                    System.Drawing.Image imageQRCode;
                    String gerar = descricaoQrcode;
                    imageQRCode = qrCodeEncoder.Encode(gerar);
                    Bitmap qrcode = qrCodeEncoder.Encode(gerar);

                    pictureBox1.Image = qrcode;
                    pictureBox1.Image.Save(pathQRCode + "QRCode.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    GerarCodigoBarra();

                    // Imagem.
                    var codB = iTextSharp.text.Image.GetInstance(pathCodBarra + "CodBarra.jpg");
                    codB.ScaleToFit(90, 30);
                    codB.SetAbsolutePosition(50, 350);
                    contentByte.AddImage(codB);


                    // Imagem.
                    var image_ = iTextSharp.text.Image.GetInstance(pathQRCode + "QRCode.jpg");
                    image_.ScaleToFit(30, 30);
                    image_.SetAbsolutePosition(90, 280);
                    contentByte.AddImage(image_);

                    ImprimiCupomNaoFiscal();

                    document.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string arquivoPdf = pathCustodia + idCfe.ToString() + ".pdf";
            GerarPDFRedu(arquivoPdf);
        }

        public void GerarCodigoBarra()
        {

            BarcodeLib.Barcode codigo = new BarcodeLib.Barcode();
            codigo.IncludeLabel = false;

            idCfe = idCfe.Replace("CFe", "");
            chaveVenda = idCfe;
            pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
            pictureBox2.Image = codigo.Encode(BarcodeLib.TYPE.CODE128, idCfe, 365, 35);
            pictureBox2.Image.Save(pathCodBarra + "CodBarra.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
        }


        public void ImprimeirVendaPDF()
        {
            try
            {
                //imprmi impressora padrao.......................................................
                ProcessStartInfo info = new ProcessStartInfo();
                info.Verb = "print";
                info.FileName = pathCustodia + Ultimachave + ".pdf";
                info.CreateNoWindow = true;
                info.WindowStyle = ProcessWindowStyle.Hidden;
                Process p = new Process();
                p.StartInfo = info;
                p.Start();
                p.WaitForInputIdle();
                System.Threading.Thread.Sleep(3000);
                if (false == p.CloseMainWindow())
                    p.Kill();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ComunicarImporessora()
        {
            var configImpressora = new PrinterSettings();
            MessageBox.Show(configImpressora.PrinterName);
        }

        private void btnVerificarImpressora_Click(object sender, EventArgs e)
        {
            ComunicarImporessora();
        }

        public void AbreCupomNaoFiscal()
        {
            string espaco1 = " ";
            string espaco2 = "  ";
            string pularlinha1 = "\n";
            string pontoFinal = "\r";

            A = (" -----------------------------------------------------------------------------------\n" +
                                                                                  nomeEmit + "\n" +
                                 logradouroEmit + espaco1 + numImit + espaco1 + cidadeEmit + "\n" +
                                                            bairroEmit + espaco1 + cepEmit + "\n" +
                                                   "CNPJ:" + cnpjEmit + "-" + "I.E:" + ieEmi + "\n" +
                                                                       "I.M:" + imEmi + pontoFinal);
        }

        public void VendeItemNaoFiscal()
        {
            try
            {
                string textoVendeItemCupomNaoFiscal = "";
                string descricaoItemCupomNaoFiscal = "";
                string espaco1 = " ";
                string espaco2 = "  ";
                string espaco7 = "                                       ";
                string espaco5 = "     ";
                string espaco3 = "    ";
                string pularlinha1 = "\n";
                string pontoFinal = "\r";
                somaCompra = 0;
                int numCupom = frmVenda.numVenda;
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                dadosTabelaVenda = novaVenda.PesquisaTabelaVenda(Convert.ToInt32(lblEntrada.Text), frmVenda.numcaixa);


                //modelovisual do cupom nao fiscal............................................................................
                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    //01234567890123456789012345678901234567890123456789012345678901234567890123456789
                    B = ("------------------------------------------------------------------------------------\n" +
                         " CPF/CNPJ: " + cpfDest + "\n" +
                         " -----------------------------------------------------------------------------------\n" +
                         "                                 EXTRATO No." + nCfeEmit + "\n" +
                         "                        CUPOM FISCAL ELETRONICO - SAT                               \n" +
                         "                                                                                    \n" +
                         "------------------------------------------------------------------------------------" +
                         "#|COD|DESC|QTD|UNI|VL UN R$|(VL R$)*DESC|VLITEM R$                                   " +
                         "------------------------------------------------------------------------------------");

                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        item = dadosTabelaVenda.Rows[i]["ITEM"].ToString();
                        descri = dadosTabelaVenda.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                        codigoB = dadosTabelaVenda.Rows[i]["COD_BARRA"].ToString();
                        qtde = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["QUANT"]).ToString();
                        preco = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["PRECO"]).ToString();
                        alicota = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                        un = dadosTabelaVenda.Rows[i]["UNID"].ToString();
                        totalProd = dadosTabelaVenda.Rows[i]["TOTAL"].ToString();
                        somaCompra += Convert.ToDecimal(totalProd);
                        //replace retira espaco nulo e vazio....................................
                        codigoB = codigoB.Replace(" ", "");
                        alicota = alicota.Replace(" ", "");
                        un = un.Replace(" ", "");

                        //tratamento de tamanhos dos abjetos....................................

                        string espaco = "_______________________";

                        if (descri.Length < 25)
                        {
                            descri += espaco;
                        }

                        if (descri.Length >= 25)
                        {
                            descri = descri.Substring(0, 21);
                        }

                        int item_ = Convert.ToInt32(item);
                        item = item.PadLeft(3, '0');
                        item = item.Substring(0, 3);

                        codigoB = codigoB.Substring(8, 5);

                        textoVendeItemCupomNaoFiscal = (item + codigoB + descri + un + qtde + preco + totalProd);

                        string I = textoVendeItemCupomNaoFiscal.Substring(0, 3);
                        string II = textoVendeItemCupomNaoFiscal.Substring(3, 5);
                        string III = textoVendeItemCupomNaoFiscal.Substring(8, 21);
                        string IV = textoVendeItemCupomNaoFiscal.Substring(29, 2);
                        string V = textoVendeItemCupomNaoFiscal.Substring(31, 5);
                        string VI = textoVendeItemCupomNaoFiscal.Substring(36, 5);
                        string VII = textoVendeItemCupomNaoFiscal.Substring(41, 4);

                        F += (I + " " + II + " " + III + "  " + IV + " " + V + " " + "x" + VI + " " + VII + "\n");
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void printDocument2_PrintPage(object sender, PrintPageEventArgs e)
        {
            nomeImpressora = nomeImpressora.Replace(" ", "");

            if (nomeImpressora == "BEMASAT")
            {
                string mensagem = (A + "\n" + B + "\n" + F + "\n\n" + C + "\n" + D + "\n" + E + "\n" + G + "\n" + H + "\n" + I + "\n" + J + "\n" + L + "\n" + M + N).ToString();

                var pri = sender as System.Drawing.Printing.PrintDocument;
                using (var fontes = new Font("Arial", 7))
                using (var brush = new SolidBrush(System.Drawing.Color.Black))
                {
                    e.Graphics.DrawString(
                        mensagem,
                        fontes,
                        brush,
                        new RectangleF(0, 0, pri.DefaultPageSettings.PrintableArea.Width, pri.DefaultPageSettings.PrintableArea.Height));
                }
            }
            else if (nomeImpressora == "COMUM")
            {

            }
            else if (nomeImpressora == "LPT")
            {
                string mensagem = (A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + "\n\n\n").ToString();

                var pri = sender as System.Drawing.Printing.PrintDocument;
                using (var font = new Font("Courier", 8))
                using (var brush = new SolidBrush(System.Drawing.Color.Black))
                {
                    e.Graphics.DrawString(
                        mensagem,
                        font,
                        brush,
                        new RectangleF(0, 0, pri.DefaultPageSettings.PrintableArea.Width, pri.DefaultPageSettings.PrintableArea.Height));
                }
            }
        }

        public void ImprimiCupomNaoFiscal()
        {
            PrintDialog printDialog1 = new PrintDialog();
            string texto = "";

            leitor = new StringReader(texto);

            this.printDocument2.Print();
        }

        public void imprimirCod(object o, PrintPageEventArgs e)
        {
            try
            {
                System.Drawing.Image i = pictureBox2.Image;
                pictureBox2.SizeMode = PictureBoxSizeMode.CenterImage;
                //local de impressão 50 x, 50 y.
                e.Graphics.DrawImage(i, -50, 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void imprimirQrCode(object o, PrintPageEventArgs e)
        {
            try
            {
                System.Drawing.Image i = pictureBox1.Image;
                //local de impressão 50 x, 50 y.
                e.Graphics.DrawImage(i, 95, 50);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void imprimirEspaco(object o, PrintPageEventArgs e)
        {
            try
            {
                System.Drawing.Image i = pictureBox3.Image;
                //local de impressão 50 x, 50 y.
                e.Graphics.DrawImage(i, 80, 50);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Time_Tick(object sender, EventArgs e)
        {
            //if (pbFechamentoVenda.Value < 100)
            //{
            //    this.pbFechamentoVenda.Increment(1);
            //}
            //else
            //{
            //    Time.Stop();
            //    pbFechamentoVenda.Visible = false;
            //    pbFechamentoVenda.Value = 0;
            //}
        }

        private void gdvTipoPgto_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            try
            {
                if (impressaoAutomatica == true)
                {
                    MontarCupomAbertoComum();

                    string mensagem = (A + "\n" + cupomComumAberto).ToString();

                    var pri = sender as System.Drawing.Printing.PrintDocument;
                    using (var font = new Font("Courier", 7))
                    using (var brush = new SolidBrush(System.Drawing.Color.Black))
                    {
                        e.Graphics.DrawString(
                            mensagem,
                            font,
                            brush,
                            new RectangleF(0, 0, pri.DefaultPageSettings.PrintableArea.Width, pri.DefaultPageSettings.PrintableArea.Height));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void MontarCupomAbertoComum()
        {
            try
            {
                novoCliente = new RegraNegocio.ClienteRegraNegocio();
                DataTable dadosTabelaCliente = new DataTable();
                dadosTabelaCliente = novoCliente.PesquisarCliente(idCliente);

                nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                cupomComumAberto = ("\n\n    *** COMPROVANTE DE VENDA EM ABERTO ***  " +
                "\n\n" +
                "VALOR TOTAL..........................." + valorAberto.ToString("C2")) + "\n" +
                "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada," +
                " no Valor de " + valorAberto.ToString("C2") + "\n" +
                "ASS:___________________________________________                     \n" +
                "Nº CUPOM:" + frmVenda.numCupom.ToString() + " - Operador:" + frmVenda.operadorAtuante.ToString() +
                "\n" + DateTime.Now.ToShortDateString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DeltarArquivoXml()
        {
            try
            {
                // caminho e nome do arquivo
                string arquivo = (pathVendaXML + _numVenda + "Cx" + frmVenda.numcaixa + ".xml");

                // vamos excluir o arquivo
                File.Delete(arquivo);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtRecebimento_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                frmVenda.PesquisarNumCaixa_NumVenda();
                this.Close();
            }
        }

       
        public void btnFecharVenda_Click(object sender, EventArgs e)
        {
            if (txtTotal.Text != "0,00")
            {
                if ((nomeImpressora == "SAT") || (nomeImpressora == "BEMATECH"))
                {
                    pbFechamentoVenda.Visible = true;
                    this.Time.Start();
                    FecharVenda();
                    PesquisarCupomImagem();
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                }
                else
                {
                    pbFechamentoVenda.Visible = true;
                    this.Time.Start();
                    FecharVenda();
                    PesquisarCupomImagem();
                    pictureBox1.Visible = true;
                    pictureBox2.Visible = true;
                }

                ImprimiCupomAberto();
            }
            else
            {
                frmCliente frmCliente = new frmCliente(this);
                frmCliente.ShowDialog();
            }

            this.Close();
            frmVenda.LoadTela();
        }

        public void CadastrarContaReceber()
        {
            try
            {
                if (idPagamentoVenda <= 0)
                {
                    MessageBox.Show("Codigo de Pagamento Venda não Pode ser Zero", "Atenção");
                }
                else if (valorReceber <= 0)
                {
                    MessageBox.Show("Valor Recebido da Venda não Pode ser Zero", "Atenção");
                }
                else
                {
                    // data vencimento
                    TimeSpan difrenca = TimeSpan.Parse("30");
                    inicio = DateTime.Now.Date;
                    dataVencimento = (inicio + difrenca);
                    valoRecebido = 0;
                    idUsuario = 0;
                    multa = 0;
                    juros = 0;
                    baixado = false;

                    novaContaReceber = new ContaReceberRegraNegocios();
                    novaContaReceber.CadastrarContaReceber(idPagamentoVenda, valoRecebido, valorReceber, dataVencimento, idUsuario, multa, juros, baixado);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
