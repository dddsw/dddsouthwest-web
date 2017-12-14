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
            private readonly QueryAnyNewsById _queryEventById;

            public Handler(QueryAnyNewsById queryEventById)
            {
                _queryEventById = queryEventById;
            }

            public async Task<NewsModel> Handle(Query message)
            {
                var model = await _queryEventById.Invoke(message.Id);

                return new NewsModel
                {
                    BodyMarkdown = model.BodyMarkdown,
                    DatePosted = model.DatePosted,
                    Filename = model.Filename,
                    Id = model.Id,
                    IsLive = model.IsLive,
                    Title = model.Title
                };
            }
        }
    }
}