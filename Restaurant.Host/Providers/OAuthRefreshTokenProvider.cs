using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security.Infrastructure;
using Swaksoft.Infrastructure.Crosscutting.Authorization.Token;

namespace Restaurant.Host.Providers
{
    public class OAuthRefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly IDependencyResolver _dependencyResolver;

        public OAuthRefreshTokenProvider(IDependencyResolver dependencyResolver)
        {
            _dependencyResolver = dependencyResolver;
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            using (var authenticationTokenService = _dependencyResolver.GetService<IAuthenticationTokenFactory>())
            {
                var refreshToken = await authenticationTokenService.CreateRefreshTokenAsync(
                    context.Ticket, context.SerializeTicket());

                if (!string.IsNullOrWhiteSpace(refreshToken))
                {
                    context.SetToken(refreshToken);
                }
            }
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            //var hashedTokenId = CryptoAes.GetHash(context.Token);
            using (var authenticationTokenService = _dependencyResolver.GetService<IAuthenticationTokenFactory>())
            {
                var ticket = await authenticationTokenService.IssueAuthenticationTicketAsync(context.Token);
                if (ticket != null)
                {
                    context.SetTicket(ticket);
                }
            }
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new NotImplementedException();
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new NotImplementedException();
        }
    }
}