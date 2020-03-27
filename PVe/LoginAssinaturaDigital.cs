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
    public partial class LoginAssinaturaDigital : Form
    {
        public LoginAssinaturaDigital()
        {
            InitializeComponent();
        }

        RegraNegocio.SenhaRegraNegocio novaSenha;

        private void txtAcessoPrincipal_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((Keys)e.KeyChar == Keys.Escape)
            {
                btnSair_Click(sender, e);
            }

            if ((Keys)e.KeyChar == Keys.Enter)
            {
                PesquisarAcessoAssinaturaDigital();
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            PesquisarAcessoAssinaturaDigital();
        }

        public void PesquisarAcessoAssinaturaDigital() 
        {
            try
            {
                novaSenha = new RegraNegocio.SenhaRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaSenha.PesquisarSenhaCancelamentoVenda(txtAcessoAssinaturaDigital.Text);

                if (dadosTabela.Rows.Count > 0)
                {
                    frmAssiDig frmAssinaturaDigital = new frmAssiDig();
                    frmAssinaturaDigital.ShowDialog();

                    this.Close();
                }
                else
                {
                    MessageBox.Show("Acesso não Permitido.","Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
