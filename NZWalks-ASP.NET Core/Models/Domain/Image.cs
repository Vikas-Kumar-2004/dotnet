using System.ComponentModel.DataAnnotations.Schema;

namespace NZWalks_ASP.NET_Core.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        // Used only to receive the uploaded file.
        // EF Core will not create a column for this property.
        [NotMapped]
        public IFormFile? File { get; set; }

        public string FileName { get; set; } = string.Empty;

        public string? FileDescription { get; set; }

        public string FileExtension { get; set; } = string.Empty;

        public long FileSizeInBytes { get; set; }

        // Stores the URL or local path of the uploaded file.
        public string FilePath { get; set; } = string.Empty;
    }
}

/*
 


* `IFormFile` is used only to receive the uploaded file from the HTTP request.
* It is **not stored in the database**.
* After you save the file (to disk, Azure Blob Storage, Cloudinary, etc.), you store its metadata in the other properties, such as `FileName`, `FilePath`, `FileExtension`, and `FileSizeInBytes`.

For example:

1. Client uploads `image.jpg`.
2. `IFormFile File` receives the uploaded file.
3. Your API saves the file to:

```
wwwroot/images/image.jpg
```

4. Then you save an `Image` entity like:

| Id   | FileName  | FilePath          | FileExtension | FileSizeInBytes |
| ---- | --------- | ----------------- | ------------- | --------------- |
| Guid | image.jpg | /images/image.jpg | .jpg          | 152348          |

Notice that **`File` is not stored**. Only the metadata is.

 
 
 
 
 */