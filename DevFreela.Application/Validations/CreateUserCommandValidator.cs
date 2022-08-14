using DevFreela.Application.Commands.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevFreela.Application.Validations
{
    public class NewUserCommandValidator : AbstractValidator<NewUserCommand>
    {
        public NewUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .WithMessage("E-mail não válido!");

            RuleFor(x => x.Fullname)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome é obrigatório!");
        }
    }
}
