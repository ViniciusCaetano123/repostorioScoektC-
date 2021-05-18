using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebScoket.Dao
{
    public class ClienteDao
    {
        readonly Cl_conexao Sql = new Cl_conexao();
        public void AtualizarIdConexao(dynamic Context, string hash)
        {
            string query = $"UPDATE Cli_Site_Teste_a SET idConexao = '{Context.ConnectionId}' where  mac = '{hash}'";
            Sql.Conectar();
            SqlCommand comando = new SqlCommand(query, Sql.conexao);
            comando.ExecuteNonQuery();
            Sql.Desconectar();
        }
        public int QuantCli(Cliente cliente)
        {
            int quant = 0;
            string query = $"SELECT count(idCliente) as quant FROM Cli_Site_Teste_a   WHERE idCliente = '{cliente.IdCliente}'";
            Sql.Conectar();

            SqlCommand comando = new SqlCommand(query, Sql.conexao);
            SqlDataReader reader = comando.ExecuteReader();
            if (reader.Read())
            {
                quant = Convert.ToInt32(reader["quant"]);
            }
            Sql.Desconectar();
            return quant;
        }
        public Cliente InserirCli(Cliente cliente)
        {
            string query = "INSERT INTO Cli_Site_Teste_a(idCliente,mac,dataEntrada)VALUES(@idCliente,@mac,((sysdatetimeoffset() AT TIME ZONE 'E. South America Standard Time')))";
            Sql.Conectar();
            SqlCommand comando = new SqlCommand(query, Sql.conexao);
            comando.Parameters.Add("@idCliente", SqlDbType.NVarChar).Value = cliente.IdCliente;
            comando.Parameters.Add("@mac", SqlDbType.NVarChar).Value = cliente.Hash;
            comando.ExecuteNonQuery();
            Sql.Desconectar();
            return cliente;
        }
        public void DeletarCliConexetado(dynamic Context)
        {
            string query = $"DELETE TOP(1) FROM Cli_Site_Teste_a WHERE idConexao = '{Context.ConnectionId}'";
            Sql.Conectar();
            SqlCommand comando = new SqlCommand(query, Sql.conexao);
            comando.ExecuteNonQuery();
            Sql.Desconectar();
        }
        public int  QuantHash(Cliente cli)
        {
            int quantCliMAc = 0;
            string quantCliMAcStr = $"SELECT count(mac) FROM Cli_Site_Teste_a   WHERE mac = '{cli.Hash}'";
            Sql.Conectar();
            SqlCommand comando2 = new SqlCommand(quantCliMAcStr, Sql.conexao);
            SqlDataReader reader = comando2.ExecuteReader();
            if (reader.Read())
            {
                quantCliMAc = Convert.ToInt32(reader[0]);
            }
            Sql.Desconectar();
            return quantCliMAc;
        }
    }
}
