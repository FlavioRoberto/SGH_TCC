using Microsoft.EntityFrameworkCore;
using SGH.Api.Testes.Factory.Contratos;
using SHG.Data.Contexto;

namespace SGH.Api.Testes.Factory
{
    public class BancoTesteFactory : IBancoTesteFactory
    {
        private readonly IUsuarioPerfilBancoTeste _usuarioPerfil;
        private readonly IUsuarioBancoTeste _usuario;
        private readonly IProfessorBancoTeste _professor;
        private readonly ITipoDisciplinaBancoTeste _tipoDisciplina;
        private readonly IDisciplinaBancoTeste _disciplinaTeste;
        private readonly ICursoBancoTeste _cursoTeste;
        private readonly ICurriculoBancoTeste _curriculoTeste;
        private readonly ICargoBancoTeste _cargoTeste;

        public BancoTesteFactory(IUsuarioPerfilBancoTeste usuarioPerfil, 
                                 IUsuarioBancoTeste usuario,
                                 IProfessorBancoTeste professor,
                                 ITipoDisciplinaBancoTeste tipoDisciplina,
                                 IDisciplinaBancoTeste disciplinaBancoTeste,
                                 ICursoBancoTeste cursoTeste,
                                 ICurriculoBancoTeste curriculoTeste,
                                 ICargoBancoTeste cargoTeste)
        {
            _usuarioPerfil = usuarioPerfil;
            _usuario = usuario;
            _professor = professor;
            _tipoDisciplina = tipoDisciplina;
            _disciplinaTeste = disciplinaBancoTeste;
            _cursoTeste = cursoTeste;
            _curriculoTeste = curriculoTeste;
            _cargoTeste = cargoTeste;
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
            _cargoTeste.InicializarBanco();
        }
    }
}

