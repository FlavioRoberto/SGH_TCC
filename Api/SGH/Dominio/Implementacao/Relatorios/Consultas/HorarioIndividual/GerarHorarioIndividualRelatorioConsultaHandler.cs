using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using SGH.Dominio.Core;
using SGH.Dominio.Core.DomainObjects.Datasets;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Reports;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Services;
using SGH.Dominio.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Relatorios.Consultas.HorarioIndividual
{
    public class GerarHorarioIndividualRelatorioConsultaHandler : IRequestHandler<GerarHorarioIndividualRelatorioConsulta, Resposta<string>>
    {
        private readonly IRelatorioServico _relatorioServico;
        private readonly IProfessorRepositorio _professorRepositorio;
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;
        private readonly ICurriculoRepositorio _curriculoRepositorio;
        private readonly IAulaRepositorio _aulaRepositorio;

        public GerarHorarioIndividualRelatorioConsultaHandler(IRelatorioServico relatorioServico,
                                                              IProfessorRepositorio professorRepositorio,
                                                              ICargoRepositorio cargoRepositorio,
                                                              ICargoDisciplinaRepositorio cargoDisciplinaRepositorio,
                                                              ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                              ICurriculoRepositorio curriculoRepositorio,
                                                              IAulaRepositorio aulaRepositorio)
        {
            _relatorioServico = relatorioServico;
            _professorRepositorio = professorRepositorio;
            _cargoRepositorio = cargoRepositorio;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
            _curriculoRepositorio = curriculoRepositorio;
            _aulaRepositorio = aulaRepositorio;
        }

        public async Task<Resposta<string>> Handle(GerarHorarioIndividualRelatorioConsulta request, CancellationToken cancellationToken)
        {
            var dados = await GerarDadosRelatorioIndividual(request);

            var bytesRelatorio = _relatorioServico.GerarRelatorioHorarioIndividual(dados);

            var base64 = Convert.ToBase64String(bytesRelatorio);

            return new Resposta<string>(base64, "");
        }

        private async Task<HorarioIndividualRelatorioData> GerarDadosRelatorioIndividual(GerarHorarioIndividualRelatorioConsulta request)
        {
            var professor = await _professorRepositorio.Consultar(lnq => lnq.Codigo == request.CodigoProfessor);

            var cargos = await _cargoRepositorio.Listar(lnq => lnq.CodigoProfessor == request.CodigoProfessor &&
                                                               lnq.Ano == request.Ano &&
                                                               lnq.Semestre == request.Semestre);

            if (cargos == null || cargos.Count <= 0)
                return new HorarioIndividualRelatorioData();

            var cargosId = cargos.Select(lnq => lnq.Codigo);

            var disciplinasCargo = await _cargoDisciplinaRepositorio.Listar(lnq => cargosId.Contains(lnq.CodigoCargo));

            return new HorarioIndividualRelatorioData
            {
                Ano = request.Ano,
                Semestre = request.Semestre.RetornarDescricao(),
                Cargo = cargos.Select(lnq => $"Cargo: {lnq.Numero} - Edital: {lnq.Edital}").Join(", "),
                Professor = professor.Nome,
                DisciplinasMinistradas = await ListarDisciplinasMinistradas(disciplinasCargo),
                Aulas = await CarregarAulas(disciplinasCargo)
            };
        }

        private async Task<IList<HorarioIndividualDisciplinaData>> ListarDisciplinasMinistradas(List<CargoDisciplina> disciplinasCargo)
        {
            var disciplinasHorario = new List<HorarioIndividualDisciplinaData>();

            if (disciplinasCargo == null || disciplinasCargo.Count <= 0)
                return new List<HorarioIndividualDisciplinaData>();

            foreach(var disciplinaCargo in disciplinasCargo)
            {
                var disciplinaCurriculo = await _curriculoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == disciplinaCargo.CodigoCurriculoDisciplina);

                var disciplina = await ConsultarDisciplinaCurriculo(disciplinaCargo.CodigoCurriculoDisciplina);

                var curso = await _curriculoRepositorio.ConsultarCurso(disciplinaCurriculo.CodigoCurriculo);

                disciplinasHorario.Add(new HorarioIndividualDisciplinaData
                {
                    Curso = curso.Descricao,
                    Descricao = disciplina.Descricao,
                    Periodo = (int)disciplinaCurriculo.Periodo,
                    QuantidadeHoraPratica = disciplinaCurriculo.AulasSemanaisPratica,
                    QuantidadeHoraTeorica = disciplinaCurriculo.AulasSemanaisTeorica,
                    Turno = disciplinaCargo.Turno.Descricao
                });
            };

            return disciplinasHorario;
            
        }

        private async Task<List<HorarioIndividualAulasData>> CarregarAulas(List<CargoDisciplina> disciplinasCargos)
        {
            var aulasRelatorio = new List<HorarioIndividualAulasData>();

            var codigosDisciplinas = disciplinasCargos.Select(lnq => lnq.Codigo);

            var aulas = await _aulaRepositorio.Listar(lnq => codigosDisciplinas.Contains(lnq.CodigoDisciplina));

            var aulasPorHorario = aulas.GroupBy(lnq => lnq.Reserva.Hora).Select(lnq => lnq);

            foreach(var aulasHorarios in aulasPorHorario)
            {
                var aulaIndividual = new HorarioIndividualAulasData();
                foreach (var aula in aulasHorarios)
                {
                    var disciplinaCargo = disciplinasCargos.FirstOrDefault(lnq => lnq.Codigo == aula.CodigoDisciplina);

                    aulaIndividual.Hora = aula.Reserva.Hora;
                    aulaIndividual.Turno = disciplinaCargo.Turno.Descricao;

                    if (string.IsNullOrEmpty(aulaIndividual.DisciplinaSegunda))
                        aulaIndividual.DisciplinaSegunda = await RetornarDisciplinaDia("Segunda", aula, disciplinaCargo);

                    if (string.IsNullOrEmpty(aulaIndividual.DisciplinaTerca))
                        aulaIndividual.DisciplinaTerca = await RetornarDisciplinaDia("Terça", aula, disciplinaCargo);

                    if (string.IsNullOrEmpty(aulaIndividual.DisciplinaQuarta))
                        aulaIndividual.DisciplinaQuarta = await RetornarDisciplinaDia("Quarta", aula, disciplinaCargo);

                    if (string.IsNullOrEmpty(aulaIndividual.DisciplinaQuinta))
                        aulaIndividual.DisciplinaQuinta = await RetornarDisciplinaDia("Quinta", aula, disciplinaCargo);

                    if (string.IsNullOrEmpty(aulaIndividual.DisciplinaSexta))
                        aulaIndividual.DisciplinaSexta = await RetornarDisciplinaDia("Sexta", aula, disciplinaCargo);

                    if (string.IsNullOrEmpty(aulaIndividual.DisciplinaSabado))
                        aulaIndividual.DisciplinaSabado = await RetornarDisciplinaDia("Sábado", aula, disciplinaCargo);
                }
                aulasRelatorio.Add(aulaIndividual);
            }
            return aulasRelatorio;
        }

        private async Task<string> RetornarDisciplinaDia(string dia, Aula aula, CargoDisciplina disciplinaCargo)
        {
            if (!aula.Reserva.DiaSemana.Equals(dia))
                return "";

            var disciplinaCurriculo = await _curriculoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == disciplinaCargo.CodigoCurriculoDisciplina);

            var disciplina = await ConsultarDisciplinaCurriculo(disciplinaCargo.CodigoCurriculoDisciplina);

            var periodo = (int)disciplinaCurriculo.Periodo;

            return $"({periodo}° Período){Environment.NewLine}{disciplina.Descricao}";
        }

        private async Task<Disciplina> ConsultarDisciplinaCurriculo(long codigoCurriculoDisciplina)
        {
            return await _cargoDisciplinaRepositorio.RetornarDisciplina(codigoCurriculoDisciplina);
        }
    }
}
