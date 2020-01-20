using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Repositorio.Contratos;
using Aplicacao.Contratos;
using Aplicacao.Exceptions;
using SGH.Dominio.Core;

namespace Aplicacao.Implementacao
{
    public class ProfessorServico : IProfessorService
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly IMapper _mapper;

        public ProfessorServico(IProfessorRepositorio repositorio, IMapper mapper) 
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

      
        public async Task<Resposta<List<ProfessorViewModel>>> ListarAtivos()
        {
            var professores = _mapper.Map<List<ProfessorViewModel>>(await _repositorio.ListarPor(prof => prof.Ativo == true));

            if (professores.Count <= 0)
                return new Resposta<List<ProfessorViewModel>>(null, "Não foram encontrados professores ativos!");

            return new Resposta<List<ProfessorViewModel>>(professores);
        }

        public async Task<Resposta<List<ProfessorViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<ProfessorViewModel>>(_mapper.Map<List<ProfessorViewModel>>(resultado));
        }

        public async Task<Resposta<Paginacao<ProfessorViewModel>>> ListarComPaginacao(Paginacao<ProfessorViewModel> entidade)
        {
            try
            {
                var resultado = await _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<Professor>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<ProfessorViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<ProfessorViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<ProfessorViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<ProfessorViewModel>>(null, $"Ocorreu um erro ao listar os professores: {e.Message}");
            }
        }

        public async Task<Resposta<bool>> Remover(long id)
        {
            try
            {
                var result = await _repositorio.Remover(lnq => lnq.Codigo == id);

                if (result)
                    return new Resposta<bool>(result);

                return new Resposta<bool>(false, $"Não foi possível remover o professor!");

            }
            catch (Exception e)
            {
                if (e.GetType() == typeof(ValidacaoException))
                    return new Resposta<bool>(false, e.Message);

                return new Resposta<bool>(false, $"Não foi possível remover o professor: {e.Message}!");

            }
        }

        private async Task<bool> ValidarSeMatriculaExistente(string matricula, int codigo)
        {
            var resultado = await _repositorio.ListarPor(lnq => lnq.Matricula == matricula && lnq.Codigo != codigo);
            return resultado.Count > 0;
        }

    }
}
