using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Xunit;

namespace Blockchain.Investments.Api 
{
    public class BlockchainInvestmentsApiRequestTest
    {
        private readonly TestServer _server;
        private readonly HttpClient _client;
        string testMessage;
        public BlockchainInvestmentsApiRequestTest() 
        {
            // Arrange
            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>());
            _client = _server.CreateClient();
            testMessage = "TestAPI";
        }

        [Fact]
       public async Task ReturnHelloWorld()
        {
            // Act
            string uri = string.Format("/api/about/echo?message={0}", testMessage);
            var response = await _client.GetAsync(uri);
            response.EnsureSuccessStatusCode();

            var responseString = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(testMessage,
                responseString);
        }

    }
}