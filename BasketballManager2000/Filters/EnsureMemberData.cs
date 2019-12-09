using BasketballManager2000.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasketballManager2000
{
    public class EnsureMemberData : IAsyncActionFilter
    {
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _dbContext;
        
        public EnsureMemberData(UserManager<IdentityUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = await _userManager.GetUserAsync(context.HttpContext.User);
            if (user == null)
            {
                // not signed in
                await next();
                return; 
            }

            var matchingMembers = _dbContext.Members.Where(m => m.UserId == user.Id);
            if(!matchingMembers.Any())
            {
                // Create member record
                var member = new Member()
                {
                    UserId = user.Id,
                    Role = MemberRole.TeamMember
                };
                _dbContext.Members.Add(member);
                await _dbContext.SaveChangesAsync();
            }
            
            await next();
        }
    }
}
