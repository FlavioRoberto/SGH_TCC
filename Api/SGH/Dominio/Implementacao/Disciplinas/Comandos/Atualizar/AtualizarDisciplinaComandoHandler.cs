using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;
using System.Threading;
using System.Threading.Tasks;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Comandos.Atualizar
{
    public class AtualizarDisciplinaComandoHandler : IRequestHandler<AtualizarDisciplinaComando, Resposta<Disciplina>>
    {
        private readonly IDisciplinaRepositorio _repositorio;

        public AtualizarDisciplinaComandoHandler(IDisciplinaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<Disciplina>> Handle(AtualizarDisciplinaComando request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.Atualizar(request.ConverterParaDisciplina());
            return new Resposta<Disciplina>(resultado);
        }
    }
}
