using NZWalks_ASP.NET_Core.Models.Domain;

namespace NZWalks_ASP.NET_Core.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllAsync(); // provide all list region then used by controller we expose interface to application not implentation

    }
}
