using SocialNetwork.Core.Application.ViewModels.Publications;
using SocialNetwork.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IPublicationService : IGenericService<SavePublicationViewModel, PublicationViewModel, Publication>
    {
        Task<List<PublicationViewModel>> GetAllViewModelFromUser();
        Task<List<PublicationViewModel>> GetAllViewModelFromFriends();
    }
}
