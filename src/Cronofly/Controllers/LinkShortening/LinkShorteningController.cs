using System;
using System.Threading.Tasks;
using Cronofly.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cronofly.Controllers.LinkShortening
{
    [Route("")]
    public class LinkShorteningController : Controller
    {
        private readonly ILinkShorteningService _linkShorteningService;
        private readonly string _redirectPath;

        public LinkShorteningController(ILinkShorteningService linkShorteningService)
        {
            _linkShorteningService = linkShorteningService;
            _redirectPath = "/l/";
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View("~/Controllers/LinkShortening/LinkShortening.cshtml");
        }

        [HttpPost]
        public async Task<IActionResult> ShortenLink(LinkShorteningResource resource)
        {
            if (!ModelState.IsValid)
                return View("~/Controllers/LinkShortening/LinkShortening.cshtml", resource);

            var shortenedUrl = await _linkShorteningService.GetShortenedLink(resource.UrlToShorten);

            return View("~/Controllers/LinkShortening/SuccessResult.cshtml", new SuccessResource(_redirectPath + shortenedUrl));
        }
    }
}
