using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class DepartamentoRegraNegocio
    {
        AcessoDados.DepartamentoAcessoDados novoDepartmento;

        public DataTable PesquisarDepartamento()
        {
            try
            {
                novoDepartmento = new AcessoDados.DepartamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoDepartmento.PesquisarDepartamento();
                return dadosTabela;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PopularCb()
        {
            try
            {
                novoDepartmento = new AcessoDados.DepartamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoDepartmento.PopularCbDepartemnto();
                return dadosTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable PesquisarDepartamento(int numDep)
        {
            try
            {
                novoDepartmento = new AcessoDados.DepartamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoDepartmento.PopularCbDepartemnto();
                return dadosTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable PopularCb(string numDep)
        {
            try
            {
                novoDepartmento = new AcessoDados.DepartamentoAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoDepartmento.PopularCbDepartemnto(numDep);
                return dadosTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
