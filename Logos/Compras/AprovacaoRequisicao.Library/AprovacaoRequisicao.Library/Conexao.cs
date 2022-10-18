using System;
using System.Collections.Generic;
using System.Text;

namespace AprovacaoRequisicao.Library
{
    public static class Conexao
    {
        public static string stringConexao()
        {
            return "Data Source=192.168.10.4;Initial Catalog=sa;User ID=sa;Password=@logos092005$";
        }
    }
}