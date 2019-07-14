using Dominio.Model.Autenticacao;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Mapeamento.AutenticacaoMapeamento
{
    public class UsuarioPerfilMapeamento : EntidadeMapeamento<UsuarioPerfil>
    {
        public UsuarioPerfilMapeamento(EntityTypeBuilder<UsuarioPerfil> builder) : base(builder)
        {
        }

        public override EntidadeMapeamento<UsuarioPerfil> Map()
        {
            builder.HasKey(p => p.Codigo);

            builder.Property(p => p.Codigo)
                .ValueGeneratedOnAdd()
                .HasColumnName("usuPrf_codigo");

            builder.Property(p => p.Descricao)
                .IsRequired(true)
                .HasColumnName("usuPrf_descricao")
                .HasMaxLength(45);

            builder.Property(p => p.Administrador)
                .HasDefaultValue(false)
                .HasColumnName("usuPrf_administrador");

            builder.ToTable("Usuario_Perfil");
            
            return this;
        }
    }
}
