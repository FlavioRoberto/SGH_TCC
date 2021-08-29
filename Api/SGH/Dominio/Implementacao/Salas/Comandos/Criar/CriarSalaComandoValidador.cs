using SGH.Dominio.Core.Commands;using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Implementacao.Salas.Comandos.Base;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar
{
    public class CriarSalaComandoValidador : SalaComandoValidador<CriarSalaComando>, IValidador<CriarSalaComando>
    {
        public CriarSalaComandoValidador(IBlocoRepositorio blocoRepositorio) : base(blocoRepositorio)
        {      
        }      
    }
}
