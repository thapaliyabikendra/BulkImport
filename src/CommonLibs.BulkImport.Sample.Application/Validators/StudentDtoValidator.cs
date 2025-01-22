using CommonLibs.BulkImport.Sample.Dtos;
using FluentValidation;
using System;
using System.Linq;

namespace CommonLibs.BulkImport.Sample.Validators;

public class StudentDtoValidator : AbstractValidator<StudentDto>
{
    public StudentDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("The 'Name' field is required.")
            .MaximumLength(50).WithMessage("The 'Name' field cannot exceed 50 characters.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("The 'Surname' field is required.")
            .MaximumLength(50).WithMessage("The 'Surname' field cannot exceed 50 characters.");

        RuleFor(x => x.Grade)
            .NotEmpty().WithMessage("The 'Grade' field is required.")
            .Must(grade => new[] { "A", "B", "C", "D", "F" }.Contains(grade))
            .WithMessage("The 'Grade' field must be one of the following: A, B, C, D, F.");
    }
}