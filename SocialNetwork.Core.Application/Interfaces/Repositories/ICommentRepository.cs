using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<List<Comment>> GetAllWithIncludeAsync(List<string> properties);
    }
}
