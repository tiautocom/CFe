using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class VendaRegraNegocios
    {
        AcessoDados.VendaAcessoDados novaVenda;
        AcessoDados.ProdutoAcessoDados novoProduto;

        public DataTable PesquisarCodigoBarra(string codigoBarra)
        {
            try
            {
                novoProduto = new AcessoDados.ProdutoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoProduto.PesquisaCodigoBarra(codigoBarra);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable AtualizarGriVenda(int numCupom, bool baixado, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.AtualizarGridVenda(numCupom, baixado, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarUltimoItem(int numCupom)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarUltimoItem(numCupom);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SomaTotalEntrada(int _numVenda, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.SomaTotalEntrada(_numVenda, numCaixa);
                return dadosTabela;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaNumCupom()
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarNumCupom();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarAtualizado(bool atualizado)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.AlterarAtualizado(atualizado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarNumEntrada(int _numEntrada)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();

                _numEntrada = _numEntrada + 1;
                novaVenda.AlterarNumeroEntrada(_numEntrada);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ListarEntrada(int nVenda, bool atualizado, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();

                dadosTabela = novaVenda.PesquisaEntrada(atualizado, nVenda, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaUltimoMNumEntrada()
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();

                dadosTabela = novaVenda.PesquisarUltimoNumEntrada();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void VendeItem(int idproduto, decimal qtde, decimal preco, decimal total, int idUsuario, int item, DateTime dataCadastro, decimal custo, bool baixado, int numCupom, string codBarra, string descricao, string aliquota, int idParametro, string _valorPis, string cstPis, string valorCofins, string _cstCofins, string _cfop, string _ncm, string _icmCst, string _origemProduto, Boolean fechado, string cest, int numCaixa, string _unid)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.VendeItem(idproduto, qtde, preco, total, idUsuario, item, dataCadastro, custo, baixado, numCupom, codBarra, descricao, aliquota, idParametro, _valorPis, cstPis, valorCofins, _cstCofins, _cfop, _ncm, _icmCst, _origemProduto, fechado, cest, numCaixa, _unid);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaTabelaVenda(int numVenda, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarTabelaVenda(numVenda, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarTextoEntrada(int numCupom)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarTextoEntrada(numCupom);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaVendas(int _numVenda, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVenda(_numVenda, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CancelarUltimaVenda(int ultimoVenda)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.CancelarUltimaVenda(ultimoVenda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaItens(int numVenda)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaItens(numVenda);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaCancelaItem(int numVenda, int item)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaCancelaItem(numVenda, item);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaVendaTotalAll()
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVenaTotaAll();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CancelarVendaAtual(int numVenda)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.CancelarVendaAtual(numVenda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ExcluirDetalhes(int itemAtual, int numVenda, int numcaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.ExcluirDetalhes(itemAtual, numVenda, numcaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaUltimaVenda(int numCupom)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaUltimaVenda(numCupom);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DevolucaoVendaBanco(int idproduto, decimal qtde, decimal preco, decimal total, int idUsuario, int item, DateTime dataCadastro, decimal custo, bool baixado, int numCupom, string codBarra, string descricao, string aliquota, int idParametro, string _valorPis, string cstPis, string valorCofins, string _cstCofins, string _cfop, string _ncm, string _icmCst, string _origemProduto, Boolean fechado, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.DevolucaoVendaBanco(idproduto, qtde, preco, total, idUsuario, item, dataCadastro, custo, baixado, numCupom, codBarra, descricao, aliquota, idParametro, _valorPis, cstPis, valorCofins, _cstCofins, _cfop, _ncm, _icmCst, _origemProduto, fechado, numCaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaVendaNegativo(int ultimoVenda)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVendaNegativo(ultimoVenda);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable FecharVendaDepartamentos(int idUsuario, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.FecharVendaDepartamentos(idUsuario, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarVendaTotalTurno(int idUsuario, DateTime data, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable daodsTabela = new DataTable();
                daodsTabela = novaVenda.PesquisarVendaTotalTurno(idUsuario, data, numCaixa);
                return daodsTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaValorTotal(int idUsuario, DateTime data)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaValorTotal(idUsuario, data);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarBaixadoVenda(int idUsuario, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.AlterarBaixadoVenda(idUsuario, numCaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable PesquisaItemVenda(int itemAtual, int numVenda)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaItemVenda(itemAtual, numVenda);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarVendaCancelado(int idUsuario, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarVendaCancelado(idUsuario, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaVendaSetor(int idUsuario, DateTime data, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVendaSetor(idUsuario, data, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarSomaTotalDia(int idUsuario, DateTime data)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarSomaTotalDia(idUsuario, data);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message); ;
            }
        }

        public DataTable GerarRealtorioSetor1(int idUsuario, int setor, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.GerarRealtorioSetor1(idUsuario, setor, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GerarRealtorioSetor2(int idUsuario, int setor, int numCaixa)
        {

            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.GerarRealtorioSetor1(idUsuario, setor, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void GravarDadosSangria(int idUsuario, decimal valorSangria, string motivo, DateTime data, int numCaixa, bool tipo, bool fechado)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.GravarDadosSangria(idUsuario, valorSangria, data, motivo, numCaixa, tipo, fechado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarUltimaVenda()
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarUltimaVenda();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ReimprimirUltimaVenda(int ultimaVenda, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.ReimprimirUltimaVenda(ultimaVenda, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GerarRelatorioVendaTotal(int idUsuario, DateTime data)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.GerarRelatorioVendaTotal(idUsuario, data);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable SomaTotalGrid(int numCupom, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.SomaTotalGrid(numCupom, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaVendaNaoFechado(bool vendaNaoFechado, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisaVendaNaoFechado(vendaNaoFechado, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void FecharVenda(int codInternoprod)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.FecharVenda(codInternoprod);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarQtde_QtdeEstoque(int idUsuario, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarQtde_QtdeEstoque(idUsuario, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarProdutoItem(int numCupom, int itemAtual, int numCaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarProdutoItem(numCupom, itemAtual, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarVenda(int numCupom)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarVenda(numCupom);
                return dadosTabela;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public DataTable VendaX(int idUsuario, int numCaixa, string data)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.VendaX(idUsuario, numCaixa, data);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarVendaCancelada(int idUsuario, int numcaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarVendaCancelada(numcaixa, idUsuario);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarESinclonizarItensVenda(int itemA, int numVenda, int idVenda)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.AlterarESinclonizarItensVenda(itemA, numVenda, idVenda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarRelatorioTotalMes(int numcaixa, int mes)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarRelatorioTotalMes(numcaixa, mes);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarRelatorioAliquotaTotalMes(int numcaixa, int mes, int ano)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarRelatorioAliquotaTotalMes(numcaixa, mes, ano);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarDataVendaAberto(string numCaixaXml)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarDataVendaAberto(numCaixaXml);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ExcluirVenda(int numcaixa, int numCupom, int idUsuarioLogado)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.ExcluirVenda(numcaixa, numCupom, idUsuarioLogado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarRelatorioTotalMesTotalAliquota(int numcaixa, int dia,int ano)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarRelatorioTotalMesTotalAliquota(numcaixa, dia, ano);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarUltimaVendaNumCaixa(int numcaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarUltimaVendaNumCaixa(numcaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarEstoqueIncialMes(int numeroSetor)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarEstoqueIncialMes(numeroSetor);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GerarRelatorioVendaTotal(int idUsuario, int numcaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.GerarRelatorioVendaTotal(idUsuario, numcaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable GerarRealtorioEstoque(int idUsuario, int numcaixa)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.GerarRealtorioEstoque(idUsuario, numcaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarUltimaVenda(int numcaixa, int idUsuarioLogado)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaVenda.PesquisarUltimaVenda(idUsuarioLogado, numcaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void FecharVendaIdVenda(int idvenda)
        {
            try
            {
                novaVenda = new AcessoDados.VendaAcessoDados();
                novaVenda.FecharVendaIdVenda(idvenda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
