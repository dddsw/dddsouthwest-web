using System;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.UpdateExistingEvent
{
    public class UpdateExistingEvent
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string EventName { get; set; }
            public string EventFilename { get; set; }
            public DateTime EventDate { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command>
        {
            private readonly UpdateExistingEventValidation _validation;
            private readonly ClientConfigurationOptions _options;

            public Handler(UpdateExistingEventValidation validation, ClientConfigurationOptions options)
            {
                _validation = validation;
                _options = options;
            }

            public async Task Handle(Command message)
            {
                _validation.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    int totalClashingEvents = await connection.QuerySingleOrDefaultAsync<int>("SELECT COUNT(*) FROM events WHERE EventFilename = @EventFilename AND Id != @Id", new { Id = message.Id, EventFilename = message.EventFilename});
                    if (totalClashingEvents > 0)
                        throw new DuplicateRecordException($"Event with filename '{message.EventFilename}' already exists");

                    await connection.ExecuteAsync(@"UPDATE events SET EventName = @EventName, EventFilename = @EventFilename WHERE Id = @Id", message);
                }
            }
        }
    }
}