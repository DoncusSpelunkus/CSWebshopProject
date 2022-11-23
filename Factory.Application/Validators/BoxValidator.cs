using Factory.Application.PostBoxDTO;
using FluentValidation;

namespace Factory.Application.Validators
{
    public class BoxValidator : AbstractValidator<BoxDTO>
    {
        public BoxValidator()
        {
            
            RuleFor(p => p.length).GreaterThan(0);
            RuleFor(p => p.width).GreaterThan(0);
            RuleFor(p => p.height).GreaterThan(0);
        }
    }
}
