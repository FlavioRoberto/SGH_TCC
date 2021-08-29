using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;

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

            var entidade = await _repositorio.Consultar(lnq => lnq.Codigo == request.Codigo);

            entidade.Codigo = request.Codigo;
            entidade.Ativo = request.Ativo ?? false;
            entidade.Email = request.Email;
            entidade.Matricula = request.Matricula;
            entidade.Nome = request.Nome;
            entidade.Telefone = request.Telefone;
            entidade.Contratacao = request.Contratacao;

            var professor = await _repositorio.Atualizar(entidade);
            return new Resposta<Professor>(professor);
        }
    }
}
