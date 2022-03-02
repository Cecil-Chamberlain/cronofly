using System.Threading.Tasks;
using Data;
using Data.Entities;

namespace Cronofly.Services
{
    public class LinkShorteningService : ILinkShorteningService
    {
        private readonly ILinkSaver _linkSaver;

        public LinkShorteningService(ILinkSaver linkSaver)
        {
            _linkSaver = linkSaver;
        }

        public async Task<string> GetShortenedLink(string longUrl)
        {
            var link = Link.Create(longUrl);

            await _linkSaver.SaveAsync(link);

            return link.ShortLinkId;
        }
    }
}
