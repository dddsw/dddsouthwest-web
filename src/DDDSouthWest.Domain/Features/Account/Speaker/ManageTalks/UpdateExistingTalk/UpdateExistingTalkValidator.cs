using Dapper;
using FluentValidation;
using FluentValidation.Validators;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.UpdateExistingTalk
{
    public class UpdateExistingTalkValidator : AbstractValidator<UpdateExistingTalk.Command>
    {
        private readonly ClientConfigurationOptions _options;

        public UpdateExistingTalkValidator(ClientConfigurationOptions options)
        {
            _options = options;

            /*RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Filename).NotEmpty();
            RuleFor(x => x.Filename).Must(FilenameIsUnique).WithMessage("'News Filename' must be unqiue");*/
        }

        private bool FilenameIsUnique(Admin.ManagePages.UpdateExistingPage.UpdateExistingPage.Command command, string newsFilename, PropertyValidatorContext context)
        {
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                const string sql = "SELECT COUNT(*) FROM pages WHERE Filename = @Filename AND Id != @Id";
                var totalClashes = connection.QuerySingleOrDefault<int>(sql, new {command.Id, Filename = newsFilename});

                return totalClashes == 0;
            }
        }
    }
}