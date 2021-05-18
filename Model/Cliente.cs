using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebScoket
{
    public class Cliente
    {
        public string IdCliente { get; set; }      
        public string Hash { get; set; }
        public bool AcessLimit { get; set; }
        public string GerarHash(string idCli)
        {
            string chars = "abcdefghjkmnpqrstuvwxyz023456789";
            string pass = "";
            Random random = new Random();
            for (int f = 0; f < 9; f++)
            {
                pass = pass + chars.Substring(random.Next(0, chars.Length - 1), 1);
            }
            return pass;
        }
    }
}
