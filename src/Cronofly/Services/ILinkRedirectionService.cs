using System.Threading.Tasks;

namespace Cronofly.Services
{
    public interface ILinkRedirectionService
    {
        Task<string> GetUrl(string shortLinkId);
    }
}
