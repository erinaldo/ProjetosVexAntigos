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
    
    public partial class Usuario
    {
        public Usuario()
        {
            this.Aviso = new HashSet<Aviso>();
            this.BancoConta = new HashSet<BancoConta>();
            this.ColetorConferencia = new HashSet<ColetorConferencia>();
            this.ColetorConferenciaLog = new HashSet<ColetorConferenciaLog>();
            this.DocumentoOrcamento = new HashSet<DocumentoOrcamento>();
            this.DT = new HashSet<DT>();
            this.DT1 = new HashSet<DT>();
            this.DTHistorico = new HashSet<DTHistorico>();
            this.EdiControleDeArquivo = new HashSet<EdiControleDeArquivo>();
            this.EstoqueDivisaoMov = new HashSet<EstoqueDivisaoMov>();
            this.InventarioContagemProduto = new HashSet<InventarioContagemProduto>();
            this.Mapa = new HashSet<Mapa>();
            this.Mapa1 = new HashSet<Mapa>();
            this.Movimentacao = new HashSet<Movimentacao>();
            this.MovimentacaoRomaneioItem = new HashSet<MovimentacaoRomaneioItem>();
            this.Pallet = new HashSet<Pallet>();
            this.Pallet1 = new HashSet<Pallet>();
            this.Pallet2 = new HashSet<Pallet>();
            this.Respostas = new HashSet<Respostas>();
            this.RomaneioDocumentoConferencia = new HashSet<RomaneioDocumentoConferencia>();
            this.RomaneioOcorrencia = new HashSet<RomaneioOcorrencia>();
            this.RPCI = new HashSet<RPCI>();
            this.Titulo = new HashSet<Titulo>();
            this.TituloDuplicataHistorico = new HashSet<TituloDuplicataHistorico>();
            this.TituloHistorico = new HashSet<TituloHistorico>();
            this.UsuarioAlerta = new HashSet<UsuarioAlerta>();
            this.UsuarioCentroDeCusto = new HashSet<UsuarioCentroDeCusto>();
            this.UsuarioCliente = new HashSet<UsuarioCliente>();
            this.UsuarioCompra = new HashSet<UsuarioCompra>();
            this.UsuarioFavoritos = new HashSet<UsuarioFavoritos>();
            this.UsuarioFilial = new HashSet<UsuarioFilial>();
            this.UsuarioGrade = new HashSet<UsuarioGrade>();
            this.UsuarioOperacao = new HashSet<UsuarioOperacao>();
            this.UsuarioPerfil = new HashSet<UsuarioPerfil>();
            this.UsuarioPerfil1 = new HashSet<UsuarioPerfil>();
        }
    
        public int IDUsuario { get; set; }
        public Nullable<int> IDCadastro { get; set; }
        public Nullable<int> IDGrupo { get; set; }
        public Nullable<int> IDPerfil { get; set; }
        public Nullable<int> UltimaEmpresa { get; set; }
        public Nullable<int> UltimaFilial { get; set; }
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public string CriaUsuario { get; set; }
        public string Administrador { get; set; }
        public string AutoOcultarMenu { get; set; }
        public Nullable<int> LarguraMenu { get; set; }
        public Nullable<int> AlturaFavoritos { get; set; }
        public Nullable<System.DateTime> DataDeCadastro { get; set; }
        public string Tipo { get; set; }
        public string Ativo { get; set; }
        public string AtivaProtecaoTela { get; set; }
        public Nullable<System.DateTime> SenhaValidaAte { get; set; }
        public string TipoDeSistema { get; set; }
        public Nullable<decimal> ValorLimite { get; set; }
        public Nullable<int> ExpirarSenha { get; set; }
        public string AlterarSenhaNoLogin { get; set; }
        public string Site { get; set; }
        public string ValidarUsuarioNoBD { get; set; }
        public Nullable<System.DateTime> UltimoAcesso { get; set; }
        public string DadosMaquinaLocal { get; set; }
    
        public virtual ICollection<Aviso> Aviso { get; set; }
        public virtual ICollection<BancoConta> BancoConta { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual ICollection<ColetorConferencia> ColetorConferencia { get; set; }
        public virtual ICollection<ColetorConferenciaLog> ColetorConferenciaLog { get; set; }
        public virtual ICollection<DocumentoOrcamento> DocumentoOrcamento { get; set; }
        public virtual ICollection<DT> DT { get; set; }
        public virtual ICollection<DT> DT1 { get; set; }
        public virtual ICollection<DTHistorico> DTHistorico { get; set; }
        public virtual ICollection<EdiControleDeArquivo> EdiControleDeArquivo { get; set; }
        public virtual ICollection<EstoqueDivisaoMov> EstoqueDivisaoMov { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual ICollection<InventarioContagemProduto> InventarioContagemProduto { get; set; }
        public virtual ICollection<Mapa> Mapa { get; set; }
        public virtual ICollection<Mapa> Mapa1 { get; set; }
        public virtual ICollection<Movimentacao> Movimentacao { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItem { get; set; }
        public virtual ICollection<Pallet> Pallet { get; set; }
        public virtual ICollection<Pallet> Pallet1 { get; set; }
        public virtual ICollection<Pallet> Pallet2 { get; set; }
        public virtual ICollection<Respostas> Respostas { get; set; }
        public virtual ICollection<RomaneioDocumentoConferencia> RomaneioDocumentoConferencia { get; set; }
        public virtual ICollection<RomaneioOcorrencia> RomaneioOcorrencia { get; set; }
        public virtual ICollection<RPCI> RPCI { get; set; }
        public virtual ICollection<Titulo> Titulo { get; set; }
        public virtual ICollection<TituloDuplicataHistorico> TituloDuplicataHistorico { get; set; }
        public virtual ICollection<TituloHistorico> TituloHistorico { get; set; }
        public virtual ICollection<UsuarioAlerta> UsuarioAlerta { get; set; }
        public virtual ICollection<UsuarioCentroDeCusto> UsuarioCentroDeCusto { get; set; }
        public virtual ICollection<UsuarioCliente> UsuarioCliente { get; set; }
        public virtual ICollection<UsuarioCompra> UsuarioCompra { get; set; }
        public virtual ICollection<UsuarioFavoritos> UsuarioFavoritos { get; set; }
        public virtual ICollection<UsuarioFilial> UsuarioFilial { get; set; }
        public virtual ICollection<UsuarioGrade> UsuarioGrade { get; set; }
        public virtual ICollection<UsuarioOperacao> UsuarioOperacao { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfil { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfil1 { get; set; }
    }
}