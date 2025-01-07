namespace CodeChallenger.Lancamentos.Domain.Entity
{
    using System;

    public class Operacao : AbstractBaseEntity
    {
        public virtual decimal ValorTotal { get; protected set; }
        public virtual Categoria Categoria { get; protected set; }
        public virtual Movimento Movimento { get; protected set; }
        public virtual StatusOperacao Status { get; protected set; }
        public virtual Guid Identificador { get; protected set; } = Guid.NewGuid();
        public virtual int TotalParcelas { get; protected set; } = 1;
        public virtual int NumeroParcela { get; protected set; } = 1;
        public virtual decimal ValorParcela { get; protected set; } = decimal.Zero;
        public virtual DateTime DataPrevista { get; protected set; }
        public virtual DateTime? DataRealizacao { get; protected set; }
        public virtual string? Descricao { get; protected set; }

        #region Setters

        public Operacao SetValorTotal(decimal valor)
        {
            this.ValorTotal = valor;
            return this;
        }

        public Operacao SetCategoria(Categoria categoria)
        {
            this.Categoria = categoria;
            return this;
        }

        public Operacao SetMovimento(Movimento movimento)
        {
            this.Movimento = movimento;
            return this;
        }

        public Operacao SetStatus(StatusOperacao status)
        {
            this.Status = status;
            return this;
        }

        public Operacao SetDataPrevista(DateTime data)
        {
            this.DataPrevista = data;
            return this;
        }

        public Operacao SetDataRealizacao(DateTime? data)
        {
            this.DataRealizacao = data;
            return this;
        }

        public Operacao SetDescricao(string? descricao)
        {
            this.Descricao = descricao;
            return this;
        }

        public Operacao SetIdentificador(Guid identificador)
        {
            this.Identificador = identificador;
            return this;
        }

        public Operacao SetTotalParcelas(int totalParcelas)
        {
            this.TotalParcelas = totalParcelas;
            return this;
        }

        public Operacao SetNumeroParcela(int numeroParcela)
        {
            this.NumeroParcela = numeroParcela;
            return this;
        }

        public Operacao SetValorParcela(decimal valorParcela)
        {
            this.ValorParcela = valorParcela;
            return this;
        }

        #endregion

        #region Support

        public static IEnumerable<Operacao> DefinirOperacesParceladas(decimal valorTotal, Movimento movimento,
            DateTime data, int numeroDeParcelas, string? descricao, Categoria categoria)
        {
            var identificador = Guid.NewGuid();
            var valorParcela = decimal.Round (valorTotal / numeroDeParcelas, 2);
            
            var parcelas = Enumerable
                .Range(1, numeroDeParcelas)
                .Select(x => new Operacao()
                    .SetCategoria(categoria)
                    .SetNumeroParcela(x)
                    .SetDescricao($"{descricao} - {x}/{numeroDeParcelas}")
                    .SetDataRealizacao(x == 1 ? data.Date : null!)
                    .SetDataPrevista(data.Date.AddMonths(x - 1))
                    .SetIdentificador(identificador)
                    .SetMovimento(movimento)
                    .SetTotalParcelas(numeroDeParcelas)
                    .SetValorParcela(valorParcela)
                    .SetValorTotal(valorTotal)
                    .SetStatus(x == 1 && data.Date <= DateTime.UtcNow.Date ? StatusOperacao.EFETIVADO : StatusOperacao.PREVISTO));

            var somaParcelas = parcelas.Sum(x => x.ValorParcela);
            
            var ultimaParcela = somaParcelas > valorTotal
                ? valorParcela - (somaParcelas - valorTotal)
                : valorParcela + (valorTotal - somaParcelas);

            parcelas.LastOrDefault()?.SetValorParcela(ultimaParcela);

            return parcelas;
        }

        #endregion
    }
}
