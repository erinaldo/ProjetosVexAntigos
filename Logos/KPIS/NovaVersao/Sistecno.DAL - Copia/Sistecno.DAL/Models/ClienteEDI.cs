using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ClienteEDI
    {
        public int IDClienteEDI { get; set; }
        public int IDCliente { get; set; }
        public int IDEDI { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Ativo { get; set; }
        public string NomePadraoDoArquivo { get; set; }
        public string CaixaPostalDoCliente { get; set; }
        public string CaixaPostalDaTransportadora { get; set; }
        public string Sequencia { get; set; }
        public string UsaSequencia { get; set; }
        public string SequenciaDiaria { get; set; }
        public string PastaPadrao { get; set; }
        public Nullable<System.DateTime> Inicio { get; set; }
        public string HoraInicio { get; set; }
        public Nullable<int> SeqArquivos { get; set; }
        public string servidor { get; set; }
        public string conexao { get; set; }
        public string usuarioconexao { get; set; }
        public string senhaconexao { get; set; }
        public string portaconexao { get; set; }
        public string hostconexao { get; set; }
        public string PastaBackup { get; set; }
        public string Seg { get; set; }
        public string Ter { get; set; }
        public string Qua { get; set; }
        public string Qui { get; set; }
        public string Sex { get; set; }
        public string Sab { get; set; }
        public string Dom { get; set; }
        public Nullable<int> IdFilialImportacao { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual EDI EDI { get; set; }
        public virtual Filial Filial { get; set; }
    }
}
