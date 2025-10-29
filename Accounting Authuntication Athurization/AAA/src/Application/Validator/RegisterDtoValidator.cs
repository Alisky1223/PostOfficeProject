using AAA.src.Domain.Model;
using CommonDll.Dto;
using FluentValidation;
using Microsoft.Extensions.Options;

namespace AAA.src.Application.Validator
{
    public class RegisterDtoValidator: AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator(IOptions<PasswordPolicy> policy)
        {
            var p = policy.Value;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username too short.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Equal(x => x.ConfirmPassword).WithMessage("Passwords do not match.")
                .When(_ => p.RequireStrongPassword, ApplyConditionTo.CurrentValidator)
                .Must(pass =>
                {
                    var errors = new List<string>();

                    if (pass.Length < p.MinLength)
                        errors.Add($"at least {p.MinLength} characters");

                    if (p.RequireUppercase && !pass.Any(char.IsUpper))
                        errors.Add("one uppercase");

                    if (p.RequireLowercase && !pass.Any(char.IsLower))
                        errors.Add("one lowercase");

                    if (p.RequireDigit && !pass.Any(char.IsDigit))
                        errors.Add("one digit");

                    if (p.RequireSpecialChar && !pass.Any(ch => p.SpecialChars.Contains(ch)))
                        errors.Add($"one of: {p.SpecialChars}");

                    return errors.Count == 0 ? true : false;
                })
                .WithMessage(x =>
                {
                    if (!p.RequireStrongPassword) return null;
                    return $"Password too weak. Must contain: {GetRequirements(p)}";
                });
        }

        private string GetRequirements(PasswordPolicy p)
        {
            var reqs = new List<string>();
            if (p.MinLength > 0) reqs.Add($"{p.MinLength}+ chars");
            if (p.RequireUppercase) reqs.Add("uppercase");
            if (p.RequireLowercase) reqs.Add("lowercase");
            if (p.RequireDigit) reqs.Add("digit");
            if (p.RequireSpecialChar) reqs.Add("special char");
            return string.Join(", ", reqs);
        }
    }
}
