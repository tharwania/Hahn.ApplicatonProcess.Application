using System.Data;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using FluentValidation;
using Hahn.ApplicatonProcess.December2020.Web.Models.Applicant;
using Hahn.ApplicatonProcess.December2020.Web.Resources;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Localization;

namespace Hahn.ApplicatonProcess.December2020.Web.Applicant.Validators
{
    public class CreateApplicantRequestValidator : AbstractValidator<CreateApplicantRequest>
    {
        private static readonly HttpClient _client = new HttpClient();
        public CreateApplicantRequestValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage(localizer["Name is required."])
                .MinimumLength(5)
                .WithMessage(localizer["Name should be minimum 5 characters."]);

            RuleFor(x => x.FamilyName)
                .NotEmpty()
                .WithMessage(localizer["FamilyName is required."])
                .MinimumLength(5)
                .WithMessage(localizer["FamilyName should be minimum 5 characters."]);

            RuleFor(x => x.Address)
                .NotEmpty()
                .WithMessage(localizer["Address is required."])
                .MinimumLength(10)
                .WithMessage(localizer["Address should be minimum 10 characters."]);

            RuleFor(x => x.CountryOfOrigin).NotEmpty().Must(country =>
            {

                var url = $"https://restcountries.eu/rest/v2/name/" + country + "?fullText=true";
                var response = _client.GetAsync(url);
                return response.Result.IsSuccessStatusCode;
            }).WithMessage(localizer["Country name is invalid."]);


            RuleFor(x => x.EmailAddress).NotEmpty()
                .WithMessage(localizer["EmailAddress is required."])
                .EmailAddress()
                .WithMessage(localizer["EmailAddress should be a valid email address."]);

            RuleFor(x => x.Age)
                .NotEmpty()
                .WithMessage(localizer["Age is required."])
                .InclusiveBetween(20, 60)
                .WithMessage(localizer["Age should be between 20 to 60."]);
        }
    }
}
