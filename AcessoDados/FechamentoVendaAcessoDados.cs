using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class FechamentoVendaAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public void SalvarEntrada(int idCliente, int idTipoPagamento, decimal valor, string cnpj, int _numVenda, bool baixado, DateTime data, decimal troco, int idUsuario, bool fechado, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("insert into PAGAMENTO_VENDA (ID_CLIENTE, TIPO_PAGAMENTO, VALOR, CNPJ, NUM_CUPOM, BAIXADO, DT, TROCO, ID_USUARIO, FECHADO, NUM_CAIXA ) values (@ID_CLIENTE, @TIPO_PAGAMENTO, @VALOR, @CNPJ, @NUM_CUPOM, @BAIXADO, @DT, @TROCO, @ID_USUARIO, @FECHADO, @NUM_CAIXA)");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_CLIENTE", idCliente));
                    comandoSql.Parameters.Add(new SqlParameter("@TIPO_PAGAMENTO", idTipoPagamento));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR", valor));
                    comandoSql.Parameters.Add(new SqlParameter("@CNPJ", cnpj));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", _numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@BAIXADO", baixado));
                    comandoSql.Parameters.Add(new SqlParameter("@DT", data));
                    comandoSql.Parameters.Add(new SqlParameter("@TROCO", troco));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@FECHADO", fechado));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método SalvarEntrada. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarUltimoPagamentoVenda(int numcaixa, int idUsuario)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT TOP(1) PAGAMENTO_VENDA.ID, PAGAMENTO_VENDA.VALOR, PAGAMENTO_VENDA.ID_CLIENTE FROM PAGAMENTO_VENDA WHERE PAGAMENTO_VENDA.ID_USUARIO=@ID_USUARIO AND PAGAMENTO_VENDA.NUM_CAIXA=@NUM_CAIXA AND BAIXADO=0 AND FECHADO=0 ORDER BY ID DESC");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Ultimo Pagamento Venda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
