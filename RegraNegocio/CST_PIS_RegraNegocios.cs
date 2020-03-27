using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class CST_PIS_RegraNegocios
    {
        AcessoDados.CST_PIS_AcessoDados novoPis;

        public DataTable PesquisarAliquotaPis(string cstPis)
        {
            try
            {
                novoPis = new AcessoDados.CST_PIS_AcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoPis.PesquisarAliquotaPis(cstPis);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
