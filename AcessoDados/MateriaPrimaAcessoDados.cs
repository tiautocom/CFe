using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AcessoDados
{
    public class MateriaPrimaAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public DataTable PesquisarQtdeEstoqueMateriaPrima(int idComposto)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT DESCRICAO, PRODUTO.ESTOQUE, QTDE FROM PRODUTO JOIN MATERIA_PRIMA ON PRODUTO.COD_INT=MATERIA_PRIMA.ID_COMPOSTO WHERE MATERIA_PRIMA.ID_COMPOSTO=@ID_COMPOSTO");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_COMPOSTO", idComposto));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Qtde Estoque Materia Prima. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void BaixaEstoqueMateriaPrima(int idComposto, decimal bemp)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE MATERIA_PRIMA SET QTDE=@QTDE WHERE MATERIA_PRIMA.ID_COMPOSTO=@ID_COMPOSTO");

                    comandoSql.Parameters.Add(new SqlParameter("@QTDE", bemp));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_COMPOSTO", idComposto));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método Baixa Estoque Materia Prima. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }
    }
}
