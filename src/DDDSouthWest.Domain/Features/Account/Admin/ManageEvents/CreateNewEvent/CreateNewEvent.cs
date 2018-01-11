using System;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageEvents.CreateNewEvent
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
            private readonly CreateNewEventValidator _validator;
            private readonly ClientConfigurationOptions _options;

            public Handler(CreateNewEventValidator validator, ClientConfigurationOptions options)
            {
                _validator = validator;
                _options = options;
            }
            
            public async Task<Response> Handle(Command message)
            {
                _validator.ValidateAndThrow(message);
                
                int eventId;
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string createEventSql = "INSERT INTO events (EventName, EventFilename) Values (@EventName, @EventFilename) RETURNING Id";
                    eventId = await connection.QuerySingleAsync<int>(createEventSql, message);
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