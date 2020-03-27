using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class MateriaPrimaRegraNegocio
    {
        AcessoDados.MateriaPrimaAcessoDados novaMateriaPrima;

        public DataTable PesquisarQtdeEstoqueMateriaPrima(int idComposto)
        {
            try
            {
                novaMateriaPrima = new AcessoDados.MateriaPrimaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaMateriaPrima.PesquisarQtdeEstoqueMateriaPrima(idComposto);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void BaixaEstoqueMateriaPrima(int idComposto, decimal bemp)
        {
            try
            {
                novaMateriaPrima = new AcessoDados.MateriaPrimaAcessoDados();
                novaMateriaPrima.BaixaEstoqueMateriaPrima(idComposto, bemp);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
