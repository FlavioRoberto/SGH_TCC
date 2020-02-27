using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Services.Extensions;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Professores.Comandos.Criar
{
    public class CriarProfessorComandoHandler : IRequestHandler<CriarProfessorComando, Resposta<Professor>>
    {
        private readonly IProfessorRepositorio _repositorio;
        private readonly ICriarProfessorComandoValidador _validador;

        public CriarProfessorComandoHandler(IProfessorRepositorio repositorio, ICriarProfessorComandoValidador validador)
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
                Telefone = request.Telefone
            };

            var professorCadastrado = await _repositorio.Criar(professor);

            return new Resposta<Professor>(professorCadastrado);

        }
    }
}
