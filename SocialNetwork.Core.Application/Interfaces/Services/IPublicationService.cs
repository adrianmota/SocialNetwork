using System.Collections.Generic;
using System.Threading.Tasks;
using SocialNetwork.Core.Domain.Entities;
using SocialNetwork.Core.Application.ViewModels.Publications;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IPublicationService : IGenericService<SavePublicationViewModel, PublicationViewModel, Publication>
    {
        Task<List<PublicationViewModel>> GetAllViewModelFromUser();
        Task<List<PublicationViewModel>> GetAllViewModelFromFriends();
    }
}
