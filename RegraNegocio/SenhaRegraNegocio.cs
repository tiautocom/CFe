using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class SenhaRegraNegocio
    {
        AcessoDados.SenhaAcessoDados novaSenha;

        public DataTable PesquisarSenhaCancelamentoVenda(string senha)
        {
            try
            {
                novaSenha = new AcessoDados.SenhaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaSenha.PesquisarSenhaCancelamentoVenda(senha);
                return dadosTabela;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public DataTable PesquisaSenhaLogado()
        {
            try
            {
                novaSenha = new AcessoDados.SenhaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaSenha.PesquisaSenhaLogado();
                return dadosTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
