using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
     public class CidadeRegraNegocio
    {
         AcessoDados.CidadeAcesso_Dados novaCidade;


         public DataTable PesquisarCidade()
         {
             try
             {
                 novaCidade = new AcessoDados.CidadeAcesso_Dados();
                 DataTable dadosTabela = new DataTable();
                 dadosTabela = novaCidade.PesquisarCidade();
                 return dadosTabela;
             }
             catch (Exception ex)
             {
                 throw new Exception(ex.Message);
             }
         }
    }
}
