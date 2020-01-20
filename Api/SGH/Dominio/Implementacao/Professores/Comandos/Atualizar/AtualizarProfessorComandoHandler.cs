using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Implementacao.Professores.Comandos.Atualizar
{
    public class AtualizarProfessorComandoHandler : IRequestHandler<AtualizarProfessorComando, Resposta<Professor>>
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly IAtualizarProfessorComandoValidador _validador;

        public AtualizarProfessorComandoHandler(IProfessorRepositorio repositorio, IAtualizarProfessorComandoValidador validador)
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
