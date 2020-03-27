using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using AcessoDados;

namespace RegraNegocio
{
    public class PagamentoVendaAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        internal DataTable PesquisarNumCaixa_NumVenda(int numcaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM NUM_CAIXA WHERE NUM_CAIXA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar NumCaixa NumVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        internal void PesquisarNumCaixa_NumVenda(int numCaixa, int numVenda)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE NUM_CAIXA SET NUM_VENDA=@NUM_VENDA WHERE NUM_CAIXA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar NumCaixa NumVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        internal void FecharStatusCaixa(int numCaixa, bool statusCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE NUM_CAIXA SET STATUS_CAIXA=@STATUS_CAIXA WHERE NUM_CAIXA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@STATUS_CAIXA", statusCaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Fechar Status Caixa. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        internal void AbrirCaixa(int numCaixa, bool status)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE NUM_CAIXA SET STATUS_CAIXA=@STATUS_CAIXA WHERE NUM_CAIXA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@STATUS_CAIXA", status));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Abrir Caixa. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        internal DataTable PesquisarEstatusCaixa(int numcaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT STATUS_CAIXA FROM NUM_CAIXA WHERE NUM_CAIXA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisa rEstatus Caixa. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
