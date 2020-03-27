using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class FormaPagamentoRegraNegocio
    {
        AcessoDados.FormaPagamentoAcessoDados novaFormaPgto;

        public DataTable PesquisarFromaPagamento()
        {
            try
            {
                novaFormaPgto = new AcessoDados.FormaPagamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaFormaPgto.PesquisarFormaPagtos();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarFromaPagamento(string codTipoPagamento)
        {
            try
            {
                novaFormaPgto = new AcessoDados.FormaPagamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaFormaPgto.PesquisarFormaPagtos(codTipoPagamento);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarFromaPagamento(int cod)
        {
            try
            {
                novaFormaPgto = new AcessoDados.FormaPagamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaFormaPgto.PesquisarFormaPagtos(cod);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarFPgto(DateTime data, int idUsuario, int numCaixa)
        {
            try
            {
                novaFormaPgto = new AcessoDados.FormaPagamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaFormaPgto.PesquisarFPgto(data, idUsuario, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlteraVendaPagamentoFechado(int idUsuario, int numCaixa)
        {
            try
            {
                novaFormaPgto = new AcessoDados.FormaPagamentoAcessoDados();
                novaFormaPgto.AlteraVendaPagamentoFechado(idUsuario, numCaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarUltimaVenda(int numCupom)
        {
            try
            {
                novaFormaPgto = new AcessoDados.FormaPagamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaFormaPgto.DevolucaoPagamentoVenda(numCupom);
                return dadosTabela;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DevolucaoPagamentoVenda(int idUsuario, decimal valorVendaDevolvido)
        {
            try
            {
                novaFormaPgto = new AcessoDados.FormaPagamentoAcessoDados();
                novaFormaPgto.DevolucaoPagamentoVenda(idUsuario, valorVendaDevolvido);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void DevolucaoPagamentosVenda(int idUsuario, int idCliente, int tipoP, decimal valorVendaDevolvido, string cnpj, int numVenda, bool baixado, DateTime data, decimal troco, bool fechado, int numCaixa)
        {
            try
            {
                novaFormaPgto = new AcessoDados.FormaPagamentoAcessoDados();
                novaFormaPgto.DevolucaoPagamentosVenda(idUsuario, idCliente, tipoP, valorVendaDevolvido, cnpj, numVenda, baixado, data, troco, fechado, numCaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
    }
}
