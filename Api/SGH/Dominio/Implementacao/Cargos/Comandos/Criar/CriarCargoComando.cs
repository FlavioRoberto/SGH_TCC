using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Enums;
using SGH.Dominio.Services.Implementacao.Cargos.Comandos.Base;
using SGH.Dominio.ViewModel;
using System.Collections.Generic;

namespace SGH.Dominio.Services.Implementacao.Cargos.Comandos.Criar
{
    public class CriarCargoComando : IRequest<Resposta<CargoViewModel>>, ICargoComando
    {
        public int? Codigo { get; set; }
        public int Numero { get; set; }
        public string Edital { get; set; }
        public int Ano { get; set; }
        public ESemestre Semestre { get; set; }
        public int? CodigoProfessor { get; set; }
    }
}
