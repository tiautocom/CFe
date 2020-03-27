using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AcessoDados;

namespace RegraNegocio
{
    public class SangriaRegraNegocios
    {
        AcessoDados.SangriaAcessoDados novaSangria;

        public DataTable PesquisarSangriaCaixa(int idUsuario, int numCaixa)
        {
            try
            {
                novaSangria = new AcessoDados.SangriaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaSangria.PesquisarSangriaCaixa(idUsuario, numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarSomaTotalDia(int idUsuario, bool fechado, int numCaixa, bool tipo)
        {
            try
            {
                novaSangria = new SangriaAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novaSangria.PesquisarSomaTotalDia(idUsuario, numCaixa, fechado, tipo);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void FecharSangria(int idUsuario, int numCaixa)
        {
            try
            {
                novaSangria = new SangriaAcessoDados();
                novaSangria.FecharSangria(idUsuario, numCaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
