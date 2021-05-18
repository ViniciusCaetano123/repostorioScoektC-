using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebScoket
{
    public class Cl_conexao
    {

        public SqlConnection conexao = new SqlConnection("Data Source=lefisc.database.windows.net;Initial Catalog=lefisc_x;User ID=lefisc;Password=W5o6k6y0;");

        public void Conectar()
        {
            try
            {
                conexao.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                conexao.Dispose();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Desconectar()
        {
            try
            {
                conexao.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
