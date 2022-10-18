using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteTipoDeMaterialDivisaoMap : EntityTypeConfiguration<ClienteTipoDeMaterialDivisao>
    {
        public ClienteTipoDeMaterialDivisaoMap()
        {
            // Primary Key
            this.HasKey(t => t.IdClienteTipoDeMaterialDivisao);

            // Properties
            this.Property(t => t.IdClienteTipoDeMaterialDivisao)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("ClienteTipoDeMaterialDivisao");
            this.Property(t => t.IdClienteTipoDeMaterialDivisao).HasColumnName("IdClienteTipoDeMaterialDivisao");
            this.Property(t => t.IdClienteTipoDeMaterial).HasColumnName("IdClienteTipoDeMaterial");
            this.Property(t => t.IdClienteDivisao).HasColumnName("IdClienteDivisao");
        }
    }
}
