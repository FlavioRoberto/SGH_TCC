using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Relatorios.Contratos;
using SGH.Relatorios.DataSets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Relatorios.Consultas.HorarioGeral
{
    public class GerarRelatorioHorarioGeralConsultaHandler : IRequestHandler<GerarHorarioGeralRelatorioConsulta, Resposta<string>>
    {
        private readonly IValidador<GerarHorarioGeralRelatorioConsulta> _validador;
        private readonly IRelatorioServico _relatorioServico;
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;
        private readonly ITurnoRepositorio _turnoRepositorio;
        private readonly IAulaRepositorio _aulaRepositorio;
        private readonly ICursoRepositorio _cursoRepositorio;
        private readonly ICurriculoRepositorio _curriculoRepositorio;
        private readonly ISalaRepositorio _salaRepositorio;
        private readonly ICargoService _cargoService;

        public GerarRelatorioHorarioGeralConsultaHandler(IValidador<GerarHorarioGeralRelatorioConsulta> validador,
                                                         IRelatorioServico relatorioServico,
                                                         IHorarioAulaRepositorio horarioAulaRepositorio,
                                                         IAulaRepositorio aulaRepositorio,
                                                         ITurnoRepositorio turnoRepositorio,
                                                         ICursoRepositorio cursoRepositorio,
                                                         ICurriculoRepositorio curriculoRepositorio,
                                                         ISalaRepositorio salaRepositorio,
                                                         ICargoService cargoService)
        {
            _validador = validador;
            _relatorioServico = relatorioServico;
            _horarioAulaRepositorio = horarioAulaRepositorio;
            _turnoRepositorio = turnoRepositorio;
            _aulaRepositorio = aulaRepositorio;
            _cursoRepositorio = cursoRepositorio;
            _curriculoRepositorio = curriculoRepositorio;
            _salaRepositorio = salaRepositorio;
            _cargoService = cargoService;
        }

        public async Task<Resposta<string>> Handle(GerarHorarioGeralRelatorioConsulta request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<string>(erro);

            var curso = await RetornarDescricaoCurso(request.CursoId);

            var turno = await RetornarDescricaoTurno(request.TurnoId);

            var horarios = await RetornarHorarios(request);

            var aulas = await RetornarAulas(horarios);

            var semestre = request.Semestre.RetornarDescricao();

            var dados = new HorarioRelatorioData(request.Ano, curso, turno, semestre, horarios, aulas);

            var bytesRelatorio = _relatorioServico.GerarRelatorioHorario(dados);

            var base64 = Convert.ToBase64String(bytesRelatorio);

            return new Resposta<string>(base64);
        }

        private async Task<string> RetornarDescricaoCurso(int cursoId)
        {
            var curso = await _cursoRepositorio.Consultar(lnq => lnq.Codigo == cursoId);
            return curso.Descricao;
        }

        private async Task<string> RetornarDescricaoTurno(int turnoId)
        {
            var turno = await _turnoRepositorio.Consultar(lnq => lnq.Codigo == turnoId);
            return turno.Descricao;
        }

        private async Task<IList<QuadroHorario>> RetornarHorarios(GerarHorarioGeralRelatorioConsulta request)
        {
            var curriculosCurso = await _curriculoRepositorio.ListarCodigos(lnq => lnq.CodigoCurso == request.CursoId);

            if (curriculosCurso.Count <= 0)
                return new List<QuadroHorario>();

            var horarios = await _horarioAulaRepositorio.Listar(lnq => curriculosCurso.Contains(lnq.CodigoCurriculo) &&
                                                                       lnq.Ano == request.Ano &&
                                                                       lnq.CodigoTurno == request.TurnoId &&
                                                                       lnq.Semestre == request.Semestre);
            return horarios.Select(lnq => new QuadroHorario
            {
                Avisos = "",
                Codigo = lnq.Codigo,
                Periodo = lnq.Periodo.RetornarDescricao()
            }).ToList();
        }

        private async Task<IList<AulaData>> RetornarAulas(IEnumerable<QuadroHorario> horarios)
        {
            var aulasRelatorio = new List<AulaData>();

            foreach (var horario in horarios) {
                var aulaRelatorio = await GerarAulasPorHorario(horario);
                aulasRelatorio.AddRange(aulaRelatorio);
            }

            return aulasRelatorio;
        }

        private async Task<List<AulaData>> GerarAulasPorHorario(QuadroHorario horario)
        {
            var aulasRelatorio = new List<AulaData>();

            var aulas = await _aulaRepositorio.ListarComDisciplinas(lnq => lnq.CodigoHorario == horario.Codigo);

            var aulasPorHorario = aulas.GroupBy(lnq => lnq.Reserva.Hora).Select(lnq => lnq).ToList();

            foreach (var aula in aulasPorHorario)
            {
                aulasRelatorio.Add(new AulaData
                {
                    DisciplinaSegunda = await RetornarDisciplinaPorDiaSemanaHora(aula, "Segunda"),
                    DisciplinaTerca = await RetornarDisciplinaPorDiaSemanaHora(aula, "Terça"),
                    DisciplinaQuarta = await RetornarDisciplinaPorDiaSemanaHora(aula, "Quarta"),
                    DisciplinaQuinta = await RetornarDisciplinaPorDiaSemanaHora(aula, "Quinta"),
                    DisciplinaSexta = await RetornarDisciplinaPorDiaSemanaHora(aula, "Sexta"),
                    DisciplinaSabado = await RetornarDisciplinaPorDiaSemanaHora(aula, "Sábado"),
                    Hora = aula.Key,
                    HorarioCodigo = horario.Codigo
                });
            }

            return aulasRelatorio;
        }

        private async Task<string> RetornarDisciplinaPorDiaSemanaHora(IGrouping<string, Aula> aula, string diaSemana)
        {
            var disciplina = aula.FirstOrDefault(lnq => lnq.Reserva.DiaSemana == diaSemana && lnq.Reserva.Hora == aula.Key);

            if (disciplina == null)
                return "";

            var descricaoCargo = await _cargoService.RetornarProfessor(disciplina.Disciplina.CodigoCargo);
            var descricaoSala = await RetornarDescricaoSala(disciplina.CodigoSala);

            return $"{disciplina.Disciplina.Descricao} \r\n ({descricaoCargo}) \r\n {descricaoSala}";
        }

        private async Task<string> RetornarDescricaoSala(int codigoSala)
        {
            var sala = await _salaRepositorio.Consultar(lnq => lnq.Codigo == codigoSala);
            
            if (sala == null)
                return "Sala não encontrada";
            
            return sala.Descricao;
        }

    }
}
