using System;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageNews.CreateNews
{
    public class CreateNews
    {
        public class Command : IRequest<Response>
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public string Filename { get; set; }

            public DateTime DatePosted { get; set; }

            public string BodyHtml { get; set; }
            
            public string BodyMarkdown { get; set; }

            public bool IsLive { get; set; }

            public DateTime LastModified { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly CreateNewsValidation _validation;
            private readonly ClientConfigurationOptions _options;

            public Handler(CreateNewsValidation validation, ClientConfigurationOptions options)
            {
                _validation = validation;
                _options = options;
            }

            public async Task<Response> Handle(Command message, CancellationToken cancellationToken)
            {
                _validation.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string query =
                        "INSERT INTO News (Title, Filename, BodyHtml, BodyMarkdown, DatePosted, IsLive) Values (@Title, @Filename, @BodyHtml, @BodyMarkdown, @DatePosted, @IsLive) RETURNING Id";

                    return new Response
                    {
                        Id = await connection.QuerySingleAsync<int>(query, new
                        {
                            Title = message.Title,
                            Filename = message.Filename,
                            BodyHtml = message.BodyHtml,
                            BodyMarkdown = message.BodyMarkdown,
                            IsLive = message.IsLive,
                            DatePosted = message.DatePosted
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