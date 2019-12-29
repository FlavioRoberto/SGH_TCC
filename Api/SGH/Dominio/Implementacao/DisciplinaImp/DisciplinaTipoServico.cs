using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Dominio.Model.DisciplinaModel;
using Aplicacao.Contratos;

namespace Aplicacao.Implementacao.DisciplinaImp
{
    public class DisciplinaTipoServico : IDisciplinaTipoService
    {
        private readonly IRepositorio<DisciplinaTipo> _repositorio;
        private readonly IMapper _mapper;

        public DisciplinaTipoServico(IRepositorio<DisciplinaTipo> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public virtual async Task<Resposta<DisciplinaTipoViewModel>> Atualizar(DisciplinaTipoViewModel entidadeViewModel)
        {
            try
            {
                var entidade = _mapper.Map<DisciplinaTipo>(entidadeViewModel);
                var resultado = _mapper.Map<DisciplinaTipoViewModel>(await _repositorio.Atualizar(entidade));
                return new Resposta<DisciplinaTipoViewModel>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaTipoViewModel>(entidadeViewModel, $"Ocorreu um erro ao atualizar o  tipo: {e.Message}");
            }

        }

        public virtual async Task<Resposta<DisciplinaTipoViewModel>> Criar(DisciplinaTipoViewModel entidade)
        {
            try
            {
                var resultado = await _repositorio.Criar(_mapper.Map<DisciplinaTipo>(entidade));
                return new Resposta<DisciplinaTipoViewModel>(_mapper.Map<DisciplinaTipoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaTipoViewModel>(entidade, $"Ocorreu um erro ao criar o  tipo: {e.Message}");
            }
        }

        public async Task<Resposta<Paginacao<DisciplinaTipoViewModel>>> ListarComPaginacao(Paginacao<DisciplinaTipoViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<DisciplinaTipo>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<DisciplinaTipoViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<DisciplinaTipoViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<DisciplinaTipoViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<DisciplinaTipoViewModel>>(null, $"Ocorreu um erro ao listar o  tipo: {e.Message}");
            }
        }

        public async Task<Resposta<List<DisciplinaTipoViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<DisciplinaTipoViewModel>>(_mapper.Map<List<DisciplinaTipoViewModel>>(resultado));
        }

        public async Task<Resposta<bool>> Remover(long id)
        {
            try
            {

                var result = await _repositorio.Remover(lnq => lnq.Codigo == id);

                if (result)
                    return new Resposta<bool>(result);

                return new Resposta<bool>(false, $"Não foi possível remover o tipo!");

            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Não foi possível remover o tipo: {e.Message}!");
            }
        }


    }
}
