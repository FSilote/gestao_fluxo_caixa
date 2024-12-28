﻿namespace CodeChallenger.Lancamentos.Domain.Exceptions
{
    using System;

    public class NotFoundException : Exception
    {
        public NotFoundException()
            : base() { }

        public NotFoundException(string message)
            : base(message) { }
    }
}
