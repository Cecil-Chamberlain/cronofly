using System.Threading.Tasks;
using Cronofly.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cronofly.Controllers.LinkRedirection
{
    [Route("l")]
    public class LinkRedirectionController : Controller
    {
        private readonly ILinkRedirectionService _linkRedirectionService;

        public LinkRedirectionController(ILinkRedirectionService linkRedirectionService)
        {
            _linkRedirectionService = linkRedirectionService;
        }

        [Route("{shortLinkId}")]
        public async Task<IActionResult> RedirectTo(string shortLinkId)
        {
            var redirectUrl = await _linkRedirectionService.GetUrl(shortLinkId);
            return redirectUrl != null ?
                Redirect(redirectUrl) :
                NotFound();
        }
    }
}
