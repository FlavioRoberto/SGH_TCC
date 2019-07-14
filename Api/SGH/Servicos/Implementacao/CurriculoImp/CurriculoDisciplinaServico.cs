using System;
using AutoMapper;
using Dominio.Model.CurriculoModel;
using Dominio.ViewModel.CurriculoViewModel;
using Global;
using Repositorio;

namespace Servico.Implementacao.CurriculoImp
{
    public class CurriculoDisciplinaServico : BaseService<CurriculoDisciplinaViewModel, CurriculoDisciplina>
    {
        public CurriculoDisciplinaServico(IRepositorio<CurriculoDisciplina> repositorio, IMapper mapper) : base(repositorio, mapper, "Disciplina do currículo")
        { }


        protected override Resposta<CurriculoDisciplinaViewModel> ListarPeloCodigo(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                var resultadoViewModel = _mapper.Map<CurriculoDisciplinaViewModel>(resultado);
                return new Resposta<CurriculoDisciplinaViewModel>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<CurriculoDisciplinaViewModel>(null, $"Ocorreu um erro ao listar a disciplina com código {id}: {e.Message}");
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
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover a disciplina com código {id}: {e.Message}");
            }
        }
    }
}
