using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class NumCaixaRegraNegocios
    {
        RegraNegocio.PagamentoVendaAcessoDados novoNumCaixa;

        public DataTable PesquisarNumCaixa_NumVenda(int numcaixa)
        {
            try
            {
                novoNumCaixa = new PagamentoVendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoNumCaixa.PesquisarNumCaixa_NumVenda(numcaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarNumVendaNumCaixa(int numCaixa, int numVenda)
        {
            try
            {
                novoNumCaixa = new PagamentoVendaAcessoDados();
                novoNumCaixa.PesquisarNumCaixa_NumVenda(numCaixa, numVenda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void FecharStatusCaixa(bool statusCaixa, int numCaixa)
        {
            try
            {
                novoNumCaixa = new PagamentoVendaAcessoDados();
                novoNumCaixa.FecharStatusCaixa(numCaixa, statusCaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AbrirCaixa(bool status, int numCaixa)
        {
            try
            {
                novoNumCaixa = new PagamentoVendaAcessoDados();
                novoNumCaixa.AbrirCaixa(numCaixa, status);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarEstatusCaixa(int numcaixa)
        {
            try
            {
                novoNumCaixa = new PagamentoVendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoNumCaixa.PesquisarEstatusCaixa(numcaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
