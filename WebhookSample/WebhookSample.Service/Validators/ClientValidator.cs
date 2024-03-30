using FluentValidation;
using WebhookSample.Domain.Requests.Clients;

namespace WebhookSample.Service.Validators
{
    public class ClientValidator : AbstractValidator<BaseClientRequest>
    {
        public ClientValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(c => c.BirthDate).NotEmpty().NotEqual(new DateOnly()).WithMessage("Birth date is required");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Email address is required").EmailAddress().WithMessage("Invalid email format");
        }
    }
}
