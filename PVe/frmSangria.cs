using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace PVe
{
    public partial class frmSangria : Form
    {
        RegraNegocio.VendaRegraNegocios novaVenda;
        RegraNegocio.SangriaRegraNegocios novaSangria;
        RegraNegocio.EscPos esc;

        frmVenda frmVenda;
        decimal valorSangria;
        int idRetorno;
        int idUsuario, contador = 0;
        DateTime data;
        string operador = "";
        bool tipo;
        string descricao, nomeImpressora = "";

        public frmSangria(frmVenda venda)
        {
            InitializeComponent();
            this.frmVenda = venda;
            nomeImpressora = frmVenda.nomeImpressora;
        }

        public void Sangria()
        {
            try
            {
                data = DateTime.Now.Date;
                decimal valorT = 0;
                string aberturaCupom = "";

                novaVenda = new RegraNegocio.VendaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarSomaTotalDia(frmVenda.idUsuarioLogado, data);

                if (dadosTabela.Rows.Count > 0)
                {
                    valorT = Convert.ToDecimal(dadosTabela.Rows[0]["Total"].ToString());

                    if ((valorT > 0))
                    {
                        if (!String.IsNullOrEmpty(txtValorSangria.Text.Trim()) && (!String.IsNullOrEmpty(txtMotivoSangria.Text.Trim()) && (txtMotivoSangria.Text.Trim() != "0,00")))
                        {
                            valorSangria = Convert.ToDecimal(txtValorSangria.Text);

                            if (cbxSangria.Checked == true)
                            {
                                tipo = true;
                                descricao = "SANGRIA";
                            }
                            else if (cbxSangria.Checked == false)
                            {
                                tipo = false;
                                descricao = "DESPESA";
                            }

                            for (int i = 0; i < 2; i++)
                            {
                                if (nomeImpressora == "BEMATECH")
                                {
                                    aberturaCupom = ("----------------------------------\n" +
                                                     "         " + descricao + "         " +
                                                     "-----------------------------------");
                                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(aberturaCupom + "\n");
                                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial(descricao.ToString() + ": " + lblQtde.Text + " ......................." + valorSangria.ToString("C2"));
                                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("MOTIVO: " + txtMotivoSangria.Text);
                                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("OPERADOR: " + frmVenda.operadorAtuante.ToString());
                                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_RelatorioGerencial("Ass: ________________________________");
                                    idRetorno = RegraNegocio.CupomFiscal.Bematech_FI_FechaRelatorioGerencial();
                                }
                                else if ((nomeImpressora == "BEMASAT") || (nomeImpressora == "MP4200"))
                                {
                                    aberturaCupom = ("-----------------------------------------------\n" +
                                                     "                " + descricao + "            \n" +
                                                     "------------------------------------------------");

                                    string dadosSangriaBemasat = "";
                                    frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();

                                    //ABRE IMPRESSORA
                                    idRetorno = RegraNegocio.MP2032.ConfiguraModeloImpressora(7);
                                    idRetorno = RegraNegocio.MP2032.IniciaPorta(frmVenda.numComimp);

                                    dadosSangriaBemasat = (descricao.ToString() + ": " + lblQtde.Text + " ......................." + valorSangria.ToString("C2") + "\n" +
                                                          "MOTIVO: " + txtMotivoSangria.Text + "\n" +
                                                          "OPERADOR: " + frmVenda.operadorAtuante.ToString() + "\n" +
                                                          "Ass: ________________________________\n");


                                    idRetorno = RegraNegocio.MP2032.BematechTX(aberturaCupom + "\n" + dadosSangriaBemasat);

                                    RegraNegocio.MP2032.BematechTX("\n\n\n\n");
                                    RegraNegocio.MP2032.AcionaGuilhotina(0);
                                    RegraNegocio.MP2032.FechaPorta();
                                }
                                else if (nomeImpressora == "ELGIN")
                                {
                                    string dadoSangriaElgin = "";

                                    aberturaCupom = ("---------------------------------------------\n" +
                                                     "                " + descricao + "            \n" +
                                                     "---------------- ------------------------------");

                                    var configImpressora = new PrinterSettings();
                                    string printerName = configImpressora.PrinterName;

                                    frmVenda.AbreCupomSegViaBemasat();
                                    string cabecalhoElgin = frmVenda.A;

                                    esc = new RegraNegocio.EscPos();
                                    this.esc.normalModeText(printerName);
                                    this.esc.printText(printerName, cabecalhoElgin + "\n");

                                    dadoSangriaElgin = (descricao.ToString() + ": " + lblQtde.Text + " ......................." + valorSangria.ToString("C2") + "\n" +
                                                        "MOTIVO: " + txtMotivoSangria.Text + "\n" +
                                                        "OPERADOR: " + frmVenda.operadorAtuante.ToString() + "\n" +
                                                        "Ass: ________________________________");

                                    esc = new RegraNegocio.EscPos();
                                    this.esc.printText(printerName, aberturaCupom + "\n" + dadoSangriaElgin);
                                    //this.esc.printBarcodeB(printerName, "{A012345678901234567", 73);

                                    this.esc.lineFeed(printerName, 2);
                                    feedAndCutter(printerName, 5);
                                }
                            }

                            novaVenda.GravarDadosSangria(frmVenda.idUsuarioLogado, Convert.ToDecimal(txtValorSangria.Text), txtMotivoSangria.Text, data, frmVenda.numcaixa, tipo, false);

                            MessageBox.Show("Registro de " + descricao + " foi Realizado com Sucesso.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            frmVenda.AtualizarGridAberto();
                            frmVenda.Refresh();
                            this.Close();
                            frmVenda.AtualizarGridAberto();
                            frmVenda.LimpaCampos();
                        }
                        else
                        {
                            MessageBox.Show("Informe Valor Desejado do Motivo da Retirada para Finalizar Sangria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtMotivoSangria.Text = "";
                            txtMotivoSangria.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Não Contém Valor para Sangria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LimparCampos();
                    }
                }

                else
                {
                    MessageBox.Show("Não foi Possivél Realizar a Operação Sangria de Caixa.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Saldo no Caixa Insuficiente para Realizar uma Sangria.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LimparCampos();
            }
        }


        //linefeed and paper cutter
        public void feedAndCutter(string printerName, int numLines)
        {
            System.Threading.Thread.Sleep(500);
            this.esc.lineFeed(printerName, numLines);
            this.esc.CutPaper(printerName);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            Sangria();
            frmVenda.LimpaCampos();
        }

        private void txtValorSangria_TextChanged(object sender, EventArgs e)
        {
            Moeda(ref txtValorSangria);
        }

        public static void Moeda(ref TextBox txtValorSangria)
        {
            string n = string.Empty;
            decimal v = 0;

            try
            {
                n = txtValorSangria.Text.Replace(",", "").Replace(".", "");

                if (n.Equals(""))
                    n = "";
                n = n.PadLeft(3, '0');
                if (n.Length > 3 & n.Substring(0, 1) == "0")
                    n = n.Substring(1, n.Length - 1);
                v = Convert.ToDecimal(n) / 100;
                txtValorSangria.Text = string.Format("{0:N}", v);
                txtValorSangria.SelectionStart = txtValorSangria.Text.Length;
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Moeda.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtValorSangria_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMotivoSangria.Focus();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void LimparCampos()
        {
            txtValorSangria.Text = "0,00";
            txtValorSangria.Focus();
        }

        private void frmSangria_Load(object sender, EventArgs e)
        {
            LimparCampos();
            ContadorLinhaSangria();
        }

        private void txtMotivoSangria_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            frmVenda.LimpaCampos();
            this.Close();
            frmVenda.LimpaCampos();
        }

        private void txtMotivoSangria_KeyPress(object sender, KeyPressEventArgs e)
        {
            //metodo que aciona ao aperta a tecla enter sem precisar apertao o botao
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnSalvar_Click(sender, e);
            }
        }

        private void cbxSangria_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxSangria.Checked == true)
            {
                cbxSangria.Text = "Sangria";
                gpSangria.Text = "Motivo da Sangria";
                txtMotivoSangria.Focus();
                tipo = true;
                ContadorLinhaSangria();
                descricao = "Sangria";
            }
            else
            {
                cbxSangria.Text = "Despesas";
                gpSangria.Text = "Motivo da Despesa";
                txtMotivoSangria.Focus();
                tipo = false;
                ContadorLinhaSangria();
                descricao = "Despesas";
            }
        }

        public void ContadorLinhaSangria()
        {
            try
            {
                novaSangria = new RegraNegocio.SangriaRegraNegocios();
                DataTable dadosTabela = new DataTable();
                data = DateTime.Now;

                if (cbxSangria.Checked == true)
                {
                    tipo = true;
                }
                else if (cbxSangria.Checked == false)
                {
                    tipo = false;
                }

                dadosTabela = novaSangria.PesquisarSomaTotalDia(frmVenda.idUsuarioLogado, false, frmVenda.numcaixa, tipo);

                if (dadosTabela.Rows.Count > 0)
                {
                    contador = (Convert.ToInt32(dadosTabela.Rows.Count) + 1);
                    lblQtde.Text = contador.ToString().Trim().PadLeft(2, '0');
                }
                else
                {
                    lblQtde.Text = "01";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void gpSangria_Enter(object sender, EventArgs e)
        {

        }

        private void txtMotivoSangria_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblQtde_Click(object sender, EventArgs e)
        {

        }
    }
}
