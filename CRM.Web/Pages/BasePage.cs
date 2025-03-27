using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CRM.Web.Pages;

public class AuthenticatedPageModel : PageModel
{
    public override void OnPageHandlerExecuting(Microsoft.AspNetCore.Mvc.Filters.PageHandlerExecutingContext context)
    {
        var token = HttpContext.Session.GetString("JWT");
        if (string.IsNullOrEmpty(token))
        {
            context.Result = new RedirectToPageResult("/Account/Login");
        }
    }
}
