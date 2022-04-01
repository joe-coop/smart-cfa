﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
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

    public string FullPath { get; set; }
    public IndexModel(ILoggerFactory loggerFactory
        , IOptions<ProxyPass> proxyPassOptions)
    {
        _logger = loggerFactory.CreateLogger<IndexModel>();
    }

    // public void OnGet(int? id)
    // {
    //     if (id is not null)
    //     {
    //         HttpContext.ProxyRedirect( SelectedUser);
    //     }
    // }

    public ActionResult OnPostRedirect(string url)
    {
        return RedirectToPage("/cfa", new {id = Int32.Parse(SelectedUser)});
    }
}
