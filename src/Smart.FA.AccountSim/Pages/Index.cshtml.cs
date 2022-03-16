using AccountSimulation.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;

namespace AccountSimulation.Pages;

public class IndexModel : PageModel
{
    public string HtmlContent { get; set; }

    private readonly ILogger<IndexModel> _logger;
    private readonly HttpContext _context;
   public string SelectedUser { get; set; }

    public IndexModel(ILogger<IndexModel> logger
        , IOptions<ProxyPass> proxyPassOptions)
    {

    }

    public async Task OnPostRedirectAsync(string userId)
    { 
        HttpContext.ProxyRedirect("/", userId);
    }
}
