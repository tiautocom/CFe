using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class FabricanteRegraNegocio
    {
        AcessoDados.FabricanteAcessoDados novoFabricante;

        public DataTable PesquisarFabricante()
        {
            try
            {
                novoFabricante = new AcessoDados.FabricanteAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoFabricante.PesquisarFabricante();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
