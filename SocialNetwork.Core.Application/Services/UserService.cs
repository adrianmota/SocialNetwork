using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.ViewModels.Users;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Services
{
    public class UserService : GenericService<SaveUserViewModel, UserViewModel, User>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IFriendRepository _friendRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper, IFriendRepository friendRepository) : base(userRepository, mapper)
        {
            _userRepository = userRepository;
            _friendRepository = friendRepository;
            _mapper = mapper;
        }

        public override async Task<SaveUserViewModel> Add(SaveUserViewModel saveViewModel)
        {
            List<User> users = await _userRepository.GetAllAsync();

            User user = users.Where(user => user.Username == saveViewModel.Username).FirstOrDefault();

            if (user != null)
            {
                return null;
            }

            string passwordEncrypted = PasswordEncryption.ComputeSha256Hash(saveViewModel.Password);
            saveViewModel.Password = passwordEncrypted;
            saveViewModel.Active = false;

            SaveUserViewModel newSaveViewModel = await base.Add(saveViewModel);
            User newUser = _mapper.Map<User>(newSaveViewModel);
            Friend friend = _mapper.Map<Friend>(newUser);
            await _friendRepository.AddAsync(friend);

            return newSaveViewModel;
        }

        public async Task<UserViewModel> Login(LoginViewModel login)
        {
            User user = await _userRepository.Login(login);

            if (user == null)
            {
                return null;
            }

            UserViewModel viewModel = _mapper.Map<UserViewModel>(user);
            return viewModel;
        }

        public async Task<UserViewModel> ResetPassword(ResetUserPasswordViewModel resetPasswordViewModel)
        {
            List<User> users = await _userRepository.GetAllAsync();
            User user = users.Where(user => user.Username == resetPasswordViewModel.Username).FirstOrDefault();

            if (user == null)
            {
                return null;
            }

            StringBuilder passwordStringBuilder = new();
            int length = 16;
            Random random = new Random();

            while (passwordStringBuilder.Length < length)
            {
                char c = (char)random.Next(32, 126);
                passwordStringBuilder.Append(c);
            }

            string password = passwordStringBuilder.ToString();
            user.Password = PasswordEncryption.ComputeSha256Hash(password);
            await _userRepository.UpdateAsync(user, user.Id);

            UserViewModel viewModel = _mapper.Map<UserViewModel>(user);
            viewModel.Password = password;
            return viewModel;
        }

        public async Task Activate(int id)
        {
            User user = await _userRepository.GetByIdAsync(id);
            user.Active = true;
            await _userRepository.UpdateAsync(user, id);
        }
    }
}