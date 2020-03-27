using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class ContaReceberAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public void CadastrarContaReceber(int idPagamentoVenda, decimal valoRecebido, decimal valorReceber, DateTime dataVencimento, int idUsuario, decimal multa, decimal juros, bool baixado)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("INSERT INTO CONTAS_A_RECEBER(ID_PAGTO_VENDA, VALOR_A_RECEBER, VALOR_RECEBIDO, DT_VECTO, ID_USUARIO, MULTA, JUROS, BAIXADO)VALUES(@ID_PAGTO_VENDA, @VALOR_A_RECEBER, @VALOR_RECEBIDO, @DT_VECTO, @ID_USUARIO, @MULTA, @JUROS, @BAIXADO)");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_PAGTO_VENDA", idPagamentoVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR_A_RECEBER", valorReceber));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR_RECEBIDO", valoRecebido));
                    comandoSql.Parameters.Add(new SqlParameter("@DT_VECTO", dataVencimento));
              
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@MULTA", multa));
                    comandoSql.Parameters.Add(new SqlParameter("@JUROS", juros));
                    comandoSql.Parameters.Add(new SqlParameter("@BAIXADO", baixado));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Cadastrar Conta Receber. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }
    }
}
