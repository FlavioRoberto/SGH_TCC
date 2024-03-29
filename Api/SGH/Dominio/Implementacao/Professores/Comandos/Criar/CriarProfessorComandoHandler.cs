﻿using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Services;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Criar
{
    public class CriarProfessorComandoHandler : IRequestHandler<CriarProfessorComando, Resposta<Professor>>
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly IValidador<CriarProfessorComando> _validador;

        public CriarProfessorComandoHandler(IProfessorRepositorio repositorio, IValidador<CriarProfessorComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<Professor>> Handle(CriarProfessorComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<Professor>(erros);

            var professor = new Professor
            {
                Ativo = request.Ativo ?? false,
                Email = request.Email,
                Matricula = request.Matricula,
                Nome = request.Nome,
                Telefone = request.Telefone,
                Contratacao = request.Contratacao
            };

            var professorCadastrado = await _repositorio.Criar(professor);

            return new Resposta<Professor>(professorCadastrado);

        }
    }
}
