using backend;
using backend.Model;
using Microsoft.EntityFrameworkCore;

public class ImageService : IImageService
{
  private RedditliteContext context;
  public ImageService(RedditliteContext context)
    => this.context = context;
    
  public async Task AddImage(IFormFile file)
  {
    if (file != null && file.Length > 0)
    {
      using MemoryStream ms = new MemoryStream();

      await file.CopyToAsync(ms);
      var data = ms.GetBuffer();
      ImageDatum img = new ImageDatum { Photo = data };

      await context.ImageData.AddAsync(img);
      await context.SaveChangesAsync();

      var lastImgId = await GetLastImageId();

      var location = new Location
      {
        Nome = file.Name,
        Photo = lastImgId
      };

      await context.Locations.AddAsync(location);
      await context.SaveChangesAsync();
    }
  }

  public async Task<int> GetLastImageId()
  {
    var img = await context.ImageData.LastOrDefaultAsync();
    if(img != null)
      return img.Id;
    
    return 0;
  }
}
