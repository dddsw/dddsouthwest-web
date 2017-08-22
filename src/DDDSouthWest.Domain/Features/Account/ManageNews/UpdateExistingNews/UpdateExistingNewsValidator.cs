using Dapper;
using DDDSouthWest.Domain.Features.Account.ManageEvents.UpdateExistingEvent;
using FluentValidation;
using FluentValidation.Validators;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageNews.UpdateExistingNews
{
    public class UpdateExistingNewsValidator : AbstractValidator<UpdateExistingNews.Command>
    {
        private readonly ClientConfigurationOptions _options;

        public UpdateExistingNewsValidator(ClientConfigurationOptions options)
        {
            _options = options;
            
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.Filename).NotEmpty();
            RuleFor(x => x.Filename).Must(FilenameIsUnique).WithMessage("'News Filename' must be unqiue");
        }

        private bool FilenameIsUnique(UpdateExistingNews.Command command, string newsFilename, PropertyValidatorContext context)
        {
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                const string sql = "SELECT COUNT(*) FROM news WHERE Filename = @Filename AND Id != @Id";
                var totalClashes = connection.QuerySingleOrDefault<int>(sql, new { Id = command.Id, Filename = newsFilename });

                return totalClashes == 0;
            }
        }
    }
}