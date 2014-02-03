using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using NSubstitute;
using Octokit.Tests.Helpers;
using Xunit;

namespace Octokit.Tests.Clients
{
    public class StatisticsClientTests
    {
        public class TheConstructor
        {
            [Fact]
            public void DoesThrowOnBadArguments()
            {
                Assert.Throws<ArgumentNullException>(() => new StatisticsClient(null));
            }
        }

        public class TheGetContributorsMethod
        {
            [Fact]
            public void RequestsCorrectUrl()
            {
                var expectedEndPoint = new Uri("/repos/username/repositoryName/stats/contributors", UriKind.Relative);

                var connection = Substitute.For<IConnection>();
                var client = Substitute.For<IApiConnection>();
                client.Connection.Returns(connection);
                var statisticsClient = new StatisticsClient(client);

                statisticsClient.Contributors("username","repositoryName");

                connection.Received().GetAsync<IList<Contributor>>(expectedEndPoint);
            }

            [Fact]
            public async Task ThrowsIfGivenNullOwner()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.Contributors(null,"repositoryName"));
            }

            [Fact]
            public async Task ThrowsIfGivenNullRepositoryName()
            {
                var statisticsClient = new StatisticsClient(Substitute.For<IApiConnection>());
                await AssertEx.Throws<ArgumentNullException>(() => statisticsClient.Contributors("owner", null));
            }
        }
    }
}