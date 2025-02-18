namespace pd311_mvc_aspnet.Services.Image
{
    public class ImageService(IWebHostEnvironment environment) 
        : IImageService
    {
        public bool DeleteFile(string path)
        {
            var rootPath = environment.WebRootPath;
            var filePath = Path.Combine(rootPath, path);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }

            return false;
        }

        public async Task<string?> SaveImageAsync(IFormFile file, string path)
        {
            var types = file.ContentType.Split("/");
            if (types[0] != "image")
            {
                return null;
            }

            string fileName = $"{Guid.NewGuid()}.{types[1]}";
            string rootPath = environment.WebRootPath;
            string filePath = Path.Combine(rootPath, path, fileName);
            using (var stream = file.OpenReadStream())
            {
                using var fileStream = File.Create(filePath);
                await stream.CopyToAsync(fileStream);
            }

            return fileName;
        }
    }
}
