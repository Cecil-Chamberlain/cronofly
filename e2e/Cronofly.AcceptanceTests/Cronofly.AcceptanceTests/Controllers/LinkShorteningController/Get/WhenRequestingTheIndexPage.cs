using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Cronofly.AcceptanceTests.Controllers.LinkShorteningController.Get
{
    [Collection(nameof(Fixture))]
    public class WhenRequestingTheIndexPage : IClassFixture<WhenRequestingTheIndexPage.Invocation>
    {
        public class Invocation : IAsyncLifetime
        {
            private readonly Fixture _fixture;
            public HttpResponseMessage Response;

            public Invocation(Fixture fixture)
            {
                _fixture = fixture;
            }

            public async Task InitializeAsync()
            {
                var request = new HttpRequestMessage(HttpMethod.Get, "");
                Response = await _fixture.HttpClient.SendAsync(request);
            }

            public Task DisposeAsync() => Task.CompletedTask;
        }

        private Invocation _invocation;

        public WhenRequestingTheIndexPage(Invocation invocation)
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
            responseContent.Should().Contain("Link Shortening");
        }
    }
}
