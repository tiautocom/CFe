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
    public partial class LoginAcessoPrincipal : Form
    {
        frmVenda frmVenda;

        public LoginAcessoPrincipal(frmVenda venda)
        {
            InitializeComponent();
            this.frmVenda = venda;
        }

        RegraNegocio.SenhaRegraNegocio novaSenha;

        private void txtAcessoPrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            //metodo que aciona ao aperta a tecla enter sem precisar apertao o botao
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnLogar_Click(sender, e);
            }


            if ((Keys)e.KeyChar == Keys.Escape)
            {
                btnSair_Click(sender, e);
            }
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            PesquisarAcessoPrincipal();
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        public void PesquisarAcessoPrincipal()
        {
            try
            {
                novaSenha = new RegraNegocio.SenhaRegraNegocio();
                DataTable dadosTabela = new DataTable();
                
                
                dadosTabela = novaSenha.PesquisarSenhaCancelamentoVenda(txtAcessoPrincipal.Text);

                if (dadosTabela.Rows.Count > 0)
                {
                    frmParametros frmParametro = new frmParametros(frmVenda);
                    frmParametro.ShowDialog();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Acesso não Permitido.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
