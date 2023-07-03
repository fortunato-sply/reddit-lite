public interface IImageService
{
  Task AddImage(IFormFile file);
  Task<int> GetLastImageId();
}
