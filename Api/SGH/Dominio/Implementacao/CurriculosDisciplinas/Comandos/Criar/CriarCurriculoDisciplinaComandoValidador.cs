using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Base;

namespace SGH.Dominio.Services.Implementacao.CurriculosDisciplinas.Comandos.Criar
{
    public class CriarCurriculoDisciplinaComandoValidador : CurriculoDisciplinaComandoBaseValidador<CriarCurriculoDisciplinaComando>, ICriarCurriculoDisciplinaComandoValidador
    {
        public CriarCurriculoDisciplinaComandoValidador(ICurriculoDisciplinaRepositorio curriculoDisciplinaRepositorio, IDisciplinaRepositorio disciplinaRepositorio) : base(curriculoDisciplinaRepositorio, disciplinaRepositorio)
        {
        }
    }
}
