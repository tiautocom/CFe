using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class FechamentoVendaReegraNegocio
    {
        AcessoDados.FechamentoVendaAcessoDados novoFechamento;

        public void SalvarFechamentoVenda(int idCliente, int idTipoPagamento, decimal valor, string cnpj, int _numVenda, bool baixado, DateTime data, decimal troco, int idUsuario, bool fechado, int numCaixa)
        {
            try
            {
                novoFechamento = new AcessoDados.FechamentoVendaAcessoDados();
                novoFechamento.SalvarEntrada(idCliente, idTipoPagamento, valor, cnpj, _numVenda, baixado, data, troco, idUsuario, fechado, numCaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarUltimoPagamentoVenda(int numcaixa, int idUsuario)
        {
            try
            {
                novoFechamento = new AcessoDados.FechamentoVendaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoFechamento.PesquisarUltimoPagamentoVenda(numcaixa, idUsuario);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
