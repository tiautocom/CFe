using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcessoDados;
using System.Data;

namespace RegraNegocio
{
    public class PagamentoVendaRegraNegocios
    {
        AcessoDados.PagamentoVendaAcessoDados novoPagamentoVenda;
        DataTable dadosTabela = new DataTable();

        public DataTable PesquisarTipoPagamentoVenda(int numCupom, int numCaixa)
        {
            try
            {
                novoPagamentoVenda = new AcessoDados.PagamentoVendaAcessoDados();
                dadosTabela = new DataTable();
                dadosTabela = novoPagamentoVenda.PesquisarTipoPagamentoVenda(numCupom, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public DataTable LeituraX(int numcaixa, int idUsuario, string data, string trib)
        {
            try
            {
                novoPagamentoVenda = new AcessoDados.PagamentoVendaAcessoDados();
                dadosTabela = new DataTable();
                dadosTabela = novoPagamentoVenda.LeituraX(numcaixa, idUsuario, data, trib);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
