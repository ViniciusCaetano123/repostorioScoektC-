using Microsoft.AspNetCore.Mvc;
using System;
using WebScoket.Dao;
using WebScoket.Model;

namespace WebScoket.Controllers
{
    [Route("login")]
    public class LoginController : ControllerBase
    {
        LoginDao loginDao = new LoginDao();
        ChaveAcesso chaveAcesso = new ChaveAcesso();
        ClienteDao clienteDao = new ClienteDao();

        [HttpPost]
        [Route("logar/{token}")]
        public IActionResult PostBuscarDocumentos([FromBody] Login login, string token)
        {            
            if (token == chaveAcesso.Chave)
            {
                try
                {
                    Cliente cli = new Cliente();
                    cli = loginDao.Logar(cli, login);

                    if (!String.IsNullOrEmpty(cli.IdCliente))
                    {
                        if (clienteDao.QuantCli(cli) >= 1)
                        {
                            return NotFound("Limiti maximo atingido");
                        }
                        return Ok(cli.IdCliente.ToString());
                    }
                    else
                    {
                        return NotFound("E-mail/Senha inválido");
                    }
                }
                catch (Exception ex)
                {
                    return NotFound("Erro Conexão, tente novamente");
                }
            }
            else
            {
                return NotFound("Token Invalido");
            }
        }
    }
}
