﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SHG.Data.Contexto;

namespace SGH.Data.Migrations
{
    [DbContext(typeof(MySqlContext))]
    [Migration("20191005210704_Versao 003")]
    partial class Versao003
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

            modelBuilder.Entity("Dominio.Model.Autenticacao.Usuario", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("usu_codigo");

                    b.Property<int>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("usu_ativo")
                        .HasDefaultValue(1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("usu_email")
                        .HasMaxLength(50);

                    b.Property<string>("Foto")
                        .HasColumnName("usu_foto");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnName("usu_login")
                        .HasMaxLength(30);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("usu_nome")
                        .HasMaxLength(45);

                    b.Property<int>("PerfilCodigo")
                        .HasColumnName("usuPrf_Perfil");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnName("usu_senha")
                        .HasMaxLength(35);

                    b.Property<string>("Telefone")
                        .HasColumnName("usu_telefone")
                        .HasMaxLength(12);

                    b.HasKey("Codigo");

                    b.HasIndex("PerfilCodigo");

                    b.ToTable("usuario");
                });

            modelBuilder.Entity("Dominio.Model.Autenticacao.UsuarioPerfil", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("usuPrf_codigo");

                    b.Property<int>("Administrador")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("usuPrf_administrador")
                        .HasDefaultValue(0);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("usuPrf_descricao")
                        .HasMaxLength(45);

                    b.HasKey("Codigo");

                    b.ToTable("Usuario_Perfil");
                });

            modelBuilder.Entity("Dominio.Model.Cargo", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Ano")
                        .HasColumnName("cargo_ano");

                    b.Property<int>("CodigoProfessor")
                        .HasColumnName("cargo_professor");

                    b.Property<int>("Edital")
                        .HasColumnName("cargo_edital");

                    b.Property<int>("Numero")
                        .HasColumnName("cargo_numero");

                    b.Property<int>("Semestre")
                        .HasColumnName("cargo_semestre");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoProfessor");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("Dominio.Model.CargoDisciplina", b =>
                {
                    b.Property<int>("CodigoCargo")
                        .HasColumnName("cardis_cargo");

                    b.Property<int>("CodigoCurriculoDisciplina")
                        .HasColumnName("cardis_disciplina");

                    b.HasKey("CodigoCargo", "CodigoCurriculoDisciplina");

                    b.HasIndex("CodigoCurriculoDisciplina");

                    b.ToTable("CargoDisciplina");
                });

            modelBuilder.Entity("Dominio.Model.Curriculo", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("curric_codigo");

                    b.Property<int>("Ano")
                        .HasColumnName("curric_ano");

                    b.Property<int>("CodigoCurso")
                        .HasColumnName("curric_curso");

                    b.Property<int?>("TurnoCodigo");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoCurso");

                    b.HasIndex("TurnoCodigo");

                    b.ToTable("curriculo");
                });

            modelBuilder.Entity("Dominio.Model.CurriculoModel.CurriculoDisciplina", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("curdis_codigo");

                    b.Property<int>("AulasSemanaisPratica")
                        .HasColumnName("curdis_quantidade_aulas_semanal_pratica");

                    b.Property<int>("AulasSemanaisTeorica")
                        .HasColumnName("curdis_quantidade_aulas_semanais_teorica");

                    b.Property<int>("CodigoCurriculo")
                        .HasColumnName("curdis_curriculo");

                    b.Property<int?>("CodigoDisciplina")
                        .HasColumnName("curdis_disciplina");

                    b.Property<int>("Credito")
                        .HasColumnName("curdis_credito");

                    b.Property<int>("Periodo")
                        .HasColumnName("curdis_periodo");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoCurriculo");

                    b.HasIndex("CodigoDisciplina");

                    b.ToTable("curriculo_disciplina");
                });

            modelBuilder.Entity("Dominio.Model.CurriculoModel.CurriculoDisciplinaPreRequisito", b =>
                {
                    b.Property<int>("CodigoCurriculoDisciplina")
                        .HasColumnName("disPre_curriculo_disciplina");

                    b.Property<int>("CodigoDisciplina")
                        .HasColumnName("disPre_disciplina");

                    b.HasKey("CodigoCurriculoDisciplina", "CodigoDisciplina");

                    b.HasIndex("CodigoDisciplina");

                    b.ToTable("curriculo_disciplina_pre_requisito");
                });

            modelBuilder.Entity("Dominio.Model.Curso", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("curso_codigo");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("curso_descricao");

                    b.HasKey("Codigo");

                    b.ToTable("curso");
                });

            modelBuilder.Entity("Dominio.Model.DisciplinaModel.Disciplina", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("dis_codigo");

                    b.Property<int>("CodigoTipo")
                        .HasColumnName("dis_tipo");

                    b.Property<string>("Descricao")
                        .HasColumnName("dis_descricao");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoTipo");

                    b.ToTable("disciplina");
                });

            modelBuilder.Entity("Dominio.Model.DisciplinaModel.DisciplinaTipo", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("distip_codigo");

                    b.Property<string>("Descricao")
                        .HasColumnName("distip_descricao");

                    b.HasKey("Codigo");

                    b.ToTable("disciplina_tipo");
                });

            modelBuilder.Entity("Dominio.Model.Professor", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("prof_codigo");

                    b.Property<int>("Ativo")
                        .HasColumnName("prof_ativo")
                        .HasMaxLength(10);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("prof_email")
                        .HasMaxLength(50);

                    b.Property<string>("Matricula")
                        .HasColumnName("prof_matricula")
                        .HasMaxLength(10);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("prof_nome")
                        .HasMaxLength(45);

                    b.Property<string>("Telefone")
                        .HasColumnName("prof_telefone")
                        .HasMaxLength(12);

                    b.HasKey("Codigo");

                    b.ToTable("professor");
                });

            modelBuilder.Entity("Dominio.Model.Turno", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("turno_codigo");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("turno_descricao");

                    b.HasKey("Codigo");

                    b.ToTable("turno");
                });

            modelBuilder.Entity("Dominio.Model.Autenticacao.Usuario", b =>
                {
                    b.HasOne("Dominio.Model.Autenticacao.UsuarioPerfil", "Perfil")
                        .WithMany("Usuarios")
                        .HasForeignKey("PerfilCodigo")
                        .HasConstraintName("FK_Perfil")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Model.Cargo", b =>
                {
                    b.HasOne("Dominio.Model.Professor", "Professor")
                        .WithMany("Cargos")
                        .HasForeignKey("CodigoProfessor")
                        .HasConstraintName("FK_Professor")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Model.CargoDisciplina", b =>
                {
                    b.HasOne("Dominio.Model.Cargo", "Cargo")
                        .WithMany("Cargos")
                        .HasForeignKey("CodigoCargo")
                        .HasConstraintName("FK_Cargo")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Model.CurriculoModel.CurriculoDisciplina", "CurriculoDisciplina")
                        .WithMany("Cargos")
                        .HasForeignKey("CodigoCurriculoDisciplina")
                        .HasConstraintName("FK_Cargo_Disciplina")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Model.Curriculo", b =>
                {
                    b.HasOne("Dominio.Model.Curso", "Curso")
                        .WithMany("Curriculos")
                        .HasForeignKey("CodigoCurso")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Model.Turno")
                        .WithMany("Curriculos")
                        .HasForeignKey("TurnoCodigo");
                });

            modelBuilder.Entity("Dominio.Model.CurriculoModel.CurriculoDisciplina", b =>
                {
                    b.HasOne("Dominio.Model.Curriculo", "Curriculo")
                        .WithMany("Disciplinas")
                        .HasForeignKey("CodigoCurriculo")
                        .HasConstraintName("FK_Curriculo")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Model.DisciplinaModel.Disciplina", "Disciplina")
                        .WithMany("CurriculoDisciplinas")
                        .HasForeignKey("CodigoDisciplina")
                        .HasConstraintName("FK_Disciplina");
                });

            modelBuilder.Entity("Dominio.Model.CurriculoModel.CurriculoDisciplinaPreRequisito", b =>
                {
                    b.HasOne("Dominio.Model.CurriculoModel.CurriculoDisciplina", "CurriculoDisciplina")
                        .WithMany("CurriculoDisciplinaPreRequisito")
                        .HasForeignKey("CodigoCurriculoDisciplina")
                        .HasConstraintName("FK_Curriculo_Disciplina")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Model.DisciplinaModel.Disciplina", "Disciplina")
                        .WithMany("CurriculoDisciplinaPreRequisito")
                        .HasForeignKey("CodigoDisciplina")
                        .HasConstraintName("FK_Curriculo_Disciplina_Pre_Req")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Model.DisciplinaModel.Disciplina", b =>
                {
                    b.HasOne("Dominio.Model.DisciplinaModel.DisciplinaTipo", "DisciplinaTipo")
                        .WithMany("Disciplinas")
                        .HasForeignKey("CodigoTipo")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
