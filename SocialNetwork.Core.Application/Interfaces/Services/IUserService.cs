using System.Threading.Tasks;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Core.Application.ViewModels.Users;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel, User>
    {
        Task<UserViewModel> Login(LoginViewModel login);
        Task Activate(int id);
        Task<UserViewModel> ResetPassword(ResetUserPasswordViewModel resetPasswordViewModel);
    }
}
