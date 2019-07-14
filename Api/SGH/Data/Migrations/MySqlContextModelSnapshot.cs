﻿// <auto-generated />
using Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(MySqlContext))]
    partial class MySqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.3-servicing-35854");

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

            modelBuilder.Entity("Dominio.Model.Curriculo", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("curric_codigo");

                    b.Property<int>("Ano")
                        .HasColumnName("curric_ano");

                    b.Property<int>("CodigoCurso")
                        .HasColumnName("curric_curso");

                    b.Property<int>("CodigoTurno")
                        .HasColumnName("curric_turno");

                    b.Property<int>("Periodo")
                        .HasColumnName("curric_periodo");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoCurso");

                    b.HasIndex("CodigoTurno");

                    b.ToTable("curriculo");
                });

            modelBuilder.Entity("Dominio.Model.CurriculoModel.CurriculoDisciplina", b =>
                {
                    b.Property<int>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("curdis_codigo");

                    b.Property<int>("CargaHorariaSemanalPratica")
                        .HasColumnName("curdis_carga_horaria_semanal_pratica");

                    b.Property<int>("CargaHorariaSemanalTeorica")
                        .HasColumnName("curdis_carga_horaria_semanal_teoricoa");

                    b.Property<int>("CargaHorariaSemanalTotal");

                    b.Property<int>("CodigoCurriculo")
                        .HasColumnName("curdis_curriculo");

                    b.Property<int>("CodigoDisciplina")
                        .HasColumnName("curdis_disciplina");

                    b.Property<int>("Credito")
                        .HasColumnName("curdis_credito");

                    b.Property<int>("HoraAulaTotal")
                        .HasColumnName("curdis_hora_aula_total");

                    b.Property<int>("HoraTotal")
                        .HasColumnName("curdis_hora_total");

                    b.Property<bool>("PreRequisito")
                        .HasColumnName("curdis_pre_requisito");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoCurriculo");

                    b.HasIndex("CodigoDisciplina");

                    b.ToTable("curriculo_disciplina");
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

            modelBuilder.Entity("Dominio.Model.Curriculo", b =>
                {
                    b.HasOne("Dominio.Model.Curso", "Curso")
                        .WithMany("Curriculos")
                        .HasForeignKey("CodigoCurso")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Model.Turno", "Turno")
                        .WithMany("Curriculos")
                        .HasForeignKey("CodigoTurno")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Dominio.Model.CurriculoModel.CurriculoDisciplina", b =>
                {
                    b.HasOne("Dominio.Model.Curriculo", "Curriculo")
                        .WithMany("Disciplinas")
                        .HasForeignKey("CodigoCurriculo")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Dominio.Model.DisciplinaModel.Disciplina", "Disciplina")
                        .WithMany("CurriculoDisciplinas")
                        .HasForeignKey("CodigoDisciplina")
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
