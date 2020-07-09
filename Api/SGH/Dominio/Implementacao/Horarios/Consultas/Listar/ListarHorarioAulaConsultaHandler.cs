using AutoMapper;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Horarios.Consultas.Listar
{
    public class ListarHorarioAulaConsultaHandler : IRequestHandler<ListarHorarioAulaConsulta, List<HorarioAulaViewModel>>
    {
        private readonly IHorarioAulaRepositorio _horarioAulaRepositorio;
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMapper _mapper;
        private readonly IUsuarioResolverService _usuarioResolverService;

        public ListarHorarioAulaConsultaHandler(IHorarioAulaRepositorio horarioAulaRepositorio, 
                                                IMapper mapper,
                                                IUsuarioResolverService usuarioResolverService,
                                                IUsuarioRepositorio usuarioRepositorio)
        {
            _horarioAulaRepositorio = horarioAulaRepositorio;
            _mapper = mapper;
            _usuarioResolverService = usuarioResolverService;
            _usuarioRepositorio = usuarioRepositorio;
        }

        public async Task<List<HorarioAulaViewModel>> Handle(ListarHorarioAulaConsulta request, CancellationToken cancellationToken)
        {
            List<HorarioAula> horarios = await ListarHorarios(request);

            var horariosViewModel = _mapper.Map<List<HorarioAulaViewModel>>(horarios);

            return horariosViewModel;
        }

        private async Task<List<HorarioAula>> ListarHorarios(ListarHorarioAulaConsulta request)
        {
            return await _horarioAulaRepositorio.Listar(new ListarHorarioFiltro
            {
                Ano = request.Ano,
                CodigoCurriculo = request.CodigoCurriculo,
                Periodo = request.Periodo,
                Semestre = request.Semestre,
                codigoUsuario = await RetornarUsuario()
            });
        }

        private async Task<int> RetornarUsuario()
        {
            var codigoUsuarioLogado = _usuarioResolverService.RetornarCodigoUsuario();
          
            var perfil = await _usuarioRepositorio.ConsultarPerfil(codigoUsuarioLogado);

            if (perfil.Administrador)
                return 0;

            return codigoUsuarioLogado;
        }
    }
}
