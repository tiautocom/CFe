using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class ProdutoRegraNegocio
    {
        AcessoDados.ProdutoAcessoDados novoProduto;

        int idCodigo = 0;

        public DataTable PesquisarAll()
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarAll();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarProdutoNome(string nomeProduto)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarProdutoNome(nomeProduto);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaCodigoBarra(string codigoBarra)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisaCodigoBarraProduto(codigoBarra);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable Listar(int _idProduto)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.Listar(_idProduto);
                return dadosTabela;

                idCodigo = Convert.ToInt32(dadosTabela.Rows[0]["COD_INT"].ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarPrecoZero(String idCodProduto, int numVenda, decimal novoPreco)
        {
            novoProduto = new AcessoDados.ProdutoAcessoDados();
            novoProduto.AlterarPrecoZeroTabela(idCodProduto, numVenda, novoPreco);
        }

        public DataTable PesquisaGranel(string codBarra)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisaGranel(codBarra);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarProdutoBalanca(string codProd)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarProdutoBalanca(codProd);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable RetornarEstoqueProduto(int idProdutoItem)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.RetornarEstoqueProduto(idProdutoItem);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AtualizarEstoque(int idProduto, decimal estoqueAtual)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                novoProduto.AtualizarEstoque(idProduto, estoqueAtual);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarProdutoDepartamento(string numDepartamento)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarProdutoDepartamento(numDepartamento);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarDepratamentoAll()
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarDepratamentoAll();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarEstoqueTotal()
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarEstoqueTotal();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SomaTotalEstoqueDep(string numDepartamento)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.SomaTotalEstoqueDep(numDepartamento);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarTotalProduto()
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarTotalProduto();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaEstoqueAtual(int codInternoprod)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisaEstoqueAtual(codInternoprod);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarProdutoComposto(int codInternoprod)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarProdutoComposto(codInternoprod);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarEstoqueComposto(int idComposto)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisarEstoqueComposto(idComposto);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
