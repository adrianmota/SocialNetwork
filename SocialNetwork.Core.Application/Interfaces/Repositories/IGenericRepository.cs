using System.Collections.Generic;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity>
        where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);
        Task UpdateAsync(Entity entity, int id);
        Task DeleteAsync(int id);
        Task DeleteAsync(params int[] id);
        Task<List<Entity>> GetAllAsync();
        Task<Entity> GetByIdAsync(int id);
    }
}