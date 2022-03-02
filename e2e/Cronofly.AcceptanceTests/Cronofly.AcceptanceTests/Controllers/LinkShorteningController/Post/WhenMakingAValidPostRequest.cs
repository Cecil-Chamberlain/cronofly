using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.Model;
using FluentAssertions;
using Xunit;

namespace Cronofly.AcceptanceTests.Controllers.LinkShorteningController.Post
{
    [Collection(nameof(Fixture))]
    public class WhenMakingAValidPostRequest : IClassFixture<WhenMakingAValidPostRequest.Invocation>
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
                var request = new HttpRequestMessage(HttpMethod.Post, "");
                request.Content = new StringContent($"UrlToShorten=https://google.com", Encoding.UTF8, "application/x-www-form-urlencoded");

                Response = await _fixture.HttpClient.SendAsync(request);

                ScanResponse = await _fixture.DynamoDB.ScanAsync(new ScanRequest
                {
                    TableName = "cronofly-table"
                });
            }

            public async Task DisposeAsync()
            {
                var tasks = ScanResponse.Items.Select(i =>
                    _fixture.DynamoDB.DeleteItemAsync("cronofly-table",
                        new Dictionary<string, AttributeValue>
                        {
                            ["ShortLinkId"] = i["ShortLinkId"]
                        }));

                await Task.WhenAll(tasks);
            }
        }

        private Invocation _invocation;

        public WhenMakingAValidPostRequest(Invocation invocation)
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
            responseContent.Should().Contain("Link:");
        }

        [Fact]
        public void TheLinkIsPersistedToTheDatabase()
        {
            _invocation.ScanResponse.Items[0]
                .Should().ContainKey("RedirectUrl")
                .WhoseValue.S.Should().Be("https://google.com");
        }
    }
}
