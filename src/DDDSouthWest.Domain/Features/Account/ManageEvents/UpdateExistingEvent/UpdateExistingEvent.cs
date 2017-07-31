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
            private readonly UpdateExistingEventValidator _validator;
            private readonly ClientConfigurationOptions _options;

            public Handler(UpdateExistingEventValidator validator, ClientConfigurationOptions options)
            {
                _validator = validator;
                _options = options;
            }

            public async Task Handle(Command message)
            {
                _validator.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    await connection.ExecuteAsync(@"UPDATE events SET EventName = @EventName, EventFilename = @EventFilename WHERE Id = @Id", message);
                }
            }
        }
    }
}