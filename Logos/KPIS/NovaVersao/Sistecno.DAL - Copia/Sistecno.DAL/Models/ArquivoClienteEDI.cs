using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ArquivoClienteEDI
    {
        public string ChaveRegistro { get; set; }
        public string NomeDoArquivo { get; set; }
        public string Cliente { get; set; }
        public Nullable<System.DateTime> DataHoraImportacao { get; set; }
        public string TipoDeArquivo { get; set; }
        public string usuario { get; set; }
        public Nullable<decimal> TotalLinhas { get; set; }
        public Nullable<decimal> TotalPedidosNFS { get; set; }
        public Nullable<decimal> TotalNovos { get; set; }
        public string Situacao { get; set; }
        public Nullable<System.DateTime> DataHoraFimImportacao { get; set; }
        public string Arquivo { get; set; }
        public Nullable<int> Sequencia { get; set; }
        public Nullable<int> Fornecedor { get; set; }
        public Nullable<int> FornecedorFilial { get; set; }
        public string FilialLancamento { get; set; }
        public Nullable<System.DateTime> DataInicioReImportacao { get; set; }
        public Nullable<System.DateTime> DataFimReImportacao { get; set; }
        public Nullable<int> totalnovosImportacao { get; set; }
        public Nullable<int> TotalnovosreeImportacao { get; set; }
    }
}
