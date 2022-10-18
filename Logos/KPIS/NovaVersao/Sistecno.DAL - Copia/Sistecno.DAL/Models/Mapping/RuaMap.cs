using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class RuaMap : EntityTypeConfiguration<Rua>
    {
        public RuaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDRua, t.Cep, t.Nome });

            // Properties
            this.Property(t => t.IDRua)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Cep)
                .IsRequired()
                .IsFixedLength()
                .HasMaxLength(8);

            this.Property(t => t.Tipo)
                .HasMaxLength(30);

            this.Property(t => t.Preposicao)
                .HasMaxLength(10);

            this.Property(t => t.Titulo)
                .HasMaxLength(80);

            this.Property(t => t.Nome)
                .IsRequired()
                .HasMaxLength(80);

            this.Property(t => t.Complemento)
                .HasMaxLength(40);

            this.Property(t => t.TemGrandeUsuario)
                .IsFixedLength()
                .HasMaxLength(1);

            this.Property(t => t.TipoDeRegistro)
                .IsFixedLength()
                .HasMaxLength(1);

            // Table & Column Mappings
            this.ToTable("Rua");
            this.Property(t => t.IDRua).HasColumnName("IDRua");
            this.Property(t => t.IDCidade).HasColumnName("IDCidade");
            this.Property(t => t.IDBairroInicial).HasColumnName("IDBairroInicial");
            this.Property(t => t.IDBairroFinal).HasColumnName("IDBairroFinal");
            this.Property(t => t.Cep).HasColumnName("Cep");
            this.Property(t => t.Tipo).HasColumnName("Tipo");
            this.Property(t => t.Preposicao).HasColumnName("Preposicao");
            this.Property(t => t.Titulo).HasColumnName("Titulo");
            this.Property(t => t.Nome).HasColumnName("Nome");
            this.Property(t => t.Complemento).HasColumnName("Complemento");
            this.Property(t => t.TemGrandeUsuario).HasColumnName("TemGrandeUsuario");
            this.Property(t => t.TipoDeRegistro).HasColumnName("TipoDeRegistro");
        }
    }
}
