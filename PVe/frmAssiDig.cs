using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace PVe
{
    public partial class frmAssiDig : Form
    {
        string pathAssDigitalXML = Path.GetDirectoryName(Application.ExecutablePath) + "\\Banco\\ASS_DIG.xml";
        string pathAssCodAtivacaoXML = Path.GetDirectoryName(Application.ExecutablePath) + "\\Banco\\COD_ATIVACAO.xml";

        public frmAssiDig()
        {
            InitializeComponent();
        }

        private void btnAssiDigital_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirmação da Assinatura Digital", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
              
                    string numAssinaturaDigital = txtAssinaturaDigital.Text;
                    XmlTextWriter writer = new XmlTextWriter(pathAssDigitalXML, null);

                    //inicia o documento xml
                    writer.WriteStartDocument();
                    //escreve o elmento raiz
                    writer.WriteStartElement("ASS_DIG");
                    //Escreve os sub-elementos
                    writer.WriteElementString("numAssinatura", numAssinaturaDigital.ToString());

                    writer.Close();
                
                MessageBox.Show("Assinatura Digital em XML gerado com sucesso.");
                LimparCampo();
            }
            else
            {
                MessageBox.Show("");
            }
        }

        public void LimparCampo() 
        {
            txtAssinaturaDigital.Text = "";
        }

        private void btnCodAtivacao_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Confirmação Código de Ativação Sat.", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                if (txtCodAtivavao.Text != "")
                {
                    string codAtivaocao = txtCodAtivavao.Text;

                    XmlTextWriter writer = new XmlTextWriter(pathAssCodAtivacaoXML, null);

                    //inicia o documento xml
                    writer.WriteStartDocument();
                    //escreve o elmento raiz
                    writer.WriteStartElement("CodAtivacao");
                    //Escreve os sub-elementos
                    writer.WriteElementString("COD_ATIVACAO", codAtivaocao);

                    // encerra o elemento raiz
                    writer.WriteEndElement();
                    //Escreve o XML para o arquivo e fecha o objeto escritor
                    writer.Close();

                    MessageBox.Show("Código de Ativação Gerado com Sucesso.");
                    LimparCampo();
                }
                else
                {
                    MessageBox.Show("Campo está Vazio.", "Atenção");
                }
            }
            else
            {
                MessageBox.Show("");
            }
        }
    }
}
