using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Services.Implementacao.Disciplinas.Comandos.Criar
{
    public class CriarDisciplinaComandoHandler : IRequestHandler<CriarDisciplinaComando, Resposta<Disciplina>>
    {
        private readonly IDisciplinaRepositorio _repositorio;

        public CriarDisciplinaComandoHandler(IDisciplinaRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<Resposta<Disciplina>> Handle(CriarDisciplinaComando request, CancellationToken cancellationToken)
        {
            var resultado = await _repositorio.Criar(request.ConverterParaDisciplina());
            return new Resposta<Disciplina>(resultado);
        }
    }
}
