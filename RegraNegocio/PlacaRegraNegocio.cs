using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
   public class PlacaRegraNegocio
    {
       AcessoDados.PlacaAcessoDados novaPlaca;

        public void InserirPlaca(int idVenda, string placa, string km, string data)
        {
            try
            {
                novaPlaca = new AcessoDados.PlacaAcessoDados();
                novaPlaca.InserirPlaca(idVenda, placa, km, data);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarPlaca(string numCupom)
        {
            try
            {
                novaPlaca = new AcessoDados.PlacaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaPlaca.PesquisarPlaca(numCupom);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
