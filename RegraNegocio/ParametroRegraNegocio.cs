using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class ParametroRegraNegocio
    {
        AcessoDados.ParametroAcessoDados novoParametro;

        public void SalvarVenda(bool numVenda, int numCupom)
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                novoParametro.SalvarParametro(numVenda, numCupom);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void SalvarParametro(bool abrirVenda, int numVenda)
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                novoParametro.SalvarParametro(abrirVenda, numVenda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarNumVendaFecharVenda(int novoNum, bool FecharCupom, int numCaixa)
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                novoParametro.AlterarParametroNumFecharVenda(novoNum, FecharCupom, numCaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarUltimoVenda()
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisarUltimaVenda();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void InserirNumVendaFecharVenda(int novoNum, bool baixado)
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                novoParametro.InseriParametroNumFecharVenda(novoNum, baixado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaUltimoMNumEntrada()
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisarUltimaVenda();
                return dadosTabela;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public DataTable PesquisarCodEtiqueta()
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisaCodEtiqueta();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaParametroE()
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisaParametroEtiqueta();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public void AlterarParametros(string crt, int numCupom, string codEtiqueta, string codEtiquetaBalanca, string status, string bairro, string cep, string cidade, string cnpj, string endereco, string ie, string im, string fantasia, string numero, string razaoSocial, string telefone, string uf, decimal aliquota, string impressora, Boolean autorizarPlaca, decimal valorVenda, int numCaixa, string msg, string portaCom, int bondRonte, string portaImpressora, Boolean autorizarTexto, bool cupomImagem, int qtdeCupom)
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                novoParametro.AlterarParametro( crt ,numCupom, codEtiqueta, codEtiquetaBalanca, status, bairro, cep, cidade, cnpj, endereco, ie, im, fantasia, numero, razaoSocial, telefone, uf, aliquota, impressora, autorizarPlaca, valorVenda, numCaixa, msg, portaCom, bondRonte, portaImpressora, autorizarTexto, cupomImagem, qtdeCupom);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaIdParametro(int numCupom)
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisaIdParametro(numCupom);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarStatusFechar(int _numVenda, int numCaixa)
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                novoParametro.AlterarStatusFechar(_numVenda, numCaixa);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarAbertura(int numCupom)
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                novoParametro.AlterarAbertuara(numCupom);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlterarNumVendaFecharVenda(int numVenda)
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                novoParametro.AlteraNumVenda(numVenda);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarPortaBalanca()
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisarPortaBalanca();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarImpressaoAutmoatica()
        {
            try
            {
                novoParametro = new AcessoDados.ParametroAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoParametro.PesquisarImpressaoAutmoatica();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
