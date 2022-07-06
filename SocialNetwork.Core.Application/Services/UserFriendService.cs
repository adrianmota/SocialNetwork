using AutoMapper;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.UserFriends;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class UserFriendService : GenericService<SaveUserFriendViewModel, UserFriendViewModel, UserFriend>, IUserFriendService
    {
        private readonly IUserFriendRepository _userFriendRepository;
        private readonly IMapper _mapper;

        public UserFriendService(IUserFriendRepository userFriendRepository, IMapper mapper) : base(userFriendRepository, mapper)
        {
            _userFriendRepository = userFriendRepository;
            _mapper = mapper;
        }
    }
}
