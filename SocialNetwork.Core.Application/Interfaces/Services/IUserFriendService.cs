using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Application.ViewModels.UserFriends;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserFriendService : IGenericService<SaveUserFriendViewModel, UserFriendViewModel, UserFriend>
    {
        Task<FriendViewModel> AddFriend(AddFriendViewModel addViewModel);
        Task<List<FriendViewModel>> GetAllFriendViewModel();
    }
}