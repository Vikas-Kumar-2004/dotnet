using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks_ASP.NET_Core.Models.Domain;
using NZWalks_ASP.NET_Core.Models.DTO;
using NZWalks_ASP.NET_Core.Repositories;
using System.Linq;

namespace NZWalks_ASP.NET_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImagesController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        // POST : /api/Images/Upload
        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                var imageDomainModel = new Image
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDescription = request.FileDescription
                };

                await imageRepository.Upload(imageDomainModel);
                return Ok(imageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ImageUploadRequestDto request)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };

            if (allowedExtensions.Contains(Path.GetExtension(request.File.FileName)) == false)
            {
                ModelState.AddModelError("file", "Unsupported file extensions ");
            }

            if (request.File.Length > 10485760) // 10MB
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }
    }
}