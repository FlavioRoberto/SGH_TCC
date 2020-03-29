﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SGH.Dominio.Core.Model;

namespace SHG.Data.Mapeamento
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
                .HasConversion<int>()
                .HasColumnName("usuPrf_administrador");

            builder.ToTable("Usuario_Perfil");
            
            return this;
        }
    }
}