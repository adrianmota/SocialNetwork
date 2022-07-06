﻿using Microsoft.Extensions.DependencyInjection;
using SocialNetwork.Core.Application.Helpers;
using SocialNetwork.Core.Application.Interfaces.Services;
using SocialNetwork.Core.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region AutoMapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPublicationService, PublicationService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IFriendService, FriendService>();
            services.AddTransient<IUserFriendService, UserFriendService>();
            services.AddTransient<ImageHelper, ImageHelper>();
            #endregion
        }
    }
}
