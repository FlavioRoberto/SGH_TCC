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

            var curso = await RetornarDescricaoCurso(request.CodigoCurso);

            var turno = await RetornarDescricaoTurno(request.CodigoTurno);

            var horarios = await RetornarHorarios(request);

            var aulas = await RetornarAulas(horarios);

            var semestre = request.Semestre.RetornarDescricao();

            var dados = new HorarioGeralRelatorioData(request.Ano, curso, turno, semestre, horarios, aulas);

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
                Periodo = lnq.Periodo.RetornarDescricao()
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

            for (var i = 0; i < 5; i++)
            {
                aulasRelatorio.Add(new HorarioGeralAulaData
                {
                    DisciplinaSegunda = await RetornarDisciplinaPorDiaSemanaHora(aulas, i, "Segunda"),
                    DisciplinaTerca = await RetornarDisciplinaPorDiaSemanaHora(aulas, i, "Terça"),
                    DisciplinaQuarta = await RetornarDisciplinaPorDiaSemanaHora(aulas, i, "Quarta"),
                    DisciplinaQuinta = await RetornarDisciplinaPorDiaSemanaHora(aulas, i, "Quinta"),
                    DisciplinaSexta = await RetornarDisciplinaPorDiaSemanaHora(aulas, i, "Sexta"),
                    DisciplinaSabado = await RetornarDisciplinaPorDiaSemanaHora(aulas, i, "Sábado"),
                    HorarioCodigo = horario.Codigo
                });
            }
            return aulasRelatorio;
        }

        private async Task<DisciplinaData> RetornarDisciplinaPorDiaSemanaHora(List<Aula> aulas, int posicao, string diaSemana)
        {
            if (aulas.Count < posicao)
                return new DisciplinaData();

            var aulaPosicao = aulas.Where(lnq => lnq.Reserva.DiaSemana == diaSemana)
                                   .OrderBy(lnq => lnq.Reserva.Hora)
                                   .Skip(posicao)
                                   .FirstOrDefault();
            if (aulaPosicao == null)
                return new DisciplinaData();

            var descricaoCargo = await _cargoService.RetornarProfessor(aulaPosicao.Disciplina.CodigoCargo);
            var descricaoSala = await RetornarDescricaoSala(aulaPosicao.CodigoSala);
            var descricaoDesdobramento = !string.IsNullOrEmpty(aulaPosicao.DescricaoDesdobramento) ? $" {aulaPosicao.DescricaoDesdobramento} { Environment.NewLine}" : "";

            return new DisciplinaData
            {
                Disciplina = $"{aulaPosicao.Disciplina.Descricao} {Environment.NewLine} {descricaoDesdobramento} ({descricaoCargo}) {Environment.NewLine} {descricaoSala}",
                Hora = aulaPosicao.Reserva.Hora.ToString()
            };
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
