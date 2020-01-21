﻿using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Professores.Consultas.ListarAtivos
{
    public class ListarProfessoresAtivosConsultaHandler : IRequestHandler<ListarProfessoresAtivosConsulta, ICollection<Professor>>
    {
        private readonly IProfessorRepositorio _repositorio;

        public ListarProfessoresAtivosConsultaHandler(IProfessorRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<ICollection<Professor>> Handle(ListarProfessoresAtivosConsulta request, CancellationToken cancellationToken)
        {
            return await _repositorio.Listar(lnq => lnq.Ativo);
        }
    }
}
