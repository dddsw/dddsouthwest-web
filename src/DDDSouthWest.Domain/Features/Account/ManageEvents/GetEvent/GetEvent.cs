using System.Threading.Tasks;
using MediatR;

namespace DDDSouthWest.Domain.Features.Account.ManageEvents.GetEvent
{
    public class GetEvent
    {
        public class Query : IRequest<EventModel>
        {
            public int Id { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, EventModel>
        {
            private readonly QueryEventById _queryEventById;

            public Handler(QueryEventById queryEventById)
            {
                _queryEventById = queryEventById;
            }

            public async Task<EventModel> Handle(Query message)
            {
                return await _queryEventById.Invoke(message.Id);
            }
        }
    }
}