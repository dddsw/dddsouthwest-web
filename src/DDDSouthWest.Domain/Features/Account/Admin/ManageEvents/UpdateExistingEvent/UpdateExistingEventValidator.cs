using Dapper;
using FluentValidation;
using FluentValidation.Validators;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.UpdateExistingEvent
{
    public class UpdateExistingEventValidator : AbstractValidator<Admin.ManageEvents.UpdateExistingEvent.UpdateExistingEvent.Command>
    {
        private readonly ClientConfigurationOptions _options;

        public UpdateExistingEventValidator(ClientConfigurationOptions options)
        {
            _options = options;
            
            RuleFor(x => x.EventName).NotEmpty();
            RuleFor(x => x.EventFilename).NotEmpty();
            RuleFor(x => x.EventFilename).Must(FilenameIsUnique).WithMessage("'Event Filename' must be unqiue");
        }

        private bool FilenameIsUnique(Admin.ManageEvents.UpdateExistingEvent.UpdateExistingEvent.Command command, string eventFilename, PropertyValidatorContext context)
        {
            using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
            {
                const string sql = "SELECT COUNT(*) FROM events WHERE EventFilename = @EventFilename AND Id != @Id";
                var totalClashingEvents = connection.QuerySingleOrDefault<int>(sql, new { Id = command.Id, EventFilename = eventFilename });

                return totalClashingEvents == 0;
            }
        }
    }
}