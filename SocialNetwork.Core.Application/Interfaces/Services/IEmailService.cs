using System.Threading.Tasks;
using SocialNetwork.Core.Application.Dtos;

namespace SocialNetwork.Core.Application.Interfaces.Services
{
    public interface IEmailService
    {
        Task SendAsync(EmailRequest request);
    }
}
