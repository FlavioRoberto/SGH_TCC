using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Dominio.Core.Model;

namespace SHG.Data.Mapeamento
{
    public class UsuarioMapeamento : EntidadeMapeamento<Usuario>
    {
        public UsuarioMapeamento(EntityTypeBuilder<Usuario> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<Usuario> Map()
        {
            builder.HasKey(p => p.Codigo);

            builder.Property(p => p.Codigo)
                .ValueGeneratedOnAdd()
                .HasColumnName("Usu_Codigo");

            builder.Property(p => p.Email)
                .HasColumnName("Usu_Email")
                .IsRequired(true);

            builder.Property(p => p.Foto)
                .HasColumnName("Usu_Foto");

            builder.Property(p => p.Login)
                .HasColumnName("Usu_Login")
                .IsRequired(true)
                .HasMaxLength(30);

            builder.Property(p => p.Nome)
                .HasColumnName("Usu_Nome")
                .IsRequired(true)
                .HasMaxLength(45);

            builder.Property(p => p.Senha)
                .HasColumnName("Usu_Senha")
                .IsRequired(true)
                .HasMaxLength(35);

            builder.Property(p => p.Telefone)
                .HasColumnName("Usu_Telefone")
                .HasMaxLength(12);

            builder.Property(p => p.Ativo)
                .HasConversion<int>()
                .HasColumnName("Usu_Ativo")
                .HasDefaultValue(true);

            builder.Property(p => p.PerfilCodigo)
                .IsRequired(true)
                .HasColumnName("Usu_Perfil");

            builder.Property(p => p.CursoCodigo)
                .IsRequired(false)
                .HasColumnName("Usu_Curso");

            #region Relacionamento
            builder.HasOne(lnq => lnq.Perfil)
                   .WithMany(lnq => lnq.Usuarios)
                   .HasForeignKey(lnq => lnq.PerfilCodigo)
                   .HasConstraintName("FK_Perfil")
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(lnq => lnq.Curso)
                   .WithMany(lnq => lnq.Usuarios)
                   .HasForeignKey(lnq => lnq.CursoCodigo)
                   .HasConstraintName("FK_Curso")
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

            builder.ToTable("Usuario");

            return this;
        }
    }
}
