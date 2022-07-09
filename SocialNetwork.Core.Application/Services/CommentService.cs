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
using SocialNetwork.Core.Application.ViewModels.Comments;
using SocialNetwork.Core.Application.ViewModels.Users;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class CommentService : GenericService<SaveCommentViewModel, CommentViewModel, Comment>, ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserViewModel _user;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(commentRepository, mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
        }

        public override async Task<SaveCommentViewModel> Add(SaveCommentViewModel saveViewModel)
        {
            saveViewModel.UserId = _user.Id;
            Comment comment = _mapper.Map<Comment>(saveViewModel);
            SaveCommentViewModel newSaveViewModel = _mapper.Map<SaveCommentViewModel>(await _commentRepository.AddAsync(comment));

            return newSaveViewModel;
        }

        public override async Task<List<CommentViewModel>> GetAllViewModel()
        {
            List<Comment> comments = await _commentRepository.GetAllWithIncludeAsync(new List<string> { "Publication", "User" });
            List<CommentViewModel> commentViewModels = _mapper.Map<List<CommentViewModel>>(comments);

            return commentViewModels;
        }
    }
}
