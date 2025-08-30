using System.Security.Claims;
using InsuranceApp.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace InsuranceApp.Infrastructure.Services;

public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    public Guid? UserId
    {
        get
        {
            var claim = httpContextAccessor.HttpContext?.User?.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (claim == null)
                return null;

            return Guid.TryParse(claim.Value, out var id) ? id : null;
        }
    }
}