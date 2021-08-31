﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SHG.Data.Contexto;

namespace SGH.Data.Migrations
{
    [DbContext(typeof(Contexto))]
    partial class ContextoModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.1-servicing-10028")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("SGH.Dominio.Core.Model.Aula", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Aula_Codigo");

                    b.Property<long>("CodigoDisciplina")
                        .HasColumnName("Aula_Disciplina");

                    b.Property<long>("CodigoHorario")
                        .HasColumnName("Aula_Horarario");

                    b.Property<long?>("CodigoSala")
                        .HasColumnName("Aula_Sala");

                    b.Property<string>("DescricaoDesdobramento")
                        .HasColumnName("Aula_Descricao_Desdobramento");

                    b.Property<int>("Desdobramento")
                        .HasColumnName("Aula_Desdobramento");

                    b.Property<int>("Laboratorio")
                        .HasColumnName("Aula_Laboratorio");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoDisciplina");

                    b.HasIndex("CodigoHorario");

                    b.HasIndex("CodigoSala");

                    b.ToTable("Aulas");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.AulaDisciplinaAuxiliar", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("AulaDiscAux_Codigo");

                    b.Property<long>("CodigoAula")
                        .HasColumnName("AulaDiscAux_Aula");

                    b.Property<long>("CodigoCargoDisciplina")
                        .HasColumnName("AulaDiscAux_Disciplina");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoAula");

                    b.HasIndex("CodigoCargoDisciplina");

                    b.ToTable("AulaDisciplinaAuxiliar");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Bloco", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Bloco_Codigo");

                    b.Property<string>("Descricao")
                        .HasColumnName("Bloco_Descricao");

                    b.HasKey("Codigo");

                    b.ToTable("Bloco");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Cargo", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Ano")
                        .HasColumnName("Cargo_Ano");

                    b.Property<long?>("CodigoProfessor")
                        .HasColumnName("Cargo_Professor");

                    b.Property<string>("Edital")
                        .HasColumnName("Cargo_Edital");

                    b.Property<int>("Numero")
                        .HasColumnName("Cargo_Mumero");

                    b.Property<int>("Semestre")
                        .HasColumnName("Cargo_Semestre");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoProfessor");

                    b.ToTable("Cargo");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.CargoDisciplina", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CodigoCargo")
                        .HasColumnName("Cardis_Cargo");

                    b.Property<long>("CodigoCurriculoDisciplina")
                        .HasColumnName("Cardis_Disciplina");

                    b.Property<long>("CodigoTurno")
                        .HasColumnName("Cardis_Turno");

                    b.Property<string>("Descricao")
                        .HasColumnName("Cardis_Descricao");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoCargo");

                    b.HasIndex("CodigoCurriculoDisciplina");

                    b.HasIndex("CodigoTurno");

                    b.ToTable("Cargo_Disciplina");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Curriculo", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Curric_Codigo");

                    b.Property<long>("Ano")
                        .HasColumnName("Curric_Ano");

                    b.Property<long>("CodigoCurso")
                        .HasColumnName("Curric_Curso");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoCurso");

                    b.ToTable("Curriculo");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.CurriculoDisciplina", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Curdis_Codigo");

                    b.Property<int>("AulasSemanaisPratica")
                        .HasColumnName("Curdis_Quantidade_Aulas_Semanal_Pratica");

                    b.Property<int>("AulasSemanaisTeorica")
                        .HasColumnName("Curdis_Quantidade_Aulas_Semanais_Teorica");

                    b.Property<long>("CodigoCurriculo")
                        .HasColumnName("Curdis_Curriculo");

                    b.Property<long?>("CodigoDisciplina")
                        .HasColumnName("Curdis_Disciplina");

                    b.Property<long>("CodigoTipo")
                        .HasColumnName("Dis_Tipo");

                    b.Property<int>("Periodo")
                        .HasColumnName("Curdis_Periodo");

                    b.Property<int>("QuantidadeAulaTotal")
                        .HasColumnName("Curdis_Quantidade_Aulas_Total");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoCurriculo");

                    b.HasIndex("CodigoDisciplina");

                    b.HasIndex("CodigoTipo");

                    b.ToTable("Curriculo_Disciplina");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.CurriculoDisciplinaPreRequisito", b =>
                {
                    b.Property<long>("CodigoCurriculoDisciplina")
                        .HasColumnName("DisPre_Curriculo_Disciplina");

                    b.Property<long>("CodigoDisciplina")
                        .HasColumnName("DisPre_Disciplina");

                    b.HasKey("CodigoCurriculoDisciplina", "CodigoDisciplina");

                    b.HasIndex("CodigoDisciplina");

                    b.ToTable("Curriculo_Disciplina_Pre_Requisito");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Curso", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Curso_Codigo");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Curso_Descricao");

                    b.HasKey("Codigo");

                    b.ToTable("Curso");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Disciplina", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Dis_Codigo");

                    b.Property<string>("Descricao")
                        .HasColumnName("Dis_Descricao");

                    b.HasKey("Codigo");

                    b.ToTable("Disciplina");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.DisciplinaTipo", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Distip_Codigo");

                    b.Property<string>("Descricao")
                        .HasColumnName("Distip_Descricao");

                    b.HasKey("Codigo");

                    b.ToTable("Disciplina_Tipo");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.HorarioAula", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Horario_Codigo");

                    b.Property<int>("Ano")
                        .HasColumnName("Horario_Ano");

                    b.Property<long>("CodigoCurriculo")
                        .HasColumnName("Horario_Curriculo");

                    b.Property<long>("CodigoTurno")
                        .HasColumnName("Horario_Turno");

                    b.Property<string>("Mensagem")
                        .HasColumnName("Horario_mensagem");

                    b.Property<int>("Periodo")
                        .HasColumnName("Horario_Periodo");

                    b.Property<int>("Semestre")
                        .HasColumnName("Horario_Semestre");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoCurriculo");

                    b.HasIndex("CodigoTurno");

                    b.ToTable("Horarios_Aula");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Professor", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Prof_Codigo");

                    b.Property<int>("Ativo")
                        .HasColumnName("Prof_Ativo")
                        .HasMaxLength(10);

                    b.Property<int>("Contratacao")
                        .HasColumnName("Prof_Contratacao");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Prof_Email");

                    b.Property<string>("Matricula")
                        .HasColumnName("Prof_Matricula");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Prof_Nome")
                        .HasMaxLength(45);

                    b.Property<string>("Telefone")
                        .HasColumnName("Prof_Telefone")
                        .HasMaxLength(12);

                    b.HasKey("Codigo");

                    b.ToTable("Professor");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Sala", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Sala_Codigo");

                    b.Property<long>("CodigoBloco")
                        .HasColumnName("Sala_Bloco");

                    b.Property<string>("Descricao")
                        .HasColumnName("Sala_Descricao");

                    b.Property<int>("Laboratorio")
                        .HasColumnName("Sala_Laboratorio");

                    b.Property<int>("Numero")
                        .HasColumnName("Sala_Numero");

                    b.HasKey("Codigo");

                    b.HasIndex("CodigoBloco");

                    b.ToTable("Sala");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Turno", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Turno_Codigo");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("Turno_Horarios");

                    b.Property<string>("Horarios");

                    b.HasKey("Codigo");

                    b.ToTable("Turno");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Usuario", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Usu_Codigo");

                    b.Property<int>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("Usu_Ativo")
                        .HasDefaultValue(1);

                    b.Property<long?>("CursoCodigo")
                        .HasColumnName("Usu_Curso");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnName("Usu_Email");

                    b.Property<string>("Foto")
                        .HasColumnName("Usu_Foto");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnName("Usu_Login")
                        .HasMaxLength(30);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnName("Usu_Nome")
                        .HasMaxLength(45);

                    b.Property<long>("PerfilCodigo")
                        .HasColumnName("Usu_Perfil");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasColumnName("Usu_Senha")
                        .HasMaxLength(35);

                    b.Property<string>("Telefone")
                        .HasColumnName("Usu_Telefone")
                        .HasMaxLength(12);

                    b.HasKey("Codigo");

                    b.HasIndex("CursoCodigo");

                    b.HasIndex("PerfilCodigo");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.UsuarioPerfil", b =>
                {
                    b.Property<long>("Codigo")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UsuPrf_Codigo");

                    b.Property<int>("Administrador")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("UsuPrf_Administrador")
                        .HasDefaultValue(0);

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasColumnName("UsuPrf_Descricao")
                        .HasMaxLength(45);

                    b.HasKey("Codigo");

                    b.ToTable("Usuario_Perfil");
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Aula", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.CargoDisciplina", "Disciplina")
                        .WithMany("Aulas")
                        .HasForeignKey("CodigoDisciplina")
                        .HasConstraintName("FK_Cargo_Disciplina_Aula")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SGH.Dominio.Core.Model.HorarioAula", "Horario")
                        .WithMany("Aulas")
                        .HasForeignKey("CodigoHorario")
                        .HasConstraintName("FK_Horario_Aula")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SGH.Dominio.Core.Model.Sala", "Sala")
                        .WithMany("Aulas")
                        .HasForeignKey("CodigoSala")
                        .HasConstraintName("FK_Sala_Aula");

                    b.OwnsOne("SGH.Dominio.Core.ObjetosValor.Reserva", "Reserva", b1 =>
                        {
                            b1.Property<long>("AulaCodigo");

                            b1.Property<string>("DiaSemana")
                                .HasColumnName("Aula_Dia_Semana");

                            b1.Property<string>("Hora")
                                .HasColumnName("Aula_Hora");

                            b1.HasKey("AulaCodigo");

                            b1.ToTable("Aulas");

                            b1.HasOne("SGH.Dominio.Core.Model.Aula")
                                .WithOne("Reserva")
                                .HasForeignKey("SGH.Dominio.Core.ObjetosValor.Reserva", "AulaCodigo")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.AulaDisciplinaAuxiliar", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.Aula", "Aula")
                        .WithMany("DisciplinasAuxiliar")
                        .HasForeignKey("CodigoAula")
                        .HasConstraintName("FK_Aula")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SGH.Dominio.Core.Model.CargoDisciplina", "Disciplina")
                        .WithMany("DisciplinasAuxiliar")
                        .HasForeignKey("CodigoCargoDisciplina")
                        .HasConstraintName("FK_Disciplina")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Cargo", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.Professor", "Professor")
                        .WithMany("Cargos")
                        .HasForeignKey("CodigoProfessor")
                        .HasConstraintName("FK_Professor")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.CargoDisciplina", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.Cargo", "Cargo")
                        .WithMany("Disciplinas")
                        .HasForeignKey("CodigoCargo")
                        .HasConstraintName("FK_Cargo")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SGH.Dominio.Core.Model.CurriculoDisciplina", "Disciplina")
                        .WithMany("Cargos")
                        .HasForeignKey("CodigoCurriculoDisciplina")
                        .HasConstraintName("FK_Cargo_Disciplina")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SGH.Dominio.Core.Model.Turno", "Turno")
                        .WithMany("DisciplinasCargo")
                        .HasForeignKey("CodigoTurno")
                        .HasConstraintName("Fk_Turno")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Curriculo", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.Curso", "Curso")
                        .WithMany("Curriculos")
                        .HasForeignKey("CodigoCurso")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.CurriculoDisciplina", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.Curriculo", "Curriculo")
                        .WithMany("Disciplinas")
                        .HasForeignKey("CodigoCurriculo")
                        .HasConstraintName("FK_Curriculo")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SGH.Dominio.Core.Model.Disciplina", "Disciplina")
                        .WithMany("CurriculoDisciplinas")
                        .HasForeignKey("CodigoDisciplina")
                        .HasConstraintName("FK_Disciplina")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SGH.Dominio.Core.Model.DisciplinaTipo", "DisciplinaTipo")
                        .WithMany("Disciplinas")
                        .HasForeignKey("CodigoTipo")
                        .HasConstraintName("FK_Disciplina_Tipo")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.CurriculoDisciplinaPreRequisito", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.CurriculoDisciplina", "CurriculoDisciplina")
                        .WithMany("CurriculoDisciplinaPreRequisito")
                        .HasForeignKey("CodigoCurriculoDisciplina")
                        .HasConstraintName("FK_Curriculo_Disciplina")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SGH.Dominio.Core.Model.Disciplina", "Disciplina")
                        .WithMany("CurriculoDisciplinaPreRequisito")
                        .HasForeignKey("CodigoDisciplina")
                        .HasConstraintName("FK_Curriculo_Disciplina_Pre_Req")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.HorarioAula", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.Curriculo", "Curriculo")
                        .WithMany("HorariosAula")
                        .HasForeignKey("CodigoCurriculo")
                        .HasConstraintName("FK_Curriculo_Horario")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SGH.Dominio.Core.Model.Turno", "Turno")
                        .WithMany("HorariosAula")
                        .HasForeignKey("CodigoTurno")
                        .HasConstraintName("FK_Turno_Horario")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Sala", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.Bloco", "Bloco")
                        .WithMany("Salas")
                        .HasForeignKey("CodigoBloco")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SGH.Dominio.Core.Model.Usuario", b =>
                {
                    b.HasOne("SGH.Dominio.Core.Model.Curso", "Curso")
                        .WithMany("Usuarios")
                        .HasForeignKey("CursoCodigo")
                        .HasConstraintName("FK_Curso")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("SGH.Dominio.Core.Model.UsuarioPerfil", "Perfil")
                        .WithMany("Usuarios")
                        .HasForeignKey("PerfilCodigo")
                        .HasConstraintName("FK_Perfil")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}
