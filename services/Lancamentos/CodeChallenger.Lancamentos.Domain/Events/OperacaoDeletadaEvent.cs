namespace CodeChallenger.Lancamentos.Domain.Events
{
    public class OperacaoDeletadaEvent
    {
        public int Id { get; set; }
        public Guid Identificador { get; set; }
    }
}
