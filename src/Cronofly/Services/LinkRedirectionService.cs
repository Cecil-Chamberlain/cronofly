using System.Threading.Tasks;
using Data;

namespace Cronofly.Services
{
    public class LinkRedirectionService : ILinkRedirectionService
    {
        private readonly ILinkGetter _linkGetter;

        public LinkRedirectionService(ILinkGetter linkGetter)
        {
            _linkGetter = linkGetter;
        }

        public async Task<string> GetUrl(string shortLinkId)
        {
            var link = await _linkGetter.GetAsync(shortLinkId);
            return link?.RedirectUrl;
        }
    }
}
