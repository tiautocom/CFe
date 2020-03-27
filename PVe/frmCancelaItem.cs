using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace PVe
{
    public partial class frmCancelaItem : Form
    {
        RegraNegocio.VendaRegraNegocios novaVenda;
        frmVenda frmVenda;

        public string valorCofins;
        public string cstPis;
        public string _valorPis;
        public string _cstPis;
        public string _cstCofins;
        public string _cfop;
        public string _ncm;
        public string _icmCst;
        public string _origemProduto;
        Boolean vendafechado = false;
        Boolean fechado = false;


        public frmCancelaItem(frmVenda venda)
        {
            InitializeComponent();
            this.frmVenda = venda;
        }

        private void frmCancelaItem_Load(object sender, EventArgs e)
        {
            AtualizarForm();
        }


        public void AtualizarForm() 
        {
            try
            {
                txtCancelaItem.Focus();
                txtCancelaItem.Text = "0";
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void txtCancelaItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!Char.IsDigit(e.KeyChar) && e.KeyChar != (char)8)
            {
                e.Handled = true;
            }

            //metodo que aciona ao aperta a tecla enter sem precisar apertao o botao
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnCancelaItem_Click(sender, e);
            }
        }

        private void btnCancelaItem_Click(object sender, EventArgs e)
        {
            CancelaItem();
        }

        private void txtCancelaItem_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void CancelaItem() 
        {
            try
            {
                //metodo busca valores do produto........................................................
                int itemCancelar = Convert.ToInt32(txtCancelaItem.Text);
               
                if (itemCancelar > 0)
                {
                    int numVenda = frmVenda.numVenda;
                    int numCaixa = frmVenda.numcaixa;
                    novaVenda = new RegraNegocio.VendaRegraNegocios();
                    DataTable dadosTabela = new DataTable();
                    dadosTabela = novaVenda.PesquisaCancelaItem(numVenda, itemCancelar);

                    decimal qtde = Convert.ToDecimal(dadosTabela.Rows[0]["QUANT"].ToString());
                    qtde = qtde * -1;

                    int idCodProduto = Convert.ToInt32(dadosTabela.Rows[0]["ID_PROD"].ToString());
                    decimal preco = Convert.ToDecimal(dadosTabela.Rows[0]["PRECO"].ToString());
                    decimal total = Convert.ToDecimal(dadosTabela.Rows[0]["TOTAL"].ToString());
                    int idUsuario = Convert.ToInt32(dadosTabela.Rows[0]["ID_USUARIO"].ToString());
                    int itemAtual = Convert.ToInt32(dadosTabela.Rows[0]["ITEM"].ToString());
                    DateTime data = DateTime.Now;
                    decimal custo = Convert.ToDecimal(dadosTabela.Rows[0]["CUSTO"].ToString());
                    Boolean baixado = Convert.ToBoolean(dadosTabela.Rows[0]["BAIXADO"].ToString());
                    numVenda = frmVenda.numCupom;
                    string codBarra = dadosTabela.Rows[0]["COD_BARRA"].ToString();
                    string descricao = dadosTabela.Rows[0]["DESCRICAO_PRODUTO"].ToString();
                    string aliquota = dadosTabela.Rows[0]["ALIQUOTA"].ToString();
                    int idParametro = Convert.ToInt32(dadosTabela.Rows[0]["ID_PARAMETRO"].ToString());
                    string valorPis = dadosTabela.Rows[0]["VALOR_PIS"].ToString();
                    string cstPis = dadosTabela.Rows[0]["CST_PIS"].ToString();
                    string valorCofins = dadosTabela.Rows[0]["VALOR_COFINS"].ToString();
                    string cstCofins = dadosTabela.Rows[0]["CST_COFINS"].ToString();
                    string cfop = dadosTabela.Rows[0]["CFOP"].ToString();
                    string ncm = dadosTabela.Rows[0]["NCM"].ToString();
                    string origemProduto = dadosTabela.Rows[0]["ORIGEM_PRODUTO"].ToString();
                    string icmCst = dadosTabela.Rows[0]["ICM_CST"].ToString();
                    string cest = dadosTabela.Rows[0]["CEST"].ToString();
                    string _unid= dadosTabela.Rows[0]["UNID"].ToString();
                    

                    //metodo inserir valores do produto com qtdenegativa..........................................
                    frmVenda.AtualizarGridAberto();


                    //limpando variaveis.........................................................................
                    valorCofins = valorCofins.Replace(" ", "");
                    cstPis = cstPis.Replace(" ", "");
                    valorCofins = valorCofins.Replace(" ", "");
                    cstCofins = cstCofins.Replace(" ", "");
                    cfop = cfop.Replace(" ", "");
                    ncm = ncm.Replace(" ", "");
                    origemProduto = origemProduto.Replace(" ", "");
                    icmCst = icmCst.Replace(" ", "");

                    total = total * -1;
                    itemAtual = itemAtual + 1;
                    novaVenda.VendeItem(idCodProduto, qtde, preco, total, 1, itemAtual, DateTime.Now, custo, true, numVenda, codBarra, descricao + "  - ITEM CANCELADO", aliquota, idParametro, valorPis, cstPis, valorCofins, cstCofins, cfop, ncm, icmCst, origemProduto, fechado, cest, numCaixa,_unid);

                    frmVenda.AtualizarGridAberto();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Informe o Item Deseja para ser Cancelado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
               
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
