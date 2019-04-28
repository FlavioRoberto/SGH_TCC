using System;
using System.Collections.Generic;
using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using Repositorio;
using Servico.Contratos.DisciplinaServico;

namespace Servico.Implementacao.DisciplinaImp
{
    public class DisciplinaServico : IDisciplinaServico
    {
        private readonly IRepositorio<Disciplina> _repositorio;
        private readonly IMapper _mapper;

        public DisciplinaServico(IRepositorio<Disciplina> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public Resposta<DisciplinaViewModel> Atualizar(DisciplinaViewModel entidade)
        {
            try
            {
                var resultado = _repositorio.Atualizar(_mapper.Map<Disciplina>(entidade)).Result;
                return new Resposta<DisciplinaViewModel>(_mapper.Map<DisciplinaViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaViewModel>(entidade, $"Ocorreu um erro ao atualizar a disciplina: {e.Message}");
            }
        }

        public Resposta<DisciplinaViewModel> Criar(DisciplinaViewModel entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(_mapper.Map<Disciplina>(entidade)).Result;
                return new Resposta<DisciplinaViewModel>(_mapper.Map<DisciplinaViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaViewModel>(entidade, $"Ocorreu um erro ao criar a disciplina: {e.Message}");
            }
        }

        public Resposta<Paginacao<DisciplinaViewModel>> ListarComPaginacao(Paginacao<DisciplinaViewModel> entidade)
        {
            try
            {
                var resultado = _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<Disciplina>>(entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<DisciplinaViewModel>>(null, resultado.GetErros());

                return new Resposta<Paginacao<DisciplinaViewModel>>(_mapper.Map<Paginacao<DisciplinaViewModel>>( resultado.GetResultado()));
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<DisciplinaViewModel>>(null, $"Ocorreu um erro ao listar a disciplina: {e.Message}");
            }
        }

        public Resposta<DisciplinaViewModel> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<DisciplinaViewModel>(_mapper.Map<DisciplinaViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaViewModel>(null, $"Ocorreu um erro ao listar a disciplina com o código {id}: {e.Message}");
            }
        }

        public Resposta<List<DisciplinaViewModel>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<DisciplinaViewModel>>(_mapper.Map<List<DisciplinaViewModel>>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<List<DisciplinaViewModel>>(null, $"Ocorreu um erro ao listar as disciplinas: {e.Message}");
            }
        }

        public Resposta<bool> Remover(long id)
        {
            try
            {
                var resultado = _repositorio.Remover(lnq => lnq.Codigo == id).Result;
                return new Resposta<bool>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover a disciplina com o código {id}: {e.Message}");
            }
        }
    }
}
