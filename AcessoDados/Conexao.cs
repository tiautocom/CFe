using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AcessoDados
{
    public class Conexao
    {
        //Atributo que é a String de Conexão.
        private static string conexao = ConfigurationManager.ConnectionStrings["PVe.Properties.Settings.CFeConnectionString"].ToString();

        //Método acessor de leitura da String de Conexão.
        public static string stringConexao
        {
            get { return conexao; }
        }
    }
}
