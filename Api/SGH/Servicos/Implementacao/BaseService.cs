using AutoMapper;
using Dominio.ViewModel;
using Global;
using Repositorio;
using Servico.Contratos;
using System;
using System.Collections.Generic;


namespace Servico.Implementacao
{
    public abstract class BaseService<TViewModel, TModel> : IServicoBase<TViewModel> where TModel : class where TViewModel : class
    {
        private string _nomeEntidade;
        protected readonly IRepositorio<TModel> _repositorio;
        protected readonly IMapper _mapper;
        protected abstract Resposta<TViewModel> ListarPeloCodigo(long id);
        protected abstract Resposta<bool> RemoverPeloCodigo(long id);

        public BaseService(IRepositorio<TModel> repositorio, IMapper mapper, string nomeEntidade)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _nomeEntidade = nomeEntidade;
        }

        public Resposta<TViewModel> Atualizar(TViewModel entidadeViewModel)
        {
            try
            {
                var entidade = _mapper.Map<TModel>(entidadeViewModel);
                var resultado = _mapper.Map<TViewModel>(_repositorio.Atualizar(entidade).Result);
                return new Resposta<TViewModel>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<TViewModel>(entidadeViewModel, $"Ocorreu um erro ao atualizar {_nomeEntidade}: {e.Message}");
            }
        }

        public Resposta<TViewModel> Criar(TViewModel entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(_mapper.Map<TModel>(entidade)).Result;
                return new Resposta<TViewModel>(_mapper.Map<TViewModel>(resultado));
            }
            catch(Exception e)
            {
                return new Resposta<TViewModel>(entidade, $"Ocorreu um erro ao criar {_nomeEntidade}: {e.Message}");
            }
        }

        public Resposta<Paginacao<TViewModel>> ListarComPaginacao(Paginacao<TViewModel> entidade)
        {
            try
            {
                var resultado = _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<TModel>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<TViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<TViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<TViewModel>>(resultadoViewModel);
            }
            catch(Exception e)
            {
                return new Resposta<Paginacao<TViewModel>>(null, $"Ocorreu um erro ao listar {_nomeEntidade}: {e.Message}");
            }
        }

        public Resposta<List<TViewModel>> ListarTodos()
        {
            var resultado = _repositorio.ListarTodos().Result;
            return new Resposta<List<TViewModel>>(_mapper.Map<List<TViewModel>>(resultado));
        }

        public Resposta<bool> Remover(long id)
        {
            return RemoverPeloCodigo(id);
        }

        public Resposta<TViewModel> ListarPeloId(long id)
        {
            return ListarPeloCodigo(id);
        }
    }
}
