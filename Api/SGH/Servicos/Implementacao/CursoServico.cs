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
        
    }
}
