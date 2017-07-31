using Dapper;
using FluentValidation;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent
{
    public class CreateNewEventValidator : AbstractValidator<CreateNewEvent.Command>
    {
        private readonly ClientConfigurationOptions _options;

        public CreateNewEventValidator(ClientConfigurationOptions options)
        {
            _options = options;

            RuleFor(x => x.EventName).NotEmpty();
            RuleFor(x => x.EventFilename).Must(FilenameIsUnique).WithMessage("Event with filename already exists");
        }

        private bool FilenameIsUnique(string eventFilename)
        {
            // TODO: Probably don't want to close connection if validation passes as will only be reopened afterwards?
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                const string sql = "SELECT COUNT(*) FROM events WHERE EventFilename = @EventFilename";
                var totalClashingEvents = connection.QuerySingleOrDefault<int>(sql, new {EventFilename = eventFilename});

                return totalClashingEvents == 0;
            }
        }
    }
}