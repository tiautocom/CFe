using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using RegraNegocio;
using System.Runtime.InteropServices;
using System.Xml;
using System.IO;

namespace PVe
{
    public partial class frmParametros : Form
    {
        RegraNegocio.ParametroRegraNegocio novoParametro;
        string pathVendaXML = Path.GetDirectoryName(Application.ExecutablePath) + "\\BANCO\\VENDA\\";

        public int sessao, crt = 0;
        public string inscMunic = "";
        int numCupom = 0;
        Boolean Autorizarplaca;
        Boolean autorizarTexto;
        frmVenda frmVenda;
        bool CupomImgem, impressaoAutomatica;

        public frmParametros(frmVenda venda)
        {
            InitializeComponent();
            this.frmVenda = venda;
        }

        private void frmParametros_Load(object sender, EventArgs e)
        {
            PesquisarDadosTabealParametro();
            PesquisarPortaComXml();
            txtRazaoSocial.Focus();
        }


        public void PesquisarDadosTabealParametro()
        {
            try
            {
                string status;
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisaParametroE();

                cbCodEtiqueta.Text = dadosTabela.Rows[0]["COD_ETIQUETA"].ToString();
                status = dadosTabela.Rows[0]["STATUS"].ToString();
                cbEtiquetaBalanca.Text = dadosTabela.Rows[0]["ETIQUETA_BALANCA"].ToString();
                txtRazaoSocial.Text = dadosTabela.Rows[0]["RAZAO_SOCIAL"].ToString();
                txtNomeFantasia.Text = dadosTabela.Rows[0]["NOME_FANTASIA"].ToString();
                txtEndereco.Text = dadosTabela.Rows[0]["ENDERECO_EMPRESA"].ToString();
                txtNumero.Text = dadosTabela.Rows[0]["NUMERO"].ToString();
                txtBairro.Text = dadosTabela.Rows[0]["BAIRRO"].ToString();
                txtCep.Text = dadosTabela.Rows[0]["CEP"].ToString();
                txtCidade.Text = dadosTabela.Rows[0]["CIDADE"].ToString();
                txtUf.Text = dadosTabela.Rows[0]["UF"].ToString();
                txtTelefone.Text = dadosTabela.Rows[0]["TELEFONE"].ToString();
                txtIE.Text = dadosTabela.Rows[0]["IE"].ToString();
                txtCnpj.Text = dadosTabela.Rows[0]["CNPJ"].ToString();
                txtCupom.Text = dadosTabela.Rows[0]["NUM_CUPOM"].ToString();
                txtAliquota.Text = dadosTabela.Rows[0]["ALIQUOTA_DIA"].ToString();
                cbImpressora.Text = dadosTabela.Rows[0]["IMPRESSORA"].ToString();
                cbAutorizarPlaca.Checked = Convert.ToBoolean(dadosTabela.Rows[0]["PLACA"].ToString());
                txtLimiteValor.Text = dadosTabela.Rows[0]["LIMITE_VENDA"].ToString();
              //  txtNumCaixa.Text = dadosTabela.Rows[0]["NUM_CAIXA"].ToString();
                txtMensagem.Text = dadosTabela.Rows[0]["MSG"].ToString();
              //  txtBoundRote.Text = dadosTabela.Rows[0]["BOUD_RATE"].ToString();
              //  txtPortaCom.Text = dadosTabela.Rows[0]["PORTA_COM"].ToString();
                txtQtdeCupom.Text = dadosTabela.Rows[0]["QTDE_CUPOM"].ToString();
                cbHomologacao.Checked = Convert.ToBoolean(dadosTabela.Rows[0]["HOMOLOGACAO_TESTE"].ToString());
                rbCupomImagem.Checked = Convert.ToBoolean(dadosTabela.Rows[0]["CUPOM_IMAGEM"].ToString());
                cbImpressaoAutomatica.Checked = Convert.ToBoolean(dadosTabela.Rows[0]["IMPRESSAO_DIGITAL"].ToString());


                if (rbCupomImagem.Checked == true)
                {
                    CupomImgem = true;
                }
                else if (rbCupomImagem.Checked == false)
                {
                    CupomImgem = false;
                }

                if (status == "True")
                {
                    status = "Ativo";
                    cbStatus.Text = status;
                }
                else
                {
                    status = "Inativo";
                    cbStatus.Text = status;
                }

                if (cbAutorizarPlaca.Checked == true)
                {
                    cbAutorizarPlaca.Checked = true;
                }
                else
                {
                    cbAutorizarPlaca.Checked = false;
                }

                if (cbHomologacao.Checked == true)
                {
                    cbHomologacao.Checked = true;
                }
                else
                {
                    cbHomologacao.Checked = false;
                }

                if (cbImpressaoAutomatica.Checked == true)
                {
                    cbImpressaoAutomatica.Checked = true;
                }
                else
                {
                    cbImpressaoAutomatica.Checked = false;
                }

                frmVenda.PesquisarIdParametro();
                frmVenda.PesquisaUsuarioLogado();
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PesquisarDadosTabealParametro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        public void PesquisarPortaComXml() 
        {
            try
            {
                frmVenda.PesquisarNumCaixa_numBalanca_numPorstaCom_Xml();
                txtNumCaixa.Text = frmVenda.numCaixaXml.ToString();
                txtPortaImpressora.Text = frmVenda.numComimp.ToString();
                txtPortaCom.Text = frmVenda.numComBal.ToString();
                txtBoundRote.Text = frmVenda.bondRouteCom.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void AlterarParametro()
        {
            try
            {
                if (cbAutorizarPlaca.Checked == true)
                {
                    Autorizarplaca = true;
                }
                else
                {
                    Autorizarplaca = false;
                }


                if (cbHomologacao.Checked == true)
                {
                    autorizarTexto = true;
                }
                else
                {
                    autorizarTexto = false;
                }

                if (rbCupomImagem.Checked == true)
                {
                    CupomImgem = true;
                }
                else if (rbCupomImagem.Checked == false)
                {
                    CupomImgem = false;
                }

                if (cbImpressaoAutomatica.Checked == true)
                {
                    impressaoAutomatica = true;
                }
                else if (cbImpressaoAutomatica.Checked == false)
                {
                    impressaoAutomatica = false;
                }


                numCupom = Convert.ToInt32(txtCupom.Text);
                novoParametro = new RegraNegocio.ParametroRegraNegocio();
                novoParametro.AlterarParametros(cbbCRT.Text, numCupom, cbCodEtiqueta.Text, cbEtiquetaBalanca.Text, cbStatus.Text, txtBairro.Text, txtCep.Text, txtCidade.Text, txtCnpj.Text, txtEndereco.Text, txtIE.Text, txtIm.Text,
                txtNomeFantasia.Text, txtNumero.Text, txtRazaoSocial.Text, txtTelefone.Text, txtUf.Text, Convert.ToDecimal(txtAliquota.Text), cbImpressora.Text, Autorizarplaca, Convert.ToDecimal(txtLimiteValor.Text), Convert.ToInt32(txtNumCaixa.Text), txtMensagem.Text, txtPortaCom.Text, Convert.ToInt32(txtBoundRote.Text), txtPortaImpressora.Text, autorizarTexto, CupomImgem, Convert.ToInt32(txtQtdeCupom.Value));

                MessageBox.Show("Alteração do(s) Parametro(s) foi Realizado com Sucesso!!!", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);

                PesquisarDadosTabealParametro();
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método AlterarParametro.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            txtBairro.Text = "";
            txtCep.Text = "";
            txtCidade.Text = "";
            txtCnpj.Text = "";
            txtEndereco.Text = "";
            txtIE.Text = "";
            txtNomeFantasia.Text = "";
            txtNumero.Text = "";
            txtRazaoSocial.Text = "";
            txtTelefone.Text = "";
            txtUf.Text = "";
            cbCodEtiqueta.Text = "";
            cbEtiquetaBalanca.Text = "";
            cbStatus.Text = "";
            txtRazaoSocial.Focus();
            cbAutorizarPlaca.Checked = true;
            txtLimiteValor.Text = "";
            txtMensagem.Text = "";
            txtQtdeCupom.Value = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlterarParametro();
        }

        private void btnSair_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtRazaoSocial_KeyPress(object sender, KeyPressEventArgs e)
        {
            //metodo que aciona ao aperta a tecla enter sem precisar apertao o botao
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnSair_Click_1(sender, e);
            }
        }

        private void txtRazaoSocial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnDimep_Click(object sender, EventArgs e)
        {
            getNumberRandom();

            // RegraNegocio.CupomFiscal.ConsultarSAT(sessao);
            string ret = Marshal.PtrToStringAnsi(RegraNegocio.CupomFiscal.ConsultarSAT(sessao));

            //for (int i = 0; i < 5; i++)
            //{ }

            // lbConsultarSat.Items.Add(Sep_Delimitador('|', i, ret));
            MessageBox.Show(ret);


            //construir xml num da sessao...........................................................
            XmlTextWriter writer = new XmlTextWriter(pathVendaXML + "\\Sessao" + ".xml", null);
            //inicia o documento xml
            writer.WriteStartDocument();
            //escreve o elmento raiz
            writer.WriteStartElement("Sessao");
            //Escreve os sub-elementos
            writer.WriteElementString("NumSessao", sessao.ToString());
            // encerra o elemento raiz
            writer.WriteEndElement();
            //Escreve o XML para o arquivo e fecha o objeto escritor
            writer.Close();
        }


        public int getNumberRandom()
        {
            Random number = new Random();
            int retorno = number.Next(999999);
            sessao = retorno;
            return retorno;
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

        private void rbCupomImagem_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCupomImagem.Checked == true)
            {
                CupomImgem = true;
            }
            else if (rbCupomImagem.Checked == false)
            {
                CupomImgem = false;
            }
        }
    }
}
