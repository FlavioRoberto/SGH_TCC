using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel;
using Dominio.ViewModel.AutenticacaoViewModel;
using Global;
using Repositorio;
using Servico.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servico.Implementacao.Usuarios
{
    public class UsuarioPerfilServico : IUsuarioPerfilService
    {
        private readonly IRepositorio<UsuarioPerfil> _repositorio;
        private readonly IMapper _mapper;

        public UsuarioPerfilServico(IRepositorio<UsuarioPerfil> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public async Task<Resposta<UsuarioPerfilViewModel>> Atualizar(UsuarioPerfilViewModel entidadeViewModel)
        {
            try
            {
                var entidade = _mapper.Map<UsuarioPerfil>(entidadeViewModel);
                var resultado = _mapper.Map<UsuarioPerfilViewModel>(await _repositorio.Atualizar(entidade));
                return new Resposta<UsuarioPerfilViewModel>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<UsuarioPerfilViewModel>(entidadeViewModel, $"Ocorreu um erro ao atualizar perfil: {e.Message}");
            }

        }

        public async Task<Resposta<UsuarioPerfilViewModel>> Criar(UsuarioPerfilViewModel entidade)
        {
            try
            {
                var resultado = await _repositorio.Criar(_mapper.Map<UsuarioPerfil>(entidade));
                return new Resposta<UsuarioPerfilViewModel>(_mapper.Map<UsuarioPerfilViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<UsuarioPerfilViewModel>(entidade, $"Ocorreu um erro ao criar perfil: {e.Message}");
            }
        }

        public async Task<Resposta<Paginacao<UsuarioPerfilViewModel>>> ListarComPaginacao(Paginacao<UsuarioPerfilViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<UsuarioPerfil>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<UsuarioPerfilViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<UsuarioPerfilViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<UsuarioPerfilViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<UsuarioPerfilViewModel>>(null, $"Ocorreu um erro ao listar perfil: {e.Message}");
            }
        }

        public async Task<Resposta<List<UsuarioPerfilViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<UsuarioPerfilViewModel>>(_mapper.Map<List<UsuarioPerfilViewModel>>(resultado));
        }

        public async Task<Resposta<bool>> Remover(long id)
        {
            try
            {
                var result = await _repositorio.Remover(lnq => lnq.Codigo == id);

                if (result)
                    return new Resposta<bool>(result);

                return new Resposta<bool>(false, $"Não foi possível remover perfil!");

            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Não foi possível remover perfil: {e.Message}!");
            }
        }

    }
}
