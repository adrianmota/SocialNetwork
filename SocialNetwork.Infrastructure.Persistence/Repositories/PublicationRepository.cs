using Microsoft.EntityFrameworkCore;
using SocialNetwork.Core.Application.Interfaces.Repositories;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Infrastructure.Persistence.Repositories
{
    public class PublicationRepository : GenericRepository<Publication>, IPublicationRepository
    {
        private readonly ApplicationContext _dbContext;

        public PublicationRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task UpdateAsync(Publication publication, int id)
        {
            Publication oldPublication = await _dbContext.Set<Publication>().FindAsync(id);
            publication.Created = oldPublication.Created;
            publication.CreatedBy = oldPublication.CreatedBy;
            await base.UpdateAsync(publication, id);
        }

        public async Task<List<Publication>> GetAllWithIncludeAsync(List<string> properties)
        {
            var query = _dbContext.Set<Publication>().AsQueryable();

            foreach(string property in properties)
            {
                query = query.Include(property);
            }

            return await query.ToListAsync();
        }
    }
}
