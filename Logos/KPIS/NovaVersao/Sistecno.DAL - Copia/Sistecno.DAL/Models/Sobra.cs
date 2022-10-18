using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Sobra
    {
        public int IdSobra { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> IdFilial { get; set; }
        public string NomeDoColaborador { get; set; }
        public Nullable<int> IdCliente { get; set; }
        public string NumeroNotaFiscal { get; set; }
        public string PreNotaFiscal { get; set; }
        public string TipoDeVolume { get; set; }
        public string DescricaoDoVolume { get; set; }
        public Nullable<decimal> Quantidade { get; set; }
        public Nullable<System.DateTime> DataDeEmbarqueDoVolume { get; set; }
        public string NomeMotoristaEmbarque { get; set; }
        public string PlacaCarretaEmbarque { get; set; }
        public string RotaDoVeiculo { get; set; }
        public string Finalizado { get; set; }
        public Nullable<System.DateTime> DataFinalizacao { get; set; }
        public Nullable<int> IdUsuarioBaizado { get; set; }
        public Nullable<int> IdFilialDestino { get; set; }
        public string NumeroRoteiro { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Filial Filial { get; set; }
    }
}
