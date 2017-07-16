using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.CreateEvent
{
    public class CreateEvent
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

            public Handler(CreateEventValidation validation)
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