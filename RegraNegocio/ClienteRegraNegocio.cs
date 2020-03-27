using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
   public class ClienteRegraNegocio
    {
       AcessoDados.ClienteAcessoDados novoCliente;
       DataTable dadosTabela = new DataTable();

        public DataTable ListaCliente()
        {
            try
            {
                novoCliente = new AcessoDados.ClienteAcessoDados();
                dadosTabela = new DataTable();
                dadosTabela = novoCliente.ListaCliente();
                return dadosTabela;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public DataTable PesquisarCliente(int idCliente)
        {
            try
            {
                novoCliente = new AcessoDados.ClienteAcessoDados();
                dadosTabela = new DataTable();
                dadosTabela = novoCliente.PesquisarCliente(idCliente);
                return dadosTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable PesquisarClienteNome(string nome)
        {
            try
            {
                novoCliente = new AcessoDados.ClienteAcessoDados();
                dadosTabela = new DataTable();
                dadosTabela = novoCliente.PesquisarClienteNome(nome);
                return dadosTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable PesquisarClienteCPF_CNPJ(string cpf)
        {
            try
            {
                novoCliente = new AcessoDados.ClienteAcessoDados();
                dadosTabela = new DataTable();
                dadosTabela = novoCliente.PesquisarClienteCPF_CNPJ(cpf);
                return dadosTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable PesquisarClienteRG_IE(string rg)
        {
            try
            {
                novoCliente = new AcessoDados.ClienteAcessoDados();
                dadosTabela = new DataTable();
                dadosTabela = novoCliente.PesquisarClienteRG_IE(rg);
                return dadosTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public DataTable ContadorCliente()
        {
            try
            {
                novoCliente = new AcessoDados.ClienteAcessoDados();
                dadosTabela = new DataTable();
                dadosTabela = novoCliente.ContadorCliente();
                return dadosTabela;
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        public DataTable PesquisarLimiteCliente(int idCliente)
        {
            try
            {
                novoCliente = new AcessoDados.ClienteAcessoDados();
                dadosTabela = new DataTable();
                dadosTabela = novoCliente.PesquisarLimiteCliente(idCliente);
                return dadosTabela;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void AtualizarGastoCliente(int idCliente, decimal gastoCliente)
        {
            try
            {
                novoCliente = new AcessoDados.ClienteAcessoDados();
                novoCliente.AtualizarGastoCliente(idCliente, gastoCliente);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
