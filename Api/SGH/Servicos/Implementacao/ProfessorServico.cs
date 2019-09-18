using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Repositorio.Contratos;
using Servico.Contratos;

namespace Servico.Implementacao
{
    public class ProfessorServico : BaseService<ProfessorViewModel, Professor>, IProfessorService
    {
        public ProfessorServico(IProfessorRepositorio repositorio, IMapper mapper) : base(repositorio, mapper, "Professor")
        {
        }
    }
}
