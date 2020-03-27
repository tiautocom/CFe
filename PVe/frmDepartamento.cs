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
    public partial class frmDepartamento : Form
    {
        RegraNegocio.DepartamentoRegraNegocio novoDepartamento;

        public frmDepartamento()
        {
            InitializeComponent();
        }

        private void frmDepartamento_Load(object sender, EventArgs e)
        {
            PesquisarDepartamento();
        }

        public void PesquisarDepartamento() 
        {
            try
            {
                novoDepartamento = new RegraNegocio.DepartamentoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoDepartamento.PesquisarDepartamento();
                gdvDepartamento.DataSource = dadosTabela;

            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PesquisarDepartamento.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
