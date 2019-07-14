using System;
using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using Repositorio;

namespace Servico.Implementacao.DisciplinaImp
{
    public class DisciplinaTipoServico : BaseService<DisciplinaTipoViewModel, DisciplinaTipo>
    {

        public DisciplinaTipoServico(IRepositorio<DisciplinaTipo> repositorio, IMapper mapper) : base(repositorio, mapper, "Tipo de disciplina")
        {
        }

        protected override Resposta<DisciplinaTipoViewModel> ListarPeloCodigo(long id)
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

        protected override Resposta<bool> RemoverPeloCodigo(long id)
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
