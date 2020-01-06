using SGH.Dominio.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGH.Dominio.Implementacao.Disciplinas.Comandos
{
    public class DisciplinaComandoBase
    {
        public int? Codigo { get; set; }
        public string Descricao { get; set; }
        public int CodigoTipo { get; set; }

        public virtual Disciplina ConverterParaDisciplina()
        {
            return new Disciplina
            {
                Codigo = Codigo ?? 0,
                CodigoTipo = CodigoTipo,
                Descricao = Descricao
            };
        }
    }
}
