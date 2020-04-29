using SGH.Api.Testes.Factory.Contratos;
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
        private readonly IBancoTeste<Sala> _salaTeste;
        private readonly IBancoTeste<HorarioAula> _horarioAulaTeste;
        private readonly IBancoTeste<Aula> _aulaTeste;

        public BancoTesteFactory(IBancoTeste<UsuarioPerfil> usuarioPerfil,
                                 IBancoTeste<Usuario> usuario,
                                 IBancoTeste<Professor> professor,
                                 IBancoTeste<DisciplinaTipo> tipoDisciplina,
                                 IBancoTeste<Disciplina> disciplinaBancoTeste,
                                 IBancoTeste<Curso> cursoTeste,
                                 IBancoTeste<Curriculo> curriculoTeste,
                                 IBancoTeste<Cargo> cargoTeste,
                                 IBancoTeste<Turno> turnoTeste,
                                 IBancoTeste<Bloco> blocoTeste,
                                 IBancoTeste<Sala> salaTeste,
                                 IBancoTeste<HorarioAula> horarioAulaTeste,
                                 IBancoTeste<Aula> aulaTeste)
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
            _salaTeste = salaTeste;
            _horarioAulaTeste = horarioAulaTeste;
            _aulaTeste = aulaTeste;
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
            _salaTeste.InicializarBanco();
            _horarioAulaTeste.InicializarBanco();
            _aulaTeste.InicializarBanco();
        }
    }
}

