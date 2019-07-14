using System;
using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Repositorio;

namespace Servico.Implementacao
{
    public class CursoServico : BaseService<CursoViewModel, Curso>
    {
        public CursoServico(IRepositorio<Curso> repositorio, IMapper mapper) : base(repositorio, mapper, "Curso")
        { }

        protected override Resposta<CursoViewModel> ListarPeloCodigo(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                return new Resposta<CursoViewModel>(_mapper.Map<CursoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<CursoViewModel>(null, $"Ocorreu um erro ao listar o curso pelo id: {id}: " + e.Message);
            }
        }

        protected override Resposta<bool> RemoverPeloCodigo(long id)
        {
            try
            {
                var resultado = _repositorio.Remover(lnq => lnq.Codigo == id).Result;
                return new Resposta<bool>(true);
            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o curso com id {id}: " + e.Message);
            }
        }
    }
}
