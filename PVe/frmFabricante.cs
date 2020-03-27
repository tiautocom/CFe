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
    public partial class frmFabricante : Form
    {
        RegraNegocio.FabricanteRegraNegocio novoFabricante;

        public frmFabricante()
        {
            InitializeComponent();
        }

        private void frmFabricante_Load(object sender, EventArgs e)
        {
            PesquisarFabricante();
        }


        public void PesquisarFabricante() 
        {
            try
            {
                novoFabricante = new RegraNegocio.FabricanteRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoFabricante.PesquisarFabricante();
                gdvFabricante.DataSource = dadosTabela;
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PesquisarFabricante.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
