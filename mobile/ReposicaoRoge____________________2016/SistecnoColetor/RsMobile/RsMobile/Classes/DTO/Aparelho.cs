using System;

using System.Collections.Generic;
using System.Text;

namespace RsMobile.Classes.DTO
{
    public class Aparelho
    {
        public string Chave {get; set;}
        public string Nome  {get; set;}
        public string Tempo  {get; set;}
        public string EnviaPosicaozerada {get; set;} 
        public string NumeroFone {get; set;}
        public string EnviaFoto { get; set; }
    }


    public class Sincronizacao
    {

        //Chave, DataInicial, dataFinal, Enviado, DT
        public string Chave { get; set; }
        public string DataInicial { get; set; }
        public string DataFinal { get; set; }
        public string Enviado { get; set; }
        public string DT { get; set; }        
    }
}
