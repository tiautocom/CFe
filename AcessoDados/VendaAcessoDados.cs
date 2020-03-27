using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class VendaAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();


        public void VendeItem(int idproduto, decimal qtde, decimal preco, decimal total, int idUsuario, int item, DateTime dataCadastro, decimal custo, bool baixado, int numCupom, string codBarra, string descricao, string aliquota, int idParametro, string _valorPis, string cstPis, string valorCofins, string _cstCofins, string _cfop, string _ncm, string _icmCst, string _origemProduto, Boolean fechado, string cest, int numCaixa, string _unid)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("insert into VENDA (ID_PROD, QUANT, PRECO, TOTAL, ID_USUARIO, ITEM, DATA, CUSTO, BAIXADO, NUM_VENDA, COD_BARRA, DESCRICAO_PRODUTO, ALIQUOTA, ID_PARAMETRO, VALOR_PIS, CST_PIS, VALOR_COFINS, CST_COFINS, CFOP, NCM, ORIGEM_PRODUTO,ICM_CST,FECH,CEST,NUM_CAIXA,UNID) values(@ID_PROD, @QUANT, @PRECO, @TOTAL, @ID_USUARIO, @ITEM, @DATA, @CUSTO, @BAIXADO, @NUM_VENDA, @COD_BARRA, @DESCRICAO_PRODUTO, @ALIQUOTA, @ID_PARAMETRO, @VALOR_PIS, @CST_PIS, @VALOR_COFINS, @CST_COFINS, @CFOP, @NCM, @ORIGEM_PRODUTO,@ICM_CST,@FECH,@CEST,@NUM_CAIXA,@UNID)");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_PROD", idproduto));
                    comandoSql.Parameters.Add(new SqlParameter("@QUANT", qtde));
                    comandoSql.Parameters.Add(new SqlParameter("@PRECO", preco));
                    comandoSql.Parameters.Add(new SqlParameter("@TOTAL", total));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@ITEM", item));
                    comandoSql.Parameters.Add(new SqlParameter("@DATA", dataCadastro));
                    comandoSql.Parameters.Add(new SqlParameter("@CUSTO", custo));
                    comandoSql.Parameters.Add(new SqlParameter("@BAIXADO", baixado));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));
                    comandoSql.Parameters.Add(new SqlParameter("@COD_BARRA", codBarra));
                    comandoSql.Parameters.Add(new SqlParameter("@DESCRICAO_PRODUTO", descricao));
                    comandoSql.Parameters.Add(new SqlParameter("@ALIQUOTA", aliquota));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_PARAMETRO", idParametro));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR_PIS", _valorPis));
                    comandoSql.Parameters.Add(new SqlParameter("@CST_PIS", cstPis));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR_COFINS", valorCofins));
                    comandoSql.Parameters.Add(new SqlParameter("@CST_COFINS", _cstCofins));
                    comandoSql.Parameters.Add(new SqlParameter("@CFOP", _cfop));
                    comandoSql.Parameters.Add(new SqlParameter("@NCM", _ncm));
                    comandoSql.Parameters.Add(new SqlParameter("@ORIGEM_PRODUTO", _origemProduto));
                    comandoSql.Parameters.Add(new SqlParameter("@ICM_CST", _icmCst));
                    comandoSql.Parameters.Add(new SqlParameter("@FECH", fechado));
                    comandoSql.Parameters.Add(new SqlParameter("@CEST", cest));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@UNID", _unid));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método VendeItem. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable AtualizarGridVenda(int numCupom, bool baixado, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append(" SELECT * FROM VENDA JOIN NUM_CAIXA ON NUM_CAIXA.NUM_CAIXA=VENDA.NUM_CAIXA WHERE VENDA.NUM_CAIXA=@NUM_CAIXA AND VENDA.QUANT > 0 AND VENDA.NUM_VENDA=@NUM_VENDA ORDER BY VENDA.ITEM ASC");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));
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

        public DataTable PesquisarUltimoItem(int numCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select top (1) VENDA.ITEM from VENDA where VENDA.NUM_VENDA=@NUM_VENDA order by VENDA.ID desc");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarUltimoItem. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable SomaTotalEntrada(int entrada_, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select sum (TOTAL) from VENDA where NUM_VENDA=@NUM_VENDA AND VENDA.QUANT > 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", entrada_));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método SomaTotalEntrada. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarNumCupom()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select top (1) VENDA.NUM_ENTRADA from VENDA");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarNumCupom. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AlterarAtualizado(bool atualizado)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("update VENDA set BAIXADO = @BAIXADO");

                    comandoSql.Parameters.Add(new SqlParameter("@BAIXADO", false));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método AlterarAtualizado. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public void AlterarNumeroEntrada(int _numEntrada)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("update VENDA set NUM_ENTRADA = @NUM_ENTRADA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_ENTRADA", _numEntrada));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método AlterarNumeroEntrada. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable PesquisaEntrada(bool atualizado, int nVenda, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select *, PRODUTO.UNID AS UNIDADE from VENDA join PRODUTO on PRODUTO.COD_INT = VENDA.ID_PROD WHERE VENDA.NUM_VENDA=@NUM_VENDA and VENDA.FECH = 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA ORDER BY VENDA.ITEM ASC");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", nVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@ATUALIZADO", atualizado));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaEntrada. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarUltimoNumEntrada()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM PARAMETRO WHERE PARAMETRO.STATUS = 1 ");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarUltimoNumEntrada. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarTabelaVenda(int numVenda, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM VENDA WHERE VENDA.NUM_VENDA=@NUM_VENDA AND VENDA.QUANT > 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA order by VENDA.ITEM asc");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método PesquisarTabelaVenda. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable PesquisarTextoEntrada(int numCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM VENDA WHERE VENDA.NUM_VENDA=@NUM_VENDA order by VENDA.ITEM asc");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método PesquisarTextoEntrada. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable PesquisaVenda(int _numVenda, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from VENDA where VENDA.NUM_VENDA = @NUM_VENDA AND VENDA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", _numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método PesquisaVenda. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public void CancelarUltimaVenda(int ultimoVenda)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("update VENDA set NUM_ENTRADA = @NUM_ENTRADA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_ENTRADA", ultimoVenda));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método CancelarUltimaVenda. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable PesquisaItens(int numVenda)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from VENDA where VENDA.NUM_VENDA = @NUM_VENDA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numVenda));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método PesquisaItens. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable PesquisaCancelaItem(int numVenda, int item)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from VENDA where VENDA.NUM_VENDA = @NUM_VENDA and VENDA.ITEM = @ITEM");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@ITEM", item));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método PesquisaCancelaItem. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable PesquisaVenaTotaAll()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT COD_BARRA ,SUM (TOTAL) AS TOTAL, DESCRICAO_PRODUTO FROM VENDA GROUP BY COD_BARRA,DESCRICAO_PRODUTO ORDER BY COD_BARRA");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método PesquisaVenaTotaAll. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public void CancelarVendaAtual(int numVenda)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("delete from VENDA where VENDA.NUM_VENDA = @NUM_VENDA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numVenda));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método CancelarVendaAtual. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void ExcluirDetalhes(int itemAtual, int numVenda, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("delete from VENDA where VENDA.NUM_VENDA = @NUM_VENDA and VENDA.ITEM = @ITEM and VENDA.QUANT > 0 and VENDA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@ITEM", itemAtual));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método ExcluirDetalhes. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaUltimaVenda(int numCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from VENDA where VENDA.NUM_VENDA =@NUM_VENDA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaUltimaVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void DevolucaoVendaBanco(int idproduto, decimal qtde, decimal preco, decimal total, int idUsuario, int item, DateTime dataCadastro, decimal custo, bool baixado, int numCupom, string codBarra, string descricao, string aliquota, int idParametro, string _valorPis, string cstPis, string valorCofins, string _cstCofins, string _cfop, string _ncm, string _icmCst, string _origemProduto, Boolean fechado, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("insert into VENDA (ID_PROD, QUANT, PRECO, TOTAL, ID_USUARIO, ITEM, DATA, CUSTO, BAIXADO, NUM_VENDA, COD_BARRA, DESCRICAO_PRODUTO, ALIQUOTA, ID_PARAMETRO, VALOR_PIS, CST_PIS, VALOR_COFINS, CST_COFINS, CFOP, NCM, ORIGEM_PRODUTO,ICM_CST,FECH,NUM_CAIXA) values(@ID_PROD, @QUANT, @PRECO, @TOTAL, @ID_USUARIO, @ITEM, @DATA, @CUSTO, @BAIXADO, @NUM_VENDA, @COD_BARRA, @DESCRICAO_PRODUTO, @ALIQUOTA, @ID_PARAMETRO, @VALOR_PIS, @CST_PIS, @VALOR_COFINS, @CST_COFINS, @CFOP, @NCM, @ORIGEM_PRODUTO,@ICM_CST,@FECH,@NUM_CAIXA)");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_PROD", idproduto));
                    comandoSql.Parameters.Add(new SqlParameter("@QUANT", qtde));
                    comandoSql.Parameters.Add(new SqlParameter("@PRECO", preco));
                    comandoSql.Parameters.Add(new SqlParameter("@TOTAL", total));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@ITEM", item));
                    comandoSql.Parameters.Add(new SqlParameter("@DATA", dataCadastro));
                    comandoSql.Parameters.Add(new SqlParameter("@CUSTO", custo));
                    comandoSql.Parameters.Add(new SqlParameter("@BAIXADO", baixado));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));
                    comandoSql.Parameters.Add(new SqlParameter("@COD_BARRA", codBarra));
                    comandoSql.Parameters.Add(new SqlParameter("@DESCRICAO_PRODUTO", descricao));
                    comandoSql.Parameters.Add(new SqlParameter("@ALIQUOTA", aliquota));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_PARAMETRO", idParametro));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR_PIS", _valorPis));
                    comandoSql.Parameters.Add(new SqlParameter("@CST_PIS", cstPis));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR_COFINS", valorCofins));
                    comandoSql.Parameters.Add(new SqlParameter("@CST_COFINS", _cstCofins));
                    comandoSql.Parameters.Add(new SqlParameter("@CFOP", _cfop));
                    comandoSql.Parameters.Add(new SqlParameter("@NCM", _ncm));
                    comandoSql.Parameters.Add(new SqlParameter("@ORIGEM_PRODUTO", _origemProduto));
                    comandoSql.Parameters.Add(new SqlParameter("@ICM_CST", _icmCst));
                    comandoSql.Parameters.Add(new SqlParameter("@FECH", fechado));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método DevolucaoVendaBanco. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaVendaNegativo(int ultimoVenda)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("Select * from VENDA where VENDA.NUM_VENDA = @NUM_VENDA order by ID desc");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", ultimoVenda));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaVendaNegativo. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable FecharVendaDepartamentos(int idUsuario, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT NUM_DEPAR as DEP, DEPARTAM as DEPARTAMENTO, SUM(total) as Total FROM VENDA  INNER JOIN PRODUTO ON PRODUTO.COD_INT = VENDA.ID_PROD where VENDA.ID_USUARIO= @ID_USUARIO AND VENDA.BAIXADO = 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY NUM_DEPAR, DEPARTAM ORDER BY PRODUTO.NUM_DEPAR ASC");

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
                throw new Exception("Ocorreu um erro no método FecharVendaDepartamentos. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarVendaTotalTurno(int idUsuario, DateTime data, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    // sql.Append("SELECT COD_BARRA AS COD, DESCRICAO_PRODUTO AS DESCR, PRECO AS PRECO, SUM(TOTAL)AS TOTAL FROM VENDA INNER JOIN USUARIO ON VENDA.ID_USUARIO = USUARIO.ID_USUARIO WHERE DATA >= '" + data.ToShortDateString() + " 00:00' and DATA <= '" + data.ToShortDateString() + " 23:59:59'  AND USUARIO.ID_USUARIO = @ID_USUARIO GROUP BY VENDA.COD_BARRA, DESCRICAO_PRODUTO,PRECO ORDER BY COD_BARRA ASC");

                    sql.Append("SELECT VENDA.COD_BARRA as Cod, DESCRICAO_PRODUTO as Descricao,SUM (QUANT)AS Qtde, SUM (TOTAL) AS Total FROM VENDA join PRODUTO on PRODUTO.COD_INT = VENDA.ID_PROD where VENDA.ID_USUARIO=@ID_USUARIO AND VENDA.BAIXADO =0 AND VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY VENDA.COD_BARRA,DESCRICAO_PRODUTO, SETOR ORDER BY DESCRICAO_PRODUTO asc");

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
                throw new Exception("Ocorreu um erro no método PesquisarVendaTotalTurno. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaValorTotal(int idUsuario, DateTime data)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select VENDA.COD_BARRA as Cod, DESCRICAO_PRODUTO as Decrição, sum (TOTAL) as Total, VENDA.QUANT as Qtde from VENDA join USUARIO on VENDA.ID_USUARIO = USUARIO.ID_USUARIO where  USUARIO.ID_USUARIO = @ID_USUARIO and VENDA.DATA >=@DATA group by TOTAL, DESCRICAO_PRODUTO");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@DATA", data));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarVendaTotalTurno. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AlterarBaixadoVenda(int idUsuario, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE VENDA SET FECH = 1 where VENDA.ID_USUARIO = @ID_USUARIO AND VENDA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlterarBaixadoVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaItemVenda(int itemAtual, int numVenda)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from VENDA where VENDA.NUM_VENDA = @NUM_VENDA and VENDA.ITEM = @ITEM");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@ITEM", itemAtual));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método PesquisaCancelaItem. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable PesquisarVendaCancelado(int idUsuario, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT COD_BARRA as Cod, DESCRICAO_PRODUTO as Descricao,SUM (QUANT)AS Qtde, SUM (TOTAL) AS Total FROM VENDA where VENDA.ID_USUARIO = @ID_USUARIO AND VENDA.BAIXADO = 0  AND VENDA.QUANT < 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY COD_BARRA,DESCRICAO_PRODUTO ORDER BY DESCRICAO_PRODUTO asc");

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
                throw new Exception("Ocorreu um erro no método GerarRelatorioVendaCancelado. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaVendaSetor(int idUsuario, DateTime data, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT SETOR as Setor, SUM(VENDA.TOTAL) AS Total FROM PRODUTO JOIN VENDA ON PRODUTO.COD_INT = VENDA.ID_PROD where VENDA.ID_USUARIO = @ID_USUARIO  and VENDA.BAIXADO = 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY SETOR order by SETOR asc");

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
                throw new Exception("Ocorreu um erro no método GerarRelatorioVendaCancelado. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarSomaTotalDia(int idUsuario, DateTime data)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT SUM(VENDA.TOTAL) AS Total FROM VENDA where VENDA.ID_USUARIO = @ID_USUARIO and DATA >= '" + data.ToShortDateString() + " 00:00' and DATA <= '" + data.ToShortDateString() + " 23:59:59' and VENDA.BAIXADO = 0");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método GerarRelatorioVendaCancelado. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable GerarRealtorioSetor1(int idUsuario, int setor, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT VENDA.COD_BARRA AS Cod, DESCRICAO_PRODUTO as Descricao,SUM (QUANT)AS Qtde, SUM (TOTAL) AS Total FROM VENDA  join PRODUTO ON PRODUTO.COD_INT = VENDA.ID_PROD where PRODUTO.SETOR = @SETOR and VENDA.ID_USUARIO = @ID_USUARIO AND VENDA.BAIXADO = 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY VENDA.COD_BARRA,DESCRICAO_PRODUTO, SETOR ORDER BY DESCRICAO_PRODUTO ASC");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@SETOR", setor));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método GerarRelatorioVendaCancelado. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void GravarDadosSangria(int idUsuario, decimal valorSangria, DateTime data, String motivo, int numCaixa, bool tipo, bool fechado)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("INSERT INTO SANGRIA (ID_USUARIO, VALOR, DATA, DESCRICAO, NUM_CAIXA, TIPO, FECHADO) VALUES (@ID_USUARIO, @VALOR, @DATA, @DESCRICAO, @NUM_CAIXA, @TIPO, @FECHADO)");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@VALOR", valorSangria));
                    comandoSql.Parameters.Add(new SqlParameter("@DATA", data));
                    comandoSql.Parameters.Add(new SqlParameter("@DESCRICAO", motivo));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@TIPO", tipo));
                    comandoSql.Parameters.Add(new SqlParameter("@FECHADO", fechado));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Método GravarDadosSangria. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }

        public DataTable PesquisarUltimaVenda()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append(" SELECT  TOP(1) * FROM VENDA where VENDA.BAIXADO = 0 and VENDA.TOTAL > 0 ORDER BY ID DESC ");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Ultima Venda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable ReimprimirUltimaVenda(int ultimaVenda, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append(" SELECT * FROM VENDA JOIN NUM_CAIXA ON NUM_CAIXA.NUM_CAIXA=VENDA.NUM_CAIXA WHERE VENDA.NUM_CAIXA=@NUM_CAIXA AND VENDA.QUANT > 0 AND VENDA.NUM_VENDA=@NUM_VENDA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", ultimaVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Reimprimir Ultima Venda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable GerarRelatorioVendaTotal(int idUsuario, DateTime data)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT COD_BARRA as Cod, DESCRICAO_PRODUTO as Descricao,SUM (QUANT)AS Qtde, SUM (TOTAL) AS Total FROM VENDA where VENDA.ID_USUARIO = @ID_USUARIO AND VENDA.BAIXADO = 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY COD_BARRA,DESCRICAO_PRODUTO ORDER BY DESCRICAO_PRODUTO asc");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", idUsuario));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Gerar Relatorio Venda Total. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable SomaTotalGrid(int numCupom, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("select sum (TOTAL) as TOTAL from VENDA where VENDA.NUM_VENDA = @NUM_VENDA AND NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Soma Total Grid. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisaVendaNaoFechado(bool vendaNaoFechado, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM VENDA where VENDA.BAIXADO = 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA order by VENDA.DESCRICAO_PRODUTO asc");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    //MessageBox.Show(sql.ToString());
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisaVendaNaoFechado. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void FecharVenda(int codInternoprod)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE VENDA SET FECH = 1 WHERE VENDA.ID_PROD = @ID_PROD");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_PROD", codInternoprod));

                    comandoSql.CommandText = sql.ToString(); //Indica que o código SQL é o que deverá ser executado.
                    comandoSql.Connection = conexao; //Indica que a conexão dos comandos SQL é a string de conexão.
                    comandoSql.ExecuteNonQuery(); //Executa todo o comando para a inserção dos valores.
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método FecharVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarQtde_QtdeEstoque(int idUsuario, int numcaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    // sql.Append("SELECT VENDA.COD_BARRA as Cod, DESCRICAO_PRODUTO as Descricao,(VENDA.QUANT) AS Qtde, (ESTOQUE) AS Estoque FROM VENDA JOIN PRODUTO ON VENDA.ID_PROD = PRODUTO.COD_INT JOIN USUARIO ON VENDA.ID_USUARIO = USUARIO.ID_USUARIO WHERE USUARIO.ID_USUARIO = @ID_USUARIO AND VENDA.BAIXADO = 0 GROUP BY VENDA.COD_BARRA,DESCRICAO_PRODUTO, ESTOQUE, QUANT ORDER BY DESCRICAO_PRODUTO asc");
                    sql.Append("SELECT sum(VENDA.QUANT) AS Qtde, PRODUTO.ESTOQUE AS Estoque, VENDA.COD_BARRA AS Cod, DESCRICAO_PRODUTO as Descricao FROM VENDA JOIN PRODUTO ON PRODUTO.COD_INT = VENDA.ID_PROD WHERE VENDA.FECH=0 AND VENDA.ID_USUARIO = @ID_USUARIO AND NUM_CAIXA=@NUM_CAIXA group by VENDA.COD_BARRA , DESCRICAO_PRODUTO, PRODUTO.ESTOQUE  order by DESCRICAO_PRODUTO asc");

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
                throw new Exception("Ocorreu um erro no método Pesquisar Qtde_QtdeEstoque. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarProdutoItem(int numCupom, int itemAtual, int numCaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT DESCRICAO_PRODUTO FROM VENDA WHERE VENDA.NUM_VENDA=@NUM_VENDA AND ITEM=@ITEM AND VENDA.NUM_CAIXA=@NUM_CAIXA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));
                    comandoSql.Parameters.Add(new SqlParameter("@ITEM", itemAtual));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisa rProduto Item. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarVenda(int numCupom)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT ITEM, COD_BARRA, DESCRICAO_PRODUTO, UNID, QUANT, PRECO, TOTAL FROM VENDA WHERE VENDA.NUM_VENDA=@NUM_VENDA");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisa Pesquisar Venda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable VendaX(int idUsuario, int numCaixa, string data)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    //sql.Append("SELECT TIPO_PAGTO.TIPO_PAGTO_ID, SUM(VALOR - TROCO)AS TOTAL FROM PAGAMENTO_VENDA JOIN TIPO_PAGTO ON TIPO_PAGTO.TIPO_PAGTO_ID=PAGAMENTO_VENDA.TIPO_PAGAMENTO WHERE BAIXADO=@BAIXADO AND FECHADO=@FECHADO AND PAGAMENTO_VENDA.ID_USUARIO=@ID_USUARIO AND PAGAMENTO_VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY TIPO_PAGTO.TIPO_PAGTO_ID");

                    sql.Append("SELECT TIPO_PAGTO.TIPO_PAGTO_ID, SUM(VALOR - TROCO) AS TOTAL FROM PAGAMENTO_VENDA JOIN TIPO_PAGTO ON TIPO_PAGTO.TIPO_PAGTO_ID=PAGAMENTO_VENDA.TIPO_PAGAMENTO WHERE PAGAMENTO_VENDA.ID_USUARIO=@ID_USUARIO AND PAGAMENTO_VENDA.NUM_CAIXA=@NUM_CAIXA AND PAGAMENTO_VENDA.DT >= @DT AND BAIXADO=0 GROUP BY TIPO_PAGTO.TIPO_PAGTO_ID");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));
                    comandoSql.Parameters.Add(new SqlParameter("@DT", data));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisa Pesquisar Venda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarVendaCancelada(int numcaixa, int idUsuario)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT COUNT(*) AS QTDE , SUM(TOTAL) AS TOTAL FROM VENDA WHERE VENDA.TOTAL < 0 AND VENDA.ID_USUARIO=@ID_USUARIO  AND VENDA.NUM_CAIXA=@NUM_CAIXA AND VENDA.BAIXADO=0");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuario));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Venda Cancelada. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AlterarESinclonizarItensVenda(int itemA, int numVenda, int idVenda)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE VENDA SET ITEM=@ITEM WHERE VENDA.NUM_VENDA=@NUM_VENDA AND VENDA.ID=@ID");

                    comandoSql.Parameters.Add(new SqlParameter("@ITEM", itemA));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numVenda));
                    comandoSql.Parameters.Add(new SqlParameter("@ID", idVenda));

                    comandoSql.CommandText = sql.ToString(); //Indica que o código SQL é o que deverá ser executado.
                    comandoSql.Connection = conexao; //Indica que a conexão dos comandos SQL é a string de conexão.
                    comandoSql.ExecuteNonQuery(); //Executa todo o comando para a inserção dos valores.
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método AlterarESinclonizarItensVenda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarRelatorioTotalMes(int numcaixa, int mes)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT DAY(DATA) AS DIA, SUM(TOTAL)AS TOTAL FROM VENDA WHERE VENDA.NUM_CAIXA=@NUM_CAIXA AND MONTH(DATA) = @DATA GROUP BY DAY(DATA) ORDER BY DAY(DATA) ASC");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@DATA", mes));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisa rRelatorio Total do Mes. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarRelatorioAliquotaTotalMes(int numcaixa, int mes, int ano)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT DAY(DATA)AS DIA , SUM(TOTAL) AS TOTAL, ALIQUOTA FROM VENDA WHERE VENDA.NUM_CAIXA=@NUM_CAIXA AND MONTH(DATA)=@DATA AND YEAR(DATA)=@DATA GROUP BY ALIQUOTA, DAY(DATA) ORDER BY DAY(DATA) ASC");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@DATA", mes));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Relatorio Aliquota Total Mes. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarDataVendaAberto(string numCaixaXml)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT TOP 1 DATA FROM VENDA WHERE VENDA.NUM_CAIXA=@NUM_CAIXA ORDER BY DATA DESC");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numCaixaXml));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Pesquisar Data Venda Aberto. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void ExcluirVenda(int numcaixa, int numCupom, int idUsuarioLogado)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("DELETE VENDA WHERE VENDA.NUM_CAIXA=@NUM_CAIXA AND VENDA.NUM_VENDA=@NUM_VENDA AND VENDA.ID_USUARIO=@ID_USUARIO");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_VENDA", numCupom));
                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuarioLogado));

                    comandoSql.CommandText = sql.ToString(); //Indica que o código SQL é o que deverá ser executado.
                    comandoSql.Connection = conexao; //Indica que a conexão dos comandos SQL é a string de conexão.
                    comandoSql.ExecuteNonQuery(); //Executa todo o comando para a inserção dos valores.
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Excluir Venda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarRelatorioTotalMesTotalAliquota(int numcaixa, int mes, int ano)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT DAY(V.DATA) AS DIA, SUM(V.TOTAL)AS VENDA, VV.ALIQUOTA, SUM(VV.TOTAL) AS TOTAL FROM VENDA V FULL OUTER JOIN VENDA VV ON V.ID = VV.ID WHERE V.NUM_CAIXA=@NUM_CAIXA AND MONTH(V.DATA) = @DATA AND YEAR(V.DATA) = @DATAY GROUP BY DAY(V.DATA), VV.ALIQUOTA ORDER BY DAY(V.DATA)");

                    comandoSql.Parameters.Add(new SqlParameter("@DATA", mes));
                    comandoSql.Parameters.Add(new SqlParameter("@DATAY", ano));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));

                    //comandoSql.CommandText = sql.ToString();
                    //comandoSql.Connection = conexao;
                    //dadosTabela.Load(comandoSql.ExecuteReader());

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Relatorio Total Mes e Total Aliquota. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarUltimaVendaNumCaixa(int numcaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT TOP 1 MONTH(DATA) AS DATA FROM VENDA WHERE VENDA.NUM_CAIXA=@NUM_CAIXA ORDER BY VENDA.DATA DESC");

                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Pesquisar Ultima Venda NumCaixa. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarEstoqueIncialMes(int numeroSetor)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT SETOR, SUM(ESTOQUE * CUSTO)AS ESTOQUE_INICIAL FROM PRODUTO WHERE PRODUTO.SETOR=@SETOR GROUP BY SETOR ORDER BY SETOR ASC");

                    comandoSql.Parameters.Add(new SqlParameter("@SETOR", numeroSetor));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Estoque Incial do Mes. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable GerarRelatorioVendaTotal(int idUsuario, int numcaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT COD_BARRA as Cod, DESCRICAO_PRODUTO as Descricao,SUM (QUANT)AS Qtde, SUM (TOTAL) AS Total FROM VENDA join USUARIO on VENDA.ID_USUARIO = USUARIO.ID_USUARIO where USUARIO.ID_USUARIO = @ID_USUARIO AND VENDA.BAIXADO = 0 AND VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY COD_BARRA,DESCRICAO_PRODUTO ORDER BY DESCRICAO_PRODUTO asc");

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
                throw new Exception("Ocorreu um erro no método Gerar Relatorio Venda Total. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable GerarRealtorioEstoque(int idUsuario, int numcaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT VENDA.COD_BARRA AS Cod, VENDA.DESCRICAO_PRODUTO AS Descricao, SUM(VENDA.QUANT)AS Qtde, PRODUTO.ESTOQUE AS Estoque FROM VENDA JOIN USUARIO ON USUARIO.ID_USUARIO = VENDA.ID_USUARIO JOIN PRODUTO ON PRODUTO.COD_BARRA = VENDA.COD_BARRA WHERE USUARIO.ID_USUARIO = @ID_USUARIO AND VENDA.FECH=0 AND VENDA.NUM_CAIXA=@NUM_CAIXA GROUP BY VENDA.COD_BARRA, VENDA.DESCRICAO_PRODUTO, PRODUTO.ESTOQUE ORDER BY VENDA.DESCRICAO_PRODUTO asc");

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
                throw new Exception("Ocorreu um erro no método Gerar Realtorio Estoque Produto. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarUltimaVenda(int idUsuarioLogado, int numcaixa)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("SELECT TOP 1 DATA FROM VENDA WHERE VENDA.ID_USUARIO=@ID_USUARIO AND VENDA.NUM_CAIXA=@NUM_CAIXA ORDER BY DATA DESC");

                    comandoSql.Parameters.Add(new SqlParameter("@ID_USUARIO", idUsuarioLogado));
                    comandoSql.Parameters.Add(new SqlParameter("@NUM_CAIXA", numcaixa));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Ultima Venda. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void FecharVendaIdVenda(int idvenda)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();
                    sql.Append("UPDATE VENDA SET BAIXADO=1 WHERE VENDA.ID=@ID");

                    comandoSql.Parameters.Add(new SqlParameter("@ID", idvenda));
                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Fechar Venda ID. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
