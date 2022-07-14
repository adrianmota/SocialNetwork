using System.Threading.Tasks;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Core.Application.ViewModels.Users;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User> Login(LoginViewModel loginViewModel);
    }
}