using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class ProdutoAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public DataTable PesquisaCodigoBarra(string codigoBarra)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from PRODUTO where PRODUTO.COD_BARRA = @COD_BARRA order by PRODUTO.COD_INT");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_BARRA", codigoBarra));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaCodigoBarras. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarAll()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from PRODUTO order by PRODUTO.DESCRICAO");
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarAll. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarProdutoNome(string nomeProduto)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from PRODUTO where PRODUTO.DESCRICAO LIKE '%'+@DESCRICAO+'%'order by PRODUTO.DESCRICAO asc");

                    comandoSql.Parameters.Add(new SqlParameter("@DESCRICAO", nomeProduto));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {

                throw new Exception("Ocorreu um Erro no Método PesquisarDescricao. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable PesquisaCodigoBarraProduto(string codigoBarra)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from PRODUTO where PRODUTO.COD_BARRA LIKE '%'+@COD_BARRA+'%'order by PRODUTO.DESCRICAO asc");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_BARRA", codigoBarra));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaCodigoBarraProduto. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable Listar(int _idProduto)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from PRODUTO where PRODUTO.COD_INT = @COD_INT");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_INT", _idProduto));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método ListarCodigo. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AlterarPrecoZeroTabela(String idCodProduto, int numVenda, decimal novoPreco)
        {

            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("update PRODUTO set PRECO = @PRECO where PRODUTO.COD_BARRA = @COD_BARRA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_ENTRADA", numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@COD_BARRA", idCodProduto));
                    comandoSql.Parameters.Add(new SqlParameter("@PRECO", novoPreco));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlterarPrecoZeroTabela. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaGranel(string codBarra)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select GRANEL from PRODUTO where PRODUTO.COD_BARRA=@COD_BARRA");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_BARRA", codBarra));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaGranel. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarProdutoBalanca(string codProd)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from PRODUTO where PRODUTO.COD_BARRA = @COD_BARRA");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_BARRA", codProd));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarProdutoBalanca. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable RetornarEstoqueProduto(int idProdutoItem)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM Produto WHERE PRODUTO.COD_INT = @COD_INT");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_INT", idProdutoItem));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método RetornarEstoqueProduto. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AtualizarEstoque(int idProduto, decimal estoqueAtual)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE Produto");
                    sql.Append(" SET ESTOQUE = @ESTOQUE");
                    sql.Append(" WHERE (COD_INT = @COD_INT)");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_INT", idProduto));
                    comandoSql.Parameters.Add(new SqlParameter("@ESTOQUE", estoqueAtual));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlterarEstoque. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarProdutoDepartamento(string numDepartamento)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT COD_BARRA as Codigo, DESCRICAO as Descricao, ESTOQUE as Estoque, PRECO AS Preco, ( preco * estoque) as Total FROM PRODUTO WHERE PRODUTO.NUM_DEPAR = @NUM_DEPAR ORDER BY PRODUTO.DESCRICAO ASC");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_DEPAR", numDepartamento));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarProdutoDepartamento. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarDepratamentoAll()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT NUM_DEPAR AS DEPARTAMENTO FROM PRODUTO GROUP BY NUM_DEPAR ORDER BY NUM_DEPAR ASC");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarDepratamentoAll. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarEstoqueTotal()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT COD_BARRA as Codigo, DESCRICAO as Descricao, ESTOQUE as Estoque, PRECO AS Preco, ( preco * estoque) as Total FROM PRODUTO ORDER BY PRODUTO.DESCRICAO ASC");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Estoque Total. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable SomaTotalEstoqueDep(string numDepartamento)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT SUM(PRECO * ESTOQUE) AS TOTAL FROM PRODUTO where PRODUTO.NUM_DEPAR = @NUM_DEPAR");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_DEPAR", numDepartamento));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Soma Total Estoque Dep. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarTotalProduto()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT COUNT(COD_INT)as QTDE FROM PRODUTO");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Total Produto. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaEstoqueAtual(int codInternoprod)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT COD_INT ,ESTOQUE, DESCRICAO FROM PRODUTO where PRODUTO.COD_INT = @COD_INT");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_INT", codInternoprod));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisa Estoque Atual. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarProdutoComposto(int codInternoprod)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT PRODUTO.DESCRICAO, PRODUTO.ESTOQUE, PRODUTO.ESTOQUE, MATERIA_PRIMA.ID_COMPOSTO, MATERIA_PRIMA.QTDE FROM PRODUTO JOIN MATERIA_PRIMA ON MATERIA_PRIMA.ID_PRODUTO=PRODUTO.COD_INT WHERE PRODUTO.COD_INT=@COD_INT");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_INT", codInternoprod));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Produto Composto. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarEstoqueComposto(int idComposto)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT PRODUTO.DESCRICAO, PRODUTO.ESTOQUE, MATERIA_PRIMA.ID_COMPOSTO, MATERIA_PRIMA.QTDE FROM PRODUTO JOIN MATERIA_PRIMA ON MATERIA_PRIMA.ID_PRODUTO=PRODUTO.COD_INT WHERE PRODUTO.COD_INT=@COD_INT");

                    comandoSql.Parameters.Add(new SqlParameter("@COD_INT", idComposto));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Estoque Composto. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
