using MediatR;
using SGH.Dominio.Core;
using SGH.Dominio.Services.ViewModel;

namespace SGH.Dominio.Services.Implementacao.Salas.Comandos.Criar
{
    public class CriarSalaComando : IRequest<Resposta<SalaViewModel>>
    {
        public int Numero { get; set; }

        public string Descricao { get; set; }

        public bool Laboratorio { get; set; }

        public int CodigoBloco { get; set; }
    }
}
