using System.Threading.Tasks;
using Cronofly.Controllers.LinkShortening;
using Cronofly.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Cronofly.Tests.Controllers.LinkShortening
{
    public class LinkShorteningControllerTests
    {
        private LinkShorteningController _controller;
        private ILinkShorteningService _linkShorteningService;

        public LinkShorteningControllerTests()
        {
            _linkShorteningService = Substitute.For<ILinkShorteningService>();
            _controller = new LinkShorteningController(_linkShorteningService);
        }

        [Fact]
        public void WhenRequestingTheIndexPage_ThenTheResponseIsViewResult()
        {
            var response = _controller.Index();

            response.Should().BeOfType<ViewResult>();
        }

        [Fact]
        public async Task WhenMakingAValidPostRequest_ThenTheResponseIsViewResult()
        {
            var longUrl = "https://validurl.com";
            var shortUrlId = "blah123";

            _linkShorteningService
                .GetShortenedLink(longUrl)
                .Returns(shortUrlId);

            var response = await _controller.ShortenLink(
                new LinkShorteningResource
                {
                    UrlToShorten = longUrl
                });

            response.Should().BeOfType<ViewResult>();

            await _linkShorteningService
                .Received(1)
                .GetShortenedLink(longUrl);
        }
    }
}
