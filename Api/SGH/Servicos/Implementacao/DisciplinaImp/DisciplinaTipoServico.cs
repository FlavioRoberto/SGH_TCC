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
    public class DisciplinaTipoServico : IDisciplinaTipoServico
    {
        private readonly IRepositorio<DisciplinaTipo> _repositorio;
        private readonly IMapper _mapper;

        public DisciplinaTipoServico(IRepositorio<DisciplinaTipo> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }
        
        public Resposta<DisciplinaTipoViewModel> Atualizar(DisciplinaTipoViewModel entidade)
        {
            try
            {
                var resultado = _mapper.Map<DisciplinaTipoViewModel>(_repositorio.Atualizar(_mapper.Map<DisciplinaTipo>(entidade)).Result);
                return new Resposta<DisciplinaTipoViewModel>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaTipoViewModel>(entidade, $"Ocorreu um erro ao atualizar o tipo: {e.Message}");
            }
        }

        public Resposta<DisciplinaTipoViewModel> Criar(DisciplinaTipoViewModel entidade)
        {
            try
            {
                var resultado = _repositorio.Criar(_mapper.Map<DisciplinaTipo>(entidade)).Result;
                return new Resposta<DisciplinaTipoViewModel>(_mapper.Map<DisciplinaTipoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaTipoViewModel>(entidade, $"Ocorreu um erro ao criar o tipo: {e.Message}");
            }
        }

        public Resposta<Paginacao<DisciplinaTipoViewModel>> ListarComPaginacao(Paginacao<DisciplinaTipoViewModel> entidade)
        {
            try
            {
                var resultado = _repositorio.ListarPorPaginacao(_mapper.Map<Paginacao<DisciplinaTipo>>( entidade));
                if (resultado.TemErro())
                    return new Resposta<Paginacao<DisciplinaTipoViewModel>>(null, resultado.GetErros());

                return new Resposta<Paginacao<DisciplinaTipoViewModel>>(_mapper.Map<Paginacao<DisciplinaTipoViewModel>>(resultado.GetResultado()));
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<DisciplinaTipoViewModel>>(null, $"Ocorreu um erro ao listar o tipo: {e.Message}");
            }
        }

        public Resposta<DisciplinaTipoViewModel> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<DisciplinaTipoViewModel>(_mapper.Map<DisciplinaTipoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<DisciplinaTipoViewModel>(null, $"Ocorreu um erro ao listar o tipo com o código {id}: {e.Message}");
            }
        }

        public Resposta<List<DisciplinaTipoViewModel>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                return new Resposta<List<DisciplinaTipoViewModel>>(_mapper.Map<List<DisciplinaTipoViewModel>>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<List<DisciplinaTipoViewModel>>(null, $"Ocorreu um erro ao listar os tipos: {e.Message}");
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
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o tipo com código {id}: {e.Message}");
            }
        }
    }
}
