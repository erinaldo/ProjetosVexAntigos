using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class MapaMap : EntityTypeConfiguration<Mapa>
    {
        public MapaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDMapa);

            // Properties
            this.Property(t => t.IDMapa)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.EstoqueProcessado)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.TIPO)
                .HasMaxLength(15);

            this.Property(t => t.Impresso)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("Mapa");
            this.Property(t => t.IDMapa).HasColumnName("IDMapa");
            this.Property(t => t.IDFilial).HasColumnName("IDFilial");
            this.Property(t => t.IDUsuario).HasColumnName("IDUsuario");
            this.Property(t => t.IDUsuarioProcessamento).HasColumnName("IDUsuarioProcessamento");
            this.Property(t => t.Numero).HasColumnName("Numero");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.EstoqueProcessado).HasColumnName("EstoqueProcessado");
            this.Property(t => t.DataDeProcessamento).HasColumnName("DataDeProcessamento");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.TIPO).HasColumnName("TIPO");
            this.Property(t => t.Impresso).HasColumnName("Impresso");
            this.Property(t => t.DataDeImpressao).HasColumnName("DataDeImpressao");

            // Relationships
            this.HasRequired(t => t.Filial)
                .WithMany(t => t.Mapas)
                .HasForeignKey(d => d.IDFilial);
            this.HasRequired(t => t.Usuario)
                .WithMany(t => t.Mapas)
                .HasForeignKey(d => d.IDUsuario);
            this.HasOptional(t => t.Usuario1)
                .WithMany(t => t.Mapas1)
                .HasForeignKey(d => d.IDUsuarioProcessamento);

        }
    }
}
