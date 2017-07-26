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
            private readonly GetEventByIdQuery _getEventByIdQuery;

            public Handler(GetEventByIdQuery getEventByIdQuery)
            {
                _getEventByIdQuery = getEventByIdQuery;
            }

            public async Task<EventModel> Handle(Query message)
            {
                return await _getEventByIdQuery.Invoke(message.Id);
            }
        }
    }
}