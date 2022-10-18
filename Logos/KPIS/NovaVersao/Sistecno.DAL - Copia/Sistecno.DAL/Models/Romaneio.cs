using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Romaneio
    {
        public Romaneio()
        {
            this.Conferencias = new List<Conferencia>();
            this.DocumentoOcorrencias = new List<DocumentoOcorrencia>();
            this.DTRomaneios = new List<DTRomaneio>();
            this.Lotes = new List<Lote>();
            this.MovimentacaoRomaneios = new List<MovimentacaoRomaneio>();
            this.PalletDocumentoes = new List<PalletDocumento>();
            this.Romaneio1 = new List<Romaneio>();
            this.Romaneio11 = new List<Romaneio>();
            this.RomaneioConferencias = new List<RomaneioConferencia>();
            this.RomaneioDocumentoes = new List<RomaneioDocumento>();
            this.RomaneioDocumentoConferencias = new List<RomaneioDocumentoConferencia>();
            this.RomaneioOcorrencias = new List<RomaneioOcorrencia>();
        }

        public int IDRomaneio { get; set; }
        public int IDFilial { get; set; }
        public Nullable<int> IDPortaria { get; set; }
        public Nullable<int> IDMovimentacao { get; set; }
        public int IDUsuario { get; set; }
        public Nullable<int> IDRomaneioOrigem { get; set; }
        public Nullable<int> IDDepositoPlantaLocalizacao { get; set; }
        public Nullable<int> IdRomaneioRelacionado { get; set; }
        public System.DateTime Emissao { get; set; }
        public Nullable<System.DateTime> Liberacao { get; set; }
        public string Tipo { get; set; }
        public string Divisao { get; set; }
        public string Conferencia { get; set; }
        public string Observacao1 { get; set; }
        public string Observacao2 { get; set; }
        public string Situacao { get; set; }
        public string Andamento { get; set; }
        public Nullable<System.DateTime> DataPlanejada { get; set; }
        public string GerarRomaneioDeTransportes { get; set; }
        public Nullable<System.DateTime> DataGeracaoDoArquivo { get; set; }
        public string NomeDoArquivo { get; set; }
        public string TipoDeCarga { get; set; }
        public Nullable<System.DateTime> InicioDoRecebimento { get; set; }
        public Nullable<System.DateTime> FinalDoRecebimento { get; set; }
        public Nullable<System.DateTime> LiberacaoDocRecebimento { get; set; }
        public Nullable<System.DateTime> InicioSeparacao { get; set; }
        public Nullable<System.DateTime> FinalDaSeparacao { get; set; }
        public Nullable<System.DateTime> LiberacaoDocSeparacao { get; set; }
        public Nullable<int> QtdePaleteExpedido { get; set; }
        public Nullable<int> QtdePaleteRecebido { get; set; }
        public Nullable<int> IdRegiao { get; set; }
        public Nullable<decimal> PalletsChep { get; set; }
        public Nullable<decimal> PalletsPbr { get; set; }
        public Nullable<int> OrdemDeCarregamento { get; set; }
        public Nullable<decimal> ValorDaNota { get; set; }
        public Nullable<decimal> Volumes { get; set; }
        public Nullable<decimal> VolumesComFator { get; set; }
        public Nullable<decimal> PesoBruto { get; set; }
        public Nullable<decimal> PesoCubado { get; set; }
        public Nullable<decimal> MetragemCubica { get; set; }
        public Nullable<decimal> IcmsIss { get; set; }
        public Nullable<decimal> FreteCredito { get; set; }
        public Nullable<decimal> FreteDebito { get; set; }
        public Nullable<int> idFilialDestino { get; set; }
        public Nullable<int> IdCadastro { get; set; }
        public Nullable<int> IdVeiculoTipo { get; set; }
        public Nullable<decimal> Entrega { get; set; }
        public Nullable<decimal> Grupos { get; set; }
        public Nullable<decimal> Setor { get; set; }
        public Nullable<int> Chapatex { get; set; }
        public string SeparadoPor { get; set; }
        public Nullable<int> IdPlaca { get; set; }
        public Nullable<int> IdVeiculo { get; set; }
        public Nullable<decimal> PercentualLucratividadeDT { get; set; }
        public Nullable<decimal> PercentualOcopacaoVeiculo { get; set; }
        public Nullable<decimal> Custos { get; set; }
        public Nullable<decimal> Liquido { get; set; }
        public Nullable<int> IdUsuarioAprovou { get; set; }
        public Nullable<System.DateTime> DataAprovacao { get; set; }
        public string ObsAprovacao { get; set; }
        public Nullable<int> IdMotorista { get; set; }
        public Nullable<decimal> PercentualMaximoFrete { get; set; }
        public string Lacres { get; set; }
        public virtual ICollection<Conferencia> Conferencias { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual ICollection<DocumentoOcorrencia> DocumentoOcorrencias { get; set; }
        public virtual ICollection<DTRomaneio> DTRomaneios { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Filial Filial1 { get; set; }
        public virtual ICollection<Lote> Lotes { get; set; }
        public virtual Movimentacao Movimentacao { get; set; }
        public virtual ICollection<MovimentacaoRomaneio> MovimentacaoRomaneios { get; set; }
        public virtual ICollection<PalletDocumento> PalletDocumentoes { get; set; }
        public virtual Portaria Portaria { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual ICollection<Romaneio> Romaneio1 { get; set; }
        public virtual Romaneio Romaneio2 { get; set; }
        public virtual ICollection<Romaneio> Romaneio11 { get; set; }
        public virtual Romaneio Romaneio3 { get; set; }
        public virtual ICollection<RomaneioConferencia> RomaneioConferencias { get; set; }
        public virtual ICollection<RomaneioDocumento> RomaneioDocumentoes { get; set; }
        public virtual ICollection<RomaneioDocumentoConferencia> RomaneioDocumentoConferencias { get; set; }
        public virtual ICollection<RomaneioOcorrencia> RomaneioOcorrencias { get; set; }
    }
}
