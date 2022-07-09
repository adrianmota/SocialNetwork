using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Domain.Settings;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Infrastructure.Shared.Services;

namespace SocialNetwork.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedServices(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<MailSettings>(config.GetSection("MailSettings"));
            services.AddTransient<IEmailService, EmailService>();
        }
    }
}
