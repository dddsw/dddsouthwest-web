using System.Threading.Tasks;
using MediatR;

namespace DDDSouthWest.Domain.Features.Account.ManageNews.ViewNewsDetail
{
    public class ViewNewsDetail
    {
        public class Query : IRequest<NewsModel>
        {
            public int Id { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Query, NewsModel>
        {
            private readonly QueryNewsById _queryEventById;

            public Handler(QueryNewsById queryEventById)
            {
                _queryEventById = queryEventById;
            }

            public async Task<NewsModel> Handle(Query message)
            {
                return await _queryEventById.Invoke(message.Id);
            }
        }
    }
}