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

    }
}
