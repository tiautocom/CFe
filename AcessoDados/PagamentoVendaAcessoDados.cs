using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class PagamentoVendaAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public DataTable PesquisarTipoPagamentoVenda(int numCupom, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select TIPO_PAGAMENTO, VALOR, ID_CLIENTE, TROCO from PAGAMENTO_VENDA where PAGAMENTO_VENDA.NUM_CUPOM=@NUM_CUPOM AND NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", numCupom));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AtualizarGridVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable LeituraX(int numcaixa, int idUsuario, string data, string trib)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT ALIQUOTA, SUM(TOTAL) AS TOTAL FROM VENDA JOIN PAGAMENTO_VENDA ON VENDA.NUM_VENDA=PAGAMENTO_VENDA.NUM_CUPOM WHERE VENDA.NUM_CAIXA=@NUM_CAIXA AND VENDA.ID_USUARIO=@ID_USUARIO AND VENDA.BAIXADO=0 AND VENDA.ALIQUOTA=@ALIQUOTA GROUP BY ALIQUOTA");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@ALIQUOTA", trib));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Leitura X. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
