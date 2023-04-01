using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Otiva.AppServeces.Service.IdentityService
{
    public interface IClaimAccessor
    {
        Task<IEnumerable<Claim>> GetClaims(CancellationToken cancellation);
    }
}

