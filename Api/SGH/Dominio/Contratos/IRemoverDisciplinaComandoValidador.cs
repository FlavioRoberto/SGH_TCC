using System;
using System.Collections.Generic;
using System.Text;
using SGH.Dominio.Implementacao.Disciplinas.Comandos.Remover;

namespace SGH.Dominio.Contratos
{
    public interface IRemoverDisciplinaComandoValidador : IValidador
    {
        string Validar(RemoverDisciplinaComando request);
    }
}
