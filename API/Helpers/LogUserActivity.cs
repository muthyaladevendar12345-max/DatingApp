using System;
using API.Data;
using API.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers;

public class LogUserActivity : IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
       var resultContent= await next();
       if(context.HttpContext.User.Identity?.IsAuthenticated!=true) return;

       var memberId=resultContent.HttpContext.User.GetMemberId();

       var dbContext=resultContent.HttpContext.RequestServices.GetRequiredService<AppDbContext>();

       await dbContext.Members.Where(x=>x.Id==memberId)
       .ExecuteUpdateAsync(setter=>setter.SetProperty(x=>x.LastActive,DateTime.UtcNow));
    }
}
