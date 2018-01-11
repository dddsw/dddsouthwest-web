using System;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManagePages.CreatePage
{
    public class CreatePage
    {
        public class Command : IRequest<Response>
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Filename { get; set; }

            public string BodyMarkdown { get; set; }
            
            public string BodyHtml { get; set; }

            public bool IsLive { get; set; }

            public int PageOrder { get; set; }

            public DateTime LastModified { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command, Response>
        {
            private readonly ClientConfigurationOptions _options;
            private readonly CreatePageValidation _validation;

            public Handler(ClientConfigurationOptions options, CreatePageValidation validation)
            {
                _options = options;
                _validation = validation;
            }

            public async Task<Response> Handle(Command message)
            {
                _validation.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string query =
                        "INSERT INTO Pages (Title, Filename, BodyHtml, BodyMarkdown, DateCreated, LastModified, PageOrder, IsLive) Values (@Title, @Filename, @BodyHtml, @BodyMarkdown, current_timestamp, current_timestamp, @PageOrder, @IsLive) RETURNING Id";

                    return new Response
                    {
                        Id = await connection.QuerySingleAsync<int>(query, new
                        {
                            Title = message.Title,
                            Filename = message.Filename,
                            BodyHtml = message.BodyHtml,
                            BodyMarkdown = message.BodyMarkdown,
                            PageOrder = message.PageOrder,
                            IsLive = message.IsLive
                        })
                    };
                }
            }
        }

        public class Response
        {
            public int Id { get; set; }
        }
    }
}