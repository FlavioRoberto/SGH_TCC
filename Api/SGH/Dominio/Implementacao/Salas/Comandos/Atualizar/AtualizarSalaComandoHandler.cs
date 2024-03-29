﻿using AutoMapper;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.ViewModel;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Atualizar
{
    public class AtualizarSalaComandoHandler : IRequestHandler<AtualizarSalaComando, Resposta<SalaViewModel>>
    {
        private readonly IValidador<AtualizarSalaComando> _validador;
        private readonly ISalaRepositorio _salaRepositorio;
        private readonly IMapper _mapper;

        public AtualizarSalaComandoHandler(IValidador<AtualizarSalaComando> validador, 
                                           ISalaRepositorio salaRepositorio,
                                           IMapper mapper)
        {
            _validador = validador;
            _salaRepositorio = salaRepositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<SalaViewModel>> Handle(AtualizarSalaComando request, CancellationToken cancellationToken)
        {
            var erro = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erro))
                return new Resposta<SalaViewModel>(erro);

            var sala = _mapper.Map<Sala>(request);

            sala = await _salaRepositorio.Atualizar(sala);

            var salaViewModel = _mapper.Map<SalaViewModel>(sala);

            return new Resposta<SalaViewModel>(salaViewModel);            

        }
    }
}
