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
    public partial class frmCancelaItemVenda : Form
    {
        frmVenda frmVenda;
        RegraNegocio.SenhaRegraNegocio novaSenha;
        RegraNegocio.ProdutoRegraNegocio novoProduto;


        public frmCancelaItemVenda(frmVenda fv)
        {
            InitializeComponent();
            this.frmVenda = fv;
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            LogarCancelamentoVenda();
        }

        public void LogarCancelamentoVenda()
        {
            try
            {
                novaSenha = new RegraNegocio.SenhaRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaSenha.PesquisarSenhaCancelamentoVenda(txtCancelaItem.Text);

                if (dadosTabela.Rows.Count > 0)
                {
                    frmVenda.CancelaItemVenda();
                    frmVenda.PesquisaUltimoItem();
                    frmVenda.AtualizarGridAberto();
                    frmVenda.LimpaCampos();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Senha Incorreta.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void LimparCampos()
        {
            try
            {
                txtCancelaItem.Text = "";
                txtCancelaItem.Focus();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtCancelaItem_KeyUp(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    LogarCancelamentoVenda();
            //    this.Close();
            //}
        }

        private void txtCancelaItem_KeyPress(object sender, KeyPressEventArgs e)
        {
            //metodo que aciona ao aperta a tecla enter sem precisar apertao o botao
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnLogar_Click(sender, e);
            }
        }

        private void frmCancelaItemVenda_Load(object sender, EventArgs e)
        {
            btnLogar.Focus();
        }
    }
}
