using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Core.Model;

namespace SGH.Dominio.Implementacao.Curriculos.Comandos.Criar
{
    public class CriarCurriculoComandoHandler : IRequestHandler<CriarCurriculoComando, Resposta<Curriculo>>
    {
        private ICurriculoRepositorio _repositorio;
        private ICriarCurriculoComandoValidador _validador;

        public CriarCurriculoComandoHandler(ICurriculoRepositorio repositorio, ICriarCurriculoComandoValidador validador)
        {
            _repositorio = repositorio;
            _validador = validador;
        }

        public async Task<Resposta<Curriculo>> Handle(CriarCurriculoComando request, CancellationToken cancellationToken)
        {
            string erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<Curriculo>(erros);

            var entidade = request.ConverterParaCurriculo();

            var resultado = await _repositorio.Criar(entidade);

            return new Resposta<Curriculo>(resultado);
        }
    }
}
