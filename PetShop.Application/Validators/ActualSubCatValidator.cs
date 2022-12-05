﻿using Factory.Domain;
using FluentValidation;

namespace PetShop.Application.Validators;

public class ActualSubCatValidator
{
    public class SubCatValidator : AbstractValidator<MainCategory>
    {
        public SubCatValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.ProdList);
        }
    }
}