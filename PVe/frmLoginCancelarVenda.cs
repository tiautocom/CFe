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
    public partial class frmLoginCancelarVenda : Form
    {
        frmVenda frmVenda;
        RegraNegocio.SenhaRegraNegocio novaSenha;
        RegraNegocio.VendaRegraNegocios novaVenda;
        RegraNegocio.TempRegraNegocios novoTemp;
        RegraNegocio.ParametroRegraNegocio novoParametro;

        int numCupom;
        string descricaoProduto;


        public frmLoginCancelarVenda(frmVenda fv)
        {
            InitializeComponent();
            this.frmVenda = fv;
        }

        private void frmLoginCancelarVenda_Load(object sender, EventArgs e)
        {
            txtCancelaVenda.Focus();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void LogaCancelarVenda()
        {
            try
            {
                numCupom = frmVenda.numCupom;

                novaSenha = new RegraNegocio.SenhaRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaSenha.PesquisarSenhaCancelamentoVenda(txtCancelaVenda.Text);

                if (dadosTabela.Rows.Count > 0)
                {
                    if (MessageBox.Show("Realmente Deseja Cancelar a Venda nº " + numCupom + "?.", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        this.Close();
                        novoTemp = new RegraNegocio.TempRegraNegocios();
                        novoTemp.AlterarCpfCliente("", 0, "");
                        novoParametro = new RegraNegocio.ParametroRegraNegocio();
                       // novoParametro.AlterarStatusFechar(Convert.ToInt32(numCupom), frmVenda.numcaixa);
                        
                        frmVenda.CancelaVenda();
                        frmVenda.AlterarNumVendaNumCaixa();
                        //frmVenda.DevolucaoEstoqueProduto();
                        //frmVenda.DevolucaoTipoPaamento();
                        MessageBox.Show("Venda Cancelado com Sucesso.", "Informação");

                       // frmVenda.AtualizarGridAberto();
                        LimpaCampos();
                        frmVenda.LoadTela();
                        frmVenda.AtualizarGridAberto();
                    }
                    else
                    {
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Login Incorreto.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimpaCampos();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void LimpaCampos()
        {
            txtCancelaVenda.Text = "";
            txtCancelaVenda.Focus();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            LogaCancelarVenda();
        }

        private void txtCancelaVenda_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtCancelaVenda_KeyPress(object sender, KeyPressEventArgs e)
        {
            //metodo que aciona ao aperta a tecla enter sem precisar apertao o botao
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnLogar_Click(sender, e);
            }
        }
    }
}
