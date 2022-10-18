using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ReposicaoRoge
    {
        public ReposicaoRoge()
        {
            this.ReposicaoRogeItems = new List<ReposicaoRogeItem>();
            this.ReposicaoRogeVolumes = new List<ReposicaoRogeVolume>();
        }

        public int IdReposicaoRoge { get; set; }
        public string Chave { get; set; }
        public string IdNota { get; set; }
        public string CodigoRegiao { get; set; }
        public string NomeRegiao { get; set; }
        public Nullable<System.DateTime> DataDaInclusao { get; set; }
        public string UsuarioColetor { get; set; }
        public Nullable<System.DateTime> DataColetor { get; set; }
        public string UsuarioEnvioRoge { get; set; }
        public Nullable<System.DateTime> DataEnvioRoge { get; set; }
        public string CodigoEnvioRoge { get; set; }
        public string DescricaoEnvioRoge { get; set; }
        public Nullable<int> QuantidadeDeProduto { get; set; }
        public string Status { get; set; }
        public Nullable<System.DateTime> Inicio { get; set; }
        public Nullable<System.DateTime> Fim { get; set; }
        public string ClienteEspecial { get; set; }
        public string UsuarioEnvioAuditoria { get; set; }
        public Nullable<System.DateTime> DataEnvioAuditoria { get; set; }
        public Nullable<decimal> Valor { get; set; }
        public Nullable<System.DateTime> MercadoriaRecebida { get; set; }
        public string UsuarioMercadoriaRecebida { get; set; }
        public string StatusMercadoriaRecebida { get; set; }
        public string Observacao { get; set; }
        public virtual ICollection<ReposicaoRogeItem> ReposicaoRogeItems { get; set; }
        public virtual ICollection<ReposicaoRogeVolume> ReposicaoRogeVolumes { get; set; }
    }
}
