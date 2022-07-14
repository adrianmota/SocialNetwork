using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.Core.Domain.Entities;

namespace SocialNetwork.Core.Application.Interfaces.Repositories
{
    public interface IPublicationRepository : IGenericRepository<Publication>
    {
        Task<List<Publication>> GetAllWithIncludeAsync(List<string> properties);
    }
}