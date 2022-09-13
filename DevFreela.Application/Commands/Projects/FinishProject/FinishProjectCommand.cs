﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevFreela.Application.Commands.Projects
{
    public class FinishProjectCommand : IRequest<Unit>
    {
        public FinishProjectCommand(int id, string creditCardNumber, string cvv, string expiresAt, string fullname)
        {
            Id = id;
            CreditCardNumber = creditCardNumber;
            Cvv = cvv;
            ExpiresAt = expiresAt;
            Fullname = fullname;
        }

        public int Id { get; private set; }

        public string CreditCardNumber { get; private set; }

        public string Cvv { get; private set; }

        public string ExpiresAt { get; private set; }

        public string Fullname { get; private set; }
    }
}
