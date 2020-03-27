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
    public partial class frmQtdeR : Form
    {
        frmPrincipal frmPrinciapl;
        frmVenda frmVenda;

        public frmQtdeR(frmPrincipal principal, frmVenda venda)
        {
            InitializeComponent();
            this.frmPrinciapl = principal;
            this.frmVenda = venda;
        }


        public void LimparCampos()
        {
            try
            {
                txtQtdeReimprimir.Focus();
                btnSalvar.Focus();
                
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmQtdeR_Load(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void txtQtdeReimprimir_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SalvarQtdeVenda();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            SalvarQtdeVenda();
        }


        public void SalvarQtdeVenda() 
        {
            try
            {
                int qtdeCopia = Convert.ToInt32(txtQtdeReimprimir.Text);

                if (qtdeCopia > 0)
                {
                    for (int i = 0; i < qtdeCopia; i++)
                    {
                        frmPrinciapl.ImprimirRelatoriosFechamentoCaixa();
                    }

                    frmPrinciapl.AlteraVendaPagamentoFechado();
                    frmPrinciapl.AlterarBaixadoVenda();
                    frmPrinciapl.FechaLogin();
                    frmPrinciapl.FecharSangria();

                    this.Close();
                    frmPrinciapl.Close();
                    frmVenda.Close();
                }
                else
                {
                    MessageBox.Show("Informe a Quantidade de Cópia Desejado.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
