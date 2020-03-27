using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
   public class PlacaAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public void InserirPlaca(int idVenda, string placa, string km, string data)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("INSERT INTO PLACA (ID_VENDA, PLACA, KM) VALUES (@ID_VENDA, @PLACA, @KM)");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_VENDA", idVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@PLACA", placa));
                    comandoSql.Parameters.Add(new SqlParameter("@KM", km));
                   // comandoSql.Parameters.Add(new SqlParameter("@DATA", data));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método InserirPlaca. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarPlaca(string numCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM PLACA where PLACA.ID_VENDA = @ID_VENDA");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_VENDA", numCupom));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarPlaca. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
