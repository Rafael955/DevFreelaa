using DevFreela.Application.Commands.Project;
using FluentValidation;

namespace DevFreela.Application.Validations
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .NotNull()
                .WithMessage("Titulo é obrigatório!");

            RuleFor(x => x.Title)
                .MaximumLength(60)
                .WithMessage("Tamanho máximo permitido é de 60 caracteres!");

            RuleFor(x => x.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage("Descrição é obrigatória!");

            RuleFor(x => x.Description)
                .MaximumLength(120)
                .WithMessage("Tamanho máximo permitido é de 120 caracteres!");
        }
    }
}