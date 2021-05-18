
using Microsoft.AspNetCore.SignalR;
using System;
using System.Data.SqlClient;

using System.Threading.Tasks;
using WebScoket.Dao;

namespace WebScoket
{
    public class GameHub : Hub
    {
        ClienteDao clienteDao = new ClienteDao();    

        public void AllCliente(string mac)
        {
            clienteDao.AtualizarIdConexao(Context, mac);
        }        
        public override Task OnDisconnectedAsync(Exception exception)
        {
            clienteDao.DeletarCliConexetado(Context);
             return base.OnDisconnectedAsync(exception);
        }
        

    }
}
