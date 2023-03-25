using FirstLab.Data.Models;
using SavePets.Data.Entities.Abstract;

namespace SavePets.Data.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetByLogin(string login);
}