﻿using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using SGH.Dominio.ViewModel;
using System.Linq;

namespace SGH.Dominio.Services.Implementacao.Turnos.Comandos.Atualizar
{
    public class AtualizarTurnoComandoHandler : IRequestHandler<AtualizarTurnoComando, Resposta<TurnoViewModel>>
    {
        private readonly ITurnoRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IValidador<AtualizarTurnoComando> _validador;

        public AtualizarTurnoComandoHandler(ITurnoRepositorio repositorio, IMapper mapper, IValidador<AtualizarTurnoComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
            _mapper = mapper;
        }

        public async Task<Resposta<TurnoViewModel>> Handle(AtualizarTurnoComando request, CancellationToken cancellationToken)
        {

            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<TurnoViewModel>(erros);

            var turno = await _repositorio.Consultar(lnq => lnq.Codigo == request.Codigo);

            if (turno == null)
                return new Resposta<TurnoViewModel>("Não foi encontrado um turno com código informado.");

            turno.Descricao = request.Descricao;

            turno.Horarios = string.Join(", ", request.Horarios);

            var resultado = await _repositorio.Atualizar(turno);

            var turnoViewModel = _mapper.Map<TurnoViewModel>(resultado);

            return new Resposta<TurnoViewModel>(turnoViewModel);
        }
    }
}
