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

        protected override Resposta<UsuarioViewModel> ListarPeloCodigo(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;
                var resultadoViewModel = _mapper.Map<UsuarioViewModel>(resultado);
                return new Resposta<UsuarioViewModel>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<UsuarioViewModel>(null, $"Ocorreu um erro ao listar o usuário com código {id}: {e.Message}");
            }
        }

        protected override Resposta<bool> RemoverPeloCodigo(long id)
        {
            try
            {
                var resultado = _repositorio.Remover(lnq => lnq.Codigo == id).Result;
                return new Resposta<bool>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o usuário com código {id}: {e.Message}");
            }
        }

        public async Task<Resposta<string>> Logar(LoginViewModel viewModel)
        {
            var resultado = await GetRepositorio().RetornarUsuarioPorLoginESenha(viewModel.Login, viewModel.Senha);
               
            if (resultado == null)
                return new Resposta<string>(null, "Usuário e/ou senha inválidos!");

            string token = TokenGeradorHelper.Gerar(resultado);

            return new Resposta<string>(token);

        }

        private IUsuarioRepositorio GetRepositorio()
        {
            return _repositorio as IUsuarioRepositorio;
        }
    }
}
