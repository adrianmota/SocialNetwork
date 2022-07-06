using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Application.ViewModels.Users;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> Login(LoginViewModel loginViewModel)
        {
            string passwordEncrypted = PasswordEncryption.ComputeSha256Hash(loginViewModel.Password);
            List<User> users = await _dbContext.Set<User>().ToListAsync();

            User user = users.Where(user => user.Username == loginViewModel.Username && user.Password == passwordEncrypted)
                             .FirstOrDefault();

            return user;
        }

        public override async Task UpdateAsync(User user, int id)
        {
            User entry = await _dbContext.Set<User>().FindAsync(id);
            user.Created = entry.Created;
            user.CreatedBy = entry.CreatedBy;
            await base.UpdateAsync(user, id);
        }
    }
}
