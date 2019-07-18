﻿using AutoMapper;
using Dominio.Model.Autenticacao;
using Dominio.ViewModel.AutenticacaoViewModel;
using Repositorio;

namespace Servico.Implementacao.Autenticacao
{
    public class UsuarioPerfilServico : BaseService<UsuarioPerfilViewModel, UsuarioPerfil>
    {
        public UsuarioPerfilServico(IRepositorio<UsuarioPerfil> repositorio, IMapper mapper) : base(repositorio, mapper, "Perfil de usuário")
        { }
    }
}
