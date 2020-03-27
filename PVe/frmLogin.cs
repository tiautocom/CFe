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
    public partial class frmLogin : Form
    {
        RegraNegocio.UsuarioRegraNegocio novoUsuario;
        RegraNegocio.ParametroRegraNegocio novoParametro;
        RegraNegocio.NumCaixaRegraNegocios novoCaixa;

        frmVenda frmvenda;
        bool status;
        string status_;
        int caixaAberto, caixaUsuario = 0;

        public frmLogin(frmVenda fv)
        {
            InitializeComponent();
            this.frmvenda = fv;
        }

        public void Logar()
        {
            try
            {
                if (txtLogin.Text != "")
                {
                    novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                    DataTable dadosTabela = new DataTable();
                    dadosTabela = novoUsuario.PesquisaLoginUsuario(txtLogin.Text);

                    caixaAberto = frmvenda.numcaixa;

                    if (dadosTabela.Rows.Count > 0)
                    {
                        caixaUsuario = Convert.ToInt32(dadosTabela.Rows[0]["NUM_CAIXA"].ToString());

                        if (caixaUsuario == caixaAberto)
                        {
                            AbrirCaixa();
                            frmvenda.AlterarStatusAbertura();

                            if (frmvenda.statusVenda == true)
                            {
                                novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                                novoUsuario.AlteraStatusUsuarioAberto(txtLogin.Text);

                                frmvenda.operadorAtuante = dadosTabela.Rows[0]["NOME"].ToString();
                                frmvenda.periodoAtuante_ = dadosTabela.Rows[0]["PERIODO"].ToString();

                                // AbrirCaixa();
                                // frmvenda.AlterarStatusAbertura();

                                frmvenda.DesbloquearBotoesTelaVenda();
                                frmvenda.AtualizarGridAberto();
                                frmvenda.LoadTela();
                                frmvenda.Refresh();
                                frmvenda.abrirCaixa = true;
                                this.Close();
                            }
                            else
                            {
                                MessageBox.Show("Caixa Nº: " + caixaAberto + "está Fechado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtLogin.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Verifique se Usuário está Autorizado para Logar com Caixa Nº:" + caixaAberto + ", ou Entre em Contado com  Administrado.", "Atenção");
                            this.Close();
                            frmvenda.abrirCaixa = false;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Login está Incorreto para Realizar Operação Desejado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtLogin.Focus();
                    }
                }
                else
                {
                    txtLogin.Focus();
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Error no Método Logar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void AbrirCaixa()
        {
            try
            {
                bool status = true;
                novoCaixa = new RegraNegocio.NumCaixaRegraNegocios();
                novoCaixa.AbrirCaixa(status, frmvenda.numcaixa);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void txtLogin_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Logar();
            }

            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("Realmente Deseja Sair do Sistema.", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    this.Close();
                    frmvenda.Close();
                }
            }
        }

        private void Fechar()
        {
            try
            {
                novoUsuario = new RegraNegocio.UsuarioRegraNegocio();
                novoUsuario.AlteraStatusUsuarioFechado(frmvenda.idUsuario);

                frmvenda.AtualizarGridAberto();
                frmvenda.Refresh();
                this.Close();
                frmvenda.AtualizarGridAberto();
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método Fechar.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            PesquisaUsuarioLogado();
        }

        public void PesquisaUsuarioLogado()
        {
            status_ = frmvenda.statusLogin;

            if (status_ == "ABERTO")
            {
                lblStatusUsuario.Text = "ABERTO";
            }
            else
            {
                if (status_ == "FECHADO" || status_ == "...")
                {
                    lblStatusUsuario.Text = "FECHADO";
                }
            }
        }
    }
}