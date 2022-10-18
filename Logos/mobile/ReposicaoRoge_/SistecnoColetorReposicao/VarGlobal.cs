using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SistecnoColetor
{
    public static class VarGlobal
    {
        public static string Conexao { get; set; }
        public static  Classes.DTO.Usuario Usuario { get; set; }
        public static string NomePrograma { get; set; }

        //conexao temporaria
        public static string iip { get; set; }
        public static string iiporta { get; set; }

        //tempo
        public static string UltimaConexao { get; set; }

    }
}
