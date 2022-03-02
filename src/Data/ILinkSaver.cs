using System.Threading.Tasks;
using Data.Entities;

namespace Data
{
    public interface ILinkSaver
    {
        Task SaveAsync(Link link);
    }
}
