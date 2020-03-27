using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class TribRegraNegocio
    {
        AcessoDados.TribAcessoDados novaTrib;
        DataTable dadosTabela = new DataTable();

        public DataTable PesquisarTribAll()
        {
            try
            {
                novaTrib = new AcessoDados.TribAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaTrib.PesquisarTribAll();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
