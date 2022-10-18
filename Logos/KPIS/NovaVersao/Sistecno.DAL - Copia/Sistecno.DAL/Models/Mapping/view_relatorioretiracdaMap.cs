using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Sistecno.DAL.Models.Mapping
{
    public class view_relatorioretiracdaMap : EntityTypeConfiguration<view_relatorioretiracda>
    {
        public view_relatorioretiracdaMap()
        {
            // Primary Key
            this.HasKey(t => new { t.IDPedido, t.PedIDCliente });

            // Properties
            this.Property(t => t.IDPedido)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PedIDCliente)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.PedEntradaSaida)
                .HasMaxLength(10);

            this.Property(t => t.TipoPedido)
                .HasMaxLength(20);

            this.Property(t => t.TipoNota)
                .HasMaxLength(20);

            this.Property(t => t.NotaEntradaSaida)
                .HasMaxLength(10);

            this.Property(t => t.Serie)
                .HasMaxLength(20);

            this.Property(t => t.Solicitante)
                .HasMaxLength(20);

            this.Property(t => t.Origem)
                .HasMaxLength(10);

            this.Property(t => t.Cidade)
                .HasMaxLength(80);

            this.Property(t => t.Uf)
                .IsFixedLength()
                .HasMaxLength(2);

            this.Property(t => t.CNPJ_Destinatario)
                .HasMaxLength(20);

            this.Property(t => t.Nome_Fantasia)
                .HasMaxLength(30);

            this.Property(t => t.Razao_Social)
                .HasMaxLength(60);

            this.Property(t => t.Data_Pedido)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Hora_Pedido)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Data_Autorizacao)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Hora_Autorizacao)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Data_Liberacao)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Hora_Liberacao)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Data_Ini_Separacao)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Hora_Ini_Separacao)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Data_Fim_Separacao)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Hora_Fim_Separacao)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Data_Emissao_NF)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Hora_Emissao_NF)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Previsao_de_Entrega)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Data_de_Entrega)
                .IsFixedLength()
                .HasMaxLength(10);

            this.Property(t => t.Modal_Executado)
                .HasMaxLength(20);

            this.Property(t => t.NotaBaseExterna)
                .IsFixedLength()
                .HasMaxLength(3);

            this.Property(t => t.PedidoBaseExterna)
                .IsFixedLength()
                .HasMaxLength(3);

            // Table & Column Mappings
            this.ToTable("view_relatorioretiracda");
            this.Property(t => t.IDPedido).HasColumnName("IDPedido");
            this.Property(t => t.IDNotaFiscal).HasColumnName("IDNotaFiscal");
            this.Property(t => t.PedIDCliente).HasColumnName("PedIDCliente");
            this.Property(t => t.PedEntradaSaida).HasColumnName("PedEntradaSaida");
            this.Property(t => t.TipoPedido).HasColumnName("TipoPedido");
            this.Property(t => t.DataDeEmissao).HasColumnName("DataDeEmissao");
            this.Property(t => t.NumPedido).HasColumnName("NumPedido");
            this.Property(t => t.TipoNota).HasColumnName("TipoNota");
            this.Property(t => t.NotaIDCliente).HasColumnName("NotaIDCliente");
            this.Property(t => t.NotaEntradaSaida).HasColumnName("NotaEntradaSaida");
            this.Property(t => t.NumNF).HasColumnName("NumNF");
            this.Property(t => t.Serie).HasColumnName("Serie");
            this.Property(t => t.Solicitante).HasColumnName("Solicitante");
            this.Property(t => t.Origem).HasColumnName("Origem");
            this.Property(t => t.Cidade).HasColumnName("Cidade");
            this.Property(t => t.Uf).HasColumnName("Uf");
            this.Property(t => t.CNPJ_Destinatario).HasColumnName("CNPJ_Destinatario");
            this.Property(t => t.Nome_Fantasia).HasColumnName("Nome_Fantasia");
            this.Property(t => t.Razao_Social).HasColumnName("Razao_Social");
            this.Property(t => t.Vl_Total_NF).HasColumnName("Vl_Total_NF");
            this.Property(t => t.Qtde_Volumes_NF).HasColumnName("Qtde_Volumes_NF");
            this.Property(t => t.Peso_Real_Total_NF).HasColumnName("Peso_Real_Total_NF");
            this.Property(t => t.Peso_Cub_Total_NF).HasColumnName("Peso_Cub_Total_NF");
            this.Property(t => t.Data_Pedido).HasColumnName("Data_Pedido");
            this.Property(t => t.Hora_Pedido).HasColumnName("Hora_Pedido");
            this.Property(t => t.Data_Autorizacao).HasColumnName("Data_Autorizacao");
            this.Property(t => t.Hora_Autorizacao).HasColumnName("Hora_Autorizacao");
            this.Property(t => t.Data_Liberacao).HasColumnName("Data_Liberacao");
            this.Property(t => t.Hora_Liberacao).HasColumnName("Hora_Liberacao");
            this.Property(t => t.Data_Ini_Separacao).HasColumnName("Data_Ini_Separacao");
            this.Property(t => t.Hora_Ini_Separacao).HasColumnName("Hora_Ini_Separacao");
            this.Property(t => t.Data_Fim_Separacao).HasColumnName("Data_Fim_Separacao");
            this.Property(t => t.Hora_Fim_Separacao).HasColumnName("Hora_Fim_Separacao");
            this.Property(t => t.Data_Emissao_NF).HasColumnName("Data_Emissao_NF");
            this.Property(t => t.Hora_Emissao_NF).HasColumnName("Hora_Emissao_NF");
            this.Property(t => t.Previsao_de_Entrega).HasColumnName("Previsao_de_Entrega");
            this.Property(t => t.Data_de_Entrega).HasColumnName("Data_de_Entrega");
            this.Property(t => t.Modal_Executado).HasColumnName("Modal_Executado");
            this.Property(t => t.IDModal).HasColumnName("IDModal");
            this.Property(t => t.NotaBaseExterna).HasColumnName("NotaBaseExterna");
            this.Property(t => t.PedidoBaseExterna).HasColumnName("PedidoBaseExterna");
            this.Property(t => t.NUMERADOR).HasColumnName("NUMERADOR");
        }
    }
}
