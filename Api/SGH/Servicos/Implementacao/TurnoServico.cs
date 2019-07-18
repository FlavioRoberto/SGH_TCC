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
        
    }
}
