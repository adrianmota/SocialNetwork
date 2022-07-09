using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Application.ViewModels.UserFriends;
using SocialNetwork.Core.Application.ViewModels.Users;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class UserFriendService : GenericService<SaveUserFriendViewModel, UserFriendViewModel, UserFriend>, IUserFriendService
    {
        private readonly IFriendRepository _friendRepository;
        private readonly IUserFriendRepository _userFriendRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _user;
        private readonly IMapper _mapper;

        public UserFriendService(IUserFriendRepository userFriendRepository, IFriendRepository friendRepository, IHttpContextAccessor httpContextAccessor, IMapper mapper) : base(userFriendRepository, mapper)
        {
            _userFriendRepository = userFriendRepository;
            _friendRepository = friendRepository;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            _mapper = mapper;
        }

        public async Task<List<FriendViewModel>> GetAllFriendViewModel()
        {
            List<Friend> friends = new();
            List<UserFriend> userFriends = await _userFriendRepository.GetAllWithIncludeAsync(new List<string> { "User", "Friend" });
            List<UserFriend> friendsFromCurrentUser = userFriends.Where(uf => uf.UserId == _user.Id).ToList();

            foreach(UserFriend uf in friendsFromCurrentUser)
            {
                friends.Add(uf.Friend);
            }

            return _mapper.Map<List<FriendViewModel>>(friends);
        }

        public async Task<FriendViewModel> AddFriend(AddFriendViewModel addViewModel)
        {
            List<Friend> friends = await _friendRepository.GetAllAsync();
            Friend friend = friends.Where(friend => friend.Username == addViewModel.Username).FirstOrDefault();

            if (friend == null)
            {
                return null;
            }

            SaveUserFriendViewModel saveViewModel = new();
            saveViewModel.UserId = _user.Id;
            saveViewModel.FriendId = friend.Id;
            await base.Add(saveViewModel);

            saveViewModel.UserId = friend.Id;
            saveViewModel.FriendId = _user.Id;
            await base.Add(saveViewModel);

            FriendViewModel friendViewModel = _mapper.Map<FriendViewModel>(friend);
            return friendViewModel;
        }

        public override async Task Delete(int id)
        {
            int friendId = id;
            int userId = _user.Id;

            List<UserFriend> userFriends = await _userFriendRepository.GetAllAsync();
            List<UserFriend> relationships = userFriends
                .Where(uf => uf.FriendId == friendId && uf.UserId == userId || uf.FriendId == userId && uf.UserId == friendId)
                .ToList();

            foreach(UserFriend relationship in relationships)
            {
                await base.Delete(relationship.UserId, relationship.FriendId);
            }
        }
    }
}
