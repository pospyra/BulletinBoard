﻿
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Otiva.AppServeces.Service.IdentityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AdBoard.Infrastructure.Identity
{
    public class HttpContextClaimsAccessor : IClaimAccessor
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public HttpContextClaimsAccessor(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public async Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellation)
        {
            if (cancellation.IsCancellationRequested)
                throw new OperationCanceledException();

            return _contextAccessor.HttpContext.User.Claims;
        }
    }
}
