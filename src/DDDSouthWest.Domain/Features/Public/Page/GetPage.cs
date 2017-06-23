using System;
using System.Threading.Tasks;
using MediatR;

namespace DDDSouthWest.Domain.Features.Public.Page
{
    public class GetPage
    {
        public class Query : IRequest<Reponse>
        {
            public string Filename { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, Reponse>
        {
            public Task<Reponse> Handle(Query message)
            {
                return Task.FromResult(new Reponse
                {
                    Title = "About",
                    Filename = "about",
                    BodyContent = "Hello from the about page"
                });
            }
        }

        public class Reponse
        {
            public string Title { get; set; }

            public string Filename { get; set; }

            public string BodyContent { get; set; }
        }
    }
}