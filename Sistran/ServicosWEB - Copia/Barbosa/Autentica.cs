using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServicosWEB.Barbosa
{
    public   class Autentica
    {
        public  string login { get; set; }
        public  string senha { get; set; }


        public static bool Autenticar( string login, string senha)
        {
            if ((login == "wsbarbosa" && senha == "20191106") || (login == "wsbarbosa_prod" && senha == "prod_321500"))
                return true;
            else
                return false;
        }
    }

    public class Retorno
    {
        public bool Sucesso { get; set; }
        public string Erro { get; set; }

    }
}