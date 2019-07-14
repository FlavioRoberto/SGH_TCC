using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel.CurriculoViewModel;
using Global;
using Repositorio;
using System;

namespace Servico.Implementacao.CurriculoImp
{
    public class CurriculoServico : BaseService<CurriculoViewModel, Curriculo>
    {
        public CurriculoServico(IRepositorio<Curriculo> repositorio, IMapper mapper) : base(repositorio, mapper, "Currículo")
        { }
      
        protected override Resposta<CurriculoViewModel> ListarPeloCodigo(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                var resultadoViewModel = _mapper.Map<CurriculoViewModel>(resultado);
                return new Resposta<CurriculoViewModel>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<CurriculoViewModel>(null, $"Ocorreu um erro ao listar o currículo com código {id}: {e.Message}");
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
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o currículo com código {id}: {e.Message}");
            }
        }
    }
}
