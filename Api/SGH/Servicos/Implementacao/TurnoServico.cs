using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Repositorio;
using System;

namespace Servico.Implementacao
{
    public class TurnoServico : BaseService<TurnoViewModel, Turno>
    {
        public TurnoServico(IRepositorio<Turno> repositorio, IMapper mapper) : base(repositorio, mapper, "Turno")
        { }

        protected override Resposta<TurnoViewModel> ListarPeloCodigo(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;

                var resultadoViewModel = _mapper.Map<TurnoViewModel>(resultado);

                return new Resposta<TurnoViewModel>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<TurnoViewModel>(null, $"Ocorreu um erro ao listar o turno com código {id}: {e.Message}");
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
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o turno com código {id} : {e.Message} ");
            }
        }
    }
}
