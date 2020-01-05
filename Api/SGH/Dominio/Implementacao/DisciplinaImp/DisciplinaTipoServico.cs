using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace Aplicacao.Implementacao.DisciplinaImp
{
    public class DisciplinaTipoServico : IDisciplinaTipoService
    {
        private readonly IRepositorio<DisciplinaTipo> _repositorio;
        private readonly IMapper _mapper;

        public DisciplinaTipoServico(IRepositorio<DisciplinaTipo> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }
    
        public async Task<Resposta<List<DisciplinaTipoViewModel>>> ListarTodos()
        {
            var resultado = await _repositorio.ListarTodos();
            return new Resposta<List<DisciplinaTipoViewModel>>(_mapper.Map<List<DisciplinaTipoViewModel>>(resultado));
        }

    }
}
