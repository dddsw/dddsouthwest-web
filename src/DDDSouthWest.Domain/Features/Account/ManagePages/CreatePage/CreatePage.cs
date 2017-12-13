using System;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
namespace DDDSouthWest.Domain.Features.Account.ManagePages.CreatePage
{
    public class CreatePage
    {
        public class Command : IRequest<Response>
        {
            public int Id { get; set; }

            public string PageTitle { get; set; }

            public string PageFilename { get; set; }

            public string PageBody { get; set; }

            public DateTime LastModified { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command, Response>
        {
            private readonly CreatePageValidation _validation;

            public Handler(CreatePageValidation validation)
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