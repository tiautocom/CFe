using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;

namespace PVe
{
    static class Program
    {
       public static frmPrincipal frmPrincipal;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Pega o nome do processo deste programa
            string nomeProcesso = Process.GetCurrentProcess().ProcessName;

            //Busca os processos com este nome que estão em execução
            Process[] processos = Process.GetProcessesByName(nomeProcesso);

            //Se já houver um aberto
            if (processos.Length > 1)
            {
                //Mostra mensagem de erro e finaliza
                MessageBox.Show("Não é possível abrir o Sistema. \nVerifique se o Programa já está sendo Executado.", "Sistema Executando", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Application.Exit();
            }
            //Caso contrário continue normalmente
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new frmVenda(frmPrincipal));
            }
        }
    }
}
