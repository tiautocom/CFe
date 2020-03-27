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
    public partial class frmTrocarLogo : Form
    {
        RegraNegocio.UsuarioRegraNegocio novoUsuario;
        string senha = "";
        frmVenda frmVenda;

        public frmTrocarLogo(frmVenda venda)
        {
            InitializeComponent();
            this.frmVenda = venda;
        }

        private void frmTrocarLogo_Load(object sender, EventArgs e)
        {
            txtLogin.Focus();
        }

        public void PesquisarLoginAltorizado()
        {
            try
            {
                senha = txtLogin.Text;
                novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                DataTable dadosTabelaUsuario = new DataTable();
                dadosTabelaUsuario = novoUsuario.PesquisarLoginAltorizado(senha);

                if (dadosTabelaUsuario.Rows.Count > 0 && senha != "")
                {
                    senha = dadosTabelaUsuario.Rows[0]["SENHA"].ToString();
                    senha = senha.Replace(" ", "");

                    frmVenda.LimpaCampos();
                    frmVenda.TrocarLogo();
                    frmVenda.LimpaCampos();
                    //   MessageBox.Show("Imgem do logo Realizado com Sucesso.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Login Incorreto, ou não tem Autorização para Realizar essa Operação.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LimparCampos();
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLogar_Click(object sender, EventArgs e)
        {
            PesquisarLoginAltorizado();
        }

        public void LimparCampos()
        {
            txtLogin.Text = "";
            txtLogin.Focus();
        }

        private void txtLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            //metodo que aciona ao aperta a tecla enter sem precisar apertao o botao
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnLogar_Click(sender, e);
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
