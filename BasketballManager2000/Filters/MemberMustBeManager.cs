using BasketballManager2000.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace BasketballManager2000 
{
    public class MemberMustBeManager : ActionFilterAttribute, IAsyncActionFilter
    {
        private UserManager<IdentityUser> _userManager;
        private ApplicationDbContext _dbContext;

        public MemberMustBeManager(UserManager<IdentityUser> userManager, ApplicationDbContext dbContext)
        {
            _userManager = userManager;
            _dbContext = dbContext;
        }


        public async override Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var user = await _userManager.GetUserAsync(context.HttpContext.User);
            if (user == null)
            {
                // not signed in
                await next();
                return;
            }

            var member = await _dbContext.Members.FirstOrDefaultAsync(m => m.UserId == user.Id);
            if (member == null || member.Role != MemberRole.TeamManager)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            await next();
        }
    }
}
