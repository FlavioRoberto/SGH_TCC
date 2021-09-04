using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Core.Reports;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Core.DomainObjects.Datasets;
using SGH.Dominio.Services.Contratos;

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
        private readonly List<DisciplinaData> _disciplinasSabado;

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
            _disciplinasSabado = new List<DisciplinaData>();
        }

        public async Task<Resposta<string>> Handle(GerarHorarioGeralRelatorioConsulta request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<string>(erro);

            var curso = await RetornarDescricaoCurso(request.CodigoCurso);

            var turno = await RetornarDescricaoTurno(request.CodigoTurno);

            var horarios = await RetornarHorarios(request);

            var aulas = await RetornarAulas(horarios);

            var semestre = request.Semestre.RetornarDescricao();
                       
            var dados = new HorarioGeralRelatorioData(request.Ano, curso, turno, semestre, horarios, aulas, _disciplinasSabado);


            var bytesRelatorio = _relatorioServico.GerarRelatorioHorarioGeral(dados);

            var base64 = Convert.ToBase64String(bytesRelatorio);

            return new Resposta<string>(base64, "");
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

        private async Task<IList<QuadroHorarioData>> RetornarHorarios(GerarHorarioGeralRelatorioConsulta request)
        {
            var curriculosCurso = await _curriculoRepositorio.ListarCodigos(lnq => lnq.CodigoCurso == request.CodigoCurso);

            if (curriculosCurso.Count <= 0)
                return new List<QuadroHorarioData>();

            var horarios = await _horarioAulaRepositorio.Listar(lnq => curriculosCurso.Contains(lnq.CodigoCurriculo) &&
                                                                       lnq.Ano == request.Ano &&
                                                                       lnq.CodigoTurno == request.CodigoTurno &&
                                                                       lnq.Semestre == request.Semestre);
            return horarios.Select(lnq => new QuadroHorarioData
            {
                Avisos = lnq.Mensagem,
                Codigo = lnq.Codigo,
                Periodo = ((int)lnq.Periodo).ToString(),
                TurnoId = lnq.CodigoTurno
            }).ToList();
        }

        private async Task<IList<HorarioGeralAulaData>> RetornarAulas(IEnumerable<QuadroHorarioData> horarios)
        {
            var aulasRelatorio = new List<HorarioGeralAulaData>();

            foreach (var horario in horarios)
            {
                var aulaRelatorio = await GerarAulasPorHorario(horario);
                aulasRelatorio.AddRange(aulaRelatorio);
            }

            return aulasRelatorio;
        }

        private async Task<List<HorarioGeralAulaData>> GerarAulasPorHorario(QuadroHorarioData horario)
        {
            var aulasRelatorio = new List<HorarioGeralAulaData>();

            var aulas = await _aulaRepositorio.ListarComDisciplinas(lnq => lnq.CodigoHorario == horario.Codigo);

            var aulasSabado = aulas.Where(lnq => lnq.Reserva.DiaSemana == "Sábado")
                                   .OrderBy(lnq => TimeSpan.Parse(lnq.Reserva.Hora));

            foreach (var aula in aulasSabado)
            {
                var aulaSabado = await RetornarDescricaoDisciplinaData(aula);
                _disciplinasSabado.Add(aulaSabado);
            }

            var turno = await _turnoRepositorio.Consultar(lnq => lnq.Codigo == horario.TurnoId);

            var horariosTurno =  turno.Horarios.Split(',');

            foreach (var horas in horariosTurno)
            {
                var aula = new HorarioGeralAulaData
                {
                    DisciplinaSegunda = await RetornarDisciplinaPorDiaSemanaHora(aulas, horas, "Segunda"),
                    DisciplinaTerca = await RetornarDisciplinaPorDiaSemanaHora(aulas, horas, "Terça"),
                    DisciplinaQuarta = await RetornarDisciplinaPorDiaSemanaHora(aulas, horas, "Quarta"),
                    DisciplinaQuinta = await RetornarDisciplinaPorDiaSemanaHora(aulas, horas, "Quinta"),
                    DisciplinaSexta = await RetornarDisciplinaPorDiaSemanaHora(aulas, horas, "Sexta"),
                    HorarioCodigo = horario.Codigo
                };

                if (!AulaEstaVazia(aula)){
                    var disciplinaDefault = new DisciplinaData { Hora = horas };
                    aula.DisciplinaSegunda = aula.DisciplinaSegunda ?? disciplinaDefault;
                    aula.DisciplinaTerca = aula.DisciplinaTerca ?? disciplinaDefault;
                    aula.DisciplinaQuarta = aula.DisciplinaQuarta ?? disciplinaDefault;
                    aula.DisciplinaQuinta = aula.DisciplinaQuinta ?? disciplinaDefault;
                    aula.DisciplinaSexta = aula.DisciplinaSexta ?? disciplinaDefault;
                    aulasRelatorio.Add(aula);
                }
            }

            return aulasRelatorio;
        }

        private bool AulaEstaVazia(HorarioGeralAulaData aula)
        {
            return aula.DisciplinaSegunda == null &&
                   aula.DisciplinaTerca == null &&
                   aula.DisciplinaQuarta == null &&
                   aula.DisciplinaQuinta == null &&
                   aula.DisciplinaSexta == null;
        }

        private async Task<DisciplinaData> RetornarDisciplinaPorDiaSemanaHora(List<Aula> aulas, string horas, string diaSemana)
        {
            var aulaPosicao = aulas.Where(lnq => lnq.Reserva.DiaSemana == diaSemana && lnq.Reserva.Hora.Equals(horas))
                                   .FirstOrDefault();

            if (aulaPosicao == null)
                return null;

            return await RetornarDescricaoDisciplinaData(aulaPosicao);
        }

        private async Task<string> RetornarDescricaoSala(long? codigoSala)
        {
            var sala = await _salaRepositorio.Consultar(lnq => lnq.Codigo == codigoSala);

            if (sala == null)
                return "";

            return sala.Descricao;
        }

        private async Task<DisciplinaData> RetornarDescricaoDisciplinaData(Aula aulaPosicao)
        {
            var descricaoCargo = await _cargoService.RetornarProfessor(aulaPosicao);
            var descricaoSala = await RetornarDescricaoSala(aulaPosicao.CodigoSala);
            var descricaoDesdobramento = !string.IsNullOrEmpty(aulaPosicao.DescricaoDesdobramento) ? $" {aulaPosicao.DescricaoDesdobramento} { Environment.NewLine}" : "";

            return new DisciplinaData
            {
                Disciplina = $"{aulaPosicao.Disciplina.Descricao} {Environment.NewLine} {descricaoDesdobramento} ({descricaoCargo}) {Environment.NewLine} {descricaoSala}",
                Hora = aulaPosicao.Reserva.Hora.ToString(),
                HorarioCodigo = aulaPosicao.CodigoHorario
            };
        }

    }
}
