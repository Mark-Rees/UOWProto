using Dapper;
using ProtoType.Core.Entities;
using ProtoType.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtoType.Data.Repositories
{
    public class FixtureRepository : Repository, IFixtureRepository
    {
        public FixtureRepository(IDbTransaction transaction) : base(transaction) { }

        public async Task<Fixture> Get(int id)
        {
            var sql = @"select f.*, th.*, ta.*, thp.*, tap.*
                        from dbo.Fixture f with (nolock)
                            join dbo.Team th with (nolock) on f.HomeTeamId = th.Id
                            join dbo.Team ta with (nolock) on f.AwayTeamId = ta.Id
                            join dbo.Player thp with (nolock) on thp.TeamId = th.Id
                            join dbo.Player tap with (nolock) on tap.TeamId = ta.Id
                        where f.Id = @id";

            var fixtureDictionary = new Dictionary<int, Fixture>();

            var fixtures = await Transaction.Connection.QueryAsync<Fixture, Team, Team, Player, Player, Fixture>(sql, map: (f, th, ta, thp, tap) =>
            {
                if (!fixtureDictionary.TryGetValue(f.Id, out var fixture))
                {
                    fixture = f;

                    fixture.HomeTeam = new Team
                    {
                        Id = th.Id,
                        Name = th.Name
                    };

                    fixture.AwayTeam = new Team
                    {
                        Id = ta.Id,
                        Name = ta.Name
                    };

                    fixtureDictionary.Add(fixture.Id, fixture);
                }

                if (!fixture.HomeTeam.Players.Any(x => x.Id == thp.Id))
                {
                    fixture.HomeTeam.Players.Add(thp);
                }

                if (!fixture.AwayTeam.Players.Any(x => x.Id == tap.Id))
                {
                    fixture.AwayTeam.Players.Add(tap);
                }

                return fixture;

            }, new { id }, transaction: Transaction);

            return fixtures.Distinct().FirstOrDefault();
        }
    }
}
