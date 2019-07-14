using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel.AutenticacaoViewModel;
using Global;
using Repositorio;
using System;

namespace Servico.Implementacao.Autenticacao
{
    public class UsuarioServico : BaseService<UsuarioViewModel, Usuario>
    {
        public UsuarioServico(IRepositorio<Usuario> repositorio, IMapper mapper) : base(repositorio, mapper, "Usuário")
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
    }
}
