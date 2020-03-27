using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegraNegocio
{
    public class CuponsRegraNegocios
    {
        public static string MontarCupomElgin(string nomeCliente, string enderecoCliente, string numeroCliente, string bairroCliente, string cidadeCliente, string ufCliente, string cepCliente, string cnpjCliente, string ieCliente)
        {
            try
            {
                StringBuilder sb = new StringBuilder();

                sb.Append("------------------------------------------------/n");
                sb.Append(enderecoCliente.Trim() + ", " + numeroCliente.Trim().PadRight(49,' ') + "/n");
                sb.Append(bairroCliente.Trim() + ", " + cidadeCliente.Trim() + " - " + ufCliente.Trim().PadRight(49,' '));
                sb.Append("CEP: " + cepCliente.Trim().PadRight(49,' ') + "n");
                sb.Append("CNPJ: " + cnpjCliente.Trim() + "I.E: " + ieCliente.Trim().PadRight(49,' ') + "n");
                sb.Append("-------------------------------------------------\n");

                return sb.ToString();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
