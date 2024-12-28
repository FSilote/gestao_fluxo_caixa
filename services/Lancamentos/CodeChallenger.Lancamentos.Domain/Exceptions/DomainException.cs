﻿namespace CodeChallenger.Lancamentos.Domain.Exceptions
{
    using System;

    public class DomainException : Exception
    {
        public DomainException()
            : base() { }

        public DomainException(string message)
            : base(message) { }
    }
}
