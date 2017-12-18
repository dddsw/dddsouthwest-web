using System;
using System.Threading.Tasks;
using FluentValidation;
using InfluxDB.Collector;
using InfluxDB.Collector.Diagnostics;
using InfluxDB.Net;
using InfluxDB.Net.Enums;
using InfluxDB.Net.Infrastructure.Influx;
using InfluxDB.Net.Models;
using MediatR;

namespace DDDSouthWest.Domain.Features.Account.RegisterNewUser
{
    public class RegisterNewUser
    {
        public class Command : INotification
        {
            public string EmailAddress { get; set; }

            public string Password { get; set; }
        }

        public class Handler : IAsyncNotificationHandler<Command>
        {
            private readonly RegisterNewUserValidator _validator;
            private readonly CreateNewRegisteredUser _createNewRegisteredUser;
            private readonly IRegistrationConfirmation _registrationConfirmation;

            public Handler(RegisterNewUserValidator validator, CreateNewRegisteredUser createNewRegisteredUser, IRegistrationConfirmation registrationConfirmation)
            {
                _validator = validator;
                _createNewRegisteredUser = createNewRegisteredUser;
                _registrationConfirmation = registrationConfirmation;
            }

            public async Task Handle(Command message)
            {
                var validationResult = _validator.Validate(message);
                if (!validationResult.IsValid)
                    throw new ValidationException(validationResult.Errors);
                
                CollectorLog.RegisterErrorHandler((m, exception) =>
                {
                    Console.WriteLine($"{m}: {exception}");
                });
                
                Metrics.Collector = new CollectorConfiguration()
                    .Tag.With("host", Environment.GetEnvironmentVariable("COMPUTERNAME"))
                    .Batch.AtInterval(TimeSpan.FromSeconds(2))
                    .WriteTo.InfluxDB("http://0.0.0.0:8086", "data")
                    .CreateCollector();
                

                await _createNewRegisteredUser.Invoke(message);
                await _registrationConfirmation.Notify(message.EmailAddress);
            }
        }
    }
}