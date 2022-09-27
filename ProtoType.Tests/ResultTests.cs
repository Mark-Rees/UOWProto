using Microsoft.Extensions.DependencyInjection;
using ProtoType.Business.Services;
using ProtoType.Core.Entities;
using ProtoType.Core.Enums;

namespace ProtoType.Tests
{
    public class ResultTests : IClassFixture<TestSetup>
    {
        private ResultService _resultService;
        private ServiceProvider _serviceProvider;
        private readonly Fixture _fixture;

        public ResultTests(TestSetup testSetup)
        {
            _serviceProvider = testSetup.ServiceProvider;
            _resultService = _serviceProvider.GetService<ResultService>();
            _fixture = new Fixture
            {
                HomeTeamScore = 1,
                AwayTeamScore = 2
            };
        }

        [Theory]
        [InlineData(0, 1)]
        [InlineData(1, 0)]
        [InlineData(1, 2)]
        [InlineData(1, 1)]
        public void DoesResultCorrectScore(int homeTeamScore, int awayTeamScore)
        {
            var result = _resultService.ResultCorrectScore(_fixture, homeTeamScore, awayTeamScore);

            Assert.True(result == TipResult.WIN);
        }
    }
}