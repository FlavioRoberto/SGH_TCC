﻿using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace SGH.Dominio.Services.Implementacao.Aulas.Consulta.ListarPorHorario
{
    public class ListarAulaPorHorarioConsultaHandler : IRequestHandler<ListarAulaPorHorarioConsulta, Resposta<ICollection<AulaViewModel>>>
    {
        private readonly IAulaRepositorio _aulaRepositorio;
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly ISalaRepositorio _salaRepositorio;
        private readonly ICurriculoDisciplinaRepositorio _curriculoDisciplinaRepositorio;
        private readonly IValidador<ListarAulaPorHorarioConsulta> _validador;


        public ListarAulaPorHorarioConsultaHandler(IAulaRepositorio aulaRepositorio,
                                                   ICargoDisciplinaRepositorio cargoDisciplinaRepositorio,
                                                   ISalaRepositorio salaRepositorio,
                                                   ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio,
                                                   IValidador<ListarAulaPorHorarioConsulta> validador)
        {
            _aulaRepositorio = aulaRepositorio;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _validador = validador;
            _salaRepositorio = salaRepositorio;
            _curriculoDisciplinaRepositorio = curriculoDisciplinaRepositorio;
        }

        public async Task<Resposta<ICollection<AulaViewModel>>> Handle(ListarAulaPorHorarioConsulta request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<ICollection<AulaViewModel>>(erros);

            var aulas = await _aulaRepositorio.Listar(lnq => lnq.CodigoHorario == request.CodigoHorario);

            var aulasViewModel = await PrepararAulasViewModel(aulas);

            return new Resposta<ICollection<AulaViewModel>>(aulasViewModel);
        }

        private async Task<ICollection<AulaViewModel>> PrepararAulasViewModel(List<Aula> aulas)
        {
            var aulasViewModel = new List<AulaViewModel>();

            foreach(var aula in aulas)
            {
                var cargo = await RetornarCargoDisciplina(aula.CodigoDisciplina);
                var professor =  RetornarProfessores(aula, cargo);
                var disciplina = await RetornarDisciplina(aula.CodigoDisciplina);
                var sala = await RetornarSalaDisciplina(aula.CodigoSala);
                var horarioExtrapolado = await VerificarSeHorarioExtrapolado(aula.CodigoHorario, disciplina, aula.Laboratorio);

                var aulaViewModel = new AulaViewModel
                {
                    CodigoHorario = aula.CodigoHorario,
                    Codigo = aula.Codigo,
                    CodigoCargo = cargo.Codigo,
                    CodigoSala = aula.CodigoSala ?? 0,
                    DescricaoDesdobramento = aula.DescricaoDesdobramento,
                    HorarioExtrapolado = horarioExtrapolado,
                    Laboratorio = aula.Laboratorio,
                    Sala = sala?.Descricao,
                    Disciplina = disciplina.Descricao,
                    Professor = professor,
                    Reserva = aula.Reserva
                };

                aulasViewModel.Add(aulaViewModel);
            }

            return aulasViewModel;
        }

        private string RetornarProfessores(Aula aula, Cargo cargo)
        {
            var professor = cargo.RetornarDescricao();

            var professoresAuxiliares = aula.DisciplinasAuxiliar.Select(lnq => lnq.Disciplina.Cargo.RetornarDescricao());

            return professoresAuxiliares.Count() > 0 ? $"{professor}, {string.Join(", ", professoresAuxiliares)}" : professor;
        }

        private async Task<bool> VerificarSeHorarioExtrapolado(long codigoHorario, CargoDisciplina disciplina, bool laboratorio)
        {
            var curriculoDisciplina = await _curriculoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == disciplina.CodigoCurriculoDisciplina);

            var totalAula = await RetornarQuantidadeAulaDistribuida(codigoHorario, disciplina, laboratorio);

            var totalAulaLimite = laboratorio ? curriculoDisciplina.AulasSemanaisPratica : curriculoDisciplina.AulasSemanaisTeorica;

            var aulaExtrapolada = totalAula > totalAulaLimite;

            return aulaExtrapolada;
        }

        private async Task<double> RetornarQuantidadeAulaDistribuida(long codigoHorario, CargoDisciplina disciplina, bool laboratorio)
        {
            var aulasDistribuidas = await _aulaRepositorio.Listar(lnq => lnq.CodigoHorario == codigoHorario &&
                                                                         lnq.CodigoDisciplina == disciplina.Codigo &&
                                                                         lnq.Laboratorio == laboratorio);
            var totalAula = 0.0;

            foreach (var aula in aulasDistribuidas)
            {
                if (!aula.Desdobramento)
                    totalAula += 1;
                else
                    totalAula += 0.5;
            }

            return totalAula;
        }

        private async Task<Sala> RetornarSalaDisciplina(long? codigoSala)
        {
            return await _salaRepositorio.Consultar(lnq => lnq.Codigo == codigoSala);
        }

        private async Task<CargoDisciplina> RetornarDisciplina(long codigoDisciplina)
        {
            return await _cargoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == codigoDisciplina);
        }

        private async Task<Cargo> RetornarCargoDisciplina(long codigoDisciplina)
        {
            return await _cargoDisciplinaRepositorio.ConsultarCargo(codigoDisciplina);
        }
    }
}
