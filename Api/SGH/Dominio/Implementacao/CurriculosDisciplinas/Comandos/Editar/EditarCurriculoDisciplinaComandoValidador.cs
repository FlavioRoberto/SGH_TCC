using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Editar
{
    public class EditarCurriculoDisciplinaComandoValidador : CurriculoDisciplinaComandoBaseValidador<EditarCurriculoDisciplinaComando>, IEditarCurriculoDisciplinaComandoValidador
    {
        public EditarCurriculoDisciplinaComandoValidador(ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio, IDisciplinaRepositorio disciplinaRepositorio) : base(curriculoDisciplinaRepositorio, disciplinaRepositorio)
        {
        }
    }
}
