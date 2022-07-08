﻿using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Application.ViewModels.UserFriends;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IUserFriendService : IGenericService<SaveUserFriendViewModel, UserFriendViewModel, UserFriend>
    {
        Task<FriendViewModel> AddFriend(AddFriendViewModel addViewModel);
        Task<List<FriendViewModel>> GetAllFriendViewModel();
    }
}
