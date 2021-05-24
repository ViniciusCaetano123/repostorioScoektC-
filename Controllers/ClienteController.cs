using System;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using WebScoket.Dao;

namespace WebScoket
{

    [Produces("application/json")]
    [Route("cliente")]
    public class ClienteController : ControllerBase
    {

        readonly Cl_conexao Sql = new Cl_conexao();
        ClienteDao clienteDao = new ClienteDao();

        [HttpPost]
        [Route("verificaAcesso")]
        public Cliente GetClienteLimit([FromBody] Cliente cli)
        {
            var cliente = new Cliente();
            cliente = cli;
            if (cli.Hash == null)
            {
                cli.Hash = "";
            }
            bool strEmpty = !String.IsNullOrEmpty(cli.Hash);
            if (clienteDao.QuantHash(cliente) == 0)
            {
                if (clienteDao.QuantCli(cliente) >= 1)
                {
                    cliente.AcessLimit = true;
                    return cliente;
                }
                if (strEmpty)
                {
                    cliente.Hash = cli.GerarHash(cli.IdCliente);
                }
                cliente = clienteDao.InserirCli(cliente);
                cliente.AcessLimit = false;
                return cliente;
            }
            else
            {
                if (clienteDao.QuantCli(cliente) >= 1)
                {
                    cliente.AcessLimit = true;
                    return cliente;
                }
                cliente.AcessLimit = false;
                return cliente;
            }          
            

        }
    }
}
