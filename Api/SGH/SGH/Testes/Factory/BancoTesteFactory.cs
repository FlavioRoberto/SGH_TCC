﻿using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;

namespace SGH.Api.Testes.Factory
{
    public class BancoTesteFactory : IBancoTesteFactory
    {
        private readonly IBancoTeste<UsuarioPerfil> _usuarioPerfil;
        private readonly IBancoTeste<Usuario> _usuario;
        private readonly IBancoTeste<Professor> _professor;
        private readonly IBancoTeste<DisciplinaTipo> _tipoDisciplina;
        private readonly IBancoTeste<Disciplina> _disciplinaTeste;
        private readonly IBancoTeste<Curso> _cursoTeste;
        private readonly IBancoTeste<Curriculo> _curriculoTeste;
        private readonly IBancoTeste<Cargo> _cargoTeste;
        private readonly IBancoTeste<Turno> _turnoTeste;
        private readonly IBancoTeste<Bloco> _blocoTeste;

        public BancoTesteFactory(IBancoTeste<UsuarioPerfil> usuarioPerfil,
                                 IBancoTeste<Usuario> usuario,
                                 IBancoTeste<Professor> professor,
                                 IBancoTeste<DisciplinaTipo> tipoDisciplina,
                                 IBancoTeste<Disciplina> disciplinaBancoTeste,
                                 IBancoTeste<Curso> cursoTeste,
                                 IBancoTeste<Curriculo> curriculoTeste,
                                 IBancoTeste<Cargo> cargoTeste,
                                 IBancoTeste<Turno> turnoTeste,
                                 IBancoTeste<Bloco> blocoTeste)
        {
            _usuarioPerfil = usuarioPerfil;
            _usuario = usuario;
            _professor = professor;
            _tipoDisciplina = tipoDisciplina;
            _disciplinaTeste = disciplinaBancoTeste;
            _cursoTeste = cursoTeste;
            _curriculoTeste = curriculoTeste;
            _cargoTeste = cargoTeste;
            _turnoTeste = turnoTeste;
            _blocoTeste = blocoTeste;
        }
               
        public void InicializarBanco()
        {
            _usuarioPerfil.InicializarBanco();
            _usuario.InicializarBanco();
            _professor.InicializarBanco();
            _tipoDisciplina.InicializarBanco();
            _disciplinaTeste.InicializarBanco();
            _cursoTeste.InicializarBanco();
            _curriculoTeste.InicializarBanco();
            _turnoTeste.InicializarBanco();
            _cargoTeste.InicializarBanco();
            _blocoTeste.InicializarBanco();
        }
    }
}

