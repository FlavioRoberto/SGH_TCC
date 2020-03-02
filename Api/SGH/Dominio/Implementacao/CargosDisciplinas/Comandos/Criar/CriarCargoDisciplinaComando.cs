using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.ViewModel;

namespace SGH.Dominio.Services.Implementacao.CargosDisciplinas.Comandos.Criar
{
    public class CriarCargoDisciplinaComando : IRequest<Resposta<CargoDisciplinaViewModel>>
    {
        public int CodigoCargo { get; set; }
        public int CodigoCurriculoDisciplina { get; set; }
        public int CodigoTurno { get; set; }
    }
}
