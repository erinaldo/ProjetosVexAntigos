using System;
using System.Collections.Generic;

namespace Sistecno.DAL.Models
{
    public partial class ModuloOpcao
    {
        public ModuloOpcao()
        {
            this.Ajudas = new List<Ajuda>();
            this.MenuClientes = new List<MenuCliente>();
            this.ModuloMenus = new List<ModuloMenu>();
            this.ModuloOpcaoAcaos = new List<ModuloOpcaoAcao>();
            this.ModuloOpcaoTabelas = new List<ModuloOpcaoTabela>();
            this.UsuarioOpcaos = new List<UsuarioOpcao>();
            this.TESControles = new List<TESControle>();
        }

        public int IDModuloOpcao { get; set; }
        public string Descricao { get; set; }
        public string Programa { get; set; }
        public string Pacote { get; set; }
        public string Ativo { get; set; }
        public Nullable<System.DateTime> Versao { get; set; }
        public string Tipo { get; set; }
        public string Link { get; set; }
        public string Status { get; set; }
        public virtual ICollection<Ajuda> Ajudas { get; set; }
        public virtual ICollection<MenuCliente> MenuClientes { get; set; }
        public virtual ICollection<ModuloMenu> ModuloMenus { get; set; }
        public virtual ICollection<ModuloOpcaoAcao> ModuloOpcaoAcaos { get; set; }
        public virtual ICollection<ModuloOpcaoTabela> ModuloOpcaoTabelas { get; set; }
        public virtual ICollection<UsuarioOpcao> UsuarioOpcaos { get; set; }
        public virtual ICollection<TESControle> TESControles { get; set; }
    }
}
