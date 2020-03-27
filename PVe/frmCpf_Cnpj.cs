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
    public partial class frmCpf_Cnpj : Form
    {
        RegraNegocio.TempRegraNegocios novoTemp;
        frmVenda frmVenda;

        public frmCpf_Cnpj(frmVenda frmV)
        {
            InitializeComponent();
            this.frmVenda = frmV;
        }

        private void txtCpf_KeyPress(object sender, KeyPressEventArgs e)
        {
            //metodo que aciona ao aperta a tecla enter sem precisar apertao o botao
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnCpfCliente_Click(sender, e);
            }
        }

        private void btnSalvaCpf_Click(object sender, EventArgs e)
        {

            novoTemp = new RegraNegocio.TempRegraNegocios();
            novoTemp.AlterarCpfCliente("", 0, "");

            frmVenda.PesquisaUltimoItem();
            CancelarVenda();
            frmVenda.LimpaCampos();
            this.Close();
            LimparCampo();
        }

        private void txtCpf_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                frmVenda.PesquisaUltimoItem();
                CancelarVenda();
                frmVenda.LimpaCampos();
            }

            if (e.KeyCode == Keys.F3)
            {
                Dispose();
                frmPlacaVeiculo fpv = new frmPlacaVeiculo(frmVenda);
                fpv.ShowDialog();
            }
        }

        private void btnCpfCliente_Click(object sender, EventArgs e)
        {
            if (rbCpf.Checked == true)
            {
                string valorCpf = txtCnpj.Text;
                int tipo = 1;
                string nome = "";

                valorCpf = valorCpf.Replace(" ", "");
                valorCpf = valorCpf.Replace(",", "");
                valorCpf = valorCpf.Replace("-", "");
                valorCpf = valorCpf.Replace(".", "");

                if (valorCpf != "")
                {
                    valorCpf = txtCnpj.Text;

                    if (RegraNegocio.TempRegraNegocios.IsCpf(valorCpf))
                    {
                        valorCpf = valorCpf.Replace(" ", "");
                        valorCpf = valorCpf.Replace(",", "");
                        valorCpf = valorCpf.Replace("-", "");
                        valorCpf = valorCpf.Replace(".", "");

                        if (valorCpf == "00000000000")
                        {
                            valorCpf = "";
                            tipo = 1;
                            novoTemp = new RegraNegocio.TempRegraNegocios();
                            novoTemp.AlterarCpfCliente(valorCpf, tipo, nome);
                            this.Close();
                        }
                        else
                        {
                            novoTemp = new RegraNegocio.TempRegraNegocios();
                            novoTemp.AlterarCpfCliente(valorCpf, tipo, nome);
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Atenção CPF " + valorCpf + " é Inválido.\nPor Favor Informe outro CPF Válido.", "Informação do CPF", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCnpj.Focus();
                    }
                }
                else
                {
                    novoTemp = new RegraNegocio.TempRegraNegocios();
                    novoTemp.AlterarCpfCliente(valorCpf, tipo, nome);
                    this.Close();
                }
            }
            else
            {
                if (RBCnpj.Checked == true)
                {
                    string valor = txtCnpj.Text;
                    valor = txtCnpj.Text;
                    valor = valor.Trim();
                    int tipo = 2;
                    string nome = "";

                    valor = valor.Replace("/", "");
                    valor = valor.Replace(",", "");
                    valor = valor.Replace("-", "");
                    valor = valor.Replace(" ", "");

                    if (valor != "")
                    {

                        if (RegraNegocio.TempRegraNegocios.IsCnpj(valor))
                        {
                            valor = valor.Trim();
                            valor = valor.Replace("/", "");
                            valor = valor.Replace(",", "");
                            valor = valor.Replace("-", "");
                            valor = valor.Replace(" ", "");

                            if (valor == "00000000000000")
                            {
                                valor = "";
                                novoTemp = new RegraNegocio.TempRegraNegocios();
                                novoTemp.AlterarCpfCliente(valor, tipo, nome);
                                this.Close();
                            }
                            else
                            {
                                novoTemp = new RegraNegocio.TempRegraNegocios();
                                novoTemp.AlterarCpfCliente(valor, tipo, nome);
                                this.Close();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Atenção CNPJ " + valor + " é Inválido.\nPor Favor Informe outro CNPJ Válido.", "Informação do CNPJ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtCnpj.Focus();
                        }
                    }
                    else
                    {
                        if (valor == "00000000000000")
                        {
                            valor = "";
                        }
                        else
                        {
                            novoTemp = new RegraNegocio.TempRegraNegocios();
                            novoTemp.AlterarCpfCliente(valor, tipo, nome);
                            this.Close();
                        }
                    }
                }
            }
        }

        private void frmCpf_Cnpj_Load(object sender, EventArgs e)
        {
            LimparCampo();

            if (rbCpf.Checked == true)
            {
                txtCnpj.Mask = "000-000-000-00";
            }
            else
            {
                txtCnpj.Mask = "00-000-000/0000-00";
            }
        }

        private void RBCnpj_CheckedChanged(object sender, EventArgs e)
        {
            if (RBCnpj.Checked == true)
            {
                txtCnpj.Mask = "00-000-000/0000-00";
                lblDescricao.Text = "CNPJ:";
                txtCnpj.Focus();
            }
            else
            {
                if (rbCpf.Checked == true)
                {
                    txtCnpj.Mask = "000-000-000-00";
                    lblDescricao.Text = "CPF:";
                    txtCnpj.Focus();
                }
            }
        }

        private void txtCpf_KeyUp_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F3)
            {
                Dispose();

                frmPlacaVeiculo fpv = new frmPlacaVeiculo(frmVenda);
                fpv.ShowDialog();

            }
        }

        private void txtCpf_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //metodo que aciona ao aperta a tecla enter sem precisar apertao o botao
            if ((Keys)e.KeyChar == Keys.Enter)
            {
                btnCpfCliente_Click(sender, e);
            }
        }

        public void LimparCampo()
        {
            txtCnpj.Text = "";
        }

        private void rbCpf_CheckedChanged(object sender, EventArgs e)
        {
            txtCnpj.Mask = "000-000-000-00";
            lblDescricao.Text = "CPF:";
        }

        public void CancelarVenda()
        {
            frmVenda.CancelaVenda();
            frmVenda.AtualizarGridAberto();
            frmVenda.LimpaCampos();
        }
    }
}
