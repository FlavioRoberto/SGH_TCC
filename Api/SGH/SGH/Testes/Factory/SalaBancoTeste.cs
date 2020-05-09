using SGH.Api.Testes.Factory.Contratos;
using SGH.Dominio.Core.Model;
using SHG.Data.Contexto;
using System;
using System.Collections.Generic;

namespace SGH.Api.Testes.Factory
{
    public class SalaBancoTeste : IBancoTeste<Sala>
    {
        private readonly IContexto _contexto;

        public SalaBancoTeste(IContexto contexto)
        {
            _contexto = contexto;
        }

        public void InicializarBanco()
        {
            var salas = new List<Sala> { 
                new Sala
                {
                    CodigoBloco = 3,
                    Descricao = "Sala com bloco vinculado",
                    Laboratorio = true,
                    Numero = 1
                },
                new Sala
                {
                    CodigoBloco = 3,
                    Descricao = "Sala a atualizar",
                    Laboratorio = true,
                    Numero = 1
                },
                new Sala
                {
                    CodigoBloco = 3,
                    Descricao = "Sala remover",
                    Laboratorio = true,
                    Numero = 1
                },
                new Sala //4
                {
                    CodigoBloco = 3,
                    Descricao = "Sala Teste distribuição horários",
                    Laboratorio = true,
                    Numero = 1
                },
                new Sala //5
                {
                    CodigoBloco = 4,
                    Descricao = "Sala Teste distribuição horários",
                    Laboratorio = true,
                    Numero = 3
                }
            };

            _contexto.Sala.AddRange(salas);
            _contexto.SaveChanges();
        }
    }
}
