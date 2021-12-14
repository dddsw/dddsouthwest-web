﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using MediatR;
using Npgsql;

namespace DDDSouthWest.Domain.Features.Account.Speaker.ManageTalks.ListTalks
{
    public class ListAllTalks
    {
        public class Query : IRequest<Response>
        {
            public Query(int userId)
            {
                UserId = userId;
            }
            
            public int UserId { get; }
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly ClientConfigurationOptions _options;

            public Handler(ClientConfigurationOptions options)
            {
                _options = options;
            }

            public async Task<Response> Handle(Query message, CancellationToken cancellationToken)
            {
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string sql = "SELECT Id, TalkTitle, DateAdded, LastModified, IsSubmitted, IsApproved FROM Talks WHERE UserId = @UserId ORDER BY LastModified DESC";
                    var talks = await connection.QueryAsync<TalkListModel>(sql, new
                    {
                        UserId = message.UserId
                    });

                    return new Response
                    {
                        Talks = talks.ToList()
                    };
                }
            }
        }

        public class Response
        {
            public IList<TalkListModel> Talks { get; set; }
        }
    }
}