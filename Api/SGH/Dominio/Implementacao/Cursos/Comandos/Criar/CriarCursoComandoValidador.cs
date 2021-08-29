using FluentValidation;
using SGH.Dominio.Core.Repositories;
using SGH.Dominio.Services.Implementacao.Cursos.Comandos.Base;
using System;
using System.Threading;
using System.Threading.Tasks;
using SGH.Dominio.Shared.Extensions;

namespace SGH.Dominio.Services.Implementacao.Cursos.Comandos.Criar
{
    public class CriarCursoComandoValidador : CursoComandoValidador<CriarCursoComando>
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public CriarCursoComandoValidador(ICursoRepositorio cursoRepositorio)
        {
            _cursoRepositorio = cursoRepositorio;

            RuleFor(lnq => lnq.Descricao)
                .MustAsync(ValidarSeDescricaoExiste)
                .WithMessage("Não foi possível cadastrar o curso, pois já foi cadastrado um curso com a descrição informada.");
        }

        private async Task<bool> ValidarSeDescricaoExiste(string descricao, CancellationToken arg2)
        {
            var descricaoExiste = await _cursoRepositorio.Contem(lnq => lnq.Descricao.IgualA(descricao));
            
            if (descricaoExiste)
                return false;

            return true;
        }
    }
}
