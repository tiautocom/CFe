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
    public partial class frmPlacaVeiculo : Form
    {
        RegraNegocio.PlacaRegraNegocio novaPlaca;
        frmVenda frmVenda;

        public frmPlacaVeiculo(frmVenda pfrv)
        {
            this.frmVenda = pfrv;
            InitializeComponent();
        }

        private void txtPlaca_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string placa = "";
                placa = txtPlaca.Text;
                placa = placa.Trim();

                placa = placa.Replace(" ", "");

                if (placa != "")
                {
                    txtKm.Focus();
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtKm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string placa = "";
                placa.Trim();
                placa = txtPlaca.Text;
                placa = placa.Replace(" ", "");

                if (placa != "" && placa != "")
                {
                    Inserirplaca();
                    this.Close();
                }
                else
                {
                    txtPlaca.Focus();
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void Inserirplaca()
        {
            try
            {
                int idVenda = frmVenda.numCupom;

                novaPlaca = new RegraNegocio.PlacaRegraNegocio();
                novaPlaca.InserirPlaca(idVenda, txtPlaca.Text, txtKm.Text, DateTime.Now.Date.ToShortDateString());
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Inserirplaca.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtKm_TextChanged(object sender, EventArgs e)
        {
            txtKm.CharacterCasing = CharacterCasing.Lower;
        }

        private void frmPlacaVeiculo_Load(object sender, EventArgs e)
        {

        }

    }
}
