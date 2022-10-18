using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class EDILayOutRegCpo
    {
        public int IDEDILayOutRegCpo { get; set; }
        public int IDEDILayOutReg { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public string Formatacao { get; set; }
        public string Alinhamento { get; set; }
        public string Preenchimento { get; set; }
        public int Posicao { get; set; }
        public int Tamanho { get; set; }
        public int Multiplicador { get; set; }
        public string Tabela { get; set; }
        public string Campo { get; set; }
        public string ValorFixo { get; set; }
        public string Alias { get; set; }
        public string Condicao { get; set; }
        public string obs { get; set; }
        public string Obrigatorio { get; set; }
        public string Identificador { get; set; }
        public virtual EDILayOutReg EDILayOutReg { get; set; }
    }
}
