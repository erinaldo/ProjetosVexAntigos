using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class ClienteEDIMap : EntityTypeConfiguration<ClienteEDI>
    {
        public ClienteEDIMap()
        {
            // Primary Key
            this.HasKey(t => t.IDClienteEDI);

            // Properties
            this.Property(t => t.IDClienteEDI)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.Ativo)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.NomePadraoDoArquivo)
                .HasMaxLength(100);

            this.Property(t => t.CaixaPostalDoCliente)
                .HasMaxLength(50);

            this.Property(t => t.CaixaPostalDaTransportadora)
                .HasMaxLength(50);

            this.Property(t => t.Sequencia)
                .HasMaxLength(10);

            this.Property(t => t.UsaSequencia)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.SequenciaDiaria)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PastaPadrao)
                .HasMaxLength(300);

            this.Property(t => t.HoraInicio)
                .HasMaxLength(5);

            this.Property(t => t.servidor)
                .HasMaxLength(50);

            this.Property(t => t.conexao)
                .HasMaxLength(50);

            this.Property(t => t.usuarioconexao)
                .HasMaxLength(50);

            this.Property(t => t.senhaconexao)
                .HasMaxLength(50);

            this.Property(t => t.portaconexao)
                .HasMaxLength(50);

            this.Property(t => t.hostconexao)
                .HasMaxLength(50);

            this.Property(t => t.PastaBackup)
                .HasMaxLength(300);

            this.Property(t => t.Seg)
                .IsFixedLength()
                .HasMaxLength(30);

            this.Property(t => t.Ter)
                .IsFixedLength()
                .HasMaxLength(30);

            this.Property(t => t.Qua)
                .IsFixedLength()
                .HasMaxLength(30);

            this.Property(t => t.Qui)
                .IsFixedLength()
                .HasMaxLength(30);

            this.Property(t => t.Sex)
                .IsFixedLength()
                .HasMaxLength(30);

            this.Property(t => t.Sab)
                .IsFixedLength()
                .HasMaxLength(30);

            this.Property(t => t.Dom)
                .IsFixedLength()
                .HasMaxLength(30);

            // Table & Column Mappings
            this.ToTable("ClienteEDI");
            this.Property(t => t.IDClienteEDI).HasColumnName("IDClienteEDI");
            this.Property(t => t.IDCliente).HasColumnName("IDCliente");
            this.Property(t => t.IDEDI).HasColumnName("IDEDI");
            this.Property(t => t.DataDeCadastro).HasColumnName("DataDeCadastro");
            this.Property(t => t.Ativo).HasColumnName("Ativo");
            this.Property(t => t.NomePadraoDoArquivo).HasColumnName("NomePadraoDoArquivo");
            this.Property(t => t.CaixaPostalDoCliente).HasColumnName("CaixaPostalDoCliente");
            this.Property(t => t.CaixaPostalDaTransportadora).HasColumnName("CaixaPostalDaTransportadora");
            this.Property(t => t.Sequencia).HasColumnName("Sequencia");
            this.Property(t => t.UsaSequencia).HasColumnName("UsaSequencia");
            this.Property(t => t.SequenciaDiaria).HasColumnName("SequenciaDiaria");
            this.Property(t => t.PastaPadrao).HasColumnName("PastaPadrao");
            this.Property(t => t.Inicio).HasColumnName("Inicio");
            this.Property(t => t.HoraInicio).HasColumnName("HoraInicio");
            this.Property(t => t.SeqArquivos).HasColumnName("SeqArquivos");
            this.Property(t => t.servidor).HasColumnName("servidor");
            this.Property(t => t.conexao).HasColumnName("conexao");
            this.Property(t => t.usuarioconexao).HasColumnName("usuarioconexao");
            this.Property(t => t.senhaconexao).HasColumnName("senhaconexao");
            this.Property(t => t.portaconexao).HasColumnName("portaconexao");
            this.Property(t => t.hostconexao).HasColumnName("hostconexao");
            this.Property(t => t.PastaBackup).HasColumnName("PastaBackup");
            this.Property(t => t.Seg).HasColumnName("Seg");
            this.Property(t => t.Ter).HasColumnName("Ter");
            this.Property(t => t.Qua).HasColumnName("Qua");
            this.Property(t => t.Qui).HasColumnName("Qui");
            this.Property(t => t.Sex).HasColumnName("Sex");
            this.Property(t => t.Sab).HasColumnName("Sab");
            this.Property(t => t.Dom).HasColumnName("Dom");
            this.Property(t => t.IdFilialImportacao).HasColumnName("IdFilialImportacao");

            // Relationships
            this.HasRequired(t => t.Cliente)
                .WithMany(t => t.ClienteEDIs)
                .HasForeignKey(d => d.IDCliente);
            this.HasRequired(t => t.EDI)
                .WithMany(t => t.ClienteEDIs)
                .HasForeignKey(d => d.IDEDI);
            this.HasOptional(t => t.Filial)
                .WithMany(t => t.ClienteEDIs)
                .HasForeignKey(d => d.IdFilialImportacao);

        }
    }
}
