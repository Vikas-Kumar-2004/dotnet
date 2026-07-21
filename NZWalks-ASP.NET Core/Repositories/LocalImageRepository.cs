using NZWalks_ASP.NET_Core.Models.Domain;
using NZWalks_ASP.NET_Core.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace NZWalks_ASP.NET_Core.Repositories
{
    public class LocalImageRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly NZWalksDbContext dbContext;

        public LocalImageRepository(IWebHostEnvironment webHostEnvironment,
            IHttpContextAccessor httpContextAccessor,
            NZWalksDbContext dbContext)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.dbContext = dbContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var imagesFolderPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images");
            if (!Directory.Exists(imagesFolderPath))
            {
                Directory.CreateDirectory(imagesFolderPath);
            }

            var localFilePath = Path.Combine(imagesFolderPath, $"{image.FileName}{image.FileExtension}");

            // Upload Image to Local Path
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);

            // Construct the URL path (e.g., https://localhost:1234/images/image.jpg)
            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";

            image.FilePath = urlFilePath;

            // Add Image to the Database
            await dbContext.Images.AddAsync(image);
            await dbContext.SaveChangesAsync();

            return image;
        }
    }
}
