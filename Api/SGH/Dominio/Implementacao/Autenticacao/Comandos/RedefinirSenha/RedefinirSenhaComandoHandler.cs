using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using System.Threading;
using SGH.Dominio.Core;
using SGH.Data.Repositorio.Contratos;
using SGH.Dominio.Core.Email;
using SGH.Dominio.Contratos;
using SGH.Dominio.Core.Extensions;
using SGH.Dominio.Core.Helpers;

namespace SGH.Dominio.Implementacao.Autenticacao.Comandos.RedefinirSenha
{
    public class RedefinirSenhaComandoHandler : IRequestHandler<RedefinirSenhaComando, Resposta<string>>
    {
        private readonly IUsuarioRepositorio _repositorio;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IRedefinirSenhaComandoValidador _validador;

        public RedefinirSenhaComandoHandler(IUsuarioRepositorio repositorio, IMapper mapper, IEmailService emailService, IRedefinirSenhaComandoValidador validador)
        {
            _repositorio = repositorio;
            _mapper = mapper;
            _emailService = emailService;
            _validador = validador;
        }

        public async Task<Resposta<string>> Handle(RedefinirSenhaComando request, CancellationToken cancellationToken)
        {
            var email = request.Email;
            var usuario = await _repositorio.Consultar(lnq => lnq.Email.Equals(email));

            var erros = _validador.Validar(request);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<string>(null, erros);

            string senha = SenhaHelper.Gerar();
            usuario.Senha = senha.ToMD5();
            await _repositorio.Atualizar(usuario);

            string mensagem = mensagem = $@"Sua senha no SGH foi redefinida com sucesso! <br>
                                Usuário: {usuario.Login}<br>
                                Senha: {senha}<br>
                                click <a>aqui</a> para acessar o sistema.";

            await _emailService.SendEmailAsync(usuario.Email, "Redefinição de senha no SGH", mensagem);

            return new Resposta<string>("Senha redefinida com sucesso! Foi enviado um e-mail com seus dados de acesso.");
        }
    }
}
