using AutoMapper;
using ClassLibrary.Helpers.Hubs;
using ClassLibrary.Helpers.Utils;
using ClassLibrary.Models.DTOs.UserDTO;
using ClassLibrary.Services.ServerService;
using ClassLibrary.Services.UserService;
using ClassLibrary.Models;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;

        public JwtMiddleware(RequestDelegate nextRequestDelegate)
        {
            _nextRequestDelegate = nextRequestDelegate;
        }

        public async Task Invoke(HttpContext httpContext, IUserService userService, IServerService serverService, IJwtUtils jwtUtils)
        {

            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var userId = jwtUtils.ValidateUserJwtToken(token);

            if (userId != Guid.Empty)
            {
                httpContext.Items["User"] = await userService.GetUserByIdAsync(userId);
            }

            token = httpContext.Request.Headers["Server"].FirstOrDefault()?.Split(" ").Last();

            var serverId = jwtUtils.ValidateServerJwtToken(token);

            if (serverId != Guid.Empty)
            {
                httpContext.Items["Server"] = await serverService.GetServerByIdAsync(serverId);
            }

            await _nextRequestDelegate(httpContext);
        }
    }
}
