using Factory.Core;
using FluentValidation;

namespace Factory.Application.Validators;

public class ActualBoxValidator
{
    public class BoxValidator : AbstractValidator<Box>
    {
        public BoxValidator()
        {
            RuleFor(p => p.ManFacId).GreaterThan(0);
        }
    }
}