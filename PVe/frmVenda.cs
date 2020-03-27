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
using System.Drawing.Imaging;
using System.Configuration;
using System.Xml;
using System.Runtime.InteropServices;
using System.IO.Ports;
using System.Threading;
using System.Drawing.Printing;

namespace PVe
{
    public partial class frmVenda : Form
    {
        RegraNegocio.VendaRegraNegocios novaVenda;
        RegraNegocio.ProdutoRegraNegocio novoProduto;
        RegraNegocio.ParametroRegraNegocio novoParametro;
        RegraNegocio.TempRegraNegocios novoTemp;
        RegraNegocio.UsuarioRegraNegocio novoUsuario;
        RegraNegocio.FormaPagamentoRegraNegocio novaFormaPgto;
        RegraNegocio.SenhaRegraNegocio novaSenha;
        RegraNegocio.ImagemRegraNegocio novaImagem;
        RegraNegocio.PagamentoVendaRegraNegocios novoPagamentoVenda;
        RegraNegocio.ClienteRegraNegocio novoCliente;
        RegraNegocio.NumCaixaRegraNegocios novoNumCupom;
        RegraNegocio.NumCaixaRegraNegocios novoCaixa;
        RegraNegocio.EscPos escPos;
        RegraNegocio.SetorRegraNegocios novoSetor;
        RegraNegocio.EstoqueInicialRegraNegocios novoEstoqueIncial;
        RegraNegocio.ConexaoRegraNegocios novaConexao;
        RegraNegocio.TribRegraNegocio novaTrib;
        RegraNegocio.ContaReceberRegraNegocios novaContaReceber;
        RegraNegocio.PlacaRegraNegocio placaRN;

        SerialPort _serialPort;

        //dados Cupom cancelado 
        string xitem, cnpj, xNome, xLgr, xFant, xBairro, nro, xMun, CEP, IE, IM, cProd, xProd, assinaturaQRCODE, cpfDest;
        string uCom, qCom, vProd, vItem, cMP, vMP, vCFe, vTroco, nserieSAT, nCFe, idCfe, cabecalho;
        string cup_A, cup_B, cup_C, cup_D = "";
        string align = "";


        public bool retPesqusiarProduto;

        Boolean vendaNaoFechado = false;
        int codInternoprod = 0;
        decimal qtdeVendida, estoqueAtual, novoEstoque = 0;
        string descricao_produto = "";

        //data 
        int resultado = 0;
        DateTime dataUltimaVenda;

        //VARIAVEL LEITURA X.........................
        string aliquotaX = "";
        decimal valorX = 0;
        int idTipoPagamentoX = 0;
        decimal valorvendaX = 0;
        int qtdeVendaCancelX = 0;
        decimal valorVendaCanceladaX = 0;

        //varivel numCaixa..................................................
        public string numCaixaXml = "";
        public string numComBal = "";
        public string numComimp = "";
        public string bondRouteCom = "";

        public bool fecharCaixa = false;
        public string AX, BX, CX, DX, EX = "";
        bool impressaoDigital;
        bool baixado, fechados;

        //atributos variaveis BEMASAT
        string chaveVenda = "";
        String arqu;
        string codAtivacao = "";

        //elgin
        public string printerName = "ELGIN i9";



        //nomes de arquivos xml
        string pathUltimaVenda = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\ULTIMA_VENDA.xml";
        string pathAssiDitital = Path.GetDirectoryName(Application.ExecutablePath) + "\\Banco\\ASS_DIG.xml";
        string pathVendaCancelada = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\uVENDA_CANCELADA\\";
        string pathDadosVendaCanceladaAutorizada = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\VD_CANC_TEXTO\\";
        string pathCustodia = @"C:\CFe\Custodia\";
        string pathACodAtivacao = Path.GetDirectoryName(Application.ExecutablePath) + "\\Banco\\COD_ATIVACAO.xml";
        string pathNumCaixa = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\NUM_CAIXA.xml";
        string pathCargaProduto = @"C:\CFe\EstoqueIniciaMesProduto\EstoqueIniciaMesProduto-Mes";

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
        public int sessao = 0;

        //variavel de ususario Logado.....................
        public bool abrirCaixa;
        public int idUsuarioLogado = 0;
        public string LoginAtuante, periodoAtuante_ = "";

        //variaveis para balanaça
        string portaBalanca = "";
        int boudRonte = 0;
        string peso = "";
        long tempoTotal;
        public int qtdevenda = 0;
        int milesegundos = 0;

        //atributos
        public string statusLogin;
        public string nomeImpressora = "";

        //VARIAVEIS 2 VIA DO CUPOM.....................................................
        public string A, B, C, D, E, F = "";
        int idTipoPgto = 0;
        decimal valorTipoPgto, trocoTipoPgto, somaTotalVenda = 0;
        string descricaoTipoPgto = "";
        int idClienteVendaAberto = 0;
        string cupomComumAberto = "";

        //VARIAVEIS RELATORIO TOTAL
        string AR, BR, CR, DR, ER = "";
        decimal totalGeralMes = 0;

        //atributos instanciados para chamda no form FrmProdutos
        public string codigoBarra;
        public int numcaixa = 0;
        public int _idProduto;
        public int numVenda;
        public string descricao;
        public int numCupom;
        public string aliquota;
        public int itemAtual;
        public int idUsuario;
        public string NomeUsuario;
        string unid = null;
        public string portaImpressora = "";


        //atributos do produtos impostos.......................
        public string _valorCofins;
        public string cstPis;
        public string _valorPis;
        public string _cstPis;
        public string _cstCofins;
        public string _cfop;
        public string _ncm;
        public string _icmCst;
        public string _origemProduto;
        public string operadorAtuante;
        public string periodoAtuante;
        public string _cest;
        public string _unid;
        public decimal _custo;
        public bool descAuto;
        public decimal precoDescc;
        public decimal qtde_desc;

        //-------------------
        int idCodProduto;
        int idProdutoItem;
        int item;
        int idRetorno;
        int idParametro;
        public int qtdeCupom;

        public string versaoCfe = "";
        //-------------------
        decimal soma = 0;
        decimal qtde = 0;
        decimal preco = 0;
        Boolean atualizado = true;
        Boolean fechado = false;
        string granel = null;
        DateTime dataServidor;

        string codProd;
        public string codQtde;
        public string codPeso;
        string verificarCodBalanca;
        decimal valortotal = 0;

        string codEtiquetaB;
        int parametroEtiqueta;
        public Boolean statusVenda;

        //strin codProduto do formulario Venda..... 
        string codBarra = "0000000000000";
        frmFechamentoVenda frmFecharVenda;
        frmPrincipal frmPrincipal;

        public frmVenda(frmPrincipal fprincipal)
        {
            InitializeComponent();
            gdvEntrada.AutoGenerateColumns = false;
            ApontarUltimaLinhaGrid();
            this.frmPrincipal = fprincipal;
        }

        public void DesbloquearGrid()
        {
            if (nomeImpressora == "DARUMA")
            {
                txtProduto.Text = "AGUARDE ALGUNS SEGUNDOS PARA CONECTAR COM IMPRESSORA";
            }

            gdvEntrada.Enabled = true;
            gdvEntrada.AutoResizeColumns();
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

        public void GerarRelatorioSegundaVia()
        {
            
            try
            {
                nomeImpressora = nomeImpressora.Replace(" ", "");

                if ((nomeImpressora == "SAT") || (nomeImpressora == "BEMATECH"))
                {
                    string estabelecimento, endereco, numero, cep, bairro = "";
                    string usuario = tsNome.Text;
                    string codBarra;
                    string desc;
                    string total;
                    string qtde;
                    string espaco7 = "       ";
                    string espaco5 = "   ";
                    string espaco3 = "  ";
                    decimal sTotal = 0;

                    int ultimaVenda = 0;
                    int numeroCupom = 0;

                    ultimaVenda = (numCupom - 1);

                    ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabelaVenda = new DataTable();
                    dadosTabelaVenda = novaVenda.ReimprimirUltimaVenda(ultimaVenda, numcaixa);


                    if (dadosTabelaVenda.Rows.Count > 0)
                    {
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(espaco7 + "**** SEGUNDA VIA ****");
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("#|COD|DESC|QT|UN|VUNR$|(VLR$)*DESC|VLIT R$\n");

                        for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                        {
                            codBarra = dadosTabelaVenda.Rows[i]["COD_BARRA"].ToString();
                            desc = dadosTabelaVenda.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                            qtde = dadosTabelaVenda.Rows[i]["QUANT"].ToString();
                            total = dadosTabelaVenda.Rows[i]["TOTAL"].ToString();

                            sTotal += Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());

                            total = dadosTabelaVenda.Rows[i]["TOTAL"].ToString();

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

                            total.PadLeft(5, '0');
                            total = total.Replace(" ", "");

                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(codBarra + espaco3 + desc + espaco3 + qtde + espaco5 + total.ToString() + "\r");
                            total = "";
                        }

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("TOTAL VENDA:" + sTotal.ToString("C2"));
                        sTotal = 0;

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
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Numero Venda: " + ultimaVenda.ToString());
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("\n");
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(placa.ToString());

                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                        RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);

                        MessageBox.Show("Segunda Via do Cupom Nº: " + ultimaVenda + " foi Gerado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCodigo.Focus();
                    }
                }
                else if (nomeImpressora == "COMUM")
                {
                    AbreCupomSegViaComum();
                    VendeItemComumSegVia();

                    ImprimiSeundaViaCupomFiscal();

                    if (idTipoPgto == 4)
                    {
                        ImprimiCupomAberto();
                    }
                }
                else if (nomeImpressora == "DARUMA")
                {
                    AbreCupomSegViaComum();
                    VendeItemComumSegVia();

                    int idRetonro = RegraNegocio.DLLsDaruma.iImprimirTexto_DUAL_DarumaFramework(A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + "\n" + F + "<c> Protocolo de Autorização.</c><sl>4</sl><gui></gui><l></l>", 0);

                    if (idTipoPgto == 4)
                    {
                        ImprimiCupomAberto();
                    }
                }

                else if (nomeImpressora == "ELGIN")
                {
                    AbreCupomSegViaComum();
                    VendeItemComumSegVia();

                    string dadosElgin = (A + "\n" + B + "\n" + C + "\n" + E + "\n" + F + "\n" + placa);

                    escPos = new RegraNegocio.EscPos();

                    var configImpressora = new PrinterSettings();
                    Console.WriteLine(configImpressora.PrinterName);

                    printerName = configImpressora.PrinterName;

                    this.escPos.printText(printerName, dadosElgin);
                    this.escPos.lineFeed(printerName, 2);

                    feedAndCutter(printerName, 5);

                    if (idTipoPgto == 4)
                    {
                        ImprimiCupomAberto();
                    }
                }
                else if (nomeImpressora == "MP2500")
                {
                    AbreCupomSegViaBemasat();
                    VendeItemComumSegViaBemsat();

                    // PesquisarPortaImpressora();

                    PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();

                    idRetorno = RegraNegocio.MP2032.IniciaPorta(numComimp);
                    idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);

                    string mensagem = "";

                    mensagem = (A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + "\n" + F).ToString();

                    mensagem = mensagem.PadLeft(0, '0');

                    idRetorno = RegraNegocio.MP2032.BematechTX(mensagem);
                    idRetorno = RegraNegocio.MP2032.BematechTX("\n\n");


                    RegraNegocio.MP2032.AcionaGuilhotina(0);
                    idRetorno = RegraNegocio.MP2032.FechaPorta();

                    if (idTipoPgto == 4)
                    {
                        ImprimiCupomAberto();
                    }

                    mensagem = "";
                }

                else if (nomeImpressora == "BEMASAT")
                {
                    AbreCupomSegViaBemasat();
                    VendeItemComumSegViaBemsat();

                    // PesquisarPortaImpressora();

                    PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();

                    idRetorno = RegraNegocio.MP2032.IniciaPorta(numComimp);
                    idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);

                    string mensagem = "";

                    mensagem = (A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + "\n" + F + "\n" + placa).ToString();

                    mensagem = mensagem.PadLeft(0, '0');
                    idRetorno = RegraNegocio.MP2032.BematechTX(mensagem);
                    idRetorno = RegraNegocio.MP2032.BematechTX("\n\n");


                    RegraNegocio.MP2032.AcionaGuilhotina(0);
                    idRetorno = RegraNegocio.MP2032.FechaPorta();

                    if (idTipoPgto == 4)
                    {
                        ImprimiCupomAberto();
                    }

                    mensagem = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Verifique se Impressora Instalada é Bematech.");
            }
        }

        decimal somaTotalAberto = 0;

        private void VendeItemComumSegViaBemsat()
        {
            try
            {
                string textoVendeItemCupomNaoFiscal = "";
                string descricaoItemCupomNaoFiscal = "";
                string espaco1 = " ";
                string espaco2 = "  ";
                string espaco7 = "                                                ";
                string espaco5 = "     ";
                string espaco3 = "    ";
                string pularlinha1 = "\n";
                string pontoFinal = "\r";

                decimal somaCompra = 0;
                int numCupom = Convert.ToInt32(lblNumeroVenda.Text);

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                dadosTabelaVenda = novaVenda.PesquisaTabelaVenda((numCupom - 1), numcaixa);

                string traco = "...........................";
                B = "";
                string textoVendeItem = "";

                //modelovisual do cupom nao fiscal............................................................................
                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    if (nomeImpressora == "BEMASAT")
                    {
                        B = ("-----------------------------------------\n" +
                                        "   EXTRATO No." + (numCupom - 1) + "\n" +
                                        "           *** SEGUNDA VIA ***           \n" +
                                        "-----------------------------------------\n" +
                                        "#|COD|DESC|QT|UN|VUNR$|(VLR$)*DESC|VLIT R$\n" +
                                        "-----------------------------------------"
                                        );
                    }
                    else if (nomeImpressora == "MP2500")
                    {
                        B = ("----------------------------------------------\n" +
                             "EXTRATO No." + (numCupom - 1) + "\n" +
                             "              *** SEGUNDA VIA ***             \n" +
                             "----------------------------------------------\n" +
                             "#|COD|DESC  |QT|UN|VUNR$|(VLR$) * DESC|VLIT R$\n" +
                             "------------------------------------------------"
                                        );
                    }

                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        string descri = dadosTabelaVenda.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                        string item_ = dadosTabelaVenda.Rows[i]["ITEM"].ToString();
                        string codigoB = dadosTabelaVenda.Rows[i]["COD_BARRA"].ToString();
                        decimal qtde = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["QUANT"]);
                        decimal preco = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["PRECO"]);
                        string alicota = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                        decimal total = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"]);
                        string un = dadosTabelaVenda.Rows[i]["UNID"].ToString();

                        somaCompra += Convert.ToDecimal(total);
                        somaTotalVenda = total;
                       

                        //replace retira espaco nulo e vazio....................................
                        codigoB = codigoB.Replace(" ", "");
                        alicota = alicota.Replace(" ", "");
                        un = un.Replace(" ", "");

                        //tratamento de tamanhos dos abjetos....................................

                        item_ = item_.PadLeft(3, '0');

                        codigoB = codigoB.Substring(8, 5);

                        descri += espaco7;

                        if (descri.Length > 13)
                        {
                            descri = descri.Substring(0, 12);
                        }

                        decimal qtde_ = Convert.ToDecimal(qtde);

                        string pre_p = Convert.ToString(preco);

                        if (pre_p.Length == 4)
                        {
                            pre_p = pre_p.PadLeft(1, ' ');
                        }

                        if (pre_p.Length == 6)
                        {
                            pre_p = pre_p.Substring(0, 5);
                        }

                        string somaS = Convert.ToString(total);

                        if (somaS.Length == 4)
                        {
                            somaS = somaS.PadLeft(1, '0');
                        }

                        textoVendeItem += (item_ + " " + codigoB + " " + descri + " " + un + " " + qtde_.ToString("N3") + " " + pre_p + " " + total + "\n");
                        total = 0;
                    }

                    somaTotalAberto = somaCompra;

                    C = (textoVendeItem).ToString();

                    if (nomeImpressora == "mp2500")
                    {
                        D = ("------------------------------------------");
                    }
                    else if (nomeImpressora == "BEMASAT")
                    {
                        D = ("-----------------------------------");
                    }

                    PesquisaTipoPagamentoVenda();

                    string tracoTrco = ".............................";

                    if (idTipoPgto == 1)
                    {
                        tracoTrco = "........................";
                    }
                    else
                    {
                        tracoTrco = "..........................";
                    }

                    E = ("TOTAL" + traco + somaCompra.ToString("c2") + "\n" +
                         descricaoTipoPgto + tracoTrco + valorTipoPgto.ToString("C2") + "\n" +
                         "TROCO:.........................." + trocoTipoPgto.ToString("C2")
                        );

                    E = E.PadLeft(0, ' ');

                    string user = tsNome.Text;
                    string perio = tsPeriodo.Text;

                    user = user.Replace(" ", "");
                    perio = perio.Replace(" ", "");

                    string obsContribuinte = (
                                              "------------------------------------------\n" +
                                              "OBSERVAÇOES DO CONTRIBUINTE               \n" +
                                              "Nº Caixa:" + lblNumeroCaixa.Text + ". CUPOM Nº" + (numCupom - 1) + "\n" +
                                              "OP:" + user + ". - TURNO:" + tsPeriodo.Text + "\n" +
                                              "Data:" + DateTime.Now.Date.ToShortDateString() + "  Hora:" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + "\r\r\r\n" +
                                              "TROCA DE PRODUTO(S) SOMENTE COM O CUPOM \n" +
                                              "OBRIGADO...VOLTE SEMPRE!!!\n\n\n");

                    F = obsContribuinte.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

       public decimal aliqDia = 0;

        public void AbreCupomSegViaBemasat()
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
                A = "";

                string nomeCliente = dadosTabelaParametro.Rows[0]["NOME_FANTASIA"].ToString();
                string enderecoCliente = dadosTabelaParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                string numeroCliente = dadosTabelaParametro.Rows[0]["NUMERO"].ToString();
                string bairroCliente = dadosTabelaParametro.Rows[0]["BAIRRO"].ToString();
                string cidadeCliente = dadosTabelaParametro.Rows[0]["CIDADE"].ToString();
                string ufCliente = dadosTabelaParametro.Rows[0]["UF"].ToString();
                string cepCliente = dadosTabelaParametro.Rows[0]["CEP"].ToString();
                string cnpjCliente = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                string ieCliente = dadosTabelaParametro.Rows[0]["IE"].ToString();
                aliqDia = Convert.ToDecimal(dadosTabelaParametro.Rows[0]["ALIQUOTA_DIA"]);

                if (nomeImpressora == "BEMASAT")
                {
                    A = ("------------------------------------------\n" +
                               nomeCliente + "\n" +
                               enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                               bairroCliente + "\n" +
                               cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                               "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal);
                }
                else if (nomeImpressora == "MP2500")
                {
                    A = ("------------------------------------------------\n" +
                            nomeCliente + "\n" +
                            enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                            bairroCliente + "\n" +
                            cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                            "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal);
                }

                else if (nomeImpressora == "ELGIN")
                {
                    A = ("------------------------------------------------\n" +
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

        private void ImprimiCupomAberto()
        {
            try
            {
                MontarCupomAbertoComum();

                if ((nomeImpressora != "ELGIN") && (nomeImpressora != "DARUMA") && (nomeImpressora != "MP2500"))
                {
                    PrintDialog printDialog2 = new PrintDialog();
                    this.printDocument2.Print();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ImprimiSeundaViaCupomFiscal()
        {
            PrintDialog printDialog1 = new PrintDialog();
            this.printDocument1.Print();
        }

        public void MontarCupomAbertoComum()
        {
            try
            {
                if (nomeImpressora == "COMUM")
                {
                    novoCliente = new RegraNegocio.ClienteRegraNegocio();
                    DataTable dadosTabelaCliente = new DataTable();
                    dadosTabelaCliente = novoCliente.PesquisarCliente(idClienteVendaAberto);

                    string nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                    cupomComumAberto = ("\n\n    *** COMPROVANTE DE VENDA EM ABERTO ***  " +
                    "\n\n" +
                    "VALOR TOTAL.............................." + somaTotalVenda.ToString("C2")) + "\n" +
                    "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada," +
                    " no Valor de " + valorTipoPgto.ToString("C2") + "\n\n" +
                    "ASS:___________________________________________                     \n" +
                    "Nº CUPOM:" + (numCupom - 1) + " - Operador:" + tsNome.ToString() +
                    "\n" + DateTime.Now.ToShortDateString() + "\r";
                }
                else if (nomeImpressora == "BEMASAT")
                {
                    novoCliente = new RegraNegocio.ClienteRegraNegocio();
                    DataTable dadosTabelaCliente = new DataTable();
                    dadosTabelaCliente = novoCliente.PesquisarCliente(idClienteVendaAberto);

                    string nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                    cupomComumAberto = ("\n\n       *** COMPROVANTE DE VENDA EM ABERTO ***     " +
                    "\n\n" +
                    "VALOR TOTAL......................................................" + somaTotalAberto.ToString("C2")) + "\n" +
                    "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada," +
                    " no Valor de " + somaTotalAberto.ToString("C2") + "\n\n" +
                    "ASS:______________________________________________                  \n" +
                    "Nº CUPOM:" + (numCupom - 1) + " - Operador:" + tsNome.ToString() +
                    "\n" + DateTime.Now.ToShortDateString() + "\r";
                }
                else if (nomeImpressora == "MP2500")
                {
                    novoCliente = new RegraNegocio.ClienteRegraNegocio();
                    DataTable dadosTabelaCliente = new DataTable();
                    dadosTabelaCliente = novoCliente.PesquisarCliente(idClienteVendaAberto);

                    PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                    idRetorno = RegraNegocio.MP2032.FechaPorta();

                    idRetorno = RegraNegocio.MP2032.IniciaPorta(numComimp);
                    idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);

                    string nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                    cupomComumAberto = ("\n\n       *** COMPROVANTE DE VENDA EM ABERTO ***     " +
                    "\n\n" +
                    "VALOR TOTAL................................" + somaTotalVenda.ToString("C2")) + "\n" +
                    "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada," +
                    " no Valor de " + valorTipoPgto.ToString("C2") + "\n\n" +
                    "ASS:______________________________________________                  \n" +
                    "Nº CUPOM:" + (numCupom - 1) + " - Operador:" + tsNome.ToString() +
                    "\n" + DateTime.Now.ToShortDateString() + "\n\r";

                    idRetorno = RegraNegocio.MP2032.BematechTX(cupomComumAberto);
                    idRetorno = RegraNegocio.MP2032.BematechTX("\n\n\n\n");


                    RegraNegocio.MP2032.AcionaGuilhotina(0);
                    idRetorno = RegraNegocio.MP2032.FechaPorta();

                }
                else if (nomeImpressora == "ELGIN")
                {
                    novoCliente = new RegraNegocio.ClienteRegraNegocio();
                    DataTable dadosTabelaCliente = new DataTable();
                    dadosTabelaCliente = novoCliente.PesquisarCliente(idClienteVendaAberto);

                    string nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                    cupomComumAberto = ("\n\n    *** COMPROVANTE DE VENDA EM ABERTO ***  " +
                    "\n\n" +
                    "VALOR TOTAL.............................." + valorTipoPgto.ToString("C2")) + "\n" +
                    "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada," +
                    " no Valor de " + valorTipoPgto.ToString("C2") + "\n\n" +
                    "ASS:___________________________________________                     \n" +
                    "Nº CUPOM:" + (numCupom - 1) + " - Operador:" + tsNome.ToString() +
                    "\n" + DateTime.Now.ToShortDateString() + "\r";

                    escPos = new RegraNegocio.EscPos();

                    this.escPos.printText(printerName, cupomComumAberto);
                    this.escPos.lineFeed(printerName, 2);

                    feedAndCutter(printerName, 5);
                }
                else if (nomeImpressora == "DARUMA")
                {
                    novoCliente = new RegraNegocio.ClienteRegraNegocio();
                    DataTable dadosTabelaCliente = new DataTable();
                    dadosTabelaCliente = novoCliente.PesquisarCliente(idClienteVendaAberto);

                    string nomeCliente = dadosTabelaCliente.Rows[0]["NOME"].ToString();

                    cupomComumAberto = ("\n\n    *** COMPROVANTE DE VENDA EM ABERTO ***  " +
                    "\n\n" +
                    "VALOR TOTAL.............................." + valorTipoPgto.ToString("C2")) + "\n" +
                    "Eu " + nomeCliente + " Reconheço e Pagarei a divida aqui Representada," +
                    " no Valor de " + valorTipoPgto.ToString("C2") + "\n\n" +
                    "ASS:___________________________________________                     \n" +
                    "Nº CUPOM:" + (numCupom - 1) + " - Operador:" + tsNome.ToString() +
                    "\n" + DateTime.Now.ToShortDateString() + "\r";

                    idRetorno = RegraNegocio.DLLsDaruma.iImprimirTexto_DUAL_DarumaFramework(A + "\n" + cupomComumAberto + "\n" + "<c> Protocolo de Autorização.</c><sl>4</sl><gui></gui><l></l>", 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AbreCupomSegViaComum()
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

                string nomeCliente = dadosTabelaParametro.Rows[0]["NOME_FANTASIA"].ToString();
                string enderecoCliente = dadosTabelaParametro.Rows[0]["ENDERECO_EMPRESA"].ToString();
                string numeroCliente = dadosTabelaParametro.Rows[0]["NUMERO"].ToString();
                string bairroCliente = dadosTabelaParametro.Rows[0]["BAIRRO"].ToString();
                string cidadeCliente = dadosTabelaParametro.Rows[0]["CIDADE"].ToString();
                string ufCliente = dadosTabelaParametro.Rows[0]["UF"].ToString();
                string cepCliente = dadosTabelaParametro.Rows[0]["CEP"].ToString();
                string cnpjCliente = dadosTabelaParametro.Rows[0]["CNPJ"].ToString();
                string ieCliente = dadosTabelaParametro.Rows[0]["IE"].ToString();

                A = ("------------------------------------------------\n" +
                     nomeCliente + "\n" +
                     enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                     bairroCliente + "\n" +
                     cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                     "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void VendeItemComumSegVia()
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

                decimal somaCompra = 0;
                int numCupom = Convert.ToInt32(lblNumeroVenda.Text);

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                dadosTabelaVenda = novaVenda.PesquisaTabelaVenda((numCupom - 1), numcaixa);

                string traco = "..................................";

                //modelovisual do cupom nao fiscal............................................................................
                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    B = ("-----------------------------------------------\n" +
                         "                EXTRATO No." + (numCupom - 1) + "\n" +
                         "              *** SEGUNDA VIA ***              \n" +
                         "-----------------------------------------------\n" +
                         "#|COD|DESC|QT|UN|VUNR$|(VLR$)*DESC|VLIT R$\n" +
                         "-----------------------------------------------"
                         );

                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        string descri = dadosTabelaVenda.Rows[i]["DESCRICAO_PRODUTO"].ToString();
                        string item = dadosTabelaVenda.Rows[i]["ITEM"].ToString();
                        string codigoB = dadosTabelaVenda.Rows[i]["COD_BARRA"].ToString();
                        decimal qtde = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["QUANT"]);
                        decimal preco = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["PRECO"]);
                        string alicota = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                        decimal total = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"]);
                        string un = dadosTabelaVenda.Rows[i]["UNID"].ToString();

                        somaCompra += Convert.ToDecimal(total);

                        //replace retira espaco nulo e vazio....................................
                        codigoB = codigoB.Replace(" ", "");
                        alicota = alicota.Replace(" ", "");
                        un = un.Replace(" ", "");

                        //tratamento de tamanhos dos abjetos....................................

                        item = item.PadLeft(3, '0');

                        codigoB = codigoB.Substring(8, 5);

                        descri += espaco7;

                        if (descri.Length > 10)
                        {
                            descri = descri.Substring(0, 10);
                        }

                        decimal qtde_ = Convert.ToDecimal(qtde);

                        string pre_p = Convert.ToString(preco.ToString("N2"));
                        pre_p = pre_p.PadLeft(7, ' ');

                        string qq = Convert.ToString(qtde_.ToString("N2"));
                        qq = qq.PadLeft(7, ' ');

                        string somaS = Convert.ToString(total.ToString("N2"));
                        somaS = somaS.PadLeft(7, ' ');

                        textoVendeItemCupomNaoFiscal += (item + " " + codigoB + " " + descri + " " + un + " " + qq + " " + pre_p + " " + somaS + "\r\n");
                    }

                    PesquisaTipoPagamentoVenda();

                    C = textoVendeItemCupomNaoFiscal;

                    string tracoTrco = "";

                    if (idTipoPgto == 1)
                    {
                        tracoTrco = "                                 ";
                    }
                    else
                    {
                        tracoTrco = "                                   ";
                    }

                    string s = Convert.ToString(somaCompra.ToString("N2"));
                    s = s.PadLeft(8, ' ');

                    string vtp = valorTipoPgto.ToString("N2");
                    vtp = vtp.PadLeft(8, ' ');

                    string vttp = trocoTipoPgto.ToString("N2");
                    vttp = vttp.PadLeft(8, ' ');

                    if (descricaoTipoPgto.Length == 8)
                    {
                        descricaoTipoPgto.PadRight(2, ' ');
                    }

                    if (descricaoTipoPgto.Length == 6)
                    {
                        descricaoTipoPgto = descricaoTipoPgto.PadRight(8, ' ');
                    }

                    E = ("TOTAL                                  " + s + "\n" +
                          descricaoTipoPgto + "                               " + vtp + "\n" +
                         "TROCO:                                 " + vttp);

                    string user = tsNome.Text;
                    string perio = tsPeriodo.Text;

                    user = user.Replace(" ", "");
                    perio = perio.Replace(" ", "");

                    string obsContribuinte = (
                                              "----------------------------------------------\n" +
                                              "OBSERVAÇOES DO CONTRIBUINTE                  \n" +
                                              "Nº Caixa:" + lblNumeroCaixa.Text + ". CUPOM Nº" + (numCupom - 1) + "\n" +
                                              "OP:" + user + ". - TURNO:" + tsPeriodo.Text + "\n" +
                                              "Data:" + DateTime.Now.Date.ToShortDateString() + "  Hora:" + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + "\n" +
                                              "TROCA DE PRODUTO(S) SOMENTE COM O CUPOM \n" +
                                              "OBRIGADO...VOLTE SEMPRE!!!\n");

                    F = obsContribuinte.ToString();
                    PesquisaPlaca();
                    F += "\n" + placa;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        string placa;
        private void PesquisaPlaca()
        {
            placa = "";
            try
            {
                placaRN = new RegraNegocio.PlacaRegraNegocio();   
                DataTable dadosTabela = new DataTable();
                int ultimoCupom = (numCupom - 1);
                string numCupom_= ultimoCupom.ToString();
                dadosTabela = placaRN.PesquisarPlaca(numCupom_);

                if (dadosTabela.Rows.Count > 0)
                {
                    placa = " PLACA: " + (dadosTabela.Rows[0]["PLACA"].ToString());
                    placa += " KM = "+(dadosTabela.Rows[0]["KM"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisaTipoPagamentoVenda()
        {
            try
            {
                novoPagamentoVenda = new RegraNegocio.PagamentoVendaRegraNegocios();
                DataTable dadosTabelaPagamentoVenda = new DataTable();
                int ultimoCupom = (numCupom - 1);
                dadosTabelaPagamentoVenda = novoPagamentoVenda.PesquisarTipoPagamentoVenda(ultimoCupom, numcaixa);

                if (dadosTabelaPagamentoVenda.Rows.Count > 0)
                {
                    idTipoPgto = (Convert.ToInt32(dadosTabelaPagamentoVenda.Rows[0]["TIPO_PAGAMENTO"].ToString()));
                    valorTipoPgto = Convert.ToDecimal(dadosTabelaPagamentoVenda.Rows[0]["VALOR"].ToString());
                    trocoTipoPgto = Convert.ToDecimal(dadosTabelaPagamentoVenda.Rows[0]["TROCO"].ToString());
                    idClienteVendaAberto = Convert.ToInt32(dadosTabelaPagamentoVenda.Rows[0]["ID_CLIENTE"].ToString());

                    if (idTipoPgto == 1)
                    {
                        descricaoTipoPgto = "DINHEIRO";
                    }

                    else if (idTipoPgto == 2)
                    {
                        descricaoTipoPgto = "CARTÃO";
                    }

                    else if (idTipoPgto == 3)
                    {
                        descricaoTipoPgto = "CHEQUE";
                    }
                    else if (idTipoPgto == 4)
                    {
                        descricaoTipoPgto = "ABERTO";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmVenda_Load(object sender, EventArgs e)
        {
            LoadTela();

            PesquisarImpressoa();

            if ((tsStatus.Text == "ABERTO") && idUsuarioLogado > 0)
            {
                this.Refresh();
                AtualizarGridAberto();
                versaoCfe = lblVersao.Text;
                nomeImpressora = lblImpreissora.Text;
                DesbloquearBotoesTelaVenda();
                this.Refresh();
                lblItens.Text = gdvEntrada.Rows.Count.ToString();
                SalvarEstoqueInicialMes();
            }
            else
            {
                frmLogin fl = new frmLogin(this);
                fl.ShowDialog();

                if (abrirCaixa == true)
                {
                    txtCodigo.Enabled = false;
                    AtualizarGridAberto();
                }
                else
                {
                    if ((tsStatus.Text == "ABERTO") && idUsuarioLogado > 0)
                    {
                        this.Refresh();
                        AtualizarGridAberto();
                        versaoCfe = lblVersao.Text;
                        nomeImpressora = lblImpreissora.Text;
                        DesbloquearBotoesTelaVenda();
                        this.Refresh();
                        lblItens.Text = gdvEntrada.Rows.Count.ToString();
                    }
                    else
                    {
                        fl = new frmLogin(this);
                        fl.ShowDialog();

                        if (abrirCaixa == true)
                        {
                            txtCodigo.Enabled = false;
                            AtualizarGridAberto();
                        }
                        else
                        {
                            LoadTela();
                        }
                    }
                }
            }
        }

        public void LoadTela()
        {
            txtCodigo.Enabled = false;
            PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
            PesquisarStatusCaixa();
            PesquisarNumCaixa_NumVenda();
            PesquisarLoginAtuante();
            PesquisarEntrada();
            lblItens.Text = gdvEntrada.Rows.Count.ToString();
            AtualizarGridAberto();
            ComparaDataAberturaCaixa();
           

              if (statusVenda == false)
            {
                tsNome.Text = "...";
                tsPeriodo.Text = "...";
            }
        }

        public void ComparaDataAberturaCaixa()
        {
            if (nomeImpressora == "BEMASAT")
            {
                PesquisarDataVendaAberto();

                DateTime data1 = DateTime.Now.Date;
                string data2 = dataUltimaVenda.ToShortDateString();

                resultado = DateTime.Compare(data1, Convert.ToDateTime(data2));

                if (resultado > 0)
                {
                    fecharCaixa = true;
                    AbreCupomLeituraXBemasat();
                    VendaXBemasat();
                    AliquotaXBemasat();
                    VendaCanceladaBemasat();
                    ObservacoesContruite();

                    //  PesquisarPortaImpressora();
                    PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();

                    RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                    RegraNegocio.MP2032.IniciaPorta(numComimp);

                    LerTextoCupom();

                    RegraNegocio.MP2032.BematechTX("\n\n\n\n");
                    RegraNegocio.MP2032.AcionaGuilhotina(0);

                    RegraNegocio.MP2032.FechaPorta();

                    PesquisaItemVendido();
                    //metodo altera estatus do baixado e fechado do pagamento venda............................
                    AlteraVendaPagamentoFechado();

                    //metodo altera status de baixado e fech da vendqa........................................
                    AlterarBaixadoVenda();

                    MessageBox.Show("Redução Z e Baixa na Venda do dia " + dataUltimaVenda + " não foi Realizado, retire seu Cupom", "Redução Z", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void AlterarBaixadoVenda()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                novaVenda.AlterarBaixadoVenda(idUsuario, numcaixa);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Alterar Baixado Venda.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void AlteraVendaPagamentoFechado()
        {
            try
            {
                novaFormaPgto = new RegraNegocio.FormaPagamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                int idUsuario = Convert.ToInt32(idUsuarioLogado);

                novaFormaPgto.AlteraVendaPagamentoFechado(idUsuario, numcaixa);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Altera Venda Pagamento Fechado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void PesquisaItemVendido()
        {
            try
            {
                //contador de total de produtos vendidos....................................................................

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                dadosTabelaVenda = novaVenda.PesquisaVendaNaoFechado(vendaNaoFechado, numcaixa);

                int contador = 0;

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        codInternoprod = Convert.ToInt32(dadosTabelaVenda.Rows[i]["ID_PROD"].ToString());
                        qtdeVendida = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["QUANT"].ToString());

                        //  PesquisarEstoqueAtual();

                        contador = contador + 1;
                    }

                    //lblQtdeDadosImportado.Text = contador.ToString();
                }
                else
                {
                    MessageBox.Show("Não há Dado(s) para ser(em) Baixado(s)", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                        ////metodo para fechar venda baixado............................................................
                        novaVenda = new RegraNegocio.VendaRegraNegocios();
                        novaVenda.FecharVenda(codInternoprod);
                    }
                }
                else
                {
                    MessageBox.Show("Não contem Elementos para ser(em) Baixado(s).", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarDataVendaAberto()
        {
            novaVenda = new RegraNegocio.VendaRegraNegocios();
            DataTable dadosTabelaVenda = new DataTable();

            dadosTabelaVenda = novaVenda.PesquisarDataVendaAberto(numCaixaXml);

            if (dadosTabelaVenda.Rows.Count > 0)
            {
                dataUltimaVenda = Convert.ToDateTime(dadosTabelaVenda.Rows[0]["DATA"].ToString());
            }
        }

        public void PesquisarStatusCaixa()
        {
            try
            {
                novoNumCupom = new RegraNegocio.NumCaixaRegraNegocios();
                DataTable dadosTableaNumCaixa = new DataTable();
                dadosTableaNumCaixa = novoNumCupom.PesquisarNumCaixa_NumVenda(Convert.ToInt32(numCaixaXml));

                if (dadosTableaNumCaixa.Rows.Count > 0)
                {
                    statusVenda = Convert.ToBoolean(dadosTableaNumCaixa.Rows[0]["STATUS_CAIXA"].ToString());
                    nomeImpressora = dadosTableaNumCaixa.Rows[0]["NOME_IMPRESSORA"].ToString();

                    nomeImpressora = nomeImpressora.Replace(" ", "");

                    lblImpreissora.Text = nomeImpressora;

                    if (statusVenda == true)
                    {
                        tsStatus.Text = "ABERTO";
                    }
                    else
                    {
                        tsStatus.Text = "FECHADO";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AlterarNumVendaNumCaixa()
        {
            try
            {
                numVenda = Convert.ToInt32(lblNumeroVenda.Text);
                numVenda = (numVenda + 1);
                novoNumCupom = new RegraNegocio.NumCaixaRegraNegocios();
                novoNumCupom.AlterarNumVendaNumCaixa(numcaixa, numVenda);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarNumCaixa_NumVenda()
        {
            try
            {
                novoNumCupom = new RegraNegocio.NumCaixaRegraNegocios();
                DataTable dadosTabelaNumVenda = new DataTable();

                dadosTabelaNumVenda = novoNumCupom.PesquisarNumCaixa_NumVenda(Convert.ToInt32(numCaixaXml));

                if (dadosTabelaNumVenda.Rows.Count > 0)
                {
                    numCupom = Convert.ToInt32(dadosTabelaNumVenda.Rows[0]["NUM_VENDA"].ToString());
                    lblNumeroVenda.Text = numCupom.ToString();
                }
                else
                {
                    MessageBox.Show("Não Contém Numero de Caixa nº " + numcaixa.ToString(), "Atenção");
                    txtCodigo.ReadOnly = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AtualizarGridAberto()
        {
            try
            {
                PesquisaUltimoItem();
                //  PesquisarIdParametro();

                txtCodigo.Focus();
                txtCodigo.Enabled = true;
                txtCodigo.Focus();

                //statusLogin = tsStatus.Text;
                IlustraLogo();
                MensagemCaixa();
                ApontarUltimaLinhaGrid();
                PesquisarEntrada();
                lblItens.Text = gdvEntrada.Rows.Count.ToString();
                SomaTotalGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void PesquisarLoginAtuante()
        {
            try
            {
                novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                DataTable dadosTabelaUsuario = new DataTable();

                dadosTabelaUsuario = novoUsuario.PesquisaLoginAtuante(numCaixaXml);

                if (dadosTabelaUsuario.Rows.Count > 0)
                {
                    idUsuarioLogado = Convert.ToInt32(dadosTabelaUsuario.Rows[0]["ID_USUARIO"].ToString());

                    operadorAtuante = dadosTabelaUsuario.Rows[0]["NOME"].ToString();
                    periodoAtuante_ = dadosTabelaUsuario.Rows[0]["PERIODO"].ToString();

                    operadorAtuante = operadorAtuante.Replace(" ", "");
                    periodoAtuante_ = periodoAtuante_.Replace(" ", "");

                    tsNome.Text = operadorAtuante;
                    tsPeriodo.Text = periodoAtuante_;
                }
                else
                {
                    tsNome.Text = "...";
                    tsPeriodo.Text = "...";
                    BloquearBotoes();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarNumCaixa_numBalanca_numPorstaCom_Xml()
        {
            try
            {
                XmlTextReader x = new XmlTextReader(pathNumCaixa);

                while (x.Read())
                {
                    if (x.NodeType == XmlNodeType.Element && x.Name == "Num")
                        numCaixaXml = (x.ReadString());

                    if (x.NodeType == XmlNodeType.Element && x.Name == "NumComBal")
                        numComBal = (x.ReadString());

                    if (x.NodeType == XmlNodeType.Element && x.Name == "NumComImp")
                        numComimp = (x.ReadString());

                    if (x.NodeType == XmlNodeType.Element && x.Name == "BoundRoute")
                        bondRouteCom = (x.ReadString());
                }

                x.Close();

                numCaixaXml = numCaixaXml.PadLeft(3, '0');
                lblNumeroCaixa.Text = numCaixaXml.ToString();

                numcaixa = Convert.ToInt32(numCaixaXml);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void MensagemCaixa()
        {
            if (gdvEntrada.Rows.Count == 0)
            {
                txtProduto.Text = "*** Caixa Livre ***";
                TextBox tb = new TextBox();
                tb.TextAlign = HorizontalAlignment.Center;
            }
        }

        private void PesquisarEntrada()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabea = new DataTable();
                dadosTabea = novaVenda.ListarEntrada(numCupom, atualizado, Convert.ToInt32(numCaixaXml));

                if (dadosTabea.Rows.Count > 0)
                {
                    gdvEntrada.DataSource = dadosTabea;
                    SomaTotalGrid();
                    pbVenda.Visible = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //public void PesquisaUsuarioLogado()
        //{
        //    try
        //    {

        //        novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
        //        DataTable dadosTabela = new DataTable();
        //        dadosTabela = novoUsuario.PesquisaUsuarioLogado(numcaixa);

        //        if (dadosTabela.Rows.Count > 0)
        //        {
        //            idUsuario = Convert.ToInt32(dadosTabela.Rows[0]["ID_USUARIO"].ToString());
        //            tsNome.Text = dadosTabela.Rows[0]["NOME"].ToString();
        //            tsPeriodo.Text = dadosTabela.Rows[0]["PERIODO"].ToString();
        //            // statusVenda = Convert.ToBoolean(dadosTabela.Rows[0]["ATIVADO"].ToString());
        //            NomeUsuario = tsNome.Text;

        //            periodoAtuante = tsPeriodo.Text;

        //            if (statusVenda == true)
        //            {
        //                tsStatus.Text = "ABERTO";
        //            }
        //            else
        //            {
        //                tsStatus.Text = "FECHADO";
        //            }
        //        }
        //        else
        //        {
        //            LimpaCampos();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //}

        public void PesquisaUsuarioLogado()
        {
            try
            {
                novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                DataTable dadosTabelasuario = new DataTable();

                dadosTabelasuario = novoUsuario.PesquisaUsuarioLogado(numcaixa);

                if (dadosTabelasuario.Rows.Count > 0)
                {
                    tsNome.Text = dadosTabelasuario.Rows[0]["NOME"].ToString();
                    tsPeriodo.Text = dadosTabelasuario.Rows[0]["PERIODO"].ToString();
                    DesbloquearBotoesTelaVenda();
                }
                else
                {
                    //BloquearBotoes();
                    //frmLogin frmLogin = new frmLogin(this);
                    //frmLogin.ShowDialog();

                    MessageBox.Show("Informe Login", "Atenção");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarGranel()
        {
            try
            {
                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisaGranel(codBarra);
                granel = dadosTabela.Rows[0]["GRANEL"].ToString();

                if (dadosTabela.Rows.Count < 0)
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpaCampos();
            }
        }

        public void PesquisarUltimoVendaParametro()
        {
            try
            {
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisarUltimoVenda();

                if (dadosTabela.Rows.Count > 0)
                {
                    // numVenda = Convert.ToInt32(dadosTabela.Rows[0]["NUM_CUPOM"].ToString());
                    //lblImpreissora.Text = dadosTabela.Rows[0]["IMPRESSORA"].ToString();
                    lblNumeroVenda.Text = numVenda.ToString();
                    lblImpreissora.Text = nomeImpressora;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpaCampos();
            }
        }

        public void PesquisarAtualizado()
        {
            try
            {
                atualizado = Convert.ToBoolean(gdvEntrada.Rows[0].Cells["ATUALIZADO"].Value.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpaCampos();
            }
        }

        //public void EstiloCoresLinha()
        //{

        //    try
        //    {
        //        for (int i = 0; i < gdvEntrada.Rows.Count; i += 2)
        //        {
        //            gdvEntrada.Rows[i].DefaultCellStyle.BackColor = Color.Honeydew;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        MessageBox.Show("Error no Método EstiloCoresLinha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        public void PesquisarProduto()
        {
            try
            {
                codBarra = txtCodigo.Text.PadLeft(13, '0');
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaProduto = new DataTable();
                dadosTabelaProduto = novaVenda.PesquisarCodigoBarra(codBarra);

                if (dadosTabelaProduto.Rows.Count > 0)
                {
                    txtProduto.Text = dadosTabelaProduto.Rows[0]["DESCRICAO"].ToString();
                    txtPreco.Text = dadosTabelaProduto.Rows[0]["PRECO"].ToString();
                    codigoBarra = txtCodigo.Text = dadosTabelaProduto.Rows[0]["COD_BARRA"].ToString();
                    idCodProduto = Convert.ToInt32(dadosTabelaProduto.Rows[0]["COD_INT"].ToString());
                    aliquota = dadosTabelaProduto.Rows[0]["TRIB"].ToString();
                    granel = dadosTabelaProduto.Rows[0]["GRANEL"].ToString();
                    _valorPis = dadosTabelaProduto.Rows[0]["VALOR_PIS"].ToString();
                    cstPis = _cfop = dadosTabelaProduto.Rows[0]["CST_PIS"].ToString();
                    _valorCofins = dadosTabelaProduto.Rows[0]["VALOR_CONFINS"].ToString();
                    _cstCofins = dadosTabelaProduto.Rows[0]["CST_COFINS"].ToString();
                    _cfop = dadosTabelaProduto.Rows[0]["CFOP"].ToString();
                    _ncm = dadosTabelaProduto.Rows[0]["NCM"].ToString();
                    _icmCst = dadosTabelaProduto.Rows[0]["ICMS_CST"].ToString();
                    _origemProduto = dadosTabelaProduto.Rows[0]["ORIGEM_PRODUTO"].ToString();
                    _cest = dadosTabelaProduto.Rows[0]["CEST"].ToString();
                    unid = dadosTabelaProduto.Rows[0]["UNID"].ToString();
                    descricao = dadosTabelaProduto.Rows[0]["DESCRICAO"].ToString();
                    preco = Convert.ToDecimal(dadosTabelaProduto.Rows[0]["PRECO"].ToString());
                    _custo = Convert.ToDecimal(dadosTabelaProduto.Rows[0]["CUSTO"].ToString());
                    descAuto = Convert.ToBoolean(dadosTabelaProduto.Rows[0]["DESC_AUTO"].ToString());
                    precoDescc = Convert.ToDecimal(dadosTabelaProduto.Rows[0]["DESC"].ToString());
                    qtde_desc = Convert.ToDecimal(dadosTabelaProduto.Rows[0]["QTDE_DESC"].ToString());
                    qtde = Convert.ToDecimal(txtQuantidade.Text);

                    granel = granel.Replace(" ", "");

                    if (descAuto == true)
                    {
                        if (precoDescc > 0)
                        {
                            txtPreco.Text = precoDescc.ToString();
                            preco = precoDescc;
                        }
                    }

                    if (granel == "")
                    {
                        if (precoDescc == 0)
                        {
                            precoDescc = preco;
                        }
                        else
                        {
                            if (qtde_desc == 0)
                            {
                                precoDescc = preco;
                            }

                            if (qtde >= qtde_desc)
                            {
                                preco = precoDescc;
                            }
                            else
                            {
                                preco = Convert.ToDecimal(txtPreco.Text);
                            }
                        }
                    }

                    retPesqusiarProduto = true;
                }
                else
                {
                    //if (MessageBox.Show("Produto não Encontrado.\nDeseja Cancelar Produto?", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    if (MessageBox.Show("Produto não Encontrado.", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        LimpaCampos();
                        retPesqusiarProduto = false;
                    }
                    else
                    {
                        PesquisarProduto();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Erro na Pesquisar Código de Barra.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpaCampos();

                if (lblItens.Text == "0")
                {
                    txtProduto.Text = "*** Caixa Livre ***";
                    pbVenda.Visible = true;
                    txtValorTotal.Text = "R$ 0,00";
                }
                else
                {
                    LimpaCampos();
                    txtProduto.Text = "Novo Item";
                    pbVenda.Visible = false;
                }

                if (tsPeriodo.Text == "FECHADO")
                {
                    txtProduto.Text = "Caixa Fechado";
                }
            }
        }

        public void AtualizarPadrao()
        {
            try
            {
                //  PesquisarProduto();
                SomaValorQtde();
                ControlarTotalVenda();
                AtualizarGridVenda();
                AtualizarGridAberto();
                SomaTotalGrid();
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método AtualizarPadrao.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void DesbloquearBotoesTelaVenda()
        {
            btnCancelaItem.Enabled = true;
            btnCancelarUltimaVenda.Enabled = true;
            btnCancelaVenda.Enabled = true;
            btnSangria.Enabled = true;
        }

        public void BloquearBotoes()
        {
            btnCancelaItem.Enabled = false;
            btnCancelarUltimaVenda.Enabled = false;
            btnCancelaVenda.Enabled = false;
        }

        private void txtCodigo_KeyUp(object sender, KeyEventArgs e)
        {
            codBarra = txtCodigo.Text.PadLeft(13, '0');

            if (e.KeyCode == Keys.F5)
            {
                frmProduto frmmProduto = new frmProduto(this);
                frmmProduto.ShowDialog();

                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.Listar(_idProduto);

                if (dadosTabela.Rows.Count > 0)
                {
                    txtProduto.Text = dadosTabela.Rows[0]["Descricao"].ToString();
                    txtCodigo.Text = dadosTabela.Rows[0]["COD_BARRA"].ToString();
                    txtPreco.Text = dadosTabela.Rows[0]["Preco"].ToString();
                    aliquota = dadosTabela.Rows[0]["TRIB"].ToString();
                    unid = dadosTabela.Rows[0]["UNID"].ToString();
                    descricao = dadosTabela.Rows[0]["Descricao"].ToString();
                    descAuto = Convert.ToBoolean(dadosTabela.Rows[0]["DESC_AUTO"].ToString());
                    precoDescc = Convert.ToDecimal(dadosTabela.Rows[0]["DESC"].ToString());
                    qtde_desc = Convert.ToDecimal(dadosTabela.Rows[0]["QTDE_DESC"].ToString());
                }
                else
                {
                    //tratamento para não imprimir a mensagem caso sair do pesquisa produto em branco...............................

                    if (txtCodigo.Text != "")
                    {
                        MessageBox.Show("Produto não Encontrado!!!.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }

            if (e.KeyCode == Keys.F7)
            {
                if (gdvEntrada.Rows.Count <= 0)
                {
                    if (MessageBox.Show("Confirmar Segunda Via do Cupom Nº: " + (numCupom - 1) + ".", "Confirmação 2ª Via", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        PesquisaPlaca();
                        GerarRelatorioSegundaVia();
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possível Realizar essa Operação.\n Foi Inicializado um novo Cupom.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if ((txtCodigo.Text != ""))
            {
                if (e.KeyCode == Keys.Enter)
                {
                    PesquisarStatusCaixa();

                    if (statusVenda == true)
                    {
                        DesbloquearBotoesTelaVenda();

                        this.Refresh();

                        if (gdvEntrada.Rows.Count == 0)
                        {
                            frmCpf_Cnpj frmCpfn = new frmCpf_Cnpj(this);
                            frmCpfn.ShowDialog();
                        }

                        //metodo para verificar so o codigo eh de balança................................................................
                        verificarCodBalanca = codBarra.Substring(0, 1);

                        if (verificarCodBalanca == "2")
                        {
                            PesquisarCodigoBalanca();
                        }
                        else
                        {
                            PesquisarProduto();

                            if (retPesqusiarProduto == true)
                            {
                                if (granel == null)
                                {
                                    granel = "";
                                }

                                granel = granel.Replace(" ", "");

                                if ((granel == ""))
                                {
                                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                                    DataTable dadosTabelaProduto = new DataTable();
                                    dadosTabelaProduto = novaVenda.PesquisarCodigoBarra(codBarra);

                                    if (dadosTabelaProduto.Rows.Count > 0)
                                    {
                                        AtualizarPadrao();
                                        txtValorTotal.Focus();
                                        LimpaCampos();
                                    }
                                    else
                                    {
                                        AtualizarGridVenda();
                                        PesquisaUltimoItem();
                                        PesquisarUltimoVendaParametro();
                                        SomaTotalGrid();
                                        //EstiloCoresLinha();
                                        LimpaCampos();
                                    }

                                }
                                else
                                {
                                    if ((granel == "T") || (granel == "t"))
                                    {
                                        PesquisarProduto();
                                        //txtSoma.Text = (Convert.ToDouble(txtQuantidade.Text) * Convert.ToDouble(txtPreco.Text)).ToString();
                                        txtSomas.Enabled = true;
                                        txtSomas.Focus();
                                    }

                                    if ((granel == "P") || (granel == "p"))
                                    {
                                        txtPreco.Enabled = true;
                                        txtPreco.Focus();
                                    }

                                    //txtCodigo.Text = codBarra.Substring(1, 6);
                                    if ((granel == "C") || (granel == "c"))
                                    {
                                        txtCodigo.Text = codBarra.Substring(1, 5);
                                        txtPreco.Enabled = true;
                                        txtPreco.Focus();
                                    }

                                    if ((granel == "B") || (granel == "b"))
                                    {
                                        if (txtQuantidade.Text!="1,00")
                                        {
                                            txtPreco.Enabled = true;
                                            txtPreco.Focus();
                                        }
                                        else
                                        {
                                            PesquisarSerialPorta();
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (MessageBox.Show("Para ter Acesso a Tela Venda, é preciso fazer o login. \nDeseja Realizar o Login Agora?", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Information) == DialogResult.OK)
                        {
                            frmLogin frmLogin = new frmLogin(this);
                            frmLogin.ShowDialog();
                        }
                        else
                        {
                            LimpaCampos();
                        }
                    }
                }

                if ((e.KeyCode == Keys.Multiply) || (e.KeyCode == Keys.X))
                {
                    txtQuantidade.Enabled = true;
                    txtQuantidade.Focus();
                    txtCodigo.Text = "";
                }

                if (e.KeyCode == Keys.Space)
                {
                    if ((nomeImpressora == "BEMATECH") || (nomeImpressora == "SAT"))
                    {
                        txtCodigo.Clear();

                        if (txtValorTotal.Text != "R$ 0,00")
                        {
                            //metodo para chamar formulario de cpf se for null.................
                            string contadorCpf;
                            novoTemp = new RegraNegocio.TempRegraNegocios();
                            DataTable dadosTabela = new DataTable();
                            dadosTabela = novoTemp.PesquisarCpfTemp();

                            if (dadosTabela.Rows.Count > 0)
                            {
                                contadorCpf = dadosTabela.Rows[0]["CPF_CNPJ"].ToString();

                                contadorCpf = contadorCpf.Replace(" ", "");

                                if (contadorCpf == "")
                                {
                                    frmFechamentoVenda frmFechamentoVenda = new frmFechamentoVenda(this, frmPrincipal);
                                    frmFechamentoVenda.ShowDialog();

                                    PesquisaUltimoItem();
                                    SomaValorQtde();
                                    AtualizarGridAberto();
                                    SomaTotalGrid();
                                    //  PesquisarUltimoVendaParametro();
                                }
                                else
                                {
                                    frmFechamentoVenda frmFechamentoVenda = new frmFechamentoVenda(this, frmPrincipal);
                                    frmFechamentoVenda.ShowDialog();

                                    PesquisaUltimoItem();

                                    SomaValorQtde();
                                    AtualizarGridVenda();
                                    SomaTotalGrid();
                                    PesquisarUltimoVendaParametro();
                                }
                            }
                            else
                            {
                                contadorCpf = "0";
                            }
                        }

                        LimpaCampos();
                        PesquisaUltimoItem();
                    }
                    else if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "COMUM") || (nomeImpressora == "DARUMA") || (nomeImpressora == "ELGIN") || (nomeImpressora == "LPT") || (nomeImpressora == "MP2500") || (nomeImpressora == "MP4200"))
                    {
                        txtCodigo.Clear();

                        if (txtValorTotal.Text != "R$ 0,00")
                        {
                            //metodo para chamar formulario de cpf se for null.................
                            string contadorCpf;
                            novoTemp = new RegraNegocio.TempRegraNegocios();
                            DataTable dadosTabela = new DataTable();
                            dadosTabela = novoTemp.PesquisarCpfTemp();

                            if (dadosTabela.Rows.Count > 0)
                            {
                                contadorCpf = dadosTabela.Rows[0]["CPF_CNPJ"].ToString();

                                contadorCpf = contadorCpf.Replace(" ", "");

                                if (contadorCpf == "")
                                {
                                    frmFechamentoVenda frmFechamentoVenda = new frmFechamentoVenda(this, frmPrincipal);
                                    frmFechamentoVenda.ShowDialog();

                                    PesquisaUltimoItem();
                                    SomaValorQtde();
                                    LoadTela();
                                    AtualizarGridAberto();
                                    SomaTotalGrid();
                                    //  PesquisarUltimoVendaParametro();
                                }
                                else
                                {
                                    frmFechamentoVenda frmFechamentoVenda = new frmFechamentoVenda(this, frmPrincipal);
                                    frmFechamentoVenda.ShowDialog();
                                    PesquisaUltimoItem();

                                    SomaValorQtde();
                                    AtualizarGridVenda();
                                    SomaTotalGrid();
                                    // PesquisarUltimoVendaParametro();
                                    LoadTela();
                                }
                            }
                            else
                            {
                                contadorCpf = "0";

                                frmFechamentoVenda frmFechamentoVenda = new frmFechamentoVenda(this, frmPrincipal);
                                frmFechamentoVenda.ShowDialog();

                                PesquisaUltimoItem();
                                SomaValorQtde();
                                LoadTela();
                                AtualizarGridAberto();
                                SomaTotalGrid();
                                //  PesquisarUltimoVendaParametro();
                            }
                        }

                        LimpaCampos();
                        PesquisaUltimoItem();
                    }
                    else
                    {
                        MessageBox.Show("Impressora não Encontrada.", "Atenção");
                    }
                }

                if (e.KeyCode == Keys.Q)
                {
                    txtSomas.Focus();
                    txtCodigo.Clear();
                }
            }
            else
            {
                txtCodigo.Focus();
            }

            if (e.KeyCode == Keys.Escape)
            {
                LimpaCampos();
            }

            if (e.KeyCode == Keys.F1)
            {
                if (gdvEntrada.Rows.Count > 0)
                {
                    MessageBox.Show("Para Realizar essa Operação é Preciso Concluir ou Cancelar\n Venda Nº: " + numCupom.ToString().Trim().PadLeft(3, '0') + " em Aberto!!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtCodigo.Focus();
                }
                else
                {
                    frmPrincipal frmPrincipal = new frmPrincipal(this);
                    frmPrincipal.ShowDialog();
                }
            }

            if (e.KeyCode == Keys.F11)
            {
                fecharCaixa = false;
                LeituraX();
            }

            if (e.KeyCode == Keys.F8)
            {
                if (txtValorTotal.Text != "R$ 0,00")
                {
                    frmCpf_Cnpj cpfCnpj = new frmCpf_Cnpj(this);
                    cpfCnpj.ShowDialog();

                    PesquisaUltimoItem();
                    SomaValorQtde();
                    AtualizarGridVenda();
                    SomaTotalGrid();
                    PesquisarUltimoVendaParametro();
                }
            }

            if (e.KeyCode == Keys.Delete)
            {
                if (txtValorTotal.Text != "R$ 0,00")
                {
                    //frmCancelaItem frmCancelaItem = new frmCancelaItem(this);
                    //frmCancelaItem.ShowDialog();

                    gdvEntrada.Focus();
                }
            }

            if (e.KeyCode == Keys.F3)
            {
                frmPlacaVeiculo frmPv = new frmPlacaVeiculo(this);
                frmPv.ShowDialog();
            }

            if (e.KeyCode == Keys.End)
            {
                if (gdvEntrada.Rows.Count <= 0)
                {
                    if (MessageBox.Show("Realmente Deseja Sair do Sistema ?.", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Não é possivél Fechar a Venda, Exite(m) Venda(s) em Aberto.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpaCampos();
                }
            }

            if ((e.KeyCode == Keys.Add) || (e.KeyCode == Keys.A))
            {
                InserirUltimaVenda();
            }

            //if (e.KeyCode == Keys.I)
            //{
            //    LimpaCampos();
            //    frmTrocarLogo logo = new frmTrocarLogo(this);
            //    logo.MdiParent = this.MdiParent;
            //    logo.ShowDialog();
            //}

            if (e.KeyCode == Keys.Tab)
            {
                LimpaCampos();
            }
        }

        public void LeituraX()
        {
            try
            {
                nomeImpressora = lblImpreissora.Text;
                nomeImpressora = nomeImpressora.Replace(" ", "");

                int numC = Convert.ToInt32(numCupom);
                numC = (numC - 1);

                if (MessageBox.Show("Realmente Deseja Realizar a Leitura X", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if ((nomeImpressora == "SAT") || (nomeImpressora == "BEMATECH"))
                    {
                        idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_LeituraX();
                        RegraNegocio.CupomFiscal.Analisa_iRetorno(idRetorno);

                        MessageBox.Show("Leitura X Realizado com Sucesso.");
                    }
                    else if (nomeImpressora == "LPT")
                    {
                        MontarLeirturaX();
                        ImprimiLeituraX();

                        MessageBox.Show("Leitura X Realizado com Sucesso.");
                    }
                    else if (nomeImpressora == "COMUM")
                    {
                        MontarLeirturaX();

                        PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                        idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                        idRetorno = RegraNegocio.MP2032.IniciaPorta(numComimp); //inicia a porta com o IP digitado
                        string align = "";
                        align = "" + (char)27 + (char)97 + (char)1;
                        idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);

                        idRetorno = RegraNegocio.MP2032.FormataTX(AX + BX + CX + DX + EX + (char)10 + (char)13, 1, 1, 0, 0, 0);

                        RegraNegocio.MP2032.AcionaGuilhotina(0);
                        idRetorno = RegraNegocio.MP2032.FechaPorta();
                    }
                    else if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "MP2500"))
                    {
                        MontarLeirturaX();

                        // PesquisarPortaImpressora();
                        PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                        RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                        RegraNegocio.MP2032.IniciaPorta(numComimp);

                        LerTextoCupom();

                        RegraNegocio.MP2032.BematechTX("\n\n\n\n");
                        RegraNegocio.MP2032.AcionaGuilhotina(0);
                        RegraNegocio.MP2032.FechaPorta();

                        MessageBox.Show("Leitura X Realizado com Sucesso.", "Leitura X");
                    }
                    else if (nomeImpressora == "DARUMA")
                    {
                        MontarLeirturaX();
                        int iRetorno = RegraNegocio.DLLsDaruma.iImprimirTexto_DUAL_DarumaFramework(AX + "\n" + BX + "\n" + CX + "\n" + DX + "\n" + EX + "</c>CUPOM NAO FISCAL<sl>4</sl><gui></gui><l></l>", 0);
                    }

                    else if (nomeImpressora == "ELGIN")
                    {
                        var configImpressora = new PrinterSettings();
                        printerName = configImpressora.PrinterName;

                        MontarLeirturaX();

                        string dadosElgin = (AX + "\n" + BX + "\n" + CX + "\n" + DX + "\n" + EX);

                        escPos = new RegraNegocio.EscPos();
                        this.escPos.printText(printerName, dadosElgin);
                        //this.esc.printBarcodeB(printerName, "{A012345678901234567", 73);

                        this.escPos.lineFeed(printerName, 2);

                        feedAndCutter(printerName, 5);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //linefeed and paper cutter
        public void feedAndCutter(string printerName, int numLines)
        {
            System.Threading.Thread.Sleep(500);
            this.escPos.lineFeed(printerName, numLines);
            this.escPos.CutPaper(printerName);
        }

        public void LerTextoCupom()
        {
            string align = "" + (char)27 + (char)97 + (char)0;
            RegraNegocio.MP2032.ComandoTX(align, align.Length);
            RegraNegocio.MP2032.BematechTX(AX + "\n" + BX + "\n" + CX + "\n" + DX + "\n" + EX).ToString();
        }

        public void Espera()
        {
            long t = DateTime.Now.Millisecond;

            for (int i = 0; i < 500; i++)
            {
                Console.WriteLine(i);
                milesegundos = Convert.ToInt32(i);
            }

            tempoTotal = (DateTime.Now.Millisecond - t);
        }

        public void PesquisarSerialPorta()
        {
            try
            {
                serialPort1 = new SerialPort();

                _serialPort = new SerialPort(numComBal, Convert.ToInt32(bondRouteCom), Parity.None, 8, StopBits.One);
                _serialPort.Handshake = Handshake.None;

                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
                else
                {
                    _serialPort.Open();

                    //metodo Chr para buscar e converter na tabela Hexadecimal............................
                    _serialPort.WriteLine(Chr(5).ToString());

                    //  Espera();
                    // Aguarda dois segundos...

                    System.Threading.Thread.Sleep(TimeSpan.FromSeconds(0.4));

                    peso = _serialPort.ReadExisting().ToString();

                    //metodo ExtractNumber extrai os valores hexadecima e converte para string............
                    peso = ExtractNumber(peso);

                    if (peso != "00000")
                    {
                        if (peso != "")
                        {
                            decimal qtde = Convert.ToDecimal(peso);
                            decimal soma = 0;

                            soma = (qtde / 1000);
                            peso = soma.ToString("N3");

                            txtQuantidade.Text = peso;

                            try
                            {
                                if (tempoTotal < 2000)
                                {
                                    VendeItem();
                                }
                                else
                                {
                                    txtQuantidade.Enabled = true;
                                    txtQuantidade.Focus();
                                }
                            }
                            catch
                            {
                                MessageBox.Show("Erro.");

                                if (_serialPort.IsOpen)
                                {
                                    _serialPort.Close();
                                }
                            }
                        }
                        else
                        {
                            txtQuantidade.Enabled = true;
                            txtQuantidade.Text = "0,00";
                            txtQuantidade.Focus();
                        }

                        _serialPort.Close();
                    }
                    else
                    {
                        _serialPort.Close();
                        txtCodigo.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("PESAR NOVAMENTE.\n" + ex.Message, "Atenção");

                if (_serialPort.IsOpen)
                {
                    _serialPort.Close();
                }
            }
        }

        public char Chr(int codigo)
        {
            return (char)codigo;
        }

        public string ExtractNumber(string original)
        {
            return new string(original.Where(c => Char.IsDigit(c)).ToArray());
        }

        public void ProgressBarEtart()
        {
            this.timer2.Start();
        }

        public void ProgressBarClose()
        {
            //this.timer2.Stop();
            pb.Value = 0;
        }

        public void TrocarLogo()
        {
            try
            {
                novaImagem = new RegraNegocio.ImagemRegraNegocio();
                novaImagem.LimparImagem();

                //se usuario digitar ok pq ele selecionou arquivo...........
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //nome que usuario retornou..............................
                    string nomeArquivo = openFileDialog1.FileName;

                    ///exibir imgem dentro do picture box..................

                    Bitmap btm = new Bitmap(nomeArquivo);
                    pbVenda.Image = btm;

                    //metodo para salvar no banco.........................

                    //MemoryStream herda da classe Stream.................
                    MemoryStream ms = new MemoryStream();
                    btm.Save(ms, ImageFormat.Bmp);

                    byte[] foto = ms.ToArray();

                    novaImagem = new RegraNegocio.ImagemRegraNegocio();
                    novaImagem.SalvarImagem(foto);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void IlustraLogo()
        {
            try
            {
                String strConn = ConfigurationManager.ConnectionStrings["PVe.Properties.Settings.CFeConnectionString"].ToString();
                SqlConnection connection = new SqlConnection(strConn);

                SqlCommand comand = new SqlCommand("SELECT LOGO FROM LOGO", connection);

                connection.Open();

                SqlDataReader reader = comand.ExecuteReader();
                Image imagem = null;

                if (reader.Read())
                {
                    byte[] foto = (byte[])reader["LOGO"];

                    MemoryStream ms = new MemoryStream(foto);
                    imagem = Image.FromStream(ms);
                }

                pbVenda.Image = imagem;
                LimpaCampos();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void InserirUltimaVenda()
        {
            try
            {
                if (gdvEntrada.Rows.Count > 0)
                {
                    int idVendas, idProds, idUsuarios, itens, numVendas, idParams = 0;
                    string codBars, descProds, aliquotas, valorPiss, cstPiss, valorCofinss, cstCofinss, cfops, ncms, origemProds, icmCsts, cest = "";
                    decimal qtdes, totals, custos, precos = 0;
                    DateTime datas;
                    Boolean baixados, fecs = true;

                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabela = new DataTable();
                    dadosTabela = novaVenda.PesquisarUltimaVenda();

                    idVendas = Convert.ToInt32(dadosTabela.Rows[0]["ID"].ToString());
                    idProds = Convert.ToInt32(dadosTabela.Rows[0]["ID_PROD"].ToString());
                    qtdes = Convert.ToDecimal(dadosTabela.Rows[0]["QUANT"].ToString());
                    totals = Convert.ToDecimal(dadosTabela.Rows[0]["TOTAL"].ToString());
                    idUsuarios = Convert.ToInt32(dadosTabela.Rows[0]["ID_USUARIO"].ToString());
                    itens = Convert.ToInt32(dadosTabela.Rows[0]["ITEM"].ToString());
                    itens = itens + 1;
                    datas = Convert.ToDateTime(dadosTabela.Rows[0]["DATA"].ToString());
                    custos = Convert.ToDecimal(dadosTabela.Rows[0]["CUSTO"].ToString());
                    baixados = false;
                    numVendas = Convert.ToInt32(dadosTabela.Rows[0]["NUM_VENDA"].ToString());
                    codBars = dadosTabela.Rows[0]["COD_BARRA"].ToString();
                    descProds = dadosTabela.Rows[0]["DESCRICAO_PRODUTO"].ToString();
                    precos = Convert.ToDecimal(dadosTabela.Rows[0]["PRECO"].ToString());
                    aliquotas = dadosTabela.Rows[0]["ALIQUOTA"].ToString();
                    aliquotas = aliquotas.Replace(" ", "");
                    idParams = Convert.ToInt32(dadosTabela.Rows[0]["ID_PARAMETRO"].ToString());
                    valorPiss = dadosTabela.Rows[0]["VALOR_PIS"].ToString();
                    valorPiss = valorPiss.Replace(" ", "");
                    cstPiss = dadosTabela.Rows[0]["CST_PIS"].ToString();
                    valorCofinss = dadosTabela.Rows[0]["VALOR_COFINS"].ToString();
                    valorCofinss = valorCofinss.Replace(" ", "");
                    cstCofinss = dadosTabela.Rows[0]["CST_COFINS"].ToString();
                    cstCofinss = cstCofinss.Replace(" ", "");
                    cfops = dadosTabela.Rows[0]["CFOP"].ToString();
                    cfops = cfops.Replace(" ", "");
                    ncms = dadosTabela.Rows[0]["NCM"].ToString();
                    ncms = ncms.Replace(" ", "");
                    origemProds = dadosTabela.Rows[0]["ORIGEM_PRODUTO"].ToString();
                    origemProds = origemProds.Replace(" ", "");
                    icmCsts = dadosTabela.Rows[0]["ICM_CST"].ToString();
                    icmCsts = icmCsts.Replace(" ", "");
                    cest = dadosTabela.Rows[0]["CEST"].ToString();
                    fecs = false;
                    numcaixa = Convert.ToInt32(lblNumeroCaixa.Text);

                    if (numVendas.ToString() == lblNumeroVenda.Text)
                    {
                        novaVenda.VendeItem(idProds, qtdes, precos, totals, idUsuarios, itens, datas, custos, baixados, numVendas, codBars, descProds, aliquotas, idParams, valorPiss, cstPiss, valorCofinss, cstCofinss, cfops, ncms, icmCsts, origemProds, fecs, cest, numcaixa, _unid);
                        AtualizarGridAberto();
                        ApontarUltimaLinhaGrid();
                        LimpaCampos();
                    }
                    else
                    {
                        numVendas = (numVendas - 1);
                        MessageBox.Show("Venda Referente ao Nº " + numVendas + ", não pode ser Registrado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    LimpaCampos();
                    //  MessageBox.Show("Não Consta no Sistema a Última Venda para Efetuar o(s) Registro(s).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Registro não Encontrado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpaCampos();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarCodEtiquetaB()
        {
            try
            {
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisarCodEtiqueta();
                codEtiquetaB = dadosTabela.Rows[0]["ETIQUETA_BALANCA"].ToString();
                codEtiquetaB = codEtiquetaB.Replace(" ", "");
                parametroEtiqueta = Convert.ToInt32(dadosTabela.Rows[0]["COD_ETIQUETA"]);
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PesquisarCodEtiquetaB.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void PesquisarCodigoBalanca()
        {
            try
            {
                decimal qtde = 0;
                decimal soma = 0;
                decimal peso = 0;

                //pesquisa na tabela parametro etiqueta da balanca e parametro da etiqueta...
                PesquisarCodEtiquetaB();

                if ((codEtiquetaB == "P"))
                {
                    //extrai codigo do produto do codigo de barra da balança............................
                    txtCodigo.Text = codBarra.Substring(1, parametroEtiqueta);

                    //adiciona seguintes 0 concatenando os mesmos.......................................
                    codProd = txtCodigo.Text.PadLeft(13, '0');

                    //pesquisar peso do produto.........................................................
                    codQtde = codBarra.Substring(7, 5);

                    //metodo para pesquisar produto com codigoConcatenado...........
                    novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                    dadosTabelaProduto = new DataTable();
                    dadosTabelaProduto = novoProduto.PesquisarProdutoBalanca(codProd);

                    if (dadosTabelaProduto.Rows.Count > 0)
                    {
                        qtde = (Convert.ToDecimal(codQtde) / 1000);
                        txtCodigo.Text = dadosTabelaProduto.Rows[0]["COD_BARRA"].ToString();
                        txtPreco.Text = dadosTabelaProduto.Rows[0]["PRECO"].ToString();
                        txtProduto.Text = dadosTabelaProduto.Rows[0]["DESCRICAO"].ToString();
                        txtQuantidade.Text = qtde.ToString();

                        soma = (qtde * Convert.ToDecimal(txtPreco.Text));
                        txtSomas.Text = soma.ToString();

                        AtualizarPadrao();
                    }
                    else
                    {
                        MessageBox.Show("Produto não Encontrado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpaCampos();
                    }
                }
                else
                {
                    //extrai codigo do produto do codigo de barra da balança............................
                    txtCodigo.Text = codBarra.Substring(1, parametroEtiqueta);

                    //adiciona seguintes 0 concatenando os mesmos.......................................
                    codProd = txtCodigo.Text.PadLeft(13, '0');

                    //pesquisar peso do produto.........................................................
                    codPeso = codBarra.Substring(7, 5);

                    //metodo para pesquisar produto com codigoConcatenado...........
                    novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                    dadosTabelaProduto = new DataTable();
                    dadosTabelaProduto = novoProduto.PesquisarProdutoBalanca(codProd);

                    if (dadosTabelaProduto.Rows.Count > 0)
                    {
                        txtCodigo.Text = dadosTabelaProduto.Rows[0]["COD_BARRA"].ToString();
                        txtPreco.Text = dadosTabelaProduto.Rows[0]["PRECO"].ToString();
                        txtProduto.Text = dadosTabelaProduto.Rows[0]["DESCRICAO"].ToString();
                        idCodProduto = Convert.ToInt32(dadosTabelaProduto.Rows[0]["COD_INT"].ToString());
                        aliquota = dadosTabelaProduto.Rows[0]["TRIB"].ToString();
                        peso = (Convert.ToDecimal(codPeso));
                        preco = Convert.ToDecimal(txtPreco.Text);
                        item = Convert.ToInt32(gdvEntrada.Rows.Count.ToString());

                        qtde = ((peso / preco) / 100);
                        soma = (qtde * preco);
                        txtSomas.Text = soma.ToString();
                        txtQuantidade.Text = qtde.ToString();

                        AtualizarPadrao();
                    }
                    else
                    {
                        MessageBox.Show("Produto não Encontrado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpaCampos();
                    }
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PesquisarCodigoBalanca.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void SomaValorQtde()
        {
            try
            {
                decimal somas = 0;
                qtde = Convert.ToDecimal(txtQuantidade.Text);
                preco = Convert.ToDecimal(txtPreco.Text);

                somas = (qtde * preco);
                txtSomas.Text = "";
                txtSomas.Text = string.Format("{0:0.00}", somas);

            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método SomaValorQtde.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtQuantidade_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                txtCodigo.Focus();
            }

            if (e.KeyCode == Keys.Escape)
            {
                LimpaCampos();
                txtQuantidade.Enabled = false;
            }
        }

        //if (e.KeyCode == Keys.F5)
        //{
        //    frmProduto frmmProduto = new frmProduto(this);
        //    frmmProduto.ShowDialog();

        //    novoProduto = new RegraNegocio.ProdutoRegraNegocio();
        //    DataTable dadosTabela = new DataTable();
        //    dadosTabela = novoProduto.Listar(_idProduto);

        //    if (dadosTabela.Rows.Count > 0)
        //    {
        //        txtProduto.Text = dadosTabela.Rows[0]["Descricao"].ToString();
        //        txtCodigo.Text = dadosTabela.Rows[0]["COD_BARRA"].ToString();
        //        txtPreco.Text = dadosTabela.Rows[0]["Preco"].ToString();
        //        aliquota = dadosTabela.Rows[0]["TRIB"].ToString();

        //        //qtdeAtual = Convert.ToInt32(dadosTabela.Rows[0]["ESTOQUE"].ToString());
        //    }
        //    else
        //    {
        //        //tratamento para não imprimir a mensagem caso sair do pesquisa produto em branco...............................

        //        if (txtCodigo.Text != "")
        //        {
        //            MessageBox.Show("Produto não Encontrado!!!.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //        }
        //    }
        //    }
        //}

        private void VendeItemQtde()
        {
            qtde = Convert.ToDecimal(txtQuantidade.Text);

            if (qtde > 0)
            {
                qtde = Math.Round(qtde, 3);
                preco = Math.Round((preco), 3);
                soma = (qtde * preco);
                soma = Math.Round((soma), 3);

                item = Convert.ToInt32(lblItens.Text);
                item = (item + 1);

                novaVenda.VendeItem(idCodProduto, qtde, preco, soma, idUsuarioLogado, item, DateTime.Now, Convert.ToDecimal(_custo), false, Convert.ToInt32(lblNumeroVenda.Text), codigoBarra, txtProduto.Text, aliquota, idParametro, _valorPis, cstPis, _valorCofins, _cstCofins, _cfop, _ncm, _icmCst, _origemProduto, fechado, _cest, numcaixa, unid);
                AtualizarGridVenda();
                AlterarStatusAbertura();
                PesquisaUltimoItem();
                ApontarUltimaLinhaGrid();
                PesquisarEntrada();
                txtQuantidade.Enabled = false;
            }
        }

        public void PesquisarIdParametro()
        {
            try
            {
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisaIdParametro(numCupom);

                idParametro = Convert.ToInt32(dadosTabela.Rows[0]["ID_PARAMETRO"]);
                numcaixa = Convert.ToInt32(lblNumeroCaixa.Text);
                qtdeCupom = Convert.ToInt32(dadosTabela.Rows[0]["QTDE_CUPOM"].ToString());
                PesquisarNumCaixa_NumVenda();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PesquisarImpressoa()
        {
            try
            {
                if (nomeImpressora == "DARUMA")
                {
                    PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                    PesquisarNumCaixa_NumVenda();
                    int iRetorno;

                    iRetorno = RegraNegocio.DLLsDaruma.regAlterarValor_Daruma("DUAL\\PortaComunicacao", numComimp);
                    iRetorno = RegraNegocio.DLLsDaruma.regAlterarValor_Daruma("DUAL\\Velocidade", "9600");

                    if (iRetorno > 0)
                    {
                        MessageBox.Show("Impressora Conectada com Sucesso !!!");
                        lblImpreissora.Text = "DARUMA";
                    }

                    if (iRetorno <= 0)
                    {
                        string strMsgRetorno = RegraNegocio.DLLsDaruma.TrataRetorno(iRetorno);
                        MessageBox.Show("Retorno do método: " + strMsgRetorno, "DarumaFramework - NFCe", MessageBoxButtons.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ControlarTotalVenda()
        {
            try
            {
                decimal valorPermitido = 0;
                decimal limiteSat = 0;
                decimal proxPreco = 0;

                //busca o tipo de granel do produto para......................................

                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabelaProduto = new DataTable();
                dadosTabelaProduto = novoProduto.PesquisaCodigoBarra(txtCodigo.Text);
                granel = dadosTabelaProduto.Rows[0]["GRANEL"].ToString();

                //----------------------------------------------------------------------------

                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabelaParametro = new DataTable();
                dadosTabelaParametro = novoParametro.PesquisaParametroE();

                limiteSat = Convert.ToDecimal(dadosTabelaParametro.Rows[0]["LIMITE_VENDA"].ToString());
                //----------------------------------------------------------------------------

                // concatena o valor totalmais o proximo produto para autorizar ou nao a venda do produto.........................
                if (granel == "P" || granel == "p")
                {
                    for (int i = 0; i < gdvEntrada.Rows.Count; i++)
                    {
                        valorPermitido += Convert.ToDecimal(gdvEntrada.Rows[i].Cells["TOTAL"].Value);
                    }

                    proxPreco = Convert.ToDecimal(txtPreco.Text);
                    valorPermitido = (valorPermitido + proxPreco);
                }

                if (granel == "T" || granel == "t")
                {
                    for (int i = 0; i < gdvEntrada.Rows.Count; i++)
                    {
                        valorPermitido += Convert.ToDecimal(gdvEntrada.Rows[i].Cells["TOTAL"].Value);
                    }

                    proxPreco = Convert.ToDecimal(txtSomas.Text);
                    valorPermitido = (valorPermitido + proxPreco);
                }

                if (granel == "" || granel == null)
                {
                    for (int i = 0; i < gdvEntrada.Rows.Count; i++)
                    {
                        valorPermitido += Convert.ToDecimal(gdvEntrada.Rows[i].Cells["TOTAL"].Value);
                    }

                    proxPreco = Convert.ToDecimal(txtPreco.Text);
                    valorPermitido = (valorPermitido + proxPreco);
                }

                if (valorPermitido <= limiteSat)
                {
                    VendeItem();
                }
                else
                {
                    MessageBox.Show("O Valor Total de Venda é Superior ao Limite Permitido pelo S@T no Valor de ." + limiteSat.ToString("C2") + "", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpaCampos();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void VendeItem()
        {
            try
            {
                if (txtCodigo.Text != "")
                {
                    item = Convert.ToInt32(lblItens.Text);
                    int nVenda = Convert.ToInt32(lblNumeroVenda.Text);
                    numcaixa = Convert.ToInt32(lblNumeroCaixa.Text);

                    decimal custo = 0;
                    item = gdvEntrada.Rows.Count;
                    item = item + 1;
                    lblItens.Text = item.ToString();

                    codigoBarra = txtCodigo.Text;

                    bool atualizado = true;

                    string pesquisarTotal = txtSomas.Text.ToString();

                    novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                    DataTable dadosTabelaUsuario = new DataTable();
                    dadosTabelaUsuario = novoUsuario.PesquisaUsuarioLogado(numcaixa);

                    int idUsuarioVenda = idUsuarioLogado;

                    novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                    DataTable dadosTabelaProduto = new DataTable();
                    dadosTabelaProduto = novoProduto.PesquisaCodigoBarra(codigoBarra);

                    if (dadosTabelaProduto.Rows.Count > 0)
                    {
                        granel = dadosTabelaProduto.Rows[0]["GRANEL"].ToString();
                        descricao = dadosTabelaProduto.Rows[0]["DESCRICAO"].ToString();
                        _unid = dadosTabelaProduto.Rows[0]["UNID"].ToString();
                        aliquota = dadosTabelaProduto.Rows[0]["TRIB"].ToString();
                        _cstPis = dadosTabelaProduto.Rows[0]["CST_PIS"].ToString();
                        _cstCofins = dadosTabelaProduto.Rows[0]["CST_COFINS"].ToString();
                        idCodProduto = Convert.ToInt32(dadosTabelaProduto.Rows[0]["COD_INT"].ToString());
                        _icmCst = dadosTabelaProduto.Rows[0]["ICMS_CST"].ToString();
                        _ncm = dadosTabelaProduto.Rows[0]["NCM"].ToString();
                        _valorPis = dadosTabelaProduto.Rows[0]["VALOR_PIS"].ToString();
                        _valorCofins = dadosTabelaProduto.Rows[0]["VALOR_CONFINS"].ToString();
                        _origemProduto = dadosTabelaProduto.Rows[0]["ORIGEM_PRODUTO"].ToString();
                        _cfop = dadosTabelaProduto.Rows[0]["CFOP"].ToString();
                        _cest = dadosTabelaProduto.Rows[0]["CEST"].ToString();

                        //limpar variveis.................................................................
                        granel = granel.Replace(" ", "");
                        aliquota = aliquota.Replace(" ", "");
                        _cstPis = _cstPis.Replace(" ", "");
                        _cstCofins = _cstCofins.Replace(" ", "");


                        if (granel == "T")
                        {
                            if (descAuto == true)
                            {
                                if (precoDescc > 0)
                                {
                                    preco = precoDescc;
                                }
                            }

                            if (precoDescc == 0)
                            {
                                precoDescc = preco;
                            }

                            else if (qtde <= qtde_desc)
                            {
                                if (precoDescc == 0)
                                {
                                    precoDescc = preco;
                                }
                            }

                        }
                        if (aliquota.Trim().Equals(""))
                        {
                            MessageBox.Show("Não foi Possivel Vender Produto: " + descricao + ". Campo Aliquota do Produto está Incorreto.", "Atenção");
                        }

                        if (_icmCst.Trim().Equals(""))
                        {
                            MessageBox.Show("Não foi Possivel Vender Produto: " + descricao + ". Campo CST do Produto está Incorreto.", "Atenção");
                        }

                        else if (_ncm.Trim().Equals(""))
                        {
                            MessageBox.Show("Não foi Possivel Vender Produto: " + descricao + ". Campo NCM do Produto está Incorreto.", "Atenção");
                        }

                        else if (_cstPis.Trim().Equals(""))
                        {
                            MessageBox.Show("Não foi Possivel Vender Produto: " + descricao + ". Campo PIS do Produto está Incorreto.", "Atenção");
                        }

                        else if (_cstCofins.Trim().Equals(""))
                        {
                            MessageBox.Show("Não foi Possivel Vender Produto: " + descricao + ". Campo COFINS do Produto está Incorreto.", "Atenção");
                        }

                        else
                        {
                            if (granel == "T" || granel == "t")
                            {
                                qtde = Convert.ToDecimal(txtQuantidade.Text);
                                qtde = Math.Round(qtde, 5);
                                preco = Math.Round(preco, 3);
                                decimal somas = (qtde * preco);
                                somas = Math.Round(soma, 2);

                                novaVenda.VendeItem(idCodProduto, Convert.ToDecimal(qtde), Convert.ToDecimal(preco), soma, idUsuarioVenda, item, DateTime.Now, custo, false, nVenda, codigoBarra, txtProduto.Text, aliquota, idParametro, _valorPis, cstPis, _valorCofins, _cstCofins, _cfop, _ncm, _icmCst, _origemProduto, fechado, _cest, numcaixa, _unid);
                                AtualizarGridVenda();

                                AlterarStatusAbertura();
                                PesquisaUltimoItem();
                                ApontarUltimaLinhaGrid();
                                ComparaDataAberturaCaixa();
                                ApontarUltimaLinhaGrid();
                            }

                            else if (granel == "P" || granel == "p")
                            {
                                qtde = Convert.ToDecimal(txtQuantidade.Text);
                                qtde = Math.Round(qtde, 3);
                                preco = Math.Round((preco), 3);

                                novaVenda = new RegraNegocio.VendaRegraNegocios();
                                novaVenda.VendeItem(idCodProduto, Convert.ToDecimal(txtQuantidade.Text), Convert.ToDecimal(txtPreco.Text), Convert.ToDecimal(txtSomas.Text), idUsuarioVenda, item, DateTime.Now, custo, false, nVenda, codigoBarra, txtProduto.Text, aliquota, idParametro, _valorPis, cstPis, _valorCofins, _cstCofins, _cfop, _ncm, _icmCst, _origemProduto, fechado, _cest, numcaixa, _unid);
                                AtualizarGridVenda();
                                AlterarStatusAbertura();
                                PesquisaUltimoItem();
                                ApontarUltimaLinhaGrid();
                            }
                            else if ((granel == "B") || (granel == "b"))
                            {
                                preco = Math.Round((preco), 3);
                                soma = (Convert.ToDecimal(peso) * preco);
                                soma = Math.Round((soma), 3);

                                novaVenda = new RegraNegocio.VendaRegraNegocios();
                                novaVenda.VendeItem(idCodProduto, Convert.ToDecimal(txtQuantidade.Text), Convert.ToDecimal(txtPreco.Text), soma, idUsuarioVenda, item, DateTime.Now, custo, false, nVenda, codigoBarra, txtProduto.Text, aliquota, idParametro, _valorPis, cstPis, _valorCofins, _cstCofins, _cfop, _ncm, _icmCst, _origemProduto, fechado, _cest, numcaixa, _unid);
                                AtualizarGridVenda();
                                AlterarStatusAbertura();
                                PesquisaUltimoItem();
                                ApontarUltimaLinhaGrid();
                                PesquisarEntrada();
                                ApontarUltimaLinhaGrid();
                            }
                            else if (granel == "")
                            {
                                soma = (Convert.ToDecimal(txtQuantidade.Text) * preco);
                                soma = Math.Round((soma), 3);
                                decimal somas = (qtde * preco);
                                somas = Math.Round(soma, 2);

                                novaVenda = new RegraNegocio.VendaRegraNegocios();
                                novaVenda.VendeItem(idCodProduto, Convert.ToDecimal(txtQuantidade.Text), preco, soma, idUsuarioVenda, item, DateTime.Now, custo, false, nVenda, codigoBarra, txtProduto.Text, aliquota, idParametro, _valorPis, _cstPis, _valorCofins, _cstCofins, _cfop, _ncm, _icmCst, _origemProduto, fechado, _cest, numcaixa, _unid);
                                LoadTela();
                                ApontarUltimaLinhaGrid();
                                LimpaCampos();
                                preco = 0;
                            }
                        }
                    }
                }
                else
                {
                    LimpaCampos();
                }
            }
            catch (Exception ex)
            {
                //   MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpaCampos();
            }
        }


        public void PesquisaPrecoDesconto()
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void VefrificarvendaAberto()
        {
            qtdevenda = gdvEntrada.Rows.Count;
        }

        public void LimpaCampos()
        {
            txtCodigo.Text = "";
            txtProduto.Text = "Novo Item";
            txtQuantidade.Text = "1,00";
            txtPreco.Text = "0,00";
            txtSomas.Text = "0,00";
            txtCodigo.Focus();
            txtCodigo.Focus();
        }

        public void AtualizarGridVenda()
        {
            try
            {
                int numero = Convert.ToInt32(lblNumeroVenda.Text);
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.AtualizarGriVenda(numero, atualizado, numcaixa);
                gdvEntrada.DataSource = dadosTabela;
                LimpaCampos();
                //EstiloCoresLinha();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSoma_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    soma = Convert.ToDecimal(txtSomas.Text);
                    preco = Convert.ToDecimal(txtPreco.Text);

                    qtde = ((soma / preco));
                    qtde = Convert.ToDecimal(qtde) + (Convert.ToDecimal(0.0001));

                    txtQuantidade.Text = qtde.ToString();
                    VendeItem();
                    AtualizarGridVenda();
                    SomaTotalGrid();
                    SomaValorQtde();

                    if (e.KeyCode == Keys.Escape)
                    {
                        LimpaCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetFormatting()
        {
            this.gdvEntrada.Columns["TOTAL"].DefaultCellStyle.Format = "C";
            this.gdvEntrada.Columns["PRECO_"].DefaultCellStyle.Format = "C";
        }

        public void SomaTotalGrid()
        {
            try
            {
                valortotal = 0;

                if (gdvEntrada.Rows.Count > 0)
                {
                    try
                    {
                        novaVenda = new RegraNegocio.VendaRegraNegocios();
                        DataTable dadosTabelaVendas = new DataTable();
                        dadosTabelaVendas = novaVenda.SomaTotalGrid(numCupom, numcaixa);
                        valortotal = Convert.ToDecimal(dadosTabelaVendas.Rows[0]["TOTAL"].ToString());

                        if (dadosTabelaVendas.Rows.Count > 0)
                        {
                            txtValorTotal.Text = valortotal.ToString("C");
                            SetFormatting();
                            ApontarUltimaLinhaGrid();
                        }
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void PesquisaUltimoItem()
        {
            try
            {
                int numeroVenda = Convert.ToInt32(numCaixaXml);
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.AtualizarGriVenda(numCupom, atualizado, numcaixa);
                gdvEntrada.DataSource = dadosTabela;

                //metodo para encontra numero de registros encontrado na grid.................
                lblItens.Text = gdvEntrada.Rows.Count.ToString();

                if (lblItens.Text == "0")
                {
                    txtProduto.Text = "*** Caixa Livre ***";
                    pbVenda.Visible = true;
                    txtValorTotal.Text = "R$ 0,00";
                }
                else
                {
                    txtProduto.Text = "Novo Item";
                    pbVenda.Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ApontarUltimaLinhaGrid()
        {
            if (gdvEntrada.Rows.Count > 0)
            {
                gdvEntrada.FirstDisplayedScrollingRowIndex = gdvEntrada.Rows.Count - 1;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            dataServidor = DateTime.Now;
            lblData.Text = dataServidor.ToLongDateString() + " - Hora: " + dataServidor.ToLongTimeString();
        }

        public void StatusVenda()
        {
            try
            {
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisaParametroE();

                statusVenda = Convert.ToBoolean(dadosTabela.Rows[0]["STATUS"].ToString());

                if (statusVenda == false)
                {
                    tsStatus.Text = "FECHADO";
                }
                else
                {
                    tsStatus.Text = "ABERTO";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void AlterarPrecoZero()
        {
            try
            {
                string codBarra = txtCodigo.Text;
                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                novoProduto.AlterarPrecoZero(codBarra, Convert.ToInt32(lblNumeroVenda.Text), Convert.ToDecimal(txtPreco.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtPreco_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LimpaCampos();
                txtPreco.Enabled = false;
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
            catch (Exception)
            {

                throw;
            }
        }

        private void txtSomas_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtSomas.Text == "0,00" || txtSomas.Text == "0" || txtSomas.Text == "")
                    {
                        txtSomas.Focus();
                        txtSomas.Enabled = true;
                    }
                    else
                    {
                        soma = Convert.ToDecimal(txtSomas.Text);
                        preco = Convert.ToDecimal(txtPreco.Text);

                        if (preco != 0)
                        {
                            nomeImpressora = lblImpreissora.Text;
                            nomeImpressora = nomeImpressora.Replace(" ", "");

                            if (nomeImpressora == "BEMATECH")
                            {
                                qtde = ((soma / preco));

                                if (qtde >= qtde_desc)
                                {
                                    if (precoDescc == 0)
                                    {
                                        precoDescc = preco;
                                    }
                                    else
                                    {
                                        preco = precoDescc;
                                        qtde = ((soma / preco));
                                        qtde = Convert.ToDecimal(qtde) + (Convert.ToDecimal(0.0011));
                                    }
                                }
                            }
                            else
                            {
                                if (qtde_desc == 0)
                                {
                                    qtde = ((soma / preco));
                                    //qtde = Convert.ToDecimal(qtde) + (Convert.ToDecimal(0.0001));DANTAS
                                    qtde = Convert.ToDecimal(qtde) + (Convert.ToDecimal(0.00001));
                                }
                                else
                                {
                                    qtde = ((soma / preco));

                                    if (qtde >= qtde_desc)
                                    {
                                        if (precoDescc == 0)
                                        {
                                            precoDescc = preco;
                                        }
                                        else
                                        {
                                            preco = precoDescc;
                                            qtde = ((soma / preco));
                                            qtde = Convert.ToDecimal(qtde) + (Convert.ToDecimal(0.0011));
                                        }
                                    }
                                }
                            }

                            txtQuantidade.Text = qtde.ToString();

                            SomaValorQtde();
                            ControlarTotalVenda();
                            SomaTotalGrid();
                            AtualizarGridAberto();
                            txtSomas.Enabled = false;
                            LimpaCampos();
                        }
                        else
                        {
                            MessageBox.Show("Código de Barra Inválido!!!.\nInsira outro Código de Barra ou entre em contato com Administrador.", "Erro na Operação");
                            LimpaCampos();
                            txtSomas.Enabled = false;
                        }
                    }
                }

                if (e.KeyCode == Keys.Escape)
                {
                    LimpaCampos();
                    PesquisaUltimoItem();
                    txtSomas.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        public void AlterarStatusAbertura()
        {
            try
            {
                novoCaixa = new RegraNegocio.NumCaixaRegraNegocios();
                DataTable dadosTabelaCaixa = new DataTable();

                dadosTabelaCaixa = novoCaixa.PesquisarEstatusCaixa(numcaixa);

                if (dadosTabelaCaixa.Rows.Count > 0)
                {
                    statusVenda = Convert.ToBoolean(dadosTabelaCaixa.Rows[0]["STATUS_CAIXA"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DevolucaoTipoPaamento()
        {
            try
            {
                int tipoP, idPagtoVenda = 0;
                decimal valorVendaDevolvido, troco = 0;
                DateTime data = DateTime.Now.Date;
                string cnpj;

                
                novaFormaPgto = new RegraNegocio.FormaPagamentoRegraNegocio();
                DataTable dadosTabelaFp = new DataTable();
                //pesquisar valores da Ultima venda...............................................
                dadosTabelaFp = novaFormaPgto.PesquisarUltimaVenda((numCupom - 1));

                if (dadosTabelaFp.Rows.Count > 0)
                {
                    for (int x = 0; x < dadosTabelaFp.Rows.Count; x++)
                    {
                        idPagtoVenda=Convert.ToInt32(dadosTabelaFp.Rows[x]["ID"].ToString());
                        tipoP = Convert.ToInt32(dadosTabelaFp.Rows[x]["TIPO_PAGAMENTO"].ToString());
                        valorVendaDevolvido = Convert.ToDecimal(dadosTabelaFp.Rows[x]["VALOR"].ToString());
                        troco = Convert.ToDecimal(dadosTabelaFp.Rows[x]["TROCO"].ToString());
                        idClienteVendaAberto = Convert.ToInt32(dadosTabelaFp.Rows[x]["ID_CLIENTE"].ToString());
                        cnpj = dadosTabelaFp.Rows[x]["ID_CLIENTE"].ToString();

                        valorVendaDevolvido = (valorVendaDevolvido * -1);
                        troco = (troco * -1);

                        novaFormaPgto.DevolucaoPagamentosVenda(idUsuarioLogado, idClienteVendaAberto, tipoP, valorVendaDevolvido, cnpj, (numCupom - 1), false, data, troco, false, numcaixa);
                        if(tipoP==4)
                        {
                            novaContaReceber = new RegraNegocio.ContaReceberRegraNegocios();
                            novaContaReceber.CadastrarContaReceber(idPagtoVenda, (valorVendaDevolvido), 0, data, idUsuario, 0, 0, false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void DevolucaoEstoqueProduto()
        {
            try
            {
                int ultimoVenda = Convert.ToInt32(numCupom);
                ultimoVenda = (ultimoVenda - 1);

                //pesquisar numero de venda banco, compara negativo ou nao...........................................
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVendaNegativo(ultimoVenda);

                if (dadosTabela.Rows.Count > 0)
                {
                    //atributos do produto devolvido................................................................
                    int nuCupomDev, idProdutoDev, itemDev;
                    decimal qtdeDev, precoDev, somaDev, custoDev;
                    string codBarraDev, produDev;

                    for (int i = 0; i < dadosTabela.Rows.Count; i++)
                    {
                        nuCupomDev = Convert.ToInt32(ultimoVenda);
                        idProdutoDev = Convert.ToInt32(dadosTabela.Rows[i]["ID_PROD"].ToString());
                        qtdeDev = Convert.ToDecimal(dadosTabela.Rows[i]["QUANT"].ToString());
                        precoDev = Convert.ToDecimal(dadosTabela.Rows[i]["PRECO"].ToString());
                        somaDev = Convert.ToDecimal(dadosTabela.Rows[i]["TOTAL"].ToString());
                        itemDev = Convert.ToInt32(dadosTabela.Rows[i]["ITEM"].ToString());
                        custoDev = Convert.ToDecimal(dadosTabela.Rows[i]["CUSTO"].ToString());
                        codBarraDev = dadosTabela.Rows[i]["COD_BARRA"].ToString();
                        produDev = dadosTabela.Rows[i]["DESCRICAO_PRODUTO"].ToString();

                        qtdeDev = (qtdeDev * -1);
                        somaDev = (somaDev * -1);
                        //converte em valores negativo para inserir no banco..............................................
                        novaVenda = new RegraNegocio.VendaRegraNegocios();
                        novaVenda.DevolucaoVendaBanco(idProdutoDev, Convert.ToDecimal(qtdeDev), Convert.ToDecimal(precoDev), somaDev, idUsuarioLogado, itemDev, DateTime.Now, custoDev, false, nuCupomDev, codBarraDev, produDev, aliquota, idParametro, _valorPis, cstPis, _valorCofins, _cstCofins, _cfop, _ncm, _icmCst, _origemProduto, fechado, numcaixa);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelarVenda_Click(object sender, EventArgs e)
        {
            try
            {
                nomeImpressora = lblImpreissora.Text;
                nomeImpressora = nomeImpressora.Replace(" ", "");

                if ((nomeImpressora == "BEMATECH") || (nomeImpressora == "SAT"))
                {
                    int ultimoVenda = Convert.ToInt32(numCupom);

                    ultimoVenda = (ultimoVenda - 1);

                    //pesquisar numero de venda banco, compara negativo ou nao...........................................

                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabela = new DataTable();
                    dadosTabela = novaVenda.PesquisaVendaNegativo(ultimoVenda);

                    if (dadosTabela.Rows.Count > 0)
                    {
                        decimal objComparacao = Convert.ToDecimal(dadosTabela.Rows[0]["QUANT"].ToString());

                        if ((objComparacao != 0) || (objComparacao == null))
                        {
                            if (MessageBox.Show("Realmente deseja cancelar a última venda.? \n Nº " + (ultimoVenda) + "", "Atenção", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                            {
                                DevolucaoEstoqueProduto();
                                DevolucaoTipoPaamento();
                                statusStrip1.Refresh();
                                RegraNegocio.CupomFiscal.Bematech_FI_CancelaCupomMFD(ultimoVenda.ToString(), "", "");
                                MessageBox.Show("Venda nº " + ultimoVenda.ToString() + " foi Cancelado com Sucesso.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                AtualizarGridAberto();
                                LimpaCampos();
                            }
                            else
                            {
                                LimpaCampos();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Não Contém Venda(s) para Ser(em) Cancelada(s).", "Informação Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpaCampos();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Venda nº " + ultimoVenda + " já foi Cancelado", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpaCampos();
                    }
                }
                else if (nomeImpressora == "BEMASAT")
                {
                    if (MessageBox.Show("Realmente Deseja Cancelar a Última Venda ?" , "Confirmar Cancelamento Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        CancelarUltimaVendaBemasat(); 
                    }
                    else
                    {
                        LimpaCampos();
                    }
                }
                else if (nomeImpressora == "DARUMA")
                {
                    DevolucaoEstoqueProduto();
                    DevolucaoTipoPaamento();
                }
                else if (nomeImpressora == "ELGIN")
                {
                    CancelarUltimaVendaElgin();
                    DevolucaoEstoqueProduto();
                    DevolucaoTipoPaamento();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LimpaCampos();
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

        public void CancelarUltimaVendaBemasat()
        {
            try
            {
                //variaveis.................................................................................................
                string caminho = "";
                string assinatura = "";
                string cnpjAssinatua = "";

                //  saveFileDialog1.FileName = caminho;

                //pegar valor ultimo idCfe da venda aprovada................................................................
                XmlTextReader x = new XmlTextReader(pathUltimaVenda);
                while (x.Read())
                {
                    if (x.NodeType == XmlNodeType.Element && x.Name == "id")
                        chaveVenda = (x.ReadString());
                }

                idCfe = chaveVenda;

                chaveVenda = chaveVenda.Replace("Cx" + numcaixa, "");
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
                numeroCaixa.InnerText = "00" + numcaixa.ToString();
                nodeInfCFeCanc.AppendChild(nodeIDECanc);
                XmlElement emitente = arquivoXML.CreateElement("emit");
                nodeInfCFeCanc.AppendChild(emitente);
                emitente.InnerText = "";
                XmlElement destinatario = arquivoXML.CreateElement("dest");
                nodeInfCFeCanc.AppendChild(destinatario);
                destinatario.InnerText = "";
                XmlElement total = arquivoXML.CreateElement("total");
                nodeInfCFeCanc.AppendChild(total);
                total.InnerText = "";
                //XmlElement infAdc = arquivoXML.CreateElement("infAdic");
                //nodeInfCFeCanc.AppendChild(infAdc);


                //encontrar aruivo especifico.............
                string arquivo = (pathVendaCancelada);
                //salvar no proprio arquivo renomeando para cancelado
                arquivoXML.Save(arquivo + chaveVenda + "-canc.xml");

                //lear caminho do arquivo xml...................................
                LerArqTxt(arquivo + chaveVenda + "-canc.xml");

                if (arqu != "")
                {
                    //gera numero rondomico........................................
                    getNumberRandom();

                    //pega cod.....................................................
                    PesquisarCodAticavaoXml();

                    //recupera o retorno do SAT...............................
                    string ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.CancelarUltimaVenda(sessao, codAtivacao, chaveVenda, arqu)));

                    //separa os retorno por pig...............................
                    string ret_sessao = (Sep_Delimitador('|', 0, ret));
                    string ret_sat = (Sep_Delimitador('|', 1, ret));
                    string ret_erro = (Sep_Delimitador('|', 2, ret));
                    string ret_err_ = (Sep_Delimitador('|', 3, ret));
                    string ret_0 = (Sep_Delimitador('|', 4, ret));
                    string ret_1 = (Sep_Delimitador('|', 5, ret));
                    string ret_desc = (Sep_Delimitador('|', 6, ret));
                    string ret_3 = (Sep_Delimitador('|', 7, ret));
                    string ret_id = (Sep_Delimitador('|', 8, ret));

                    if (ret_sat == "07000")
                    {
                        int ultimaVenda = Convert.ToInt32(numCupom);
                        ultimaVenda = ultimaVenda - 1;

                       
                            MessageBox.Show("Venda Nº " + ultimaVenda.ToString() + " Cancelado com Sucesso.", "Venda Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpaCampos();
                            DevolucaoEstoqueProduto();
                            DevolucaoTipoPaamento();

                            byte[] senhaBinaria = new byte[256];

                            //Descriptografa....................................................................................................
                            senhaBinaria = Convert.FromBase64String(ret_desc);

                            string senhaDescripto = ASCIIEncoding.ASCII.GetString(senhaBinaria);

                            //gera arquivo em txt aprovado......................................................................................
                            string strPathFile = pathDadosVendaCanceladaAutorizada + "00" + numcaixa.ToString() + ".txt";


                            using (FileStream fs = File.Create(strPathFile))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    sw.Write(senhaDescripto);
                                }
                            }

                            //Converte TXTpara XML e Salva em Arquivo XML............................................................................................

                            string mes = DateTime.Now.Month.ToString();
                            string ano = DateTime.Now.Year.ToString();

                            string enderecoExitente = (pathCustodia + ano + "\\" + mes + "\\" + "Cx" + numcaixa + "\\");

                            FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                            byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                            sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                            FileStream srXml = new FileStream(enderecoExitente + idCfe + "Canc.xml", FileMode.Create, FileAccess.Write);
                            StreamWriter wr = new StreamWriter(srXml);
                            srXml.Write(bytes, 0, bytes.Length);
                            sr.Close();
                            srXml.Close();

                            //MONTAR E IMPRIMIR CUPOM CANCELADO
                            MontarCupomCancelado();
                        
                    }
                    else if (ret_sat == "07007")
                    {
                        int ultimaVenda = Convert.ToInt32(numCupom);
                        ultimaVenda = ultimaVenda - 1;

                        MessageBox.Show("Venda Nº " + ultimaVenda.ToString() + " Já Foi Cancelado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpaCampos();
                    }
                    else
                    {
                        MessageBox.Show(ret.ToString());
                        LimpaCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CancelarUltimaVendaElgin()
        {
            try
            {
                //variaveis.................................................................................................
                string caminho = "";
                string assinatura = "";
                string cnpjAssinatua = "";

                //  saveFileDialog1.FileName = caminho;

                //pegar valor ultimo idCfe da venda aprovada................................................................
                XmlTextReader x = new XmlTextReader(pathUltimaVenda);
                while (x.Read())
                {
                    if (x.NodeType == XmlNodeType.Element && x.Name == "id")
                        chaveVenda = (x.ReadString());
                }

                idCfe = chaveVenda;

                chaveVenda = chaveVenda.Replace("Cx" + numcaixa, "");
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
                numeroCaixa.InnerText = "00" + numcaixa.ToString();
                nodeInfCFeCanc.AppendChild(nodeIDECanc);
                XmlElement emitente = arquivoXML.CreateElement("emit");
                nodeInfCFeCanc.AppendChild(emitente);
                emitente.InnerText = "";
                XmlElement destinatario = arquivoXML.CreateElement("dest");
                nodeInfCFeCanc.AppendChild(destinatario);
                destinatario.InnerText = "";
                XmlElement total = arquivoXML.CreateElement("total");
                nodeInfCFeCanc.AppendChild(total);
                total.InnerText = "";
                //XmlElement infAdc = arquivoXML.CreateElement("infAdic");
                //nodeInfCFeCanc.AppendChild(infAdc);


                //encontrar aruivo especifico.............
                string arquivo = (pathVendaCancelada);
                //salvar no proprio arquivo renomeando para cancelado
                arquivoXML.Save(arquivo + chaveVenda + "-canc.xml");

                //lear caminho do arquivo xml...................................
                LerArqTxt(arquivo + chaveVenda + "-canc.xml");

                if (arqu != "")
                {
                    //gera numero rondomico........................................
                    getNumberRandom();

                    //pega cod.....................................................
                    PesquisarCodAticavaoXml();

                    //recupera o retorno do SAT...............................
                    string ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.CancelarUltimaVenda(sessao, codAtivacao, chaveVenda, arqu)));

                    //separa os retorno por pig...............................
                    string ret_sessao = (Sep_Delimitador('|', 0, ret));
                    string ret_sat = (Sep_Delimitador('|', 1, ret));
                    string ret_erro = (Sep_Delimitador('|', 2, ret));
                    string ret_err_ = (Sep_Delimitador('|', 3, ret));
                    string ret_0 = (Sep_Delimitador('|', 4, ret));
                    string ret_1 = (Sep_Delimitador('|', 5, ret));
                    string ret_desc = (Sep_Delimitador('|', 6, ret));
                    string ret_3 = (Sep_Delimitador('|', 7, ret));
                    string ret_id = (Sep_Delimitador('|', 8, ret));

                    if (ret_sat == "07000")
                    {
                        int ultimaVenda = Convert.ToInt32(numCupom);
                        ultimaVenda = ultimaVenda - 1;

                        if (MessageBox.Show("Realmente Deseja Cancelar Venda Nº " + ultimaVenda + ".", "Confirmar Cancelamento Venda", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                        {
                            MessageBox.Show("Venda Nº " + ultimaVenda.ToString() + " Cancelado com Sucesso.", "Venda Cancelada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LimpaCampos();
                            DevolucaoEstoqueProduto();
                            DevolucaoTipoPaamento();

                            byte[] senhaBinaria = new byte[256];

                            //Descriptografa....................................................................................................
                            senhaBinaria = Convert.FromBase64String(ret_desc);

                            string senhaDescripto = ASCIIEncoding.ASCII.GetString(senhaBinaria);

                            //gera arquivo em txt aprovado......................................................................................
                            string strPathFile = pathDadosVendaCanceladaAutorizada + "00" + numcaixa.ToString() + ".txt";


                            using (FileStream fs = File.Create(strPathFile))
                            {
                                using (StreamWriter sw = new StreamWriter(fs))
                                {
                                    sw.Write(senhaDescripto);
                                }
                            }

                            //Converte TXTpara XML e Salva em Arquivo XML............................................................................................

                            string mes = DateTime.Now.Month.ToString();
                            string ano = DateTime.Now.Year.ToString();

                            string enderecoExitente = (pathCustodia + ano + "\\" + mes + "\\" + "Cx" + numcaixa + "\\");

                            FileStream sr = new FileStream(strPathFile, FileMode.Open, FileAccess.Read);
                            byte[] bytes = new byte[Convert.ToInt32(sr.Length)];
                            sr.Read(bytes, 0, Convert.ToInt32(sr.Length));
                            FileStream srXml = new FileStream(enderecoExitente + idCfe + "Canc.xml", FileMode.Create, FileAccess.Write);
                            StreamWriter wr = new StreamWriter(srXml);
                            srXml.Write(bytes, 0, bytes.Length);
                            sr.Close();
                            srXml.Close();

                            //MONTAR E IMPRIMIR CUPOM CANCELADO
                            MontarCupomCancelado();
                        }
                    }
                    else if (ret_sat == "07007")
                    {
                        int ultimaVenda = Convert.ToInt32(numCupom);
                        ultimaVenda = ultimaVenda - 1;

                        MessageBox.Show("Venda Nº " + ultimaVenda.ToString() + " Já Foi Cancelado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimpaCampos();
                    }
                    else
                    {
                        MessageBox.Show(ret.ToString());
                        LimpaCampos();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

        private int getNumberRandom()
        {
            Random number = new Random();
            int retorno = number.Next(999999);
            sessao = retorno;
            return retorno;
        }

        public void AlterarNumVenda()
        {
            try
            {
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                novoParametro.AlterarNumVendaFecharVenda(Convert.ToInt32(lblNumeroVenda.Text) + 1);
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisaUltimoMNumEntrada();

                lblNumeroVenda.Text = dadosTabela.Rows[0]["NUM_CUPOM"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void CancelaVenda()
        {
            try
            {
                int idUsu = 0;

                //pesquisa Venda com numero do cupomAtual... 
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVendas(Convert.ToInt32(lblNumeroVenda.Text), numcaixa);

                gdvEntrada.DataSource = dadosTabela;
                //atributos do produto devolvido................................................................
                int nuCupomDev, idProdutoDev, itemDev;
                decimal qtdeDev, precoDev, somaDev, custoDev;
                string codBarraDev, produDev;

                for (int i = 0; i < gdvEntrada.Rows.Count; i++)
                {
                    idProdutoDev = Convert.ToInt32(gdvEntrada.Rows[i].Cells["ID_PROD"].Value.ToString());
                    qtdeDev = Convert.ToDecimal(gdvEntrada.Rows[i].Cells["QUANT"].Value.ToString());
                    precoDev = Convert.ToDecimal(gdvEntrada.Rows[i].Cells["PRECO_"].Value.ToString());
                    somaDev = Convert.ToDecimal(gdvEntrada.Rows[i].Cells["TOTAL"].Value.ToString());
                    itemDev = Convert.ToInt32(gdvEntrada.Rows[i].Cells["ITEMS"].Value.ToString());
                    custoDev = Convert.ToDecimal(gdvEntrada.Rows[i].Cells["CUSTO"].Value.ToString());
                    codBarraDev = gdvEntrada.Rows[i].Cells["COD_BARRA"].Value.ToString();
                    produDev = gdvEntrada.Rows[i].Cells["DESCRICAO_PRODUTO"].Value.ToString();

                    qtdeDev = (qtdeDev * -1);
                    somaDev = (somaDev * -1);

                    //converte em valores negativo para inserir no banco..............................................
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    novaVenda.DevolucaoVendaBanco(idProdutoDev, Convert.ToDecimal(qtdeDev), Convert.ToDecimal(precoDev), somaDev, idUsuarioLogado, itemDev, DateTime.Now, custoDev, false, Convert.ToInt32(lblNumeroVenda.Text), codBarraDev, produDev, aliquota, idParametro, _valorPis, cstPis, _valorCofins, _cstCofins, _cfop, _ncm, _icmCst, _origemProduto, fechado, numcaixa);
                }
            }
            catch (Exception ex)
            {
                // MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ExcluirVenda();
            }
        }

        public void ExcluirVenda()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                novaVenda.ExcluirVenda(numcaixa, numCupom, idUsuarioLogado);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtQuantidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtTemp = (TextBox)sender;

            if ((txtTemp.Text.Contains(",") && e.KeyChar == (char)44))
            {
                e.Handled = true;
            }
        }

        private void txtSomas_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txtTemp = (TextBox)sender;

            if ((txtTemp.Text.Contains(",") && e.KeyChar == (char)44))
            {
                e.Handled = true;
            }

            //Se não for número, vírgula ou backspace trava a tecla
            if (!(Char.IsNumber(e.KeyChar)) && !(e.KeyChar == (char)8) &&
            !(e.KeyChar == (char)44))
            {
                e.Handled = true;
            }
        }

        private void btnCancelaVenda_Click(object sender, EventArgs e)
        {
            try
            {
                if (gdvEntrada.Rows.Count > 0)
                {
                    frmLoginCancelarVenda frmLoginCanVenda = new frmLoginCancelarVenda(this);
                    frmLoginCanVenda.ShowDialog();
                    LimpaCampos();
                }
                else
                {
                    MessageBox.Show("Não Contém Venda(s) para ser(em) Cancelada(s).", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpaCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelaItem_Click(object sender, EventArgs e)
        {
            if (gdvEntrada.Rows.Count > 0)
            {
                frmCancelaItemVenda frmCancelarItemVenda = new frmCancelaItemVenda(this);
                frmCancelarItemVenda.ShowDialog();
                LimpaCampos();
            }
            else
            {
                MessageBox.Show("Não contém item para ser removido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCodigo.Focus();
            }
        }

        public void CancelaItemVenda()
        {
            if (itemAtual <= 0)
            {
                MessageBox.Show("Por Favor Selecione Item Deseja para Cancelamento.", "Informação Importante", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimpaCampos();
            }
            else
            {
                //metodo para buscar descricao produto para ser cancelado..................................................

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                dadosTabelaVenda = novaVenda.PesquisarProdutoItem(numCupom, itemAtual, numcaixa);

                descricao = dadosTabelaVenda.Rows[0]["DESCRICAO_PRODUTO"].ToString();

                if (MessageBox.Show("Deseja realmente Excluir Item Nº: " + itemAtual + " Produto: " + descricao + ".?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int idUsu = 0;

                    //pesquisa usuario Logado..........................................................................
                    novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                    DataTable dadosUsuario = new DataTable();
                    dadosUsuario = novoUsuario.PesquisaUsuarioLogado(numcaixa);

                    if (dadosUsuario.Rows.Count > 0)
                    {
                        idUsu = Convert.ToInt32(dadosUsuario.Rows[0]["ID_USUARIO"].ToString());
                    }

                    idUsu = idUsuarioLogado;
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    novaVenda.ExcluirDetalhes(itemAtual, numCupom, numcaixa);

                    MessageBox.Show("Item " + itemAtual + " excluído com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AtualizarGridAberto();


                    //metodo para sincronizar numero de item apos a quebra de sequencia................................
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    int itemA = 0;

                    if (gdvEntrada.Rows.Count > 0)
                    {
                        for (int i = 0; i < gdvEntrada.Rows.Count; i++)
                        {
                            int idVenda = Convert.ToInt32(gdvEntrada.Rows[i].Cells["ID"].Value);
                            itemA++;

                            novaVenda.AlterarESinclonizarItensVenda(itemA, numCupom, idVenda);
                        }
                    }

                    if (gdvEntrada.Rows.Count <= 0)
                    {
                        novoParametro = new RegraNegocio.ParametroRegraNegocio();
                        novoParametro.AlterarStatusFechar(Convert.ToInt32(numVenda), numcaixa);
                    }
                }
                else
                {
                    LimpaCampos();
                }
            }
        }

        public void BloquearColunas()
        {
            for (int i = 0; i < gdvEntrada.Rows.Count; i++)
            {
                gdvEntrada.CurrentCell = gdvEntrada.Rows[i].Cells[3];
            }
        }

        private void gdvEntrada_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idProdutoItem = Convert.ToInt32(gdvEntrada.Rows[gdvEntrada.CurrentCell.RowIndex].Cells["ID_PROD"].Value);
            itemAtual = Convert.ToInt32(gdvEntrada.Rows[gdvEntrada.CurrentCell.RowIndex].Cells["ITEMS"].Value);
        }

        private void txtProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LimpaCampos();
            }

            if (e.KeyCode == Keys.Enter)
            {
                LimpaCampos();
            }

            if (e.KeyCode == Keys.End)
            {
                LimpaCampos();
            }

            if (e.KeyCode == Keys.Tab)
            {
                LimpaCampos();
            }
        }

        private void txtValorTotal_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LimpaCampos();
            }
            if (e.KeyCode == Keys.Enter)
            {
                LimpaCampos();
            }

            if (e.KeyCode == Keys.End)
            {
                LimpaCampos();
            }
        }

        private void gdvEntrada_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                LimpaCampos();
            }

            if (e.KeyCode == Keys.End)
            {
                LimpaCampos();
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    idProdutoItem = Convert.ToInt32(gdvEntrada.Rows[gdvEntrada.CurrentCell.RowIndex].Cells["ID_PROD"].Value);
                    itemAtual = Convert.ToInt32(gdvEntrada.Rows[gdvEntrada.CurrentCell.RowIndex].Cells["ITEMS"].Value);

                    if (itemAtual > 0)
                    {
                        frmCancelaItemVenda frmCancelarItemVenda = new frmCancelaItemVenda(this);
                        frmCancelarItemVenda.ShowDialog();
                        LimpaCampos();
                    }
                }

                LimpaCampos();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmSangria frmSangria = new frmSangria(this);
            frmSangria.ShowDialog();
            txtCodigo.Focus();
        }

        private void txtCodigo_KeyDown(object sender, KeyEventArgs e)
        {
            if ((Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.Z) || (e.KeyCode==Keys.F12))
            {
                txtCodigo.Text = "";

                if (MessageBox.Show("Confirmação para Redução Z", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    if (MessageBox.Show("Realmente Deseja Tirar uma Redução Z", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (nomeImpressora == "SAT")
                        {
                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_ReducaoZ("", "");
                            LimpaCampos();
                            MessageBox.Show("Leitura Z Realizado com Sucesso.", "Informação");
                        }
                        else if (nomeImpressora == "BEMATECH")
                        {
                            idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_ReducaoZ("", "");
                            LimpaCampos();
                            MessageBox.Show("Leitura Z Realizado com Sucesso.", "Informação");
                        }
                        else
                        {
                            //MessageBox.Show("Verefique a Impressora Ligada.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //txtCodigo.Focus();
                        }
                    }
                    else
                    {
                        txtCodigo.Focus();
                    }
                }
            }

            if (Control.ModifierKeys == Keys.Control && e.KeyCode == Keys.I)
            {
                LimpaCampos();
                frmTrocarLogo logo = new frmTrocarLogo(this);
                logo.MdiParent = this.MdiParent;
                logo.ShowDialog();
            }
        }

        private void timer2_Tick_1(object sender, EventArgs e)
        {
            if (pb.Value < 100)
            {
                this.pb.Increment(2);
            }
            else
            {
                timer2.Stop();
                pb.Value = 0;

                if (MessageBox.Show("Verifique se a Impessoara Fiscal está Ligada. \nOu entre em contato com o Administrador.", "Erro de Comunicação", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                {
                    //this.Close();
                }
            }
        }

        private void txtQuantidade_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtCodigo.Text.Trim().Equals(""))
                    {
                        qtde = Convert.ToDecimal(txtQuantidade.Text);
                        txtQuantidade.Enabled = false;
                        txtCodigo.Focus();
                    }
                    else
                    {
                        VendeItemQtde();
                    }
                }
            }
            catch (Exception)
            {
                txtQuantidade.Focus();
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                if (nomeImpressora == "COMUM")
                {
                    string mensagem = (A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + "\n" + F).ToString();

                    var pri = sender as System.Drawing.Printing.PrintDocument;
                    using (var font = new Font("Arial", 7))
                    using (var brush = new SolidBrush(System.Drawing.Color.Black))
                    {
                        e.Graphics.DrawString(
                            mensagem,
                            font,
                            brush,
                            new RectangleF(0, 0, pri.DefaultPageSettings.PrintableArea.Width, pri.DefaultPageSettings.PrintableArea.Height));
                    }
                }
                else if (nomeImpressora == "BEMASAT")
                {
                    //string mensagem = (A + "\n" + B + "\n" + C + "\n" + D + "\n" + E + "\n" + F).ToString();

                    ////var pri = sender as System.Drawing.Printing.PrintDocument;
                    ////using (var font = new Font("COURIER", 8))
                    ////using (var brush = new SolidBrush(System.Drawing.Color.Black))
                    ////{
                    ////    e.Graphics.DrawString(
                    ////        mensagem,
                    ////        font,
                    ////        brush,
                    ////        new RectangleF(0, 0, pri.DefaultPageSettings.PrintableArea.Width, pri.DefaultPageSettings.PrintableArea.Height));
                    ////}

                    //RegraNegocio.MP2032.BematechTX(mensagem).ToString(); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument2_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            try
            {
                string mensagem = (A + "\n" + cupomComumAberto).ToString();

                var pri = sender as System.Drawing.Printing.PrintDocument;
                using (var font = new Font("Arial", 7))
                using (var brush = new SolidBrush(System.Drawing.Color.Black))
                {
                    e.Graphics.DrawString(
                        mensagem,
                        font,
                        brush,
                        new RectangleF(0, 0, pri.DefaultPageSettings.PrintableArea.Width, pri.DefaultPageSettings.PrintableArea.Height));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPreco_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                double t = Convert.ToDouble(txtPreco.Text);

                if (t >= 0.01)
                {
                    qtde = Convert.ToDecimal(txtQuantidade.Text);
                    preco = Convert.ToDecimal(txtPreco.Text);
                    soma = (qtde * preco);
                    txtSomas.Text = soma.ToString();
                    ControlarTotalVenda();
                    AtualizarGridVenda();
                    LoadTela();
                    SomaTotalGrid();
                    txtPreco.Enabled = false;
                    AtualizarGridAberto();
                }
                else
                {
                    txtPreco.Focus();
                }
            }
        }

        public void MontarLeirturaX()
        {
            nomeImpressora = nomeImpressora.Replace(" ", "");

            if (nomeImpressora == "LPT")
            {
                AbreCupomLeituraX();
                VendaX();
                AliquotaX();
            }
            else if (nomeImpressora == "COMUM")
            {
                AbreCupomLeituraX();
                VendaX();
                AliquotaX();
            }
            else if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "MP2500"))
            {
                AbreCupomLeituraXBemasat();
                VendaXBemasat();
                AliquotaXBemasat();
                VendaCanceladaBemasat();
                ObservacoesContruite();
            }
            else if (nomeImpressora == "DARUMA")
            {
                AbreCupomLeituraXDaruma();
                VendaXDaruma();
                AliquotaXDaruma();
                VendaCanceladaBemasat();
                ObservacoesContruite();
            }

            else if (nomeImpressora == "ELGIN")
            {
                AbreCupomElgin();
                VendaXElgin();
                AliquotaXElgin();
                VendaCanceladaBemasat();
                ObservacoesContruite();
            }
        }

        private void AliquotaXDaruma()
        {
            try
            {
                novoPagamentoVenda = new RegraNegocio.PagamentoVendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                string textoAliquota = "";

                string data = DateTime.Now.Date.ToShortDateString();
                bool baixado = false;
                dadosTabelaVenda = novoPagamentoVenda.LeituraX(numcaixa, idUsuarioLogado, data, "");

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        aliquotaX = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                        valorX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());

                        //Limpar variaveis............................................................
                        aliquotaX = aliquotaX.Replace(" ", "");

                        if (aliquotaX.Length == 2)
                        {
                            textoAliquota += aliquotaX + ".................................." + valorX.ToString("C2").ToString() + "\n";
                        }
                        else if (aliquotaX.Length == 5)
                        {
                            textoAliquota += aliquotaX + "................................." + valorX.ToString("C2").ToString() + "\n";
                        }
                        else
                        {
                            textoAliquota += aliquotaX + "................................." + valorX.ToString("C2").ToString() + "\n";
                        }
                    }

                    CX = ("-----------------------------------------------\n" +
                          "             ***** ALIQUOTAS *****             \n" +
                          "-----------------------------------------------" +
                          "\n" +
                           textoAliquota +
                           "\r");
                }
                else
                {
                    MessageBox.Show("Não foi Possivel Realizar Leitura X.", "Atenção");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AliquotaXElgin()
        {
            try
            {
                novaTrib = new RegraNegocio.TribRegraNegocio();
                DataTable dadosTabelaTrib = new DataTable();

                string textoAliquota = "";
                string trib = "";

                string data = DateTime.Now.Date.ToShortDateString();
                bool baixado = false;
                numcaixa = Convert.ToInt32(lblNumeroCaixa.Text);

                dadosTabelaTrib = novaTrib.PesquisarTribAll();

                if (dadosTabelaTrib.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaTrib.Rows.Count; i++)
                    {
                        trib = dadosTabelaTrib.Rows[i]["DESCRICAO"].ToString().Trim();

                        novoPagamentoVenda = new RegraNegocio.PagamentoVendaRegraNegocios();
                        DataTable dadosTabelaVenda = new DataTable();
                        dadosTabelaVenda = novoPagamentoVenda.LeituraX(numcaixa, idUsuarioLogado, data, trib);

                        if (dadosTabelaVenda.Rows.Count > 0)
                        {
                            aliquotaX = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                            valorX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());

                            //Limpar variaveis............................................................
                            aliquotaX = aliquotaX.Replace(" ", "");

                            if (aliquotaX.Length == 2)
                            {
                                textoAliquota += aliquotaX + ".................................." + valorX.ToString("C2").ToString() + "\n";
                            }
                            else if (aliquotaX.Length == 5)
                            {
                                textoAliquota += aliquotaX + "................................." + valorX.ToString("C2").ToString() + "\n";
                            }
                            else
                            {
                                textoAliquota += aliquotaX + "................................." + valorX.ToString("C2").ToString() + "\n";
                            }
                        }

                        CX = ("-----------------------------------------------\n" +
                              "             ***** ALIQUOTAS *****             \n" +
                              "-----------------------------------------------" +
                              "\n" +
                               textoAliquota +
                               "\r");

                        //Limpar variaveis............................................................
                        aliquotaX = aliquotaX.Replace(" ", "");

                        if ((valorX == 0) || (valorX == null))
                        {
                            valorX = 0;
                            aliquotaX = trib;

                            if (aliquotaX.Length < 2)
                            {
                                aliquotaX = aliquotaX.PadLeft(2, ' ');
                            }
                        }

                        textoAliquota += aliquotaX + "............................" + valorX.ToString("C2").ToString() + "\n";

                        valorX = 0;
                        aliquotaX = "";
                    }
                }
                else
                {
                    MessageBox.Show("Não foi Possivel Realizar Leitura X.", "Atenção");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void VendaXElgin()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                string data = DateTime.Now.ToShortDateString();
                decimal total_x = 0;
                numcaixa = Convert.ToInt32(lblNumeroCaixa.Text);

                if (fecharCaixa == false)
                {
                    baixado = false;
                    fechados = false;
                }
                else
                {
                    baixado = true;
                    fechados = true;
                }

                dadosTabelaVenda = novaVenda.VendaX(idUsuarioLogado, numcaixa, data);

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        valorvendaX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                        idTipoPagamentoX = Convert.ToInt32(dadosTabelaVenda.Rows[i]["TIPO_PAGTO_ID"].ToString());
                        total_x += Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());

                        if (idTipoPagamentoX == 1)
                        {
                            descricaoTipoPgto = "DINHEIRO........................." + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 2)
                        {
                            descricaoTipoPgto = "CARTÃO..........................." + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 3)
                        {
                            descricaoTipoPgto = "CHEQUE..........................." + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 4)
                        {
                            descricaoTipoPgto = "ABERTO..........................." + valorvendaX.ToString("C2");
                        }
                        else
                        {
                            descricaoTipoPgto = "OUTROS..........................." + valorvendaX.ToString("C2");
                        }

                        AX += descricaoTipoPgto.ToString() + "\n\r";
                    }

                    BX += "\nTOTAL: .........................." + total_x.ToString("c2");
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

        private void VendaXDaruma()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                string data = DateTime.Now.ToShortDateString();
                decimal total_x = 0;

                if (fecharCaixa == false)
                {
                    baixado = false;
                    fechados = false;
                }
                else
                {
                    baixado = true;
                    fechados = true;
                }

                dadosTabelaVenda = novaVenda.VendaX(idUsuarioLogado, numcaixa, data);

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        valorvendaX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                        idTipoPagamentoX = Convert.ToInt32(dadosTabelaVenda.Rows[i]["TIPO_PAGTO_ID"].ToString());
                        total_x += Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());

                        if (idTipoPagamentoX == 1)
                        {
                            descricaoTipoPgto = "DINHEIRO........................." + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 2)
                        {
                            descricaoTipoPgto = "CARTÃO..........................." + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 3)
                        {
                            descricaoTipoPgto = "CHEQUE..........................." + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 4)
                        {
                            descricaoTipoPgto = "ABERTO..........................." + valorvendaX.ToString("C2");
                        }
                        else
                        {
                            descricaoTipoPgto = "OUTROS..........................." + valorvendaX.ToString("C2");
                        }

                        AX += descricaoTipoPgto.ToString() + "\n\r";
                    }

                    BX += "\nTOTAL: .........................." + total_x.ToString("c2");
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

        private void AbreCupomLeituraXDaruma()
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

                AX = ("---------------------------------------------\n" +
                     nomeCliente + "\n" +
                     enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                     bairroCliente + "\n" +
                     cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                     "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal + "\n" +
                     "-----------------------------------------------\n" +
                     "                   LEITURA X                   \n" +
                     "-----------------------------------------------\n" +
                     "DATA: " + data + "                             \n" +
                     "-----------------------------------------------\n" +
                     "               ***** VENDAS *****              \n" +
                     "-----------------------------------------------\n" +
                     "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AbreCupomElgin()
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

                AX = ("---------------------------------------------\n" +
                     nomeCliente + "\n" +
                     enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                     bairroCliente + "\n" +
                     cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                     "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal + "\n" +
                     "-----------------------------------------------\n" +
                     "                   LEITURA X                   \n" +
                     "-----------------------------------------------\n" +
                     "DATA: " + data + "                             \n" +
                     "-----------------------------------------------\n" +
                     "               ***** VENDAS *****              \n" +
                     "-----------------------------------------------\n" +
                     "\n");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ObservacoesContruite()
        {
            try
            {
                nomeImpressora = nomeImpressora.Replace(" ", "");
                DateTime data = DateTime.Now;

                if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "DARUMA") || (nomeImpressora == "ELGIN"))
                {
                    if (data == null)
                    {
                        data = DateTime.Now.Date;
                    }

                    if (operadorAtuante == null)
                    {
                        operadorAtuante = "";
                    }

                    if (numcaixa == null)
                    {
                        numcaixa = 0;
                    }

                    EX = ("DATA:" + data + "\n" +
                          "OPERADOR:" + operadorAtuante.ToString() +
                          "\n" +
                          "CX:" + numcaixa.ToString() +
                          "\n"
                          );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void VendaCanceladaBemasat()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelavenda = new DataTable();
                string data = dataUltimaVenda.ToShortDateString();

                nomeImpressora = nomeImpressora.Replace(" ", "");

                dadosTabelavenda = novaVenda.PesquisarVendaCancelada(idUsuarioLogado, numcaixa);

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
                if ((nomeImpressora == "BEMASAT") || nomeImpressora == "COMUM")
                {

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
                else if ((nomeImpressora == "DARUMA") || (nomeImpressora == "ELGIN"))
                {
                    DX = ("\n" +
                          "------------------------------------------------\n" +
                          "        ****** VENDA(S) CANCELDA(S) ******        \n" +
                          "------------------------------------------------\n" +
                          "QTDE VENDA CANCELADA:" + qtdeVendaCancelX.ToString() +
                          "\n" +
                          "TOTAL VENDA CANCELADA:" + valorVendaCanceladaX.ToString("C2") +
                          "\n" +
                          "-----------------------------------------------\n");
                }
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
                string data = dataUltimaVenda.ToShortDateString();
                decimal total_x = 0;

                baixado = false;
                fechados = false;

                // dadosTabelaVenda = novaVenda.VendaX(baixado, idUsuarioLogado, fechados, numcaixa);
                dadosTabelaVenda = novaVenda.VendaX(idUsuarioLogado, numcaixa, data);

                nomeImpressora = nomeImpressora.Replace(" ", "");

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "COMUM"))
                    {

                        for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                        {
                            valorvendaX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                            idTipoPagamentoX = Convert.ToInt32(dadosTabelaVenda.Rows[i]["TIPO_PAGTO_ID"].ToString());


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

                            AX += descricaoTipoPgto.ToString() + "\n";

                            total_x += Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                        }

                        BX = "\nTOTAL: ......................." + total_x.ToString("C2");
                    }
                    else if ((nomeImpressora == "DARUMA") || (nomeImpressora == "ELGIN"))
                    {
                        for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                        {
                            valorvendaX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                            idTipoPagamentoX = Convert.ToInt32(dadosTabelaVenda.Rows[i]["TIPO_PAGTO_ID"].ToString());


                            if (idTipoPagamentoX == 1)
                            {
                                descricaoTipoPgto = "DINHEIRO..........................." + valorvendaX.ToString("C2");
                            }
                            else if (idTipoPagamentoX == 2)
                            {
                                descricaoTipoPgto = "CARTAO............................." + valorvendaX.ToString("C2");
                            }
                            else if (idTipoPagamentoX == 3)
                            {
                                descricaoTipoPgto = "CHEQUE............................." + valorvendaX.ToString("C2");
                            }
                            else if (idTipoPagamentoX == 4)
                            {
                                descricaoTipoPgto = "ABERTO............................." + valorvendaX.ToString("C2");
                            }
                            else
                            {
                                descricaoTipoPgto = "OUTROS............................." + valorvendaX.ToString("C2");
                            }

                            AX += descricaoTipoPgto.ToString() + "\n\r";

                            total_x += Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                        }

                        BX = "\nTOTAL: ............................" + total_x.ToString("C2") + "\n";
                    }
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

        public void VendaX()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                string data = DateTime.Now.ToShortDateString();
                decimal total_x = 0;

                if (fecharCaixa == false)
                {
                    baixado = false;
                    fechados = false;
                }
                else
                {
                    baixado = true;
                    fechados = true;
                }

                dadosTabelaVenda = novaVenda.VendaX(idUsuarioLogado, numcaixa, data);

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        valorvendaX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                        idTipoPagamentoX = Convert.ToInt32(dadosTabelaVenda.Rows[i]["TIPO_PAGTO_ID"].ToString());
                        total_x += Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());

                        if (idTipoPagamentoX == 1)
                        {
                            descricaoTipoPgto = "DINHEIRO.............................." + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 2)
                        {
                            descricaoTipoPgto = "CARTÃO................................" + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 3)
                        {
                            descricaoTipoPgto = "CHEQUE................................" + valorvendaX.ToString("C2");
                        }
                        else if (idTipoPagamentoX == 4)
                        {
                            descricaoTipoPgto = "ABERTO................................" + valorvendaX.ToString("C2");
                        }
                        else
                        {
                            descricaoTipoPgto = "OUTROS................................" + valorvendaX.ToString("C2");
                        }

                        AX += descricaoTipoPgto.ToString() + "\n\r";
                    }

                    BX += "\nTOTAL: ..............................." + total_x.ToString("c2");
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

        private void ImprimiLeituraX()
        {
            try
            {
                PrintDialog printDialog3 = new PrintDialog();
                this.printDocument3.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void PesquisarUlimaVenda()
        {
            try
            {
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();

                dadosTabelaVenda = novaVenda.PesquisarUltimaVenda(numcaixa, idUsuarioLogado);

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    dataUltimaVenda = Convert.ToDateTime(dadosTabelaVenda.Rows[0]["DATA"].ToString());
                }
                else
                {
                    dataUltimaVenda = DateTime.Now;
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
                        trib = dadosTabelaTrib.Rows[i]["DESCRICAO"].ToString().Trim();

                        if (trib.Length < 2)
                        {
                            trib = trib.PadLeft(2, '0');
                        }

                        novoPagamentoVenda = new RegraNegocio.PagamentoVendaRegraNegocios();
                        DataTable dadosTabelaVenda = new DataTable();

                        dadosTabelaVenda = novoPagamentoVenda.LeituraX(numcaixa, idUsuarioLogado, DT, trib);

                        if (dadosTabelaVenda.Rows.Count > 0)
                        {
                            aliquotaX = dadosTabelaVenda.Rows[0]["ALIQUOTA"].ToString().Trim();
                            valorX = Convert.ToDecimal(dadosTabelaVenda.Rows[0]["TOTAL"].ToString());

                            CX = ("-----------------------------------------\n" +
                                  "           ***** ALIQUOTAS *****         \n" +
                                  "-----------------------------------------\n" +
                                  "\n" + textoAliquota);
                        }
                        else
                        {
                            CX = ("-----------------------------------------\n" +
                                  "           ***** ALIQUOTAS *****         \n" +
                                  "-----------------------------------------\n" +
                                   textoAliquota +
                                  "\n");
                        }

                        //Limpar variaveis............................................................
                        aliquotaX = aliquotaX.Replace(" ", "");

                        if ((valorX == 0) || (valorX == null))
                        {
                            valorX = 0;
                            aliquotaX = trib;

                            if (aliquotaX.Length < 2)
                            {
                                aliquotaX = aliquotaX.PadLeft(2, ' ');
                            }
                        }

                        textoAliquota += aliquotaX + "............................" + valorX.ToString("C2").ToString() + "\n";

                        valorX = 0;
                        aliquotaX = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AliquotaX()
        {
            try
            {
                novoPagamentoVenda = new RegraNegocio.PagamentoVendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();
                string textoAliquota = "";

                string data = DateTime.Now.Date.ToShortDateString();
                bool baixado = false;
                dadosTabelaVenda = novoPagamentoVenda.LeituraX(numcaixa, idUsuarioLogado, data, "");

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        aliquotaX = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                        valorX = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());

                        //Limpar variaveis............................................................
                        aliquotaX = aliquotaX.Replace(" ", "");

                        textoAliquota += aliquotaX + ".................................." + valorX.ToString("C2").ToString() + "\n";
                    }

                    CX = ("-------------------------------------------------------------------" +
                          "                       ***** ALIQUOTAS *****                       " +
                          "-------------------------------------------------------------------" +
                          "\n" +
                           textoAliquota +
                           "\r");
                }
                else
                {
                    MessageBox.Show("Não foi Possivel Realizar Leitura X.", "Atenção");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void printDocument3_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            string mensagem = (AX + "\n" + BX + "\n" + CX + "\n" + DX).ToString();

            var pri = sender as System.Drawing.Printing.PrintDocument;
            using (var font = new Font("Arial", 9))
            using (var brush = new SolidBrush(System.Drawing.Color.Black))
            {
                e.Graphics.DrawString(
                    mensagem,
                    font,
                    brush,
                    new RectangleF(0, 0, pri.DefaultPageSettings.PrintableArea.Width, pri.DefaultPageSettings.PrintableArea.Height));
            }
        }

        private void AbreCupomLeituraX()
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

                AX = ("------------------------------------------------------------------\n" +
                     nomeCliente + "\n" +
                     enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                     bairroCliente + "\n" +
                     cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                     "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal +
                     "-------------------------------------------------------------------" +
                     "                             LEITURA X                             " +
                     "-------------------------------------------------------------------" +
                     "DATA: " + data + "                                                 " +
                     "-------------------------------------------------------------------" +
                     "                         ***** VENDAS *****                        " +
                     "-------------------------------------------------------------------" +
                     "\n");
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
                nomeImpressora = nomeImpressora.Replace(" ", "");

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

                if (fecharCaixa == true)
                {
                    if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "COMUM"))
                    {
                        AX = ("------------------------------------------\n" +
                                      nomeCliente + "\n" +
                                      enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                                      bairroCliente + "\n" +
                                      cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                                      "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal + "\n" +
                                      "----------------------------------------" +
                                      "                 REDUÇAO Z              \n" +
                                      "----------------------------------------\n" +
                                      "DATA: " + data + "                      \n" +
                                      "----------------------------------------\n" +
                                      "            ***** VENDAS *****          \n" +
                                      "----------------------------------------\n" +
                                      "             CUPOM NAO FISCAL           \n" +
                                      "----------------------------------------\n");
                    }
                    else if ((nomeImpressora == "DARUMA"))
                    {
                        AX = ("-----------------------------------------------\n" +
                                nomeCliente + "\n" +
                                enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                                bairroCliente + "\n" +
                                cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                                "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal + "\n" +
                                "-----------------------------------------------" +
                                "                   REDUÇAO Z                  \n" +
                                "----------------------------------------------\n" +
                                "DATA: " + data + "                            \n" +
                                "----------------------------------------------\n" +
                                "                ***** VENDAS *****            \n" +
                                "----------------------------------------------\n" +
                                "                 CUPOM NAO FISCAL             \n" +
                                "----------------------------------------------\n");
                    }
                    else if (nomeImpressora == "ELGIN")
                    {
                        AX = ("-----------------------------------------------\n" +
                               nomeCliente + "\n" +
                               enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                               bairroCliente + "\n" +
                               cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                               "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal + "\n" +
                               "-----------------------------------------------" +
                               "                   LEITURA X                  \n" +
                               "----------------------------------------------\n" +
                               "DATA: " + data + "                            \n" +
                               "----------------------------------------------\n" +
                               "                ***** VENDAS *****            \n" +
                               "----------------------------------------------\n" +
                               "                 CUPOM NAO FISCAL             \n" +
                               "----------------------------------------------\n");
                    }
                }
                else
                {
                    AX = ("------------------------------------------\n" +
                           nomeCliente + "\n" +
                           enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                           bairroCliente + "\n" +
                           cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                           "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal + "\n" +
                           "----------------------------------------" +
                           "                LEITURA X               \n" +
                           "----------------------------------------\n" +
                           "DATA: " + data + "                      \n" +
                           "----------------------------------------\n" +
                           "          ***** VENDAS *****            \n" +
                           "----------------------------------------\n" +
                           "           CUPOM NAO FISCAL             \n" +
                           "----------------------------------------\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarImpressaoDigital()
        {
            try
            {
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabelaParametro = new DataTable();

                dadosTabelaParametro = novoParametro.PesquisaParametroE();

                if (dadosTabelaParametro.Rows.Count > 0)
                {
                    impressaoDigital = Convert.ToBoolean(dadosTabelaParametro.Rows[0]["IMPRESSAO_DIGITAL"].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void PesquisarVendaTotalMes()
        {
            try
            {
                int mes = DateTime.Now.Month;
                int ano = DateTime.Now.Year;
                string dia, dia_, aliquotaTot;
                decimal totalRelVenda, totalvendaAliq;
                string textoVendaRelatorioVenda = "";
                string textoVendaRelatorioAliquota = "";
                totalGeralMes = 0;

                //dados da venda total
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();

                dadosTabelaVenda = novaVenda.PesquisarRelatorioTotalMes(numcaixa, (mes - 1));

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        dia = dadosTabelaVenda.Rows[i]["DIA"].ToString();
                        totalRelVenda = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                        textoVendaRelatorioVenda += ("**" + dia + "  " + totalRelVenda.ToString() + "\n");
                    }

                    BR = (textoVendaRelatorioVenda.ToString());
                    idRetorno = RegraNegocio.MP2032.IniciaPorta(numComimp);
                    RegraNegocio.MP2032.BematechTX(BR).ToString();
                    idRetorno = RegraNegocio.MP2032.BematechTX("\n\n");
                    idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(0);
                }

                //dados da aliquota venda

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                dadosTabelaVenda = new DataTable();

                dadosTabelaVenda = novaVenda.PesquisarRelatorioAliquotaTotalMes(numcaixa, (mes - 1), ano);

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                    {
                        dia_ = dadosTabelaVenda.Rows[i]["DIA"].ToString();
                        aliquotaTot = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                        totalvendaAliq = Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());
                        totalGeralMes += Convert.ToDecimal(dadosTabelaVenda.Rows[i]["TOTAL"].ToString());

                        textoVendaRelatorioAliquota += ("**" + dia_ + " " + aliquotaTot + "   " + totalvendaAliq);
                    }
                }

                CR = ("DIA  ALIQUOTAS    TOTAL" + "\n" + textoVendaRelatorioAliquota.ToString());
                ER = "TOTAL.........................." + totalGeralMes.ToString("C2") + "\n";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void ImprimirRelatorioMes()
        {
            int mes = DateTime.Now.Month;
            int ano = DateTime.Now.Year;
            if (mes == 1)
            {
                mes = 13;
                ano = ano - 1;
            }
            string dia, aliquotas = "";
            string vendas, totalAliquotas = "";
            string textoVendaRelatorioVenda = "";
            decimal vv = 0;
            decimal somaDia = 0;
            int numero = 0;

            //dados da venda total
            novaVenda = new RegraNegocio.VendaRegraNegocios();
            DataTable dadosTabelaVenda = new DataTable();

            dadosTabelaVenda = novaVenda.PesquisarRelatorioTotalMesTotalAliquota(numcaixa, (mes - 1), ano);

            if (dadosTabelaVenda.Rows.Count > 0)
            {
                for (int i = 0; i < dadosTabelaVenda.Rows.Count; i++)
                {
                    dia = dadosTabelaVenda.Rows[i]["DIA"].ToString();
                    aliquotas = dadosTabelaVenda.Rows[i]["ALIQUOTA"].ToString();
                    vendas = dadosTabelaVenda.Rows[i]["VENDA"].ToString();
                    totalAliquotas = dadosTabelaVenda.Rows[i]["TOTAL"].ToString();
                    vv += Convert.ToDecimal(vendas);

                    numero = Convert.ToInt32(dia);

                    aliquotas = aliquotas.Trim();

                    if ((aliquotas == "") || (aliquotas == null))
                    {
                        aliquotas = "00";
                    }

                    textoVendaRelatorioVenda += ("*** " + dia.PadLeft(2, '0') + "     " + aliquotas.PadRight(8, ' ') + "           " + totalAliquotas.PadRight(20, ' ') + "\n").ToString();
                }

                int mes_ = DateTime.Now.Month;

                AbreRelatorioTotalMes();

                CR = ("DIA                  ALIQUOTA(S)    TOTAL ALIQ" + "\n");
                BR = textoVendaRelatorioVenda.ToString();
                ER = "TOTAL..........................." + vv.ToString("C2") + "\n" + "CAIXA:" + numcaixa.ToString().PadLeft(2, '0');

                idRetorno = RegraNegocio.MP2032.IniciaPorta(numComimp);
                RegraNegocio.MP2032.BematechTX(AR + "\n" + BR + "\n" + ER + "\n" + DR + "\n\n").ToString();
                idRetorno = RegraNegocio.MP2032.BematechTX("\n\n");
                idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(0);
            }
        }

        public void MontarRelatorioTotalMes()
        {
            try
            {
                int mes_ = DateTime.Now.Month;
                AbreRelatorioTotalMes();
                PesquisarVendaTotalMes();
                ObservacoesContruiteRelatorio();

                idRetorno = RegraNegocio.MP2032.IniciaPorta(numComimp);
                RegraNegocio.MP2032.BematechTX(AR + "\n" + BR + "      " + CR + "\n" + ER + "\n" + DR + "\n\n").ToString();
                idRetorno = RegraNegocio.MP2032.BematechTX("\n\n");
                idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(0);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AbreRelatorioTotalMes()
        {
            try
            {
                string espaco1 = " ";
                string espaco2 = "  ";
                string pularlinha1 = "\n";
                string pontoFinal = "\r";
                string data = DateTime.Now.ToShortDateString();
                int dataMes = DateTime.Now.Month;
                if (dataMes == 1)
                {
                    dataMes = 13;
                }
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

                if (fecharCaixa == false)
                {
                    AR = ("--------------------------------------------------\n" +
                              nomeCliente + "\n" +
                              enderecoCliente + espaco1 + numeroCliente + espaco1 + "\n" +
                              bairroCliente + "\n" +
                              cidadeCliente + espaco1 + "-" + ufCliente + " CEP:" + cepCliente + "\n" +
                              "CNPJ:" + cnpjCliente + " - IE:" + ieCliente + pontoFinal + "\n" +
                              "--------------------------------------------------\n" +
                              "          RELATORIO TOTAL VENDA MES:" + (dataMes - 1) + "\n" +
                              "--------------------------------------------------\n" +
                              "DATA: " + DateTime.Now + "                        \n" +
                              "--------------------------------------------------\n" +
                              "                CUPOM NAO FISCAL                  \n" +
                              "--------------------------------------------------\n" +
                              "DIA        ALIQUOTAS          TOTAL                 ");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void ObservacoesContruiteRelatorio()
        {
            try
            {
                nomeImpressora = nomeImpressora.Replace(" ", "");
                DateTime data = DateTime.Now;

                if (nomeImpressora == "BEMASAT")
                {
                    DR = ("DATA:" + data + "\n" +
                          "OPERADOR:" + operadorAtuante.ToString() +
                          "\n" +
                          "CX:" + lblNumeroCaixa.Text +
                          "\n"
                          );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancelarUltimaVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtCodigo.Focus();
            }
        }

        private void btnSangria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtCodigo.Focus();
            }
        }

        private void btnCancelaVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtCodigo.Focus();
            }
        }

        private void btnCancelaItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txtCodigo.Focus();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ImprimirRelatorioMes();
            // MontarCupomCancelado();
        }

        public void LerChaveUltimaVendaXml()
        {
            try
            {
                string endereco = pathUltimaVenda;

                XmlTextReader x = new XmlTextReader(endereco + "\\" + idCfe + ".xml");

                if (x.NodeType == XmlNodeType.Element && x.Name == "nCFe")
                    nCFe = (x.ReadString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void LerXmlCustodiaCencelado()
        {
            string ano, mes = "";

            ano = DateTime.Now.Year.ToString();
            mes = DateTime.Now.Month.ToString();

            string endereco = pathCustodia + ano + "\\" + mes + "\\" + "Cx" + numcaixa;
            idCfe = idCfe.Replace("Cx" + numcaixa, "");

            //pegar valor ultimo idCfe da venda aprovada................................................................
            XmlTextReader x = new XmlTextReader(endereco + "\\" + idCfe + "Canc.xml");

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

                if (x.NodeType == XmlNodeType.Element && x.Name == "CNPJ")
                    cnpj = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "IE")
                    IE = (x.ReadString());

                if (x.NodeType == XmlNodeType.Element && x.Name == "IM")
                    IM = (x.ReadString());

                //descricao

                if (x.NodeType == XmlNodeType.Element && x.Name == "det nItem")
                    vItem = (x.ReadString());

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


        public void LerCabecalho()
        {
            try
            {

                cabecalho = ("----------------------------------------------\n" +
                             xFant + "\n" +
                             xNome + "\n" +
                             xLgr + ", " + nro + "\n" + xMun + "-" + CEP + "\n" +
                             "CNPJ: " + cnpj + "\n" +
                             "IE:" + IE + "-" + "IM:" + IM + "\n" +
                             "-------------------------------------------------");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void MontarCupomCancelado()
        {
            try
            {
                if (nomeImpressora == "BEMASAT")
                {
                    //MONTAGEM CABECALHO DO CUPOM SAT CANCELAMENTO CUPOM

                    LerXmlCustodiaCencelado();
                    LerCabecalho();

                    PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                    idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                    idRetorno = RegraNegocio.MP2032.IniciaPorta(numComimp);

                    string data = DateTime.Now.ToShortDateString();

                    //alinahr esquerda
                    align = "" + (char)27 + (char)97 + (char)0;
                    idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);


                    cup_A = ("               EXTRATO N " + nCFe.PadLeft(3, '0') + "\n" +
                             "          CUPOM FISCAL ELETRONICO - SAT        \n" +
                             "                   CANCELAMENTO                \n" +
                             "-----------------------------------------------\n" +
                             "CPF/CNPJ:                                      \n" +
                             "-----------------------------------------------\n" +
                             "SAT - NUMERO: " + nserieSAT.PadLeft(3, '0') + "\n" +
                             "DATA:" + data + "                              \n" +
                             "TOTAL:" + vCFe.ToString().PadLeft(40, '.') + "\n" +

                             "-----------------------------------------------\n" +
                             "OBSERVACAO CONTRIBUITE                         \n" +
                             "NUMERO CAIXA: " + numcaixa.ToString().PadLeft(3, '0') + " VENDA N: " + (numCupom - 1) + "\n" +
                             "USUARIO: " + operadorAtuante.ToString() + "     \n\n");

                    idRetorno = RegraNegocio.MP2032.BematechTX(cabecalho + "\n" + cup_A + cup_B);

                    //alinhar ao centro
                    idCfe = idCfe.Replace("CFe", "");
                    idCfe = idCfe.Replace(" ", "");

                    align = "" + (char)27 + (char)97 + (char)1;
                    idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);
                    RegraNegocio.MP2032.ConfiguraCodigoBarras(50, 0, 1, 1, 0);

                    align = "" + (char)27 + (char)97 + (char)1;
                    idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);
                    RegraNegocio.MP2032.ImprimeCodigoBarrasCODE128(idCfe);

                    align = "" + (char)27 + (char)97 + (char)1;
                    idRetorno = RegraNegocio.MP2032.ComandoTX(align, align.Length);

                    idRetorno = RegraNegocio.MP2032.ImprimeCodigoQRCODE(1, 5, 0, 6, 1, assinaturaQRCODE);

                    idRetorno = RegraNegocio.MP2032.BematechTX("\n\n\n\n");
                    idRetorno = RegraNegocio.MP2032.AcionaGuilhotina(0);
                    idRetorno = RegraNegocio.MP2032.FechaPorta();
                }
                else if (nomeImpressora == "ELGIN")
                {
                    LerXmlCustodiaCencelado();
                    LerCabecalho();

                    string data = DateTime.Now.ToShortDateString();

                    cup_A = ("               EXTRATO N " + nCFe.PadLeft(3, '0') + "\n" +
                             "          CUPOM FISCAL ELETRONICO - SAT        \n" +
                             "                   CANCELAMENTO                \n" +
                             "-----------------------------------------------\n" +
                             "CPF/CNPJ:                                      \n" +
                             "-----------------------------------------------\n" +
                             "SAT - NUMERO: " + nserieSAT.PadLeft(3, '0') + "\n" +
                             "DATA:" + data + "                              \n" +
                             "TOTAL:" + vCFe.ToString().PadLeft(40, '.') + "\n" +

                             "-----------------------------------------------\n" +
                             "OBSERVACAO CONTRIBUITE                         \n" +
                             "NUMERO CAIXA: " + numcaixa.ToString().PadLeft(3, '0') + " VENDA N: " + (numCupom - 1) + "\n" +
                             "USUARIO: " + operadorAtuante.ToString() + "     \n\n");


                    escPos = new RegraNegocio.EscPos();

                    var configImpressora = new PrinterSettings();
                    Console.WriteLine(configImpressora.PrinterName);

                    printerName = configImpressora.PrinterName;

                    this.escPos.printText(printerName, AX + "\n" + cup_A);
                    this.escPos.lineFeed(printerName, 2);

                    //dados qrcode
                    this.escPos.printQrcode(assinaturaQRCODE, printerName);
                    this.escPos.lineFeed(printerName, 1);

                    feedAndCutter(printerName, 5);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public DataTable dadosTabelaProduto { get; set; }

        public void SalvarEstoqueInicialMes()
        {
            try
            {
                string data_atual = DateTime.Now.Month.ToString();
                int numeroSetor = 0;
                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabelaVenda = new DataTable();

                dadosTabelaVenda = novaVenda.PesquisarUltimaVendaNumCaixa(numcaixa);

                if (dadosTabelaVenda.Rows.Count > 0)
                {
                    string dataUlVenda = dadosTabelaVenda.Rows[0]["DATA"].ToString();

                    if (dataUlVenda != data_atual)
                    {
                        //pesquisar setores
                        novoSetor = new RegraNegocio.SetorRegraNegocios();
                        DataTable dadosTabelaSetor = new DataTable();
                        dadosTabelaSetor = novoSetor.PesquisarSetorNumero();

                        if (dadosTabelaSetor.Rows.Count > 0)
                        {
                            for (int i = 0; i < dadosTabelaSetor.Rows.Count; i++)
                            {
                                //PESQUISAR NUMERO SETOR
                                numeroSetor = Convert.ToInt32(dadosTabelaSetor.Rows[i]["ID"].ToString());

                                //PESQUISAR A QTDE DO ESTOQUE INCIAL DO MES
                                novaVenda = new RegraNegocio.VendaRegraNegocios();
                                dadosTabelaVenda = new DataTable();
                                dadosTabelaVenda = novaVenda.PesquisarEstoqueIncialMes(numeroSetor);

                                if (dadosTabelaVenda.Rows.Count > 0)
                                {
                                    decimal qtdeEstoqueInicial = Convert.ToDecimal(dadosTabelaVenda.Rows[0]["ESTOQUE_INICIAL"].ToString());

                                    novoEstoqueIncial = new RegraNegocio.EstoqueInicialRegraNegocios();
                                    novoEstoqueIncial.CadastraEstoqueIncialMes(qtdeEstoqueInicial, DateTime.Now, idUsuarioLogado, numeroSetor);
                                    numeroSetor = 0;
                                }
                            }
                        }

                        CargaTabelaProdutoXml();
                    }
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void CargaTabelaProdutoXml()
        {
            try
            {
                string mesEstoqueInici = DateTime.Now.Month.ToString();
                novaConexao = new RegraNegocio.ConexaoRegraNegocios();

                SqlConnection conn = new SqlConnection(novaConexao.stringConexao);
                DataSet dsProduto = new DataSet();
                SqlDataAdapter daProduto = new SqlDataAdapter("SELECT COD_BARRA, NUM_DEPAR, DEPARTAM, DESCRICAO, PRECO, UNID, TRIB, ESTOQUE, GRANEL, SETOR, CUSTO FROM PRODUTO", conn);
                daProduto.Fill(dsProduto, "PRODUTO_INI");
                dsProduto.WriteXml(pathCargaProduto + mesEstoqueInici + ".xml");
                MessageBox.Show("Foi Realizado Carga de Produto no Arquivo XML Referente ao Mês: " + mesEstoqueInici.PadLeft(2, '0') + " com Sucesso", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
        }


        public void Consultarsefaz()
        {
            getNumberRandom();

            string ret = (Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.ConsultarStatusOperacional(sessao, "12345678")));
            string ret_inf1 = (Sep_Delimitador('|', 24, ret));
        }

        private void button1_Click_2(object sender, EventArgs e)
        {
            Consultarsefaz();
        }
    }
}
