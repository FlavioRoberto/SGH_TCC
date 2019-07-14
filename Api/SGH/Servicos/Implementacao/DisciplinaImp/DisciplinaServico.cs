using System;
using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using Repositorio;

namespace Servico.Implementacao.DisciplinaImp
{
    public class DisciplinaServico : BaseService<DisciplinaViewModel, Disciplina>
    {
        public DisciplinaServico(IRepositorio<Disciplina> repositorio, IMapper mapper) : base(repositorio, mapper, "Disciplina")
        { }

        protected override Resposta<DisciplinaViewModel> ListarPeloCodigo(long id)
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

        protected override Resposta<bool> RemoverPeloCodigo(long id)
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
