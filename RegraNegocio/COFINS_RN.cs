using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class COFINS_RN
    {
        AcessoDados.COFINS_AD novoCofins;

        public DataTable PesquisarCofins(string cstPis)
        {
            try
            {
                novoCofins = new AcessoDados.COFINS_AD();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoCofins.PesquisarCofins(cstPis);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
