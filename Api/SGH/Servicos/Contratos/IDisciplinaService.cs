﻿using Dominio.ViewModel.DisciplinaViewModel;
using Global;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Servico.Contratos
{
    public interface IDisciplinaService : IServicoBase<DisciplinaViewModel>
    {
        Task<Resposta<List<DisciplinaViewModel>>> listarPorDescricao(string filtro);
    }
}