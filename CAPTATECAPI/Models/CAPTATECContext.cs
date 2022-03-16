using Microsoft.EntityFrameworkCore;
using Entity;

namespace CAPTATECAPI.Models
{
    public partial class CAPTATECContext : DbContext
    {
        public CAPTATECContext()
        {
        }

        public CAPTATECContext(DbContextOptions<CAPTATECContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<SituacaoCliente> SituacaoClientes { get; set; } = null!;
        public virtual DbSet<TipoCliente> TipoClientes { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DELLNOTE;Initial Catalog=CAPTATEC;Integrated Security=True");
                //optionsBuilder.UseSqlServer(System.Configuration.ConfigurationManager.ConnectionStrings["conexao"].ToString());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.ToTable("CLIENTE");

                entity.HasIndex(e => e.Cpf, "UQ__CLIENTE__C1F89731AA301C20")
                    .IsUnique();

                //entity.Property(e => e.Clienteid).HasColumnName("CLIENTEID");

                entity.Property(e => e.Cpf)
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("CPF");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOME");

                entity.Property(e => e.Sexo)
                    .HasMaxLength(1)
                    .IsUnicode(false)
                    .HasColumnName("SEXO");

                entity.Property(e => e.SitCli).HasColumnName("SIT_CLI");

                entity.Property(e => e.TipoCli).HasColumnName("TIPO_CLI");

                entity.HasOne(d => d.SitCliNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.SitCli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SIT_CLI");

                entity.HasOne(d => d.TipoCliNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.TipoCli)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TIPO_CLI");
            });

            modelBuilder.Entity<SituacaoCliente>(entity =>
            {
                entity.HasKey(e => e.Situacaocliid)
                    .HasName("PK__SITUACAO__345BB44AFD25A4A1");

                entity.ToTable("SITUACAO_CLIENTE");

                entity.Property(e => e.Situacaocliid).HasColumnName("SITUACAOCLIID");

                entity.Property(e => e.Situacaocliente1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("SITUACAOCLIENTE");
            });

            modelBuilder.Entity<TipoCliente>(entity =>
            {
                entity.HasKey(e => e.Tipocliid)
                    .HasName("PK__TIPO_CLI__6129817973B5FE4D");

                entity.ToTable("TIPO_CLIENTE");

                entity.Property(e => e.Tipocliid).HasColumnName("TIPOCLIID");

                entity.Property(e => e.Tipocliente1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("TIPOCLIENTE");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
