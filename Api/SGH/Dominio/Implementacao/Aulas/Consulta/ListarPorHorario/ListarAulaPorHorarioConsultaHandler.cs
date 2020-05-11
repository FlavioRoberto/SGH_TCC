using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Services.Implementacao.Aulas.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Aulas.Consulta.ListarPorHorario
{
    public class ListarAulaPorHorarioConsultaHandler : IRequestHandler<ListarAulaPorHorarioConsulta, Resposta<ICollection<AulaViewModel>>>
    {
        private readonly IAulaRepositorio _aulaRepositorio;
        private readonly ICargoDisciplinaRepositorio _cargoDisciplinaRepositorio;
        private readonly ICargoRepositorio _cargoRepositorio;
        private readonly ISalaRepositorio _salaRepositorio;
        private readonly IValidador<ListarAulaPorHorarioConsulta> _validador;
        

        public ListarAulaPorHorarioConsultaHandler(IAulaRepositorio aulaRepositorio, 
                                                   ICargoDisciplinaRepositorio cargoDisciplinaRepositorio,
                                                   ICargoRepositorio cargoRepositorio,
                                                   ISalaRepositorio salaRepositorio,
                                                   IValidador<ListarAulaPorHorarioConsulta> validador)
        {
            _aulaRepositorio = aulaRepositorio;
            _cargoDisciplinaRepositorio = cargoDisciplinaRepositorio;
            _cargoRepositorio = cargoRepositorio;
            _validador = validador;
            _salaRepositorio = salaRepositorio;
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
                var professor = await RetornarProfessorCargo(cargo);
                var disciplina = await RetornarDisciplina(aula.CodigoDisciplina);
                var sala = await RetornarSalaDisciplina(aula.CodigoSala);

                var aulaViewModel = new AulaViewModel
                {
                    CodigoHorario = aula.CodigoHorario,
                    Codigo = aula.Codigo,
                    CodigoCargo = cargo.Codigo,
                    CodigoSala = aula.CodigoSala,
                    DescricaoDesdobramento = aula.DescricaoDesdobramento,
                    HorarioExtrapolado = false,
                    Laboratorio = aula.Laboratorio,
                    Sala = sala.Descricao,
                    Disciplina = disciplina.Descricao,
                    Professor = professor,
                    Reserva = aula.Reserva
                };

                aulasViewModel.Add(aulaViewModel);
            }

            return aulasViewModel;
        }

        private async Task<Sala> RetornarSalaDisciplina(int codigoSala)
        {
            return await _salaRepositorio.Consultar(lnq => lnq.Codigo == codigoSala);
        }

        private async Task<CargoDisciplina> RetornarDisciplina(int codigoDisciplina)
        {
            return await _cargoDisciplinaRepositorio.Consultar(lnq => lnq.Codigo == codigoDisciplina);
        }

        private async Task<string> RetornarProfessorCargo(Cargo cargo)
        {
            var professor = await _cargoRepositorio.ConsultarProfessor(cargo.Codigo);
            return professor == null ? cargo.Numero.ToString() : professor.Nome;
        }

        private async Task<Cargo> RetornarCargoDisciplina(int codigoDisciplina)
        {
            return await _cargoDisciplinaRepositorio.ConsultarCargo(codigoDisciplina);
        }
    }
}
