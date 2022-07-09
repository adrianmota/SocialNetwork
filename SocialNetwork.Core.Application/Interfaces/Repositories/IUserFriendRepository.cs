using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IUserFriendRepository : IGenericRepository<UserFriend>
    {
        Task<List<UserFriend>> GetAllWithIncludeAsync(List<string> properties);
    }
}
