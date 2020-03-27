using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class TipoPagamentoAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public DataTable PesquisaTipo()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from TIPO_PAGTO order by TIPO_PAGTO_ID asc");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaTipo. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaTipoPagamento(int idTipoPagamento)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from TIPO_PAGTO where TIPO_PAGTO.TIPO_PAGTO_ID = @TIPO_PAGTO_ID order by TIPO_PAGTO_ID asc");

                    comandoSql.Parameters.Add(new SqlParameter("@TIPO_PAGTO_ID", idTipoPagamento));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaTipoPagamento. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaTipoPagamento(string codTipoPagamento)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from TIPO_PAGTO where TIPO_PAGTO.TIPO_PAGTO_ID = @TIPO_PAGTO_ID order by TIPO_PAGTO_ID asc");

                    comandoSql.Parameters.Add(new SqlParameter("@TIPO_PAGTO_ID", codTipoPagamento));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaTipoPagamento. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
