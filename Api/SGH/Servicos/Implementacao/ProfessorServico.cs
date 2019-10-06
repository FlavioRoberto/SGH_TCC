using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
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

        public async Task<Resposta<List<ProfessorViewModel>>> ListarAtivos()
        {
            var professores = _mapper.Map<List<ProfessorViewModel>>(await _repositorio.ListarPor(prof => prof.Ativo == true));

            if (professores.Count <= 0)
                return new Resposta<List<ProfessorViewModel>>(null, "Não foram encontrados professores ativos!");

            return new Resposta<List<ProfessorViewModel>>(professores);
        }
    }
}
