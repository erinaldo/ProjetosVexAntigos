using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class TabelaDeFreteRotaMap : EntityTypeConfiguration<TabelaDeFreteRota>
    {
        public TabelaDeFreteRotaMap()
        {
            // Primary Key
            this.HasKey(t => t.IDTabelaDeFreteRota);

            // Properties
            this.Property(t => t.IDTabelaDeFreteRota)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("TabelaDeFreteRota");
            this.Property(t => t.IDTabelaDeFreteRota).HasColumnName("IDTabelaDeFreteRota");
            this.Property(t => t.IDTabelaDeFrete).HasColumnName("IDTabelaDeFrete");
            this.Property(t => t.IDFilialOrigem).HasColumnName("IDFilialOrigem");
            this.Property(t => t.IDFilialDestino).HasColumnName("IDFilialDestino");
            this.Property(t => t.IDRegiaoOrigem).HasColumnName("IDRegiaoOrigem");
            this.Property(t => t.IDRegiaoDestino).HasColumnName("IDRegiaoDestino");
            this.Property(t => t.IDCidadeOrigem).HasColumnName("IDCidadeOrigem");
            this.Property(t => t.IDCidadeDestino).HasColumnName("IDCidadeDestino");
            this.Property(t => t.IDEstadoOrigem).HasColumnName("IDEstadoOrigem");
            this.Property(t => t.IDEstadoDestino).HasColumnName("IDEstadoDestino");
            this.Property(t => t.IDRemetente).HasColumnName("IDRemetente");
            this.Property(t => t.IDDestinatario).HasColumnName("IDDestinatario");
            this.Property(t => t.IDTabelaDeFreteProduto).HasColumnName("IDTabelaDeFreteProduto");
            this.Property(t => t.FatorDeCubagem).HasColumnName("FatorDeCubagem");
            this.Property(t => t.Observacao).HasColumnName("Observacao");
            this.Property(t => t.PrazoDeEntrega).HasColumnName("PrazoDeEntrega");

            // Relationships
            this.HasOptional(t => t.Cidade)
                .WithMany(t => t.TabelaDeFreteRotas)
                .HasForeignKey(d => d.IDCidadeDestino);
            this.HasOptional(t => t.Cidade1)
                .WithMany(t => t.TabelaDeFreteRotas1)
                .HasForeignKey(d => d.IDCidadeOrigem);
            this.HasOptional(t => t.Estado)
                .WithMany(t => t.TabelaDeFreteRotas)
                .HasForeignKey(d => d.IDEstadoDestino);
            this.HasOptional(t => t.Estado1)
                .WithMany(t => t.TabelaDeFreteRotas1)
                .HasForeignKey(d => d.IDEstadoOrigem);
            this.HasOptional(t => t.Filial)
                .WithMany(t => t.TabelaDeFreteRotas)
                .HasForeignKey(d => d.IDFilialDestino);
            this.HasOptional(t => t.Filial1)
                .WithMany(t => t.TabelaDeFreteRotas1)
                .HasForeignKey(d => d.IDFilialOrigem);
            this.HasOptional(t => t.Regiao)
                .WithMany(t => t.TabelaDeFreteRotas)
                .HasForeignKey(d => d.IDRegiaoDestino);
            this.HasOptional(t => t.Regiao1)
                .WithMany(t => t.TabelaDeFreteRotas1)
                .HasForeignKey(d => d.IDRegiaoOrigem);
            this.HasRequired(t => t.TabelaDeFrete)
                .WithMany(t => t.TabelaDeFreteRotas)
                .HasForeignKey(d => d.IDTabelaDeFrete);

        }
    }
}
