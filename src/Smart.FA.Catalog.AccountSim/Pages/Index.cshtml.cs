﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Options;
using Smart.FA.Catalog.AccountSimulation.Options;

namespace Smart.FA.Catalog.AccountSimulation.Pages;

public class IndexModel : PageModel
{
    public string HtmlContent { get; set; }

    private readonly ILogger<IndexModel> _logger;
    private readonly HttpContext _context;
    [BindProperty] public string SelectedUser { get; set; }

    public IndexModel(ILoggerFactory loggerFactory
        , IOptions<ProxyPass> proxyPassOptions)
    {
        _logger = loggerFactory.CreateLogger<IndexModel>();
    }

    public async Task OnPostRedirectAsync()
    {
        // _logger.LogInformation("Test");
        HttpContext.ProxyRedirect("/", SelectedUser);
    }
}
