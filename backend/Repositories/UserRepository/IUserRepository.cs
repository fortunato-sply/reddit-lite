using System.Linq.Expressions;
using backend.Model;
using backend.Repositories;

public interface IUserRepository : IRepository<DataUser>
{
  Task<DataUser?> GetUserByUsername(string username);
}