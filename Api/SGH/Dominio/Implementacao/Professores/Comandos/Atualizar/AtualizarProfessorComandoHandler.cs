using MediatR;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Services.Implementacao.Professores.Comandos.Remover;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Atualizar
{
    public class AtualizarProfessorComandoHandler : IRequestHandler<AtualizarProfessorComando, Resposta<Professor>>
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly IValidador<AtualizarProfessorComando> _validador;

        public AtualizarProfessorComandoHandler(IProfessorRepositorio repositorio, IValidador<AtualizarProfessorComando> validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<Professor>> Handle(AtualizarProfessorComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<Professor>(erros);

            var entidade = new Professor
            {
                Codigo = request.ProfessorId,
                Ativo = request.Ativo ?? false,
                Email = request.Email,
                Matricula = request.Matricula,
                Nome = request.Nome,
                Telefone = request.Telefone
            };

            var professor = await _repositorio.Atualizar(entidade);
            return new Resposta<Professor>(professor);
        }
    }
}
