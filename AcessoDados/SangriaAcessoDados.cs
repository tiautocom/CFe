using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AcessoDados
{
    public class SangriaAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public DataTable PesquisarSangriaCaixa(int idUsuario, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT TIPO, SUM(VALOR) AS TOTAL FROM SANGRIA WHERE SANGRIA.ID_USUARIO=@ID_USUARIO AND SANGRIA.NUM_CAIXA=@NUM_CAIXA AND SANGRIA.FECHADO=0 GROUP BY TIPO ORDER BY TIPO ASC");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Sangria Caixa. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarSomaTotalDia(int idUsuario, int numCaixa, bool fechado, bool tipo)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT * FROM SANGRIA WHERE SANGRIA.NUM_CAIXA=@NUM_CAIXA AND SANGRIA.ID_USUARIO=@ID_USUARIO AND SANGRIA.TIPO=@TIPO AND SANGRIA.FECHADO=@FECHADO");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@TIPO", tipo));
                    comandoSql.Parameters.Add(new SqlParameter("@FECHADO", fechado));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Soma Total Dia Sangria. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void FecharSangria(int idUsuario, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE SANGRIA SET FECHADO=1 WHERE NUM_CAIXA=@NUM_CAIXA AND ID_USUARIO=@ID_USUARIO AND FECHADO=0");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Fechar Sangria. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
