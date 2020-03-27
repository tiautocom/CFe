using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class EstoqueIncialAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public void CadastraEstoqueIncialMes(decimal qtdeEstoqueInicial, DateTime data, int idUsuario, int numeroSetor)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("INSERT INTO ESTOQUE_INICIAL(ESTOQUE_INICIAL, DATA, ID_USUARIO, ID_SETOR) VALUES (@ESTOQUE_INICIAL, @DATA, @ID_USUARIO, @ID_SETOR) ");

                    comandoSql.Parameters.Add(new SqlParameter("@ESTOQUE_INICIAL", qtdeEstoqueInicial));
                    comandoSql.Parameters.Add(new SqlParameter("@DATA", data));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_SETOR", numeroSetor));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Metodo Cadastrar Estoque Incia do lMes. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }
    }
}
