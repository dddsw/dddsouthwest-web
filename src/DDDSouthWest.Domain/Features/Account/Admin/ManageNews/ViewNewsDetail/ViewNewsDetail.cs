using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageNews.ViewNewsDetail
{
    public class ViewNewsDetail
    {
        public class Query : IRequest<NewsModel>
        {
            public int Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, NewsModel>
        {
            private readonly QueryAnyNewsById _queryEventById;

            public Handler(QueryAnyNewsById queryEventById)
            {
                _queryEventById = queryEventById;
            }

            public async Task<NewsModel> Handle(Query message, CancellationToken cancellationToken)
            {
                var model = await _queryEventById.Invoke(message.Id, false);

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