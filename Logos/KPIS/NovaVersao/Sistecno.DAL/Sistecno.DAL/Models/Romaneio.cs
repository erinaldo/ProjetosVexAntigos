//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Sistecno.DAL.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Romaneio
    {
        public Romaneio()
        {
            this.Conferencia1 = new HashSet<Conferencia>();
            this.DocumentoOcorrencia = new HashSet<DocumentoOcorrencia>();
            this.DTRomaneio = new HashSet<DTRomaneio>();
            this.Lote = new HashSet<Lote>();
            this.MovimentacaoRomaneio = new HashSet<MovimentacaoRomaneio>();
            this.PalletDocumento = new HashSet<PalletDocumento>();
            this.Romaneio1 = new HashSet<Romaneio>();
            this.Romaneio11 = new HashSet<Romaneio>();
            this.RomaneioConferencia = new HashSet<RomaneioConferencia>();
            this.RomaneioDocumento = new HashSet<RomaneioDocumento>();
            this.RomaneioDocumentoConferencia = new HashSet<RomaneioDocumentoConferencia>();
            this.RomaneioOcorrencia = new HashSet<RomaneioOcorrencia>();
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
    
        public virtual ICollection<Conferencia> Conferencia1 { get; set; }
        public virtual DepositoPlantaLocalizacao DepositoPlantaLocalizacao { get; set; }
        public virtual ICollection<DocumentoOcorrencia> DocumentoOcorrencia { get; set; }
        public virtual ICollection<DTRomaneio> DTRomaneio { get; set; }
        public virtual Filial Filial { get; set; }
        public virtual Filial Filial1 { get; set; }
        public virtual ICollection<Lote> Lote { get; set; }
        public virtual Movimentacao Movimentacao { get; set; }
        public virtual ICollection<MovimentacaoRomaneio> MovimentacaoRomaneio { get; set; }
        public virtual ICollection<PalletDocumento> PalletDocumento { get; set; }
        public virtual Portaria Portaria { get; set; }
        public virtual Regiao Regiao { get; set; }
        public virtual ICollection<Romaneio> Romaneio1 { get; set; }
        public virtual Romaneio Romaneio2 { get; set; }
        public virtual ICollection<Romaneio> Romaneio11 { get; set; }
        public virtual Romaneio Romaneio3 { get; set; }
        public virtual ICollection<RomaneioConferencia> RomaneioConferencia { get; set; }
        public virtual ICollection<RomaneioDocumento> RomaneioDocumento { get; set; }
        public virtual ICollection<RomaneioDocumentoConferencia> RomaneioDocumentoConferencia { get; set; }
        public virtual ICollection<RomaneioOcorrencia> RomaneioOcorrencia { get; set; }
    }
}