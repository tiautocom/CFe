using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace AcessoDados
{
    public class ClienteAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public DataTable ListaCliente()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM CLIENTE ORDER BY CLIENTE.NOME ASC");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Lista Cliente. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarCliente(int idCliente)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT NOME FROM CLIENTE where CLIENTE.CLIENTE_ID = @CLIENTE_ID");


                    comandoSql.Parameters.Add(new SqlParameter("@CLIENTE_ID", idCliente));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Lista Cliente. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarClienteNome(string nome)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM CLIENTE WHERE CLIENTE.NOME LIKE '%'+@NOME+'%'order by CLIENTE.NOME asc");


                    comandoSql.Parameters.Add(new SqlParameter("@NOME", nome));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Cliente Nome. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarClienteCPF_CNPJ(string cpf)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM CLIENTE WHERE CLIENTE.CPF_CNPJ LIKE '%'+@CPF_CNPJ+'%'order by CLIENTE.NOME asc");


                    comandoSql.Parameters.Add(new SqlParameter("@CPF_CNPJ", cpf));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Cliente CPF_CNPJ Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarClienteRG_IE(string rg)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM CLIENTE WHERE CLIENTE.RG_INSC_EST LIKE '%'+@RG_INSC_EST+'%'order by CLIENTE.NOME asc");


                    comandoSql.Parameters.Add(new SqlParameter("@RG_INSC_EST", rg));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Cliente CPF_CNPJ Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable ContadorCliente()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT COUNT (NOME) AS NOME FROM CLIENTE");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Contador Cliente Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarLimiteCliente(int idCliente)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("SELECT * FROM CLIENTE where CLIENTE.CLIENTE_ID = @CLIENTE_ID");

                    comandoSql.Parameters.Add(new SqlParameter("@CLIENTE_ID", idCliente));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método Pesquisar Limite Cliente. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public void AtualizarGastoCliente(int idCliente, decimal gastoCliente)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("UPDATE CLIENTE SET GASTO=@GASTO WHERE CLIENTE.CLIENTE_ID= @CLIENTE_ID");

                    comandoSql.Parameters.Add(new SqlParameter("@GASTO", gastoCliente));
                    comandoSql.Parameters.Add(new SqlParameter("@CLIENTE_ID", idCliente));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    comandoSql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um Erro no Atualizar Gasto Cliente. Caso o problema persista, entre em contato com o Administrador do Sistema");
            }
        }
    }
}
