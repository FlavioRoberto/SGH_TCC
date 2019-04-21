using AutoMapper;
using Dominio.Model;
using Dominio.ViewModel;
using Global;
using Repositorio;
using Servico.Contratos;
using System;
using System.Collections.Generic;

namespace Servico.Implementacao
{
    public class TurnoServico : ITurnoServico
    {
        private readonly IRepositorio<Turno> _repositorio;
        private readonly IMapper _mapper;

        public TurnoServico(IRepositorio<Turno> repositorio, IMapper mapper)
        {
            _repositorio = repositorio;
            _mapper = mapper;
        }

        public Resposta<TurnoViewModel> Atualizar(TurnoViewModel viewModel)
        {
            Turno entidade = _mapper.Map<Turno>(viewModel);

            try
            {
                var resultado = _repositorio.Atualizar(entidade).Result;
                return new Resposta<TurnoViewModel>(_mapper.Map<TurnoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<TurnoViewModel>(viewModel, $"Ocorreu um erro ao atualizar o turno: {e.Message}");
            }
        }

        public Resposta<TurnoViewModel> Criar(TurnoViewModel viewModel)
        {
            Turno entidade = _mapper.Map<Turno>(viewModel);

            try
            {
                if (string.IsNullOrEmpty(entidade.Descricao))
                    return new Resposta<TurnoViewModel>(null, "Descrição é obrigatório!");

                var resultado = _repositorio.Criar(entidade).Result;
                return new Resposta<TurnoViewModel>(_mapper.Map<TurnoViewModel>(resultado));
            }
            catch (Exception e)
            {
                return new Resposta<TurnoViewModel>(viewModel, $"Ocorreu um erro ao criar o turno: {e.Message}");
            }
        }

        public Resposta<Paginacao<TurnoViewModel>> ListarComPaginacao(Paginacao<TurnoViewModel> entidadeViewModel)
        {

            try
            {
                Paginacao<Turno> entidadePaginada = _mapper.Map<Paginacao<Turno>>(entidadeViewModel);

                if (entidadePaginada.Entidade == null)
                    entidadePaginada.Entidade = new Turno();

                var resultado = _repositorio.ListarPorPaginacao(entidadePaginada);


                if (resultado.TemErro())
                    return new Resposta<Paginacao<TurnoViewModel>>(null, resultado.GetErros());

                var resultadoViewModel = _mapper.Map<Paginacao<TurnoViewModel>>(resultado.GetResultado());

                return new Resposta<Paginacao<TurnoViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<Paginacao<TurnoViewModel>>(null, $"Ocorreu um erro ao listar o turno: {e.Message}");
            }
        }

        public Resposta<TurnoViewModel> ListarPeloId(long id)
        {
            try
            {
                var resultado = _repositorio.Listar(lnq => lnq.Codigo == id).Result;

                var resultadoViewModel = _mapper.Map<TurnoViewModel>(resultado);

                return new Resposta<TurnoViewModel>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<TurnoViewModel>(null, $"Ocorreu um erro ao listar o turno com código {id}: {e.Message}");
            }
        }

        public Resposta<List<TurnoViewModel>> ListarTodos()
        {
            try
            {
                var resultado = _repositorio.ListarTodos().Result;
                var resultadoViewModel = _mapper.Map<List<TurnoViewModel>>(resultado);
                return new Resposta<List<TurnoViewModel>>(resultadoViewModel);
            }
            catch (Exception e)
            {
                return new Resposta<List<TurnoViewModel>>(null, $"Ocorreu um erro ao listar os turnos: {e.Message}");
            }
        }

        public Resposta<bool> Remover(long id)
        {
            try
            {
                var resultado = _repositorio.Remover(lnq => lnq.Codigo == id).Result;
                return new Resposta<bool>(resultado);
            }
            catch (Exception e)
            {
                return new Resposta<bool>(false, $"Ocorreu um erro ao remover o turno com código {id} : {e.Message} ");
            }
        }
    }
}
