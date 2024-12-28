﻿namespace CodeChallenger.Sdk.Domain.Entity
{
    public abstract class AbstractBaseEntity
    {
        public virtual int Id { get; protected set; }
        public virtual DateTime DataCriacao { get; protected set; } = DateTime.Now;
    }
}