using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class ParametroAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public void SalvarParametro(bool abrirCupom, int numVenda)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("insert into PARAMETRO (STATUS, NUM_CUPOM) values (@STATUS, @NUM_CUPOM)");

                    comandoSql.Parameters.Add(new SqlParameter("@STATUS", numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", numVenda));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método SalvarParametro. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void InseriParametroNumFecharVenda(int novoNum, bool FecharCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("insert into PARAMETRO (STATUS, NUM_CUPOM) values (@STATUS, @NUM_CUPOM)");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", novoNum));
                    comandoSql.Parameters.Add(new SqlParameter("@STATUS", FecharCupom));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método InseriParametroNumFecharVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarUltimaVenda()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM PARAMETRO");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarUltimaVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void InserirPrimeiroVenda()
        {
            int venda = 1;

            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("INSERT INTO parametro (NUM_CUPOM) VALUES (@NUM_CUPOM)");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", venda));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método InserirPrimeiroVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void InserirParametroEntrada(int nuVenda, bool baixado)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("INSERT INTO parametro (NUM_CUPOM, STATUS) VALUES (@NUM_CUPOM, @STATUS)");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", nuVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@STATUS", baixado));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método InserirParametroEntrada. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AlterarParametroNumFecharVenda(int novoNum, bool FecharCupom, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {

                    conexao.Open();

                    sql.Append("UPDATE PARAMETRO SET NUM_CUPOM=@NUM_CUPOM, STATUS=@STATUS WHERE NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", novoNum));
                    comandoSql.Parameters.Add(new SqlParameter("@STATUS", FecharCupom));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlterarParametroNumFecharVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaCodEtiqueta()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM PARAMETRO");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaCodEtiqueta. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaParametroEtiqueta()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from PARAMETRO");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaParametroEtiqueta. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        //- Os parâmetros usados na venda de um item são:

        //Codigo: STRING até 13 caracteres com o código do produto.

        //Descricao: STRING até 29 caracteres com a descrição do produto.

        //Aliquota: STRING com o valor ou o índice da alíquota tributária. Se for o valor deve ser informado com o tamanho de 4 caracteres ou 5 com a vírgula. Se for o índice da alíquota deve ser 2 caracteres. Ex. (18,00 para o valor ou 05 para o índice).

        //TipoQuantidade: 1 (um) caracter indicando o tipo de quantidade. I - Inteira e F - Fracionária. 

        //Quantidade: STRING com até 4 dígitos para quantidade inteira e 7 dígitos para quantidade fracionária. Na quantidade fracionária são 3 casas decimais. 

        //CasasDecimais: INTEIRO indicando o número de casas decimais para o valor unitário (2 ou 3).

        //ValorUnitario: STRING até 8 dígitos para valor unitário.

        //TipoDesconto: 1 (um) caracter indicando a forma do desconto. '$' desconto por valor e '%' desconto percentual.

        //ValorDesconto: String com até 8 dígitos para desconto por valor (2 casas decimais) e 4 dígitos para desconto percentual. 

        public void AlterarParametro(string crt, int numCupom, string codEtiqueta, string codEtiquetaBalanca, string status, string bairro, string cep, string cidade, string cnpj, string endereco, string ie, string im ,string fantasia, string numero, string razaoSocial, string telefone, string uf, decimal aliquota, string impressora, Boolean autorizarPlaca, decimal valorVenda, int numCaixa, string msg, string portaCom, int bondRonte, string portaImpressora, bool autorizarTexto, bool cupomImagem, int qtdeCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {

                    conexao.Open();

                    sql.Append("UPDATE PARAMETRO SET CRT=@CRT,ETIQUETA_BALANCA=@ETIQUETA_BALANCA,COD_ETIQUETA=@COD_ETIQUETA,RAZAO_SOCIAL=@RAZAO_SOCIAL,NOME_FANTASIA=@NOME_FANTASIA, ENDERECO_EMPRESA=@ENDERECO_EMPRESA,NUMERO=@NUMERO,BAIRRO=@BAIRRO,CEP=@CEP,CIDADE=@CIDADE,UF=@UF,TELEFONE=@TELEFONE,IE=@IE, IM=@IM, CNPJ=@CNPJ,ALIQUOTA_DIA=@ALIQUOTA_DIA,IMPRESSORA=@IMPRESSORA,PLACA=@PLACA, LIMITE_VENDA=@LIMITE_VENDA, NUM_CAIXA=@NUM_CAIXA, MSG=@MSG,PORTA_COM=@PORTA_COM,BOUD_RATE=@BOUD_RATE,PORTA_COM_IMPRESSORA=@PORTA_COM_IMPRESSORA,HOMOLOGACAO_TESTE=@HOMOLOGACAO_TESTE,CUPOM_IMAGEM=@CUPOM_IMAGEM, QTDE_CUPOM=@QTDE_CUPOM");

                    comandoSql.Parameters.Add(new SqlParameter("@CRT", crt));
                    comandoSql.Parameters.Add(new SqlParameter("@ETIQUETA_BALANCA", codEtiquetaBalanca));
                    comandoSql.Parameters.Add(new SqlParameter("@COD_ETIQUETA", codEtiqueta));
                    comandoSql.Parameters.Add(new SqlParameter("@RAZAO_SOCIAL", razaoSocial));
                    comandoSql.Parameters.Add(new SqlParameter("@NOME_FANTASIA", fantasia));
                    comandoSql.Parameters.Add(new SqlParameter("@ENDERECO_EMPRESA", endereco));
                    comandoSql.Parameters.Add(new SqlParameter("@NUMERO", numero));
                    comandoSql.Parameters.Add(new SqlParameter("@BAIRRO", bairro));
                    comandoSql.Parameters.Add(new SqlParameter("@CEP", cep));
                    comandoSql.Parameters.Add(new SqlParameter("@CIDADE", cidade));
                    comandoSql.Parameters.Add(new SqlParameter("@UF", uf));
                    comandoSql.Parameters.Add(new SqlParameter("@TELEFONE", telefone));
                    comandoSql.Parameters.Add(new SqlParameter("@IE", ie));
                    comandoSql.Parameters.Add(new SqlParameter("@IM", im));
                    comandoSql.Parameters.Add(new SqlParameter("@CNPJ", cnpj));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", numCupom));
                    comandoSql.Parameters.Add(new SqlParameter("@ALIQUOTA_DIA", aliquota));
                    comandoSql.Parameters.Add(new SqlParameter("@IMPRESSORA", impressora));
                    comandoSql.Parameters.Add(new SqlParameter("@PLACA", autorizarPlaca));
                    comandoSql.Parameters.Add(new SqlParameter("@LIMITE_VENDA", valorVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@MSG", msg));
                    comandoSql.Parameters.Add(new SqlParameter("@CUPOM_IMAGEM", cupomImagem));
                    comandoSql.Parameters.Add(new SqlParameter("@QTDE_CUPOM", qtdeCupom));
                    comandoSql.Parameters.Add(new SqlParameter("@PORTA_COM", portaCom));
                    comandoSql.Parameters.Add(new SqlParameter("@BOUD_RATE", bondRonte));
                    comandoSql.Parameters.Add(new SqlParameter("@PORTA_COM_IMPRESSORA", portaImpressora));
                    comandoSql.Parameters.Add(new SqlParameter("@HOMOLOGACAO_TESTE", autorizarTexto));


                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlterarParametro. Caso o problema persista, entre em contato com o Administrador do Sistema."); ;
            }
        }

        public DataTable PesquisaIdParametro(int numCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM PARAMETRO");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", numCupom));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisa Id Parametro. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AlterarStatusFechar(int _numVenda, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {

                    conexao.Open();

                    sql.Append("update PARAMETRO set STATUS = 0 where PARAMETRO.NUM_CUPOM = @NUM_CUPOM AND NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", _numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlterarStatusFechar. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AlterarAbertuara(int numCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("update PARAMETRO set STATUS = 1 where PARAMETRO.NUM_CUPOM = @NUM_CUPOM ");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", numCupom));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlterarAbertuara. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AlteraNumVenda(int numVenda)
        {

            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE PARAMETRO SET NUM_CUPOM=@NUM_CUPOM");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", numVenda));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());

                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlteraNumVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarPortaBalanca()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM PARAMETRO");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Porta Balanca. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarImpressaoAutmoatica()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT IMPRESSAO_DIGITAL FROM PARAMETRO");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Impressao Autmoatica. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}