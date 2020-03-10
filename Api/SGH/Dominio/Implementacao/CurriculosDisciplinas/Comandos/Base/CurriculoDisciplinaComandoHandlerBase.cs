using AutoMapper;
using SGH.Dominio.Core.Model;
using SGH.Dominio.Services.ViewModel;
using SGH.Dominio.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base
{
    public abstract class CurriculoDisciplinaComandoHandlerBase
    {
        protected IMapper _mapper;

        public CurriculoDisciplinaComandoHandlerBase(IMapper mapper)
        {
            _mapper = mapper;
        }

        protected IEnumerable<CurriculoDisciplinaPreRequisito> AdicionarPreRequisitors(IEnumerable<DisciplinCurriculoPreRequisitoaViewModel> preRequisitos)
        {
            var disciplinas = _mapper.Map<List<CurriculoDisciplinaPreRequisito>>(preRequisitos);
            return disciplinas;
        }
    }
}
