using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class VW_RE
    {
        public string IMPRIMIR { get; set; }
        public int IDDT { get; set; }
        public Nullable<int> IDROMANEIO { get; set; }
        public string REGIAO { get; set; }
        public int NUMERO { get; set; }
        public Nullable<System.DateTime> EMISSAO { get; set; }
        public string PLACA { get; set; }
        public string PMNOME { get; set; }
        public string TIPODEDT { get; set; }
        public Nullable<int> FLNUMERO { get; set; }
        public string FLNOME { get; set; }
        public string FILIAL { get; set; }
        public decimal TAXAADMINISTRATIVA { get; set; }
        public decimal IMPOSTO { get; set; }
        public decimal TAXADETRANFERENCIA { get; set; }
        public decimal SEGURO { get; set; }
        public string TRNOME { get; set; }
        public Nullable<decimal> CAPACIDADEDECARGAKG { get; set; }
        public Nullable<int> GERENTE { get; set; }
        public Nullable<int> ASSISTENTE { get; set; }
        public Nullable<int> LIDEROPERACIONAL { get; set; }
        public Nullable<int> CONFERENTE { get; set; }
        public Nullable<int> SEPARADOR { get; set; }
        public Nullable<int> LIMPEZA { get; set; }
        public Nullable<int> OUTROS { get; set; }
        public int EMPILHADOR { get; set; }
        public Nullable<decimal> VOLUMES { get; set; }
        public Nullable<decimal> VALORDANOTA { get; set; }
        public Nullable<decimal> PBRUTOTOTAL { get; set; }
        public Nullable<decimal> PESOBRUTO { get; set; }
        public Nullable<int> NOTASFISCAIS { get; set; }
        public Nullable<int> ENTREGAS { get; set; }
        public Nullable<decimal> FRETEMOTORISTARATEADO { get; set; }
        public Nullable<decimal> FRETEMOTORISTA { get; set; }
        public Nullable<decimal> FRETEEMPRESA { get; set; }
        public Nullable<decimal> REENTREGA { get; set; }
        public Nullable<int> QTD_REENTREGA { get; set; }
    }
}
