namespace CodeChallenger.Saldo.Domain.Entity
{
    using CodeChallenger.Lancamentos.Domain.Entity;
    using System;

    public class Operacao : AbstractBaseEntity
    {
        public virtual decimal ValorTotal { get; protected set; }
        public virtual Movimento Movimento { get; protected set; }
        public virtual StatusOperacao Status { get; protected set; }
        public virtual Guid Identificador { get; protected set; } = Guid.NewGuid();
        public virtual int TotalParcelas { get; protected set; } = 1;
        public virtual int NumeroParcela { get; protected set; } = 1;
        public virtual decimal ValorParcela { get; protected set; } = decimal.Zero;
        public virtual DateTime DataPrevista { get; protected set; }
        public virtual DateTime? DataRealizacao { get; protected set; }
        public virtual string? Descricao { get; protected set; }

        public virtual DateTime? DataAlteracao { get; protected set; }
        public virtual int IdIntegracao { get; protected set; }

        #region Setters

        public Operacao SetValorTotal(decimal valor)
        {
            this.ValorTotal = valor;
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

        public Operacao SetIdIntegracao(int idIntegracao)
        {
            this.IdIntegracao = idIntegracao;
            return this;
        }

        public Operacao SetDataAlteracao(DateTime dataAlteracao)
        {
            this.DataAlteracao = dataAlteracao;
            return this;
        }

        #endregion
    }
}
