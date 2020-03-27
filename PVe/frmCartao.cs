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
    public partial class frmCartao : Form
    {
        RegraNegocio.CartaoRegraNegocio novoCartao;

        public frmCartao()
        {
            InitializeComponent();
        }

        private void frmCartao_Load(object sender, EventArgs e)
        {
            PesquisarCartao();
        }

        public DataTable PesquisarCartao() 
        {
            try
            {
                novoCartao = new RegraNegocio.CartaoRegraNegocio();
                DataTable dadosTabela= new DataTable();
                dadosTabela = novoCartao.PesquisarCartao();
                gdvCartao.DataSource = dadosTabela;
                return dadosTabela;
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PesquisarCartao.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return null;
            }
        }
    }
}
