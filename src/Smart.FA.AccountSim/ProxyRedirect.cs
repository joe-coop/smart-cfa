using System.Web;

namespace AccountSimulation;

public static class ProxyExtensions
{
    public static void ProxyRedirect(this HttpContext context, string page, string userId)
    {
        context.Response.Headers.Add("User_Id", userId);
        context.Response.Headers.Add("X-Accel-Redirect", "@myinternalapplication");
        context.Response.Headers.Add("X-Real-Location", page);
    }
}