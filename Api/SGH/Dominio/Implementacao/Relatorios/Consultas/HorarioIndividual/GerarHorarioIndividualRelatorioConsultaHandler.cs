using MediatR;
using Microsoft.EntityFrameworkCore.Internal;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Extensions;
using SGH.Relatorios.Contratos;
using SGH.Relatorios.DataSets;
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
        private readonly List<HorarioIndividualAulasData> _aulasRelatorio;

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
            _aulasRelatorio = new List<HorarioIndividualAulasData>();
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
            
            return new HorarioIndividualRelatorioData
            {
                Ano = request.Ano,
                Semestre = request.Semestre.RetornarDescricao(),
                Cargo = cargos.Select(lnq => $"{lnq.Numero} - {lnq.Edital}").Join(", "),
                Professor = professor.Nome,
                DisciplinasMinistradas = await ListarDisciplinasMinistradas(cargos),
                Aulas = _aulasRelatorio
            };
        }

        private async Task<IList<HorarioIndividualDisciplinaData>> ListarDisciplinasMinistradas(List<Cargo> cargos)
        {
            var disciplinasHorario = new List<HorarioIndividualDisciplinaData>();

            var cargosId = cargos.Select(lnq => lnq.Codigo);

            var disciplinasCargo = await _cargoDisciplinaRepositorio.Listar(lnq => cargosId.Contains(lnq.CodigoCargo));

            if (disciplinasCargo == null || disciplinasCargo.Count <= 0)
                return new List<HorarioIndividualDisciplinaData>();

            foreach(var disciplinaCargo in disciplinasCargo)
            {
                var disciplinaCurriculo = await _curriculoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == disciplinaCargo.CodigoCurriculoDisciplina);

                var disciplina = await ConsultarDisciplinaCurriculo(disciplinaCargo.CodigoCurriculoDisciplina);

                var curso = await _curriculoRepositorio.ConsultarCurso(disciplinaCurriculo.CodigoCurriculo);

                await CarregarAula(disciplinaCargo, disciplinaCurriculo, disciplina);

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

        private async Task CarregarAula(CargoDisciplina disciplinaCargo, CurriculoDisciplina disciplinaCurriculo, Disciplina disciplina)
        {
            var aulas = await _aulaRepositorio.Listar(lnq => lnq.CodigoDisciplina == disciplinaCargo.Codigo);

            foreach (var aula in aulas)
            {
                _aulasRelatorio.Add(new HorarioIndividualAulasData
                {
                    Hora = aula.Reserva.Hora,
                    Turno = disciplinaCargo.Turno.Descricao,
                    DisciplinaSegunda =  RetornarDisciplinaDia("Segunda", aula, (int)disciplinaCurriculo.Periodo, disciplina.Descricao),
                    DisciplinaTerca =  RetornarDisciplinaDia("Terça", aula, (int)disciplinaCurriculo.Periodo, disciplina.Descricao),
                    DisciplinaQuarta =  RetornarDisciplinaDia("Quarta", aula, (int)disciplinaCurriculo.Periodo, disciplina.Descricao),
                    DisciplinaQuinta =  RetornarDisciplinaDia("Quinta", aula, (int)disciplinaCurriculo.Periodo, disciplina.Descricao),
                    DisciplinaSexta =  RetornarDisciplinaDia("Sexta", aula, (int)disciplinaCurriculo.Periodo, disciplina.Descricao),
                    DisciplinaSabado =  RetornarDisciplinaDia("Sábado", aula, (int)disciplinaCurriculo.Periodo, disciplina.Descricao)
                });
            }
        }

        private string RetornarDisciplinaDia(string dia, Aula aula, int periodo, string descricaoDisciplina)
        {
            if (!aula.Reserva.DiaSemana.Equals(dia))
                return "";

            return $"({periodo}° Período){Environment.NewLine}{descricaoDisciplina}";
        }

        private async Task<Disciplina> ConsultarDisciplinaCurriculo(int codigoCurriculoDisciplina)
        {
            return await _cargoDisciplinaRepositorio.RetornarDisciplina(codigoCurriculoDisciplina);
        }
    }
}
