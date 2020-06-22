using System.Threading.Tasks;
using MediatR;
using System.Threading;
using SGH.Dominio.Core;
using SGH.Dominio.Core.Contratos;
using SGH.Dominio.Services.Contratos;
using SGH.Dominio.Services.Extensions;
using SGH.Data.Extensios;

namespace SGH.Dominio.Services.Implementacao.Autenticacao.Comandos.Login
{
    public class LoginComandoHandler : IRequestHandler<LoginComando, Resposta<string>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IValidador<LoginComando> _validador;

        public LoginComandoHandler(IUsuarioRepositorio repositorio, IValidador<LoginComando> validator)
        {
            _repositorio = repositorio;
            _validador = validator;
        }

        public async Task<Resposta<string>> Handle(LoginComando request, CancellationToken cancellationToken)
        {
            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<string>("", erros);

            var usuario = await _repositorio.RetornarUsuarioPorLoginESenha(request.Login, request.Senha);

            string token = TokenExtension.Gerar(usuario);

            return new Resposta<string>(token, "");
        }
    }
}
