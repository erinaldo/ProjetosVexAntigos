using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            this.Avisoes = new List<Aviso>();
            this.BancoContas = new List<BancoConta>();
            this.ColetorConferencias = new List<ColetorConferencia>();
            this.ColetorConferenciaLogs = new List<ColetorConferenciaLog>();
            this.DocumentoOrcamentoes = new List<DocumentoOrcamento>();
            this.DTs = new List<DT>();
            this.DTs1 = new List<DT>();
            this.DTHistoricoes = new List<DTHistorico>();
            this.EdiControleDeArquivoes = new List<EdiControleDeArquivo>();
            this.EstoqueDivisaoMovs = new List<EstoqueDivisaoMov>();
            this.InventarioContagemProdutoes = new List<InventarioContagemProduto>();
            this.Mapas = new List<Mapa>();
            this.Mapas1 = new List<Mapa>();
            this.Movimentacaos = new List<Movimentacao>();
            this.MovimentacaoRomaneioItems = new List<MovimentacaoRomaneioItem>();
            this.Pallets = new List<Pallet>();
            this.Pallets1 = new List<Pallet>();
            this.Pallets2 = new List<Pallet>();
            this.Respostas = new List<Resposta>();
            this.RomaneioDocumentoConferencias = new List<RomaneioDocumentoConferencia>();
            this.RomaneioOcorrencias = new List<RomaneioOcorrencia>();
            this.RPCIs = new List<RPCI>();
            this.Tituloes = new List<Titulo>();
            this.TituloDuplicataHistoricoes = new List<TituloDuplicataHistorico>();
            this.TituloHistoricoes = new List<TituloHistorico>();
            this.UsuarioAlertas = new List<UsuarioAlerta>();
            this.UsuarioCentroDeCustoes = new List<UsuarioCentroDeCusto>();
            this.UsuarioClientes = new List<UsuarioCliente>();
            this.UsuarioCompras = new List<UsuarioCompra>();
            this.UsuarioFavoritos = new List<UsuarioFavorito>();
            this.UsuarioFilials = new List<UsuarioFilial>();
            this.UsuarioGrades = new List<UsuarioGrade>();
            this.UsuarioOperacaos = new List<UsuarioOperacao>();
            this.UsuarioPerfils = new List<UsuarioPerfil>();
            this.UsuarioPerfils1 = new List<UsuarioPerfil>();
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
        public virtual ICollection<Aviso> Avisoes { get; set; }
        public virtual ICollection<BancoConta> BancoContas { get; set; }
        public virtual Cadastro Cadastro { get; set; }
        public virtual ICollection<ColetorConferencia> ColetorConferencias { get; set; }
        public virtual ICollection<ColetorConferenciaLog> ColetorConferenciaLogs { get; set; }
        public virtual ICollection<DocumentoOrcamento> DocumentoOrcamentoes { get; set; }
        public virtual ICollection<DT> DTs { get; set; }
        public virtual ICollection<DT> DTs1 { get; set; }
        public virtual ICollection<DTHistorico> DTHistoricoes { get; set; }
        public virtual ICollection<EdiControleDeArquivo> EdiControleDeArquivoes { get; set; }
        public virtual ICollection<EstoqueDivisaoMov> EstoqueDivisaoMovs { get; set; }
        public virtual Grupo Grupo { get; set; }
        public virtual ICollection<InventarioContagemProduto> InventarioContagemProdutoes { get; set; }
        public virtual ICollection<Mapa> Mapas { get; set; }
        public virtual ICollection<Mapa> Mapas1 { get; set; }
        public virtual ICollection<Movimentacao> Movimentacaos { get; set; }
        public virtual ICollection<MovimentacaoRomaneioItem> MovimentacaoRomaneioItems { get; set; }
        public virtual ICollection<Pallet> Pallets { get; set; }
        public virtual ICollection<Pallet> Pallets1 { get; set; }
        public virtual ICollection<Pallet> Pallets2 { get; set; }
        public virtual ICollection<Resposta> Respostas { get; set; }
        public virtual ICollection<RomaneioDocumentoConferencia> RomaneioDocumentoConferencias { get; set; }
        public virtual ICollection<RomaneioOcorrencia> RomaneioOcorrencias { get; set; }
        public virtual ICollection<RPCI> RPCIs { get; set; }
        public virtual ICollection<Titulo> Tituloes { get; set; }
        public virtual ICollection<TituloDuplicataHistorico> TituloDuplicataHistoricoes { get; set; }
        public virtual ICollection<TituloHistorico> TituloHistoricoes { get; set; }
        public virtual ICollection<UsuarioAlerta> UsuarioAlertas { get; set; }
        public virtual ICollection<UsuarioCentroDeCusto> UsuarioCentroDeCustoes { get; set; }
        public virtual ICollection<UsuarioCliente> UsuarioClientes { get; set; }
        public virtual ICollection<UsuarioCompra> UsuarioCompras { get; set; }
        public virtual ICollection<UsuarioFavorito> UsuarioFavoritos { get; set; }
        public virtual ICollection<UsuarioFilial> UsuarioFilials { get; set; }
        public virtual ICollection<UsuarioGrade> UsuarioGrades { get; set; }
        public virtual ICollection<UsuarioOperacao> UsuarioOperacaos { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfils { get; set; }
        public virtual ICollection<UsuarioPerfil> UsuarioPerfils1 { get; set; }
    }
}
