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
    public partial class frmCliente : Form
    {
        RegraNegocio.ClienteRegraNegocio novoCliente;
        frmFechamentoVenda frmFechamentoVenda;
        frmVenda frmVenda;
        string impressora = "";

        public decimal LimiteCliente, gastoCliente, somaTotalCompra, gastoAtual = 0;
        public int idCliente = 0;

        public frmCliente(frmFechamentoVenda fVenda)
        {
            InitializeComponent();
            this.frmFechamentoVenda = fVenda;
        }

        private void frmCliente_Load(object sender, EventArgs e)
        {
            txtPesquisa.Focus();
            ListaCliente();
            txtPesquisa.Focus();
            ContadorCliente();
        }

        public void LimparCampos()
        {
            txtPesquisa.Text = "";
            txtPesquisa.Focus();
        }


        public void ContadorCliente()
        {
            try
            {
                novoCliente = new RegraNegocio.ClienteRegraNegocio();
                DataTable dadosTabelaCliente = new DataTable();
                dadosTabelaCliente = novoCliente.ContadorCliente();

                string contador = dadosTabelaCliente.Rows[0]["NOME"].ToString();
                lblTotalCliente.Text = contador.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ListaCliente()
        {
            try
            {
                novoCliente = new RegraNegocio.ClienteRegraNegocio();
                DataTable dadosTabelaCliente = new DataTable();
                dadosTabelaCliente = novoCliente.ListaCliente();

                gdvCliente.DataSource = dadosTabelaCliente;
                EstiloCoresLinha();
                txtPesquisa.Focus();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void EstiloCoresLinha()
        {
            try
            {
                for (int i = 0; i < gdvCliente.Rows.Count; i += 2)
                {
                    gdvCliente.Rows[i].DefaultCellStyle.BackColor = Color.Honeydew;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método EstiloCoresLinha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtPesquisa_TextChanged(object sender, EventArgs e)
        {
            txtPesquisa.Focus();
        }

        private void gdvCliente_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string portaComImpressora = "";
                int idRetorno = 0;

                idCliente = frmFechamentoVenda.idCliente = Convert.ToInt32(gdvCliente.Rows[e.RowIndex].Cells["CLIENTE_ID"].Value);
                this.Close();

                somaTotalCompra = frmFechamentoVenda.somaTotalCompra;

                //metodo para buscar limite do cliente.....................................................................................
                PesquisarLimiteCliente();

                if ((LimiteCliente != null) && (LimiteCliente > 0))
                {
                    gastoCliente = (gastoCliente + somaTotalCompra);

                    //if (gastoCliente <= LimiteCliente)
                    //{
                    int n = Convert.ToInt32(gdvCliente.CurrentRow.Index);

                    if (n >= 0)
                    {
                        frmFechamentoVenda.idCliente = Convert.ToInt32(gdvCliente.Rows[n].Cells["CLIENTE_ID"].Value);

                        //frmFechamentoVenda.FecharVenda();
                        //frmFechamentoVenda.VendaAberto();

                        this.Close();

                        //metodo pra Alterar gasto NA TABELA VENDA realizado pelo cliente ................................................................
                        novoCliente = new RegraNegocio.ClienteRegraNegocio();
                        novoCliente.AtualizarGastoCliente(idCliente, gastoCliente);
                        frmFechamentoVenda.retornoVendaId = true;
                    }
                    else
                    {
                        MessageBox.Show("Informe o Cliente Realização do Relatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Cliente não Contem Limite Informado.\nPor Favor Inserir um Valor de Limite para Cliente.", "Atenção");
                    frmFechamentoVenda.Close();
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtPesquisa_TextChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (rbNome.Checked == true)
                {
                    novoCliente = new RegraNegocio.ClienteRegraNegocio();
                    DataTable dadosTabelaCliente = new DataTable();
                    dadosTabelaCliente = novoCliente.PesquisarClienteNome(txtPesquisa.Text);
                    gdvCliente.DataSource = dadosTabelaCliente;
                    EstiloCoresLinha();
                }
                else
                {
                    if (rbCpf.Checked == true)
                    {
                        novoCliente = new RegraNegocio.ClienteRegraNegocio();
                        DataTable dadosTabelaCliente = new DataTable();
                        dadosTabelaCliente = novoCliente.PesquisarClienteCPF_CNPJ(txtPesquisa.Text);
                        gdvCliente.DataSource = dadosTabelaCliente;
                        EstiloCoresLinha();
                    }
                    else
                    {
                        if (rbCnpj.Checked == true)
                        {
                            novoCliente = new RegraNegocio.ClienteRegraNegocio();
                            DataTable dadosTabelaCliente = new DataTable();
                            dadosTabelaCliente = novoCliente.PesquisarClienteRG_IE(txtPesquisa.Text);
                            gdvCliente.DataSource = dadosTabelaCliente;
                            EstiloCoresLinha();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void rbNome_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNome.Checked == false)
            {
                LimparCampos();
            }
        }

        private void rbCpf_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCpf.Checked == false)
            {
                LimparCampos();
            }
        }

        private void rbCnpj_CheckedChanged(object sender, EventArgs e)
        {
            if (rbCnpj.Checked == false)
            {
                LimparCampos();
            }
        }

        private void txtPesquisa_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                gdvCliente.Focus();
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }

            if (e.KeyCode == Keys.Down)
            {
                gdvCliente.Focus();
            }
        }


        private void gdvCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                int n = Convert.ToInt32(gdvCliente.CurrentRow.Index);

                if (n >= 0)
                {
                    frmFechamentoVenda.idCliente = Convert.ToInt32(gdvCliente.Rows[n].Cells["CLIENTE_ID"].Value);
                    frmFechamentoVenda.btnFecharVenda_Click(sender, e);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Informe o Cliente Realização do Relatório.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void PesquisarLimiteCliente()
        {
            try
            {
                novoCliente = new RegraNegocio.ClienteRegraNegocio();
                DataTable dadosTabelaCliente = new DataTable();
                dadosTabelaCliente = novoCliente.PesquisarLimiteCliente(idCliente);

                if (dadosTabelaCliente.Rows.Count > 0)
                {
                    try
                    {
                        LimiteCliente = Convert.ToDecimal(dadosTabelaCliente.Rows[0]["LIMITE"].ToString());
                        gastoCliente = Convert.ToDecimal(dadosTabelaCliente.Rows[0]["GASTO"].ToString());
                        gastoAtual = Convert.ToDecimal(dadosTabelaCliente.Rows[0]["GASTO"].ToString());
                    }
                    catch
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
