using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using FluentAssertions;
using Xunit;

namespace Cronofly.AcceptanceTests.Controllers.LinkShorteningController.Post
{
    [Collection(nameof(Fixture))]
    public class WhenMakingAnInvalidPostRequest : IClassFixture<WhenMakingAnInvalidPostRequest.Invocation>
    {
        public class Invocation : IAsyncLifetime
        {
            private readonly Fixture _fixture;
            public HttpResponseMessage Response;
            public ScanResponse ScanResponse;

            public Invocation(Fixture fixture)
            {
                _fixture = fixture;
            }

            public async Task InitializeAsync()
            {
                var body = new
                {
                    UrlToShorten = "bad-link"
                };

                var request = new HttpRequestMessage(HttpMethod.Post, "");
                request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

                Response = await _fixture.HttpClient.SendAsync(request);

                ScanResponse = await _fixture.DynamoDB.ScanAsync(new ScanRequest
                {
                    TableName = "cronofly-table"
                });
            }

            public Task DisposeAsync() => Task.CompletedTask;
        }

        private Invocation _invocation;

        public WhenMakingAnInvalidPostRequest(Invocation invocation)
        {
            _invocation = invocation;
        }

        [Fact]
        public void ResponseIs200()
        {
            _invocation.Response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task ResultContainsExpectedContent()
        {
            var responseContent = await _invocation.Response.Content.ReadAsStringAsync();
            responseContent.Should().Contain("Must be a valid url");
        }

        [Fact]
        public void TheLinkIsNotPersistedToTheDatabase()
        {
            _invocation.ScanResponse.Count.Should().Be(0);
        }
    }
}
