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
    public partial class frmOperador : Form
    {
        RegraNegocio.OperadorRegraNegocio novoOperador;

        public frmOperador()
        {
            InitializeComponent();
        }

        private void frmOperador_Load(object sender, EventArgs e)
        {
            PesquisarGridOperador();
        }


        public void PesquisarGridOperador() 
        {
            try
            {
                novoOperador = new RegraNegocio.OperadorRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoOperador.PesquisarGridOperador();
                gdvOperador.DataSource = dadosTabela;
            }
        
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
