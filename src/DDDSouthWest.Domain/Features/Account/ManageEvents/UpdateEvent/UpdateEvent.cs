using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.UpdateEvent
{
    public class UpdateEvent
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
            private readonly UpdateEventValidation _validation;

            public Handler(UpdateEventValidation validation)
            {
                _validation = validation;
            }

            public Task Handle(Command message)
            {
                _validation.ValidateAndThrow(message);

                return Task.CompletedTask;
            }
        }
    }
}