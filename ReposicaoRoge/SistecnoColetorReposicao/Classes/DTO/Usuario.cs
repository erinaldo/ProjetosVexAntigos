using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace SistecnoColetor.Classes.DTO
{
   public  class Usuario
    {
        public int IDUsuario { get; set; }
        public Nullable<int> IDCadastro { get; set; }
        public Nullable<int> IDGrupo { get; set; }
        public Nullable<int> IDPerfil { get; set; }
        public Nullable<int> UltimaEmpresa { get; set; }
        public Nullable<int> UltimaFilial { get; set; }
        public string NomeEmpresa { get; set; }
        public string NomeFilial { get; set; }

        public Nullable<int> EmpresaClienteLogado { get; set; }


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
    }
}
