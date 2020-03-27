using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class CartaoRegraNegocio
    {
        AcessoDados.CartaoAcessoDados novoCartao;

        public DataTable PesquisarCartao()
        {
            try
            {
                novoCartao = new AcessoDados.CartaoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoCartao.PesquisarCartao();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
