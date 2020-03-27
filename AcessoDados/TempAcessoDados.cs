using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace AcessoDados
{
    public class TempAcessoDados
    {
        SqlCommand comandoSql = new SqlCommand();
        StringBuilder sql = new StringBuilder();
        DataTable dadosTabela = new DataTable();

        public void AlterarCpfCliente(string cpf, int tipo, string nome)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("update TEMP set  CPF_CNPJ=@CPF_CNPJ, TIPO=@TIPO, NOME=@NOME");

                    comandoSql.Parameters.Add(new SqlParameter("@CPF_CNPJ", cpf));
                    comandoSql.Parameters.Add(new SqlParameter("@TIPO", tipo));
                    comandoSql.Parameters.Add(new SqlParameter("@NOME", nome));

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método CadastroCpfCliente. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }

        public DataTable PesquisarCpfTemp()
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(Conexao.stringConexao))
                {
                    conexao.Open();

                    sql.Append("select * from TEMP");

                    comandoSql.CommandText = sql.ToString();
                    comandoSql.Connection = conexao;
                    dadosTabela.Load(comandoSql.ExecuteReader());
                    return dadosTabela;
                }
            }
            catch (Exception)
            {
                throw new Exception("Ocorreu um erro no método PesquisarCpfTemp. Caso o problema persista, entre em contato com o Administrador do Sistema.");
            }
        }
    }
}
