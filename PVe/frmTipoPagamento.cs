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
    public partial class frmTipoPagamento : Form
    {
        RegraNegocio.TipoPagamentoRegraNegocio novoTipo;

        frmFechamentoVenda frmFechamento;

        int _tipoPagamento;

        public frmTipoPagamento(frmFechamentoVenda Fvenda)
        {
            this.frmFechamento = Fvenda;
            InitializeComponent();
        }

        private void frmTipoPagamento_Load(object sender, EventArgs e)
        {
            PesquisaTipoPagamento();
        }

        public void PesquisaTipoPagamento() 
        {
            try
            {
                novoTipo = new RegraNegocio.TipoPagamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoTipo.PesquisaTipoPagamento();
                gdvTipoPagamento.DataSource = dadosTabela;
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PesquisaTipoPagamento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gdvTipoPagamento_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode== Keys.Escape)
            {
                this.Close();
            }
        }
    }
}
