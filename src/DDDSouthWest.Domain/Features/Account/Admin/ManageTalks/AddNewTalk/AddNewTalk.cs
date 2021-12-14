﻿using System.Threading;
using System.Threading.Tasks;
using Dapper;
using FluentValidation;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Admin.ManageTalks.AddNewTalk
{
    public class AddNewTalk
    {
        public class Command : IRequest<Response>
        {
            public string TalkTitle { get; set; }
            public string TalkSummary { get; set; }
            public string TalkBodyMarkdown { get; set; }
            public string TalkBodyHtml { get; set; }
            public int UserId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly AddNewTalkValidator _validator;
            private readonly ClientConfigurationOptions _options;

            public Handler(AddNewTalkValidator validator, ClientConfigurationOptions options)
            {
                _validator = validator;
                _options = options;
            }

            public async Task<Response> Handle(Command message, CancellationToken cancellationToken)
            {
                _validator.ValidateAndThrow(message);

                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string addNewTalkSql = "INSERT INTO Talks (TalkTitle, TalkBodyHtml, TalkBodyMarkdown, TalkSummary, SubmissionDate, UserId) Values (@TalkTitle, @TalkBodyHtml, @TalkBodyMarkdown, @TalkSummary, current_timestamp, @UserId) RETURNING Id";
                    return new Response
                    {
                        Id = await connection.QuerySingleAsync<int>(addNewTalkSql, new
                        {
                            TalkTitle = message.TalkTitle,
                            TalkBodyHtml = message.TalkBodyHtml,
                            TalkBodyMarkdown = message.TalkBodyMarkdown,
                            TalkSummary = message.TalkSummary,
                            UserId = message.UserId
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