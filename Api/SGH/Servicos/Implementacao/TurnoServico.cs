using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Repositorio;
using Servico.Contratos;

namespace Servico.Implementacao
{
    public class TurnoServico : ITurnoService
    {
        private readonly IRepositorio<Turno> _repositorio;
        private readonly IMapper _mapper;


        public TurnoServico(IRepositorio<Turno> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public virtual async Task<Resposta<TurnoViewModel>> Atualizar(TurnoViewModel entidadeViewModel)
        {
            try
            {
                var entidade = _mapper.Map<Turno>(entidadeViewModel);
                var resultado = _mapper.Map<TurnoViewModel>(await _repositorio.Atualizar(entidade));
                return new Resposta<TurnoViewModel>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<TurnoViewModel>(entidadeViewModel, $"Ocorreu um erro ao atualizar o turno: {e.Message}");
            }
        }

        public virtual async Task<Resposta<TurnoViewModel>> Criar(TurnoViewModel entidade)
        {
            try
            {
                var resultado = await _repositorio.Criar(_mapper.Map<Turno>(entidade));
                return new Resposta<TurnoViewModel>(_mapper.Map<TurnoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<TurnoViewModel>(entidade, $"Ocorreu um erro ao criar o turno: {e.Message}");
            }
        }

        public async Task<Resposta<Paginacao<TurnoViewModel>>> ListarComPaginacao(Paginacao<TurnoViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<Turno>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<TurnoViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<TurnoViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<TurnoViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<TurnoViewModel>>(null, $"Ocorreu um erro ao listar o turno: {e.Message}");
            }
        }

        public async Task<Resposta<List<TurnoViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<TurnoViewModel>>(_mapper.Map<List<TurnoViewModel>>(resultado));
        }

        public async Task<Resposta<bool>> Remover(long id)
        {
            try
            {
                var result = await _repositorio.Remover(lnq => lnq.Codigo == id);

                if (result)
                    return new Resposta<bool>(result);

                return new Resposta<bool>(false, $"Não foi possível remover o turno!");

            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Não foi possível remover o turno: {e.Message}!");
            }
        }

        public async Task<Resposta<TurnoViewModel>> ListarPeloId(long id)
        {
            var result = await _repositorio.Listar(lnq => lnq.Codigo == id);

            if (result == null)
                return new Resposta<TurnoViewModel>(null, $"Não foi encontrado a o turno!");

            var viewModel = _mapper.Map<TurnoViewModel>(result);
            return new Resposta<TurnoViewModel>(viewModel);

        }
    }
}
