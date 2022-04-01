using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Smart.FA.Catalog.AccountSimulation.Pages;

public class cfa : PageModel
{
    public void OnGet(int? id)
    {
        Response.Cookies.Append("user-id", id.ToString());
        HttpContext.ProxyRedirect("/", id.ToString());
    }

}
