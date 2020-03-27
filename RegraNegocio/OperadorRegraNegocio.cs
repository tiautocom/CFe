using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
   public class OperadorRegraNegocio
    {
       AcessoDados.OperadorAcessoDados novoOperador;

        public DataTable PesquisarGridOperador()
        {
            try
            {
                novoOperador = new AcessoDados.OperadorAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoOperador.PesquisarGridOperador();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarOperadorAtuante()
        {
            try
            {
                novoOperador = new AcessoDados.OperadorAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoOperador.PesquisarOperadorAtuante();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
