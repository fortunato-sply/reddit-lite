using System.Linq.Expressions;
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
    var img = await context.ImageData.OrderByDescending(i => i.Id).FirstOrDefaultAsync();
    if(img != null)
      return img.Id;
    
    return 0;
  }

  public async Task<List<ImageDatum>> Filter(Expression<Func<ImageDatum, bool>> exp)
  {
    var result = context.ImageData.Where(exp);
    return await result.ToListAsync();
  }
}
