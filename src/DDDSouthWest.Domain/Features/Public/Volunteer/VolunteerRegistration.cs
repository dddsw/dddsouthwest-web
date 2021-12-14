using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Public.Volunteer
{
    public static class VolunteerRegistration
    {
        public class Command : IRequest<Response>
        {
            public string FullName { get; set; }

            public string EmailAddress { get; set; }

            public string PhoneNumber { get; set; }

            public bool HelpSetup { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly ClientConfigurationOptions _options;
            private readonly VolunteerRegistrationValidation _validator;

            public Handler(ClientConfigurationOptions options, VolunteerRegistrationValidation validator)
            {
                _options = options;
                _validator = validator;
            }

            public async Task<Response> Handle(Command message, CancellationToken cancellationToken)
            {
                var result = await _validator.ValidateAsync(message);
                if (!result.IsValid)
                    return new Response {Errors = result.Errors};

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string volunteer = "INSERT INTO volunteers (fullname, emailaddress, phonenumber, helpsetup, datesubmitted) Values (@FullName, @EmailAddress, @PhoneNumber, @HelpSetup, current_timestamp) RETURNING Id";
                    await connection.QuerySingleAsync<int>(volunteer, new
                    {
                        FullName = message.FullName,
                        EmailAddress = message.EmailAddress,
                        PhoneNumber = message.PhoneNumber,
                        HelpSetup = message.HelpSetup 
                    });
                }

                return new Response();
            }
        }

        public class Response {
            public IList<ValidationFailure> Errors { get; set; } = new List<ValidationFailure>();
        }
    }
}