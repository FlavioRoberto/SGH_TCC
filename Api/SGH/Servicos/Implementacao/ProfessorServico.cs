using System;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Repositorio.Contratos;
using Servico.Contratos;
using Servico.Exceptions;

namespace Servico.Implementacao
{
    public class ProfessorServico : BaseService<ProfessorViewModel, Professor>, IProfessorService
    {
        public ProfessorServico(IProfessorRepositorio repositorio, IMapper mapper) : base(repositorio, mapper, "Professor")
        { }

        private async Task<bool> ValidarSeMatriculaExistente(string matricula, int codigo)
        {
            var resultado = await _repositorio.ListarPor(lnq => lnq.Matricula == matricula && lnq.Codigo != codigo);
            return resultado.Count > 0;
        }

        public async override Task<ProfessorViewModel> ValidarInsercao(ProfessorViewModel viewModel)
        {
            var matriculaExistente = await ValidarSeMatriculaExistente(viewModel.Matricula, viewModel.Codigo);

            if (matriculaExistente)
                throw new ValidacaoException("Matrícula já existente.");
            else
                return await base.ValidarInsercao(viewModel);
        }

        public async override Task<ProfessorViewModel> ValidarEdicao(ProfessorViewModel viewModel)
        {
            var matriculaExistente = await ValidarSeMatriculaExistente(viewModel.Matricula, viewModel.Codigo);

            if (matriculaExistente)
                throw new ValidacaoException("Matrícula já existente.");
            else
                return await base.ValidarEdicao(viewModel);
        }

    }
}
