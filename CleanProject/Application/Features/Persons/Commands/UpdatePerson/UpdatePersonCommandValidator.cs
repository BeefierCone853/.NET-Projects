using Application.DTOs.Persons;
using Domain.Repositories;
using FluentValidation;

namespace Application.Features.Persons.Commands.UpdatePerson;

internal class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonDto>
{
    public UpdatePersonCommandValidator(IPersonRepository personRepository)
    {
        RuleFor(p => p.Id)
            .GreaterThan(0)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull().WithMessage("{PropertyName} is required.")
            .MustAsync(async (id, token) =>
            {
                var personExists = await personRepository.Exists(id);
                return !personExists;
            })
            .WithMessage("{PropertyName} does not exist.");
        RuleFor(p => p.FirstName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        RuleFor(p => p.LastName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
        RuleFor(p => p.CollegeName)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MaximumLength(30).WithMessage("{PropertyName} must not exceed 50 characters.");
    }
}