using FluentValidation;
using SGH.Data.Repositorio.Contratos;

namespace SGH.Dominio.Services.Implementacao.Aulas.Comandos.Base
{
    public abstract class AulaComandoBase 
    {
        public int CodigoHorario { get; set; }
        public int CodigoDisciplina { get; set; }
        public long? CodigoSala { get; set; }
        public bool Laboratorio { get; set; }
    }
}