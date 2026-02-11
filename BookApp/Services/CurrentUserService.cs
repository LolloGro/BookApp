using System.Security.Claims;

namespace BookApp.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _accessor;

    public CurrentUserService(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }
    public int UserId
    {
        get
        {
           var claim = _accessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);

           if (claim == null)
           {
               throw new UnauthorizedAccessException("User is not authenticated");
           }
           
           return int.Parse(claim.Value);
        }
    }
   
    public bool IsAuthenticated => _accessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
    
}