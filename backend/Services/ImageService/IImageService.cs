using System.Linq.Expressions;
using backend.Model;
using Microsoft.EntityFrameworkCore;

public interface IImageService
{
  Task AddImage(IFormFile file);
  Task<int> GetLastImageId();
  Task<List<ImageDatum>> Filter(Expression<Func<ImageDatum, bool>> exp);
}
