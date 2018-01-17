﻿using System.Collections.Generic;
using System.Linq;
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

        public class Handler : IAsyncRequestHandler<Query, Response>
        {
            private readonly ClientConfigurationOptions _options;

            public Handler(ClientConfigurationOptions options)
            {
                _options = options;
            }

            public async Task<Response> Handle(Query message)
            {
                using (var connection = new NpgsqlConnection(_options.Database.ConnectionString))
                {
                    const string sql = "SELECT Id, TalkTitle, DateAdded, LastModified FROM Talks WHERE UserId = @UserId";
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