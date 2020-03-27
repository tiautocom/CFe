using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class FormaPagamentoAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();


        public DataTable PesquisarFormaPagtos()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from TIPO_PAGTO order by TIPO_PAGTO.TIPO_PAGTO_ID asc");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarFormaPagtos. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarFormaPagtos(string codTipoPagamento)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from TIPO_PAGTO where TIPO_PAGTO.TIPO_PAGTO_ID = @TIPO_PAGTO_ID");

                    comandoSql.Parameters.Add(new SqlParameter("@TIPO_PAGTO_ID", codTipoPagamento));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarFormaPagtos. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarFormaPagtos(int cod)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from TIPO_PAGTO where TIPO_PAGTO.TIPO_PAGTO_ID = @TIPO_PAGTO_ID");

                    comandoSql.Parameters.Add(new SqlParameter("@TIPO_PAGTO_ID", cod));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarFormaPagtos. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarFPgto(DateTime data, int idUsuario, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    //  sql.Append(" SELECT TIPO_PAGTO.TIPO_PAGTO AS TIPO, SUM (VALOR)AS Total FROM PAGAMENTO_VENDA JOIN TIPO_PAGTO ON PAGAMENTO_VENDA.TIPO_PAGAMENTO = TIPO_PAGTO.TIPO_PAGTO_ID WHERE PAGAMENTO_VENDA.TIPO_PAGAMENTO = TIPO_PAGTO.TIPO_PAGTO_ID GROUP BY TIPO_PAGTO ORDER BY TIPO_PAGTO");

                    sql.Append("SELECT TIPO_PAGTO.TIPO_PAGTO as TIPO , SUM(VALOR - TROCO)AS VENDA FROM PAGAMENTO_VENDA JOIN TIPO_PAGTO ON PAGAMENTO_VENDA.TIPO_PAGAMENTO = TIPO_PAGTO.TIPO_PAGTO_ID WHERE PAGAMENTO_VENDA.TIPO_PAGAMENTO = TIPO_PAGTO.TIPO_PAGTO_ID AND PAGAMENTO_VENDA.ID_USUARIO = @ID_USUARIO AND PAGAMENTO_VENDA.FECHADO = 0 AND PAGAMENTO_VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY TIPO_PAGTO ORDER BY TIPO_PAGTO");

                    comandoSql.Parameters.Add(new SqlParameter("@DT", data));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarFPgto. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AlteraVendaPagamentoFechado(int idUsuario, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE PAGAMENTO_VENDA SET FECHADO = 1, BAIXADO = 1 where PAGAMENTO_VENDA.ID_USUARIO = @ID_USUARIO AND PAGAMENTO_VENDA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlteraVendaPagamentoFechado. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable DevolucaoPagamentoVenda(int numCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from PAGAMENTO_VENDA where PAGAMENTO_VENDA.NUM_CUPOM = @NUM_CUPOM ");
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", numCupom));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método DevolucaoPagamentoVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void DevolucaoPagamentoVenda(int idUsuario, decimal valorVendaDevolvido)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append(" UPDATE PAGAMENTO_VENDA SET VALOR = @VALOR where PAGAMENTO_VENDA.NUM_CUPOM = @NUM_CUPOM");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR", valorVendaDevolvido));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método DevolucaoPagamentoVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void DevolucaoPagamentosVenda(int idUsuario, int idCliente, int tipoP, decimal valorVendaDevolvido, string cnpj, int numVenda, bool baixado, DateTime data, decimal troco, bool fechado, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("insert into PAGAMENTO_VENDA (ID_CLIENTE, TIPO_PAGAMENTO, VALOR, CNPJ, NUM_CUPOM, BAIXADO, DT, TROCO, ID_USUARIO, FECHADO, NUM_CAIXA ) values (@ID_CLIENTE, @TIPO_PAGAMENTO, @VALOR, @CNPJ, @NUM_CUPOM, @BAIXADO, @DT, @TROCO, @ID_USUARIO, @FECHADO, @NUM_CAIXA)");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_CLIENTE", idCliente));
                    comandoSql.Parameters.Add(new SqlParameter("@TIPO_PAGAMENTO", tipoP));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR", valorVendaDevolvido));
                    comandoSql.Parameters.Add(new SqlParameter("@CNPJ", cnpj));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CUPOM", numVenda));
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
                throw new Exception("Ocorreu um erro no método DevolucaoPagamentosVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
