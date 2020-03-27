using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcessoDados;

namespace RegraNegocio
{
    public class EstoqueInicialRegraNegocios
    {
        AcessoDados.EstoqueIncialAcessoDados novoEstoqueIncial;

        public void CadastraEstoqueIncialMes(decimal qtdeEstoqueInicial, DateTime data, int idUsuario, int numeroSetor)
        {
            try
            {
                novoEstoqueIncial = new EstoqueIncialAcessoDados();
                novoEstoqueIncial.CadastraEstoqueIncialMes(qtdeEstoqueInicial, data, idUsuario, numeroSetor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
