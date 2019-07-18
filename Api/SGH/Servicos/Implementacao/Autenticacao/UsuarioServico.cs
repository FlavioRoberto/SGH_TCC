using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel.AutenticacaoViewModel;
using Repositorio;
using Servico.Contratos;
using System;
using System.Threading.Tasks;
using Global.Extensions;
using Global;
using System.IdentityModel.Tokens.Jwt;
using Servico.Extensions;
using Repositorio.Contratos;

namespace Servico.Implementacao.Autenticacao
{
    public class UsuarioServico : BaseService<UsuarioViewModel, Usuario>, IUsuarioService
    {
        public UsuarioServico(IUsuarioRepositorio repositorio, IMapper mapper) : base(repositorio, mapper, "Usuário")
        { }

        public async Task<Resposta<string>> Autenticar(LoginViewModel viewModel)
        {
            var resultado = await GetRepositorio().RetornarUsuarioPorLoginESenha(viewModel.Login, viewModel.Senha);
               
            if (resultado == null)
                return new Resposta<string>(null, "Usuário e/ou senha inválidos!");

            string token = TokenGeradorHelper.Gerar(resultado);

            return new Resposta<string>(token);

        }

        public override Task<UsuarioViewModel> ValidarInsercao(UsuarioViewModel viewModel)
        {
            viewModel.Senha = viewModel.Senha.ToMD5();
            return Task.FromResult(viewModel);
        }
        
        public override async Task<UsuarioViewModel> ValidarEdicao(UsuarioViewModel viewModel)
        {
            var usuarioBanco = await _repositorio.Listar(lnq => lnq.Codigo == viewModel.Codigo);

            if (usuarioBanco != null)
                return viewModel;

            if (!usuarioBanco.Senha.IgualA(viewModel.Senha))
                viewModel.Senha = viewModel.Senha.ToMD5();

            return viewModel;
        }
        

        private IUsuarioRepositorio GetRepositorio()
        {
            return _repositorio as IUsuarioRepositorio;
        }
    }
}
