using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AcessoDados;

namespace RegraNegocio
{
    public class SetorRegraNegocios
    {
        AcessoDados.SetorAcessoDados novoSetor;

        public DataTable PesquisarSetorNumero()
        {
            try
            {
                novoSetor = new SetorAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoSetor.PesquisarSetorNumero();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
