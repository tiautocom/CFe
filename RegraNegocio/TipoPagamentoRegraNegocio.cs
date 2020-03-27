using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class TipoPagamentoRegraNegocio
    {
        AcessoDados.TipoPagamentoAcessoDados novoTipo;


        public DataTable PesquisaTipoPagamento()
        {
            try
            {
                novoTipo = new AcessoDados.TipoPagamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoTipo.PesquisaTipo();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaTipoPagamento(int idTipoPagamento)
        {
            try
            {
                novoTipo = new AcessoDados.TipoPagamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoTipo.PesquisaTipoPagamento(idTipoPagamento);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaTipoPagamento(string codTipoPagamento)
        {
            try
            {
                novoTipo = new AcessoDados.TipoPagamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoTipo.PesquisaTipoPagamento(codTipoPagamento);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
