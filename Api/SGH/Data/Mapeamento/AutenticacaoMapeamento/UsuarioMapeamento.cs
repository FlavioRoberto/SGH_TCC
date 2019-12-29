using Dominio.Model.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
                .HasColumnName("usu_codigo");

            builder.Property(p => p.Email)
                .HasColumnName("usu_email")
                .IsRequired(true)
                .HasMaxLength(50);

            builder.Property(p => p.Foto)
                .HasColumnName("usu_foto");

            builder.Property(p => p.Login)
                .HasColumnName("usu_login")
                .IsRequired(true)
                .HasMaxLength(30);

            builder.Property(p => p.Nome)
                .HasColumnName("usu_nome")
                .IsRequired(true)
                .HasMaxLength(45);

            builder.Property(p => p.Senha)
                .HasColumnName("usu_senha")
                .IsRequired(true)
                .HasMaxLength(35);

            builder.Property(p => p.Telefone)
                .HasColumnName("usu_telefone")
                .HasMaxLength(12);

            builder.Property(p => p.Ativo)
                .HasConversion<int>()
                .HasColumnName("usu_ativo")
                .HasDefaultValue(true);

            builder.Property(p => p.PerfilCodigo)
                .IsRequired(true)
                .HasColumnName("usuPrf_Perfil");

            builder.HasOne(lnq => lnq.Perfil)
                .WithMany(lnq => lnq.Usuarios)
                .HasForeignKey(lnq => lnq.PerfilCodigo)
                .HasConstraintName("FK_Perfil");


            builder.ToTable("usuario");

            return this;
        }
    }
}
