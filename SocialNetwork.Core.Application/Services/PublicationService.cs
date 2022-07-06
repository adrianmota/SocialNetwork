using AutoMapper;
using Microsoft.AspNetCore.Http;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Publications;
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
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel user;

        public PublicationService(IPublicationRepository publicationRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(publicationRepository, mapper)
        {
            _publicationRepository = publicationRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SavePublicationViewModel> Add(SavePublicationViewModel saveViewModel)
        {
            saveViewModel.UserId = user.Id;
            saveViewModel.FriendId = user.Id;
            return await base.Add(saveViewModel);
        }

        public async Task<List<PublicationViewModel>> GetAllViewModelFromUser()
        {
            List<Publication> publications = await _publicationRepository.GetAllWithIncludeAsync(new List<string> { "User", "Comments" });
            List<Publication> publicationsFromUser = publications.Where(publication => publication.UserId == user.Id).ToList();
            List<PublicationViewModel> publicationViewModels = _mapper.Map<List<PublicationViewModel>>(publicationsFromUser);

            return publicationViewModels;
        }

        public async Task<List<PublicationViewModel>> GetAllViewModelFromFriends()
        {
            List<Publication> publications = await _publicationRepository.GetAllWithIncludeAsync(new List<string> { "User", "Comments" });
            List<Publication> publicationsFromUser = publications.Where(publication => publication.UserId != user.Id).ToList();
            List<PublicationViewModel> publicationViewModels = _mapper.Map<List<PublicationViewModel>>(publicationsFromUser);

            return publicationViewModels;
        }
    }
}
