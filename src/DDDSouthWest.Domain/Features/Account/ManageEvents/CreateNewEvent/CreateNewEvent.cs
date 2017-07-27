using System;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.CreateNewEvent
{
    public class CreateNewEvent
    {
        public class Command : IRequest<Response>
        {
            public string EventName { get; set; }
            public string EventFilename { get; set; }
            public DateTime EventDate { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command, Response>
        {
            private readonly CreateEventValidation _validation;
            private readonly ClientConfigurationOptions _options;

            public Handler(CreateEventValidation validation, ClientConfigurationOptions options)
            {
                _validation = validation;
                _options = options;
            }
            
            public async Task<Response> Handle(Command message)
            {
                _validation.ValidateAndThrow(message);
                
                int eventId;
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    // TODO: Move into query object, or even validation object?
                    int totalClashingEvents = await connection.QuerySingleOrDefaultAsync<int>("SELECT COUNT(*) FROM events WHERE EventFilename = @EventFilename", new {EventFilename = message.EventFilename});
                    if (totalClashingEvents > 0)
                        throw new DuplicateRecordException($"Event with filename '{message.EventFilename}' already exists");

                    eventId = await connection.QuerySingleAsync<int>(@"INSERT INTO events (EventName, EventFilename) Values (@EventName, @EventFilename) RETURNING Id;", message);
                }

                return new Response
                {
                    Id = eventId
                };
            }
        }

        public class Response
        {
            public int Id { get; set; }
        }
    }
}