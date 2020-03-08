using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar
{
    public class CriarCurriculoDisciplinaComando : IRequest<Resposta<CurriculoDisciplinaViewModel>>
    {
        public int Periodo { get; set; }

        public int CodigoDisciplina { get; set; }

        public int CodigoCurriculo { get; set; }

        public int AulasSemanaisTeorica { get; set; }

        public int AulasSemanaisPratica { get; set; }

        public IEnumerable<DisciplinaViewModel> PreRequisitos { get; set; }

    }
}
