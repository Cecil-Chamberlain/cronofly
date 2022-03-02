using System.Threading.Tasks;

namespace Cronofly.Services
{
    public interface ILinkShorteningService
    {
        Task<string> GetShortenedLink(string longUrl);
    }
}
