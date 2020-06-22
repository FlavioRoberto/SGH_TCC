using AutoMapper;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Core.Model;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Base
{
    public abstract class CargoDisciplinaComandoHandlerBase
    {
        private readonly ICurriculoDisciplinaRepositorio _disciplinaRepositorio;
        protected readonly IMapper _mapper;

        public CargoDisciplinaComandoHandlerBase(IMapper mapper, ICurriculoDisciplinaRepositorio disciplinaRepositorio)
        {
            _mapper = mapper;
            _disciplinaRepositorio = disciplinaRepositorio;
        }

        protected async Task<CargoDisciplina> MapearComandoParaDisciplina(CargoDisciplinaComandoBase request)
        {
            var cargoDisciplina = _mapper.Map<CargoDisciplina>(request);

            if (!string.IsNullOrEmpty(cargoDisciplina.Descricao))
                return cargoDisciplina;

            var disciplina = await _disciplinaRepositorio.ConsultarDisciplinaVinculadaCurriculo(lnq => lnq.Codigo == request.CodigoCurriculoDisciplina);

            if (disciplina != null)
                cargoDisciplina.Descricao = disciplina.Descricao;

            return cargoDisciplina;
        }
    }
}
