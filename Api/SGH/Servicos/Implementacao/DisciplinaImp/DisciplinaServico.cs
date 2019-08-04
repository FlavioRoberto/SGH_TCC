using AutoMapper;
using Dominio.Model.DisciplinaModel;
using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using Repositorio;
using Servico.Contratos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servico.Implementacao.DisciplinaImp
{
    public class DisciplinaServico : BaseService<DisciplinaViewModel, Disciplina>, IDisciplinaService
    {
        public DisciplinaServico(IRepositorio<Disciplina> repositorio, IMapper mapper) : base(repositorio, mapper, "Disciplina")
        { }
        
        public async Task<Resposta<List<DisciplinaViewModel>>> listarPorDescricao(string descricao)
        {
            try
            {
                var dados = await _repositorio.ListarPor(lnq => lnq.Descricao.ToLower().Contains(descricao.ToLower()));

                if (dados == null)
                    return new Resposta<List<DisciplinaViewModel>>(new List<DisciplinaViewModel>());

                var dadosViewModel = _mapper.Map<List<DisciplinaViewModel>>(dados);
                return new Resposta<List<DisciplinaViewModel>>(dadosViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<List<DisciplinaViewModel>>(null, "Ocorreu um erro ao listar as disciplinas: " + e.Message);
            }

        }

    }
}
