using System;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.ManageNews.UpdateExistingNews
{
    public class UpdateExistingNews
    {
        public class Command : IRequest
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Filename { get; set; }
            public DateTime DatePosted { get; set; }
            public string BodyMarkdown { get; set; }
            public string BodyHtml { get; set; }
            public bool IsLive { get; set; }
        }

        public class Handler : IAsyncRequestHandler<Command>
        {
            private readonly UpdateExistingNewsValidator _validator;
            private readonly ClientConfigurationOptions _options;

            public Handler(UpdateExistingNewsValidator validator, ClientConfigurationOptions options)
            {
                _validator = validator;
                _options = options;
            }

            public async Task Handle(Command message)
            {
                _validator.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string query = @"UPDATE news SET Title = @Title, Filename = @Filename, BodyMarkdown = @BodyMarkdown, BodyHtml = @BodyHtml, DatePosted = @DatePosted, IsLive = @IsLive WHERE Id = @Id";
                    await connection.ExecuteAsync(query, new
                    {
                        Id = message.Id,
                        Title = message.Title,
                        Filename = message.Filename,
                        DatePosted = message.DatePosted,
                        BodyMarkdown = message.BodyMarkdown,
                        BodyHtml = message.BodyHtml,
                        IsLive = message.IsLive
                    });
                }
            }
        }
    }
}