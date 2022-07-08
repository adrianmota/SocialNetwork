using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Comments;
using SocialNetwork.Core.Application.ViewModels.Friends;
using SocialNetwork.Core.Application.ViewModels.Publications;
using SocialNetwork.Core.Application.ViewModels.UserFriends;
using SocialNetwork.Core.Application.ViewModels.Users;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Services
{
    public class PublicationService : GenericService<SavePublicationViewModel, PublicationViewModel, Publication>, IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        private readonly IUserFriendService _userFriendService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _user;

        public PublicationService(IPublicationRepository publicationRepository, IUserService userService, IUserFriendService userFriendService, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(publicationRepository, mapper)
        {
            _publicationRepository = publicationRepository;
            _userFriendService = userFriendService;
            _userService = userService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SavePublicationViewModel> Add(SavePublicationViewModel saveViewModel)
        {
            saveViewModel.UserId = _user.Id;
            saveViewModel.FriendId = _user.Id;
            return await base.Add(saveViewModel);
        }

        public override async Task Update(SavePublicationViewModel saveViewModel, int id)
        {
            saveViewModel.UserId = _user.Id;
            saveViewModel.FriendId = _user.Id;
            await base.Update(saveViewModel, id);
        }

        public async Task<List<PublicationViewModel>> GetAllViewModelFromUser()
        {
            List<Publication> publications = await _publicationRepository.GetAllWithIncludeAsync(new List<string> { "User", "Friend", "Comments" });
            List<UserViewModel> users = await _userService.GetAllViewModel();
            List<Publication> publicationsOfCurrentUser = publications.Where(publication => publication.UserId == _user.Id).ToList();
            List<PublicationViewModel> publicationViewModels = _mapper.Map<List<PublicationViewModel>>(publicationsOfCurrentUser);

            foreach (PublicationViewModel publication in publicationViewModels)
            {
                foreach (CommentViewModel comment in publication.Comments)
                {
                    comment.User = users.Where(user => user.Id == comment.UserId).FirstOrDefault();
                }
            }

            publicationViewModels.Sort((x, y) => DateTime.Compare(y.Created, x.Created));
            return publicationViewModels;
        }

        public async Task<List<PublicationViewModel>> GetAllViewModelFromFriends()
        {
            List<Publication> publications = await _publicationRepository.GetAllWithIncludeAsync(new List<string> { "User", "Friend", "Comments" });
            List<Publication> publicationsOfOtherUsers = publications.Where(publication => publication.UserId != _user.Id).ToList();
            _user.UserFriends = await _userFriendService.GetAllViewModel();
            List<UserFriendViewModel> currentUserFriends = _user.UserFriends.Where(uf => uf.UserId == _user.Id).ToList();

            List<Publication> publicationsOfFriends = new();

            foreach (UserFriendViewModel uf in currentUserFriends)
            {
                foreach (Publication publicationOfOtherUser in publicationsOfOtherUsers)
                {
                    if (uf.FriendId == publicationOfOtherUser.FriendId)
                    {
                        publicationsOfFriends.Add(publicationOfOtherUser);
                    }
                }
            }

            List<PublicationViewModel> publicationViewModels = _mapper.Map<List<PublicationViewModel>>(publicationsOfFriends);

            //Including the users related to the comments
            List<UserViewModel> users = await _userService.GetAllViewModel();
            foreach (PublicationViewModel publication in publicationViewModels)
            {
                foreach (CommentViewModel comment in publication.Comments)
                {
                    comment.User = users.Where(user => user.Id == comment.UserId).FirstOrDefault();
                }
            }

            publicationViewModels.Sort((x, y) => DateTime.Compare(y.Created, x.Created));
            return publicationViewModels;
        }
    }
}
