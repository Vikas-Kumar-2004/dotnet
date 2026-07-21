using NZWalks_ASP.NET_Core.Models.Domain;

namespace NZWalks_ASP.NET_Core.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
