using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class EDILayOutRegMap : EntityTypeConfiguration<EDILayOutReg>
    {
        public EDILayOutRegMap()
        {
            // Primary Key
            this.HasKey(t => t.IDEDILayOutReg);

            // Properties
            this.Property(t => t.IDEDILayOutReg)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Descricao)
                .HasMaxLength(50);

            this.Property(t => t.Condicao)
                .HasMaxLength(100);

            this.Property(t => t.TabelaChave)
                .HasMaxLength(50);

            this.Property(t => t.CampoChave)
                .HasMaxLength(50);

            this.Property(t => t.TabelaPai)
                .HasMaxLength(50);

            this.Property(t => t.CampoPai)
                .HasMaxLength(50);

            this.Property(t => t.Obrigatorio)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("EDILayOutReg");
            this.Property(t => t.IDEDILayOutReg).HasColumnName("IDEDILayOutReg");
            this.Property(t => t.IDEDILayOut).HasColumnName("IDEDILayOut");
            this.Property(t => t.Descricao).HasColumnName("Descricao");
            this.Property(t => t.Ordem).HasColumnName("Ordem");
            this.Property(t => t.Pai).HasColumnName("Pai");
            this.Property(t => t.Condicao).HasColumnName("Condicao");
            this.Property(t => t.TabelaChave).HasColumnName("TabelaChave");
            this.Property(t => t.CampoChave).HasColumnName("CampoChave");
            this.Property(t => t.TabelaPai).HasColumnName("TabelaPai");
            this.Property(t => t.CampoPai).HasColumnName("CampoPai");
            this.Property(t => t.Obrigatorio).HasColumnName("Obrigatorio");

            // Relationships
            this.HasRequired(t => t.EDILayOut)
                .WithMany(t => t.EDILayOutRegs)
                .HasForeignKey(d => d.IDEDILayOut);
            this.HasOptional(t => t.EDILayOutReg2)
                .WithMany(t => t.EDILayOutReg1)
                .HasForeignKey(d => d.Pai);

        }
    }
}
