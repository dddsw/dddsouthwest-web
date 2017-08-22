using Dapper;
using FluentValidation;
using FluentValidation.Validators;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.RegisterNewUser
{
    public class RegisterNewUserValidator : AbstractValidator<RegisterNewUser.Command>
    {
        public static readonly string NotUniqueErrorCode = "NotUnique";
        private readonly ClientConfigurationOptions _options;
        private const int PasswordLength = 5;

        public RegisterNewUserValidator(ClientConfigurationOptions options)
        {
            _options = options;

            RuleFor(x => x.EmailAddress).Must(BeAnEmailAddress).WithMessage("'Email address' is a valid email address");
            RuleFor(x => x.EmailAddress).Must(BeAUniqueEmailAddress).WithErrorCode(NotUniqueErrorCode);

            RuleFor(x => x.Password).Must(BeASensiblePassword).WithMessage(
                $"'Password' must be at least {PasswordLength} characters long with at least one uppercase and one number");
        }

        private bool BeAUniqueEmailAddress(RegisterNewUser.Command command, string emailAddress, PropertyValidatorContext arg3)
        {
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                const string sql = "SELECT COUNT(*) FROM users WHERE EmailAddress = @EmailAddress";
                var totalExistingEmailAddresses = connection.QuerySingleOrDefault<int>(sql, new { EmailAddress = emailAddress });

                return totalExistingEmailAddresses == 0;
            }
        }

        private static bool BeAnEmailAddress(string emailAddress)
        {
            if (string.IsNullOrWhiteSpace(emailAddress))
                return false;

            //TODO: Email validation
            return true;
        }

        private static bool BeASensiblePassword(string password)
        {
            if (password.Length < PasswordLength)
                return false;

            //TODO: Enforce sensible password rules whatever that may be
            return true;
        }
    }
}