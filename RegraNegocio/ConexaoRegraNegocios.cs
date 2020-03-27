using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace RegraNegocio
{
    public class ConexaoRegraNegocios
    {
        AcessoDados.Conexao novoConexao;

        public static string conexao = ConfigurationManager.ConnectionStrings["PVe.Properties.Settings.CFeConnectionString"].ToString();

        //Método acessor de leitura da String de Conexão.
        public string stringConexao
        {
            get { return conexao; }
        }
    }
}
