using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Core.Application.ViewModels.Friends;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IFriendService : IGenericService<SaveFriendViewModel, FriendViewModel, Friend> { }
}