﻿namespace CodeChallenger.Saldo.Application.UseCases.ListarOperacoes
{
    using CodeChallenger.Saldo.Domain.Entity;
    using MediatR;

    public class ListarOperacoesQueryCommand : IRequest<IEnumerable<ListarOperacoesQueryResult>>
    {
        public Movimento? Movimento { get; set; }
        public bool? ComParcelamento { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
        public string? Comentario { get; set; }
    }
}
