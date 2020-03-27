using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegraNegocio
{
    public class ContaReceberRegraNegocios
    {
        AcessoDados.ContaReceberAcessoDados novaContareceber;

        public void CadastrarContaReceber(int idPagamentoVenda, decimal valoRecebido, decimal valorReceber, DateTime dataVencimento, int idUsuario, decimal multa, decimal juros, bool baixado)
        {
            try
            {
                novaContareceber = new AcessoDados.ContaReceberAcessoDados();
                novaContareceber.CadastrarContaReceber(idPagamentoVenda, valoRecebido, valorReceber, dataVencimento, idUsuario, multa, juros, baixado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
