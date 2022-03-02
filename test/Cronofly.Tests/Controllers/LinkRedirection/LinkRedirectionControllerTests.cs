using System.Threading.Tasks;
using Cronofly.Controllers.LinkRedirection;
using Cronofly.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Cronofly.Tests.Controllers.LinkRedirection
{
    public class LinkRedirectionControllerTests
    {
        private LinkRedirectionController _controller;
        private ILinkRedirectionService _linkRedirectionService;

        public LinkRedirectionControllerTests()
        {
            _linkRedirectionService = Substitute.For<ILinkRedirectionService>();

            _controller = new LinkRedirectionController(_linkRedirectionService);
        }

        [Fact]
        public async Task WhenTheShortLinkIdIsValid_ThenTheResponseIsRedirectResult()
        {
            const string shortLink = "shortLinkId";
            const string redirectLink = "https://google.com";

            _linkRedirectionService.GetUrl(shortLink)
                .Returns(redirectLink);

            var response = await _controller.RedirectTo(shortLink);

            response.Should().BeOfType<RedirectResult>()
                .Which.Url.Should().Be(redirectLink);
        }

        [Fact]
        public async Task WhenTheShortLinkIdIsNotValid_ThenTheResponseIsNotFound()
        {
            _linkRedirectionService.GetUrl(Arg.Any<string>())
                .Returns((string)null);

            var response = await _controller.RedirectTo("invalidShortLinkId");

            response.Should().BeOfType<NotFoundResult>();
        }
    }
}
