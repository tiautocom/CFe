using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace RegraNegocio
{
    public class UsuarioRegraNegocio
    {
        AcessoDados.UsuarioAcessoDados novoUsuario;

        public DataTable PesquisaStatusUsuario()
        {
            try
            {
                novoUsuario = new AcessoDados.UsuarioAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoUsuario.PesquisaStatusUsuario();
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlteraStatusUsuarioAberto(string senha)
        {
            try
            {
                novoUsuario = new AcessoDados.UsuarioAcessoDados();
                novoUsuario.AlterarStatusUsuarioAberto(senha);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaStatusUsuario(string senha)
        {
            try
            {
                novoUsuario = new AcessoDados.UsuarioAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoUsuario.PesquisaStatusUsuario(senha);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaUsuarioLogado(string senha)
        {
            try
            {
                novoUsuario = new AcessoDados.UsuarioAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoUsuario.PesquisaUsuarioLogado(senha);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaUsuarioLogado(int numCaixa)
        {
            try
            {
                novoUsuario = new AcessoDados.UsuarioAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoUsuario.PesquisaUsuarioLogado(numCaixa);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AlteraStatusUsuarioFechado(int idUsuario)
        {
            try
            {
                novoUsuario = new AcessoDados.UsuarioAcessoDados();
                novoUsuario.AlterarStatusUsuarioFechado(idUsuario);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaLoginUsuario(string senha)
        {
            try
            {
                novoUsuario = new AcessoDados.UsuarioAcessoDados();
                DataTable dadosTabela = new DataTable();
                dadosTabela = novoUsuario.PesquisaStatusUsuario(senha);
                return dadosTabela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisarLoginAltorizado(string senha)
        {
            try
            {
                novoUsuario = new AcessoDados.UsuarioAcessoDados();
                DataTable dadosTanela = new DataTable();
                dadosTanela = novoUsuario.PesquisarLoginAltorizado(senha);
                return dadosTanela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable PesquisaLoginAtuante(string numCaixaXml)
        {
            try
            {
                novoUsuario = new AcessoDados.UsuarioAcessoDados();
                DataTable dadosTanela = new DataTable();
                dadosTanela = novoUsuario.PesquisaLoginAtuante(numCaixaXml);
                return dadosTanela;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
