public interface IImageService
{
  Task AddImage(IFormFile file, string name);
  Task<int> GetLastImageId();
}
