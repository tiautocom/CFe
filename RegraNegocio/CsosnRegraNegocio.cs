using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
   public class CsosnRegraNegocio
    {
       AcessoDados.CsosnAcessoDados novoCsosn;

        public DataTable PesquisarCsosnProduto()
        {
            try
            {
                novoCsosn = new AcessoDados.CsosnAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoCsosn.PesquisarCsosnProduto();
                return dadosTabela;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public DataTable PesquisarCsosnProduto(string cstProduto)
        {
            try
            {
                novoCsosn = new AcessoDados.CsosnAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoCsosn.PesquisarCsosnProduto(cstProduto);
                return dadosTabela;
            }
            catch (Exception)
            {
                throw;
            }

        }
    }
}
