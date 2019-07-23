using AutoMapper;
using Dominio.Contratos;
using Dominio.ViewModel;
using Global;
using Repositorio;
using Servico.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servico.Implementacao
{
    public abstract class BaseService<TViewModel, TModel> : IServicoBase<TViewModel> where TModel : EntidadeBase where TViewModel : class
    {
        private string _nomeEntidade;
        protected readonly IRepositorio<TModel> _repositorio;
        protected readonly IMapper _mapper;

        public BaseService(IRepositorio<TModel> repositorio, IMapper mapper, string nomeEntidade)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _nomeEntidade = nomeEntidade;
        }

        public virtual async Task<Resposta<TViewModel>> Atualizar(TViewModel entidadeViewModel)
        {
            try
            {
                entidadeViewModel = await ValidarEdicao(entidadeViewModel);
                var entidade = _mapper.Map<TModel>(entidadeViewModel);
                var resultado = _mapper.Map<TViewModel>(await _repositorio.Atualizar(entidade));
                return new Resposta<TViewModel>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<TViewModel>(entidadeViewModel, $"Ocorreu um erro ao atualizar {_nomeEntidade}: {e.Message}");
            }
        }

        public virtual async Task<Resposta<TViewModel>> Criar(TViewModel entidade)
        {
            try
            {
                entidade = await ValidarInsercao(entidade);
                var resultado = await _repositorio.Criar(_mapper.Map<TModel>(entidade));
                return new Resposta<TViewModel>(_mapper.Map<TViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<TViewModel>(entidade, $"Ocorreu um erro ao criar {_nomeEntidade}: {e.Message}");
            }
        }

        public async Task<Resposta<Paginacao<TViewModel>>> ListarComPaginacao(Paginacao<TViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<TModel>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<TViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<TViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<TViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<TViewModel>>(null, $"Ocorreu um erro ao listar {_nomeEntidade}: {e.Message}");
            }
        }

        public async Task<Resposta<List<TViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<TViewModel>>(_mapper.Map<List<TViewModel>>(resultado));
        }

        public async Task<Resposta<bool>> Remover(long id)
        {
            id = await ValidarRemocao(id);

            var result = await _repositorio.Remover(lnq => lnq.Codigo == id);

            if (result)
                return new Resposta<bool>(result);

            return new Resposta<bool>(false, $"Não foi possível remover {_nomeEntidade}!");
        }

        public async Task<Resposta<TViewModel>> ListarPeloId(long id)
        {
            var result = await _repositorio.Listar(lnq => lnq.Codigo == id);

            if (result == null)
                return new Resposta<TViewModel>(null, $"Não foi encontrado a {_nomeEntidade}!");

            var viewModel = _mapper.Map<TViewModel>(result);
            return new Resposta<TViewModel>(viewModel);

        }

        public virtual Task<TViewModel> ValidarInsercao(TViewModel viewModel)
        {
            return Task.FromResult(viewModel);
        }

        public virtual Task<TViewModel> ValidarEdicao(TViewModel viewModel)
        {
            return Task.FromResult(viewModel);
        }
        public virtual Task<long> ValidarRemocao(long id)
        {
            return Task.FromResult(id);
        }
    }
}
