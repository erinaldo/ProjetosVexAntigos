
namespace SistranMODEL
{
    public sealed class Usuario
    {
        public int UsuarioId { get; set; }
        public string UsuarioNome { get; set; }
        public string Login { get; set; }
        public int EmpresaId { get; set; }
        public string NomeEmpresa { get; set; }
        public string RazaoSocialNome { get; set; }
        public string Ativo { get; set; }

        public Usuario()
        { }

        public Usuario(int usuario, string usuarioNome, string login, int empresaId, string nomeEmpresa, string razaoSocialNome, string ativo)
        {
            UsuarioId = usuario;
            UsuarioNome = usuarioNome;
            Login = login;
            EmpresaId = empresaId;
            NomeEmpresa = nomeEmpresa;
            RazaoSocialNome = razaoSocialNome;
            Ativo = ativo;
        }

    }
}
