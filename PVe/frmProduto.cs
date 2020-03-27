using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace PVe
{
    public partial class frmProduto : Form
    {
        frmVenda frmVendas;
        RegraNegocio.ProdutoRegraNegocio novoProduto;
        public int qtdeProduto = 0;

        public string codigoBarra;
        public string nomeProduto;

        public frmProduto(frmVenda frmvenda)
        {
            InitializeComponent();
            this.frmVendas = frmvenda;
            gdvProduto.AutoGenerateColumns = false;
        }

        private void frmProduto_Load(object sender, EventArgs e)
        {
            PesquisarAll();
            EstiloCoresLinha();
            PesquisarTotalProduto();
        }

        public static string GetStringNoAccents(string str)
        {
            /** Troca os caracteres acentuados por não acentuados **/
            string[] acentos = new string[] { "ç", "Ç", "á", "é", "í", "ó", "ú", "ý", "Á", "É", "Í", "Ó", "Ú", "Ý", "à", "è", "ì", "ò", "ù", "À", "È", "Ì", "Ò", "Ù", "ã", "õ", "ñ", "ä", "ë", "ï", "ö", "ü", "ÿ", "Ä", "Ë", "Ï", "Ö", "Ü", "Ã", "Õ", "Ñ", "â", "ê", "î", "ô", "û", "Â", "Ê", "Î", "Ô", "Û" };
            string[] semAcento = new string[] { "c", "C", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "Y", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U", "a", "o", "n", "a", "e", "i", "o", "u", "y", "A", "E", "I", "O", "U", "A", "O", "N", "a", "e", "i", "o", "u", "A", "E", "I", "O", "U" };
            for (int i = 0; i < acentos.Length; i++)
            {
                str = str.Replace(acentos[i], semAcento[i]);
            }
            /** Troca os caracteres especiais da string por “” **/
            string[] caracteresEspeciais = { "\\.", ",", "-", ":", "\\(", "\\)", "ª", "\\|", "\\\\", "°" };
            for (int i = 0; i < caracteresEspeciais.Length; i++)
            {
                str = str.Replace(caracteresEspeciais[i], "");
            }
            /** Troca os espaços no início por “” **/
            str = str.Replace("^\\s+", "");
            /** Troca os espaços no início por “” **/
            str = str.Replace("\\s+$", "");
            /** Troca os espaços duplicados, tabulações e etc por  ” ” **/
            str = str.Replace("\\s+", " ");
            return str;
        }


        /// <summary>
        /// Função para remover acentos da string
        /// </summary>
        /// <param name="valor">String para receber tratamento</param>
        /// <returns>String tratada</returns>
        public static string RemoverAcentos(string valor)
        {
            valor = Regex.Replace(valor, "[ÁÀÂÃ]", "A");
            valor = Regex.Replace(valor, "[ÉÈÊ]", "E");
            valor = Regex.Replace(valor, "[Í]", "I");
            valor = Regex.Replace(valor, "[ÓÒÔÕ]", "O");
            valor = Regex.Replace(valor, "[ÚÙÛÜ]", "U");
            valor = Regex.Replace(valor, "[Ç]", "C");
            valor = Regex.Replace(valor, "[áàâã]", "a");
            valor = Regex.Replace(valor, "[éèê]", "e");
            valor = Regex.Replace(valor, "[í]", "i");
            valor = Regex.Replace(valor, "[óòôõ]", "o");
            valor = Regex.Replace(valor, "[úùûü]", "u");
            valor = Regex.Replace(valor, "[ç]", "c");
            return valor;
        }

        public void PesquisarTotalProduto()
        {
            try
            {
                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabelaProduto = new DataTable();
                dadosTabelaProduto = novoProduto.PesquisarTotalProduto();

                qtdeProduto = Convert.ToInt32(dadosTabelaProduto.Rows[0]["QTDE"].ToString());
                lblTotalProduto.Text = qtdeProduto.ToString();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void FocuInicial()
        {
            txtDescricao.Focus();
        }

        public void EstiloCoresLinha()
        {
            try
            {
                for (int i = 0; i < gdvProduto.Rows.Count; i += 2)
                {
                    gdvProduto.Rows[i].DefaultCellStyle.BackColor = Color.Honeydew;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método EstiloCoresLinha.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void PesquisarAll()
        {
            try
            {
                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarAll();
                gdvProduto.DataSource = dadosTabela;
                txtDescricao.Focus();
            }
            catch (Exception)
            {
                MessageBox.Show("Error no Método PesquisarAll.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gdvProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                txtDescricao.Focus();
            }
            else
            {
                if (e.KeyCode == Keys.C)
                {
                    txtCodigoBarra.Focus();
                }
            }

            if (e.KeyCode == Keys.Escape)
            {
                txtDescricao.Focus();
            }

        }

        private void txtDescricao_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
                txtCodigoBarra.Text = "";
                frmVendas._idProduto = 0;
            }

            if (e.KeyCode == Keys.Enter)
            {
                gdvProduto.Focus();
            }

            if (e.KeyCode == Keys.Down)
            {
                gdvProduto.Focus();
            }
        }

        public void LimparCampos()
        {
            txtCodigoBarra.Text = "";
            txtDescricao.Text = "";
        }

        private void txtCodigoBarra_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisaCodigoBarra(txtCodigoBarra.Text);

                if (dadosTabela.Rows.Count > 0)
                {
                    gdvProduto.DataSource = dadosTabela;
                    EstiloCoresLinha();
                }
                else
                {
                    if (MessageBox.Show("Não contém Produto cadastrado com esse Código de Barra.", "Inrformação", MessageBoxButtons.OK, MessageBoxIcon.Information) == DialogResult.OK)
                    {
                        LimparCampos();
                        txtCodigoBarra.Focus();
                    }
                }

                if (e.KeyCode == Keys.Escape)
                {
                    this.Close();
                    frmVendas._idProduto = 0;
                }

                if (e.KeyCode == Keys.Down)
                {
                    gdvProduto.Focus();
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Error.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void gdvProduto_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                frmVendas._idProduto = Convert.ToInt32(gdvProduto.Rows[e.RowIndex].Cells["COD_INT"].Value);
                this.Close();
                this.Refresh();
            }
            catch (Exception)
            {
                MessageBox.Show("Error.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void txtDescricao_TextChanged(object sender, EventArgs e)
        {
            try
            {
                novoProduto = new RegraNegocio.ProdutoRegraNegocio();
                DataTable dadosTabela = new DataTable();
                string descricao = txtDescricao.Text;

                dadosTabela = novoProduto.PesquisarProdutoNome(descricao);

                if (dadosTabela.Rows.Count > 0)
                {
                    gdvProduto.DataSource = dadosTabela;
                    EstiloCoresLinha();
                }
                else
                {
                    gdvProduto.DataSource = null;
                }
            }

            catch (Exception)
            {
                MessageBox.Show("Error.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void frmProduto_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gdvProduto_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (gdvProduto.Rows.Count > 0)
                {
                    int n = Convert.ToInt32(gdvProduto.CurrentRow.Index);
                    frmVendas._idProduto = Convert.ToInt32(gdvProduto.Rows[n].Cells["COD_INT"].Value.ToString());
                    this.Close();
                    this.Refresh();
                }
                else
                {
                    txtDescricao.Focus();
                }
            }
        }

        private void txtCodigoBarra_Layout(object sender, LayoutEventArgs e)
        {

        }
    }
}
