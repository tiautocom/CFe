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
    public partial class frmCidade : Form
    {
        RegraNegocio.CidadeRegraNegocio novaCidade;

        public frmCidade()
        {
            InitializeComponent();
        }

        private void frmCidade_Load(object sender, EventArgs e)
        {
            pesquisarCidade();
        }

        public DataTable pesquisarCidade() 
        {
            try
            {
                novaCidade = new RegraNegocio.CidadeRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaCidade.PesquisarCidade();
                gdvCidade.DataSource = dadosTabela;
                return dadosTabela;

            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método pesquisarCidade.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }
    }
}
