using AutoMapper;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace Aplicacao.Implementacao.Usuarios
{
    public class UsuarioServico : IUsuarioService
    {
        private IUsuarioResolverService _userResolver;
        private readonly IEmailService _emailService;
        private readonly IRepositorio<Usuario> _repositorio;
        private readonly IMapper _mapper;

        public UsuarioServico(IUsuarioRepositorio repositorio, IMapper mapper, IUsuarioResolverService userResolver, IEmailService emailSender)
        {
            _userResolver = userResolver ;
            _emailService = emailSender;
            _repositorio = repositorio;
            _mapper = mapper;
        }

      
        public async Task<Resposta<Paginacao<UsuarioViewModel>>> ListarComPaginacao(Paginacao<UsuarioViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<Usuario>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<UsuarioViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<UsuarioViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<UsuarioViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<UsuarioViewModel>>(null, $"Ocorreu um erro ao listar o usuário: {e.Message}");
            }
        }

        public async Task<Resposta<List<UsuarioViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<UsuarioViewModel>>(_mapper.Map<List<UsuarioViewModel>>(resultado));
        }


    }
}
