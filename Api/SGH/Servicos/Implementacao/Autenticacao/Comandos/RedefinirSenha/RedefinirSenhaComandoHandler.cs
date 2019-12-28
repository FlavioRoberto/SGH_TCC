using Global;
using Repositorio.Contratos;
using Servico.Helpers;
using Servico.Implementacao.Autenticacao.Contratos;
using Global.Extensions;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.ViewModel.AutenticacaoViewModel;
using Api.Servicos.Email;
using Servico.Extensions;

namespace Servico.Implementacao.Autenticacao.Comandos.RedefinirSenha
{
    public class RedefinirSenhaComandoHandler : IRedefinirSenhaService
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

        public async Task<Resposta<string>> Redefinir(RedefinirSenhaComando comando)
        {            
            var email = comando.Email;
            var usuario = await _repositorio.Listar(lnq => lnq.Email.Equals(email));

            var erros = _validador.Validar(comando);

            if (!string.IsNullOrEmpty(erros))
                return new Resposta<string>(null, erros);

            string senha = SenhaHelper.Gerar();
            usuario.Senha = senha.ToMD5();
            await _repositorio.Atualizar(usuario);

            var usuarioViewModel = _mapper.Map<UsuarioViewModel>(usuario);

            string mensagem = mensagem = $@"Sua senha no SGH foi redefinida com sucesso! <br>
                                Usuário: {usuario.Login}<br>
                                Senha: {senha}<br>
                                click <a>aqui</a> para acessar o sistema.";

            await _emailService.SendEmailAsync(usuarioViewModel.Email, "Redefinição de senha no SGH", mensagem);

            return new Resposta<string>("Senha redefinida com sucesso! Foi enviado um e-mail com seus dados de acesso.");
        }

    }
}
