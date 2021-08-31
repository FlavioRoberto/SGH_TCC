using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SHG.Data.Mapeamento
{
    public abstract class EntidadeMapeamento<T> where T : class
    {
        protected virtual string PrefixoColuna { get { return ""; } }

        protected EntityTypeBuilder<T> builder { get; private set; }

        public EntidadeMapeamento(EntityTypeBuilder<T> builder)
        {
            this.builder = builder;
        }

        public abstract EntidadeMapeamento<T> Map();


        protected string RetornarNomeColuna(string nome)
        {
            return $"{PrefixoColuna}_{nome}";
        }
    }
}
