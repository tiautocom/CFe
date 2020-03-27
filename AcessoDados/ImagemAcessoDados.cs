using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
   public class ImagemAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public void LimparImagem()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("DELETE FROM LOGO");
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método LimparImagem. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void SalvarImagem(byte[] foto)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("insert into LOGO (LOGO) VALUES (@LOGO)");

                    //Relaciona cada valor com seu respectivo parâmetro.

                    comandoSql.Parameters.Add(new SqlParameter("@LOGO", foto));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
