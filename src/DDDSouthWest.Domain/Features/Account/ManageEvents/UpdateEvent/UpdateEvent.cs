using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.UpdateEvent
{
    public class UpdatEvent
    {
        public class Command : IRequest<Response>
        {
            public string EventName { get; set; }
            public string EventFilename { get; set; }
            public DateTime EventDate { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command, Response>
        {
            private readonly UpdateEventValidation _validation;

            public Handler(UpdateEventValidation validation)
            {
                _validation = validation;
            }
            
            public Task<Response> Handle(Command message)
            {
                _validation.ValidateAndThrow(message);
                
                return Task.FromResult(new Response
                {
                    Id = 1
                });
            }
        }

        public class Response
        {
            public int Id { get; set; }
        }
    }
}