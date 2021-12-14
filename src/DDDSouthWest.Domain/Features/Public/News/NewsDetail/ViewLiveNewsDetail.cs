using System.Threading;
using System.Threading.Tasks;
using DDDSouthWest.Domain.Features.Account.Admin.ManageNews.ViewNewsDetail;
using MediatR;

namespace DDDSouthWest.Domain.Features.Public.News.NewsDetail
{
    public class ViewLiveNewsDetail
    {
        public class Query : IRequest<NewsDetailModel>
        {
            public Query()
            {    
            }
            
            public Query(int id, string filename)
            {
                Id = id;
                Filename = filename;
            }
            
            public int Id { get; set; }

            public string Filename { get; set; }
        }

        public class Handler : IRequestHandler<Query, NewsDetailModel>
        {
            private readonly QueryAnyNewsById _newsById;

            public Handler(QueryAnyNewsById newsById)
            {
                _newsById = newsById;
            }

            public async Task<NewsDetailModel> Handle(Query message, CancellationToken cancellationToken)
            {
                var news = await _newsById.Invoke(message.Id);
                if (!news.IsLive)
                    throw new RecordNotFoundException("News item could not be found");

                return news;
            }
        }
    }
}