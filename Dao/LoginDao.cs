using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
namespace WebScoket.Dao
{
    public class LoginDao
    {
        readonly Cl_conexao Sql = new Cl_conexao();

        public Cliente Logar(Cliente cli, Login login)
        {
            string quantCliMAcStr = $"SELECT idcliente, cliente_liberado = (CASE WHEN liberado = 'Sim' THEN 'true' ELSE 'false' END) FROM geral_login WHERE login = '{login.Nome}' AND senha = '{login.Senha}'";
            Sql.Conectar();
            SqlCommand comando = new SqlCommand(quantCliMAcStr, Sql.conexao);
            SqlDataReader reader = comando.ExecuteReader();           
            if (reader.Read())
            {
                cli.IdCliente = Convert.ToString(reader["idCliente"]);
            }
            Sql.Desconectar();
            return cli;
        }
    }
}
