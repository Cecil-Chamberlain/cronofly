using System.Threading.Tasks;
using Data.Entities;

namespace Data
{
    public interface ILinkGetter
    {
        Task<Link> GetAsync(string shortLinkId);
    }
}
