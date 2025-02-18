namespace pd311_mvc_aspnet.Services.Image
{
    public interface IImageService
    {
        Task<string?> SaveImageAsync(IFormFile file, string path);
        bool DeleteFile(string path);
    }
}
