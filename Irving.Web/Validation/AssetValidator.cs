using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;
using Irving.Web.Models;

namespace Irving.Web.Validation
{
    public class AssetValidator : AbstractValidator<Asset>
    {
        public AssetValidator()
        {
            RuleFor(asset => asset.Name).NotEmpty();
        }
    }
}