namespace CodeChallenger.Lancamentos.Application.UseCases.RecuperarRelatorioLancamentosDiario
{
    using MediatR;

    public class RecuperarRelatorioLancamentosDiarioQueryCommand : IRequest<RecuperarRelatorioLancamentosDiarioQueryResult>
    {
        public DateTime Data { get; set; }
    }
}
