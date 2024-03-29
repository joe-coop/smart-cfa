using Smart.FA.Catalog.Core.Domain;
using Smart.FA.Catalog.Core.Domain.Models;
using Smart.FA.Catalog.Core.Services;

namespace Smart.FA.Catalog.Web.Identity;

/// <inheritdoc />
public class UserIdentity : IUserIdentity
{
    /// <inheritdoc />
    public int Id => Identity.Id;

    /// <inheritdoc />
    public CustomIdentity Identity { get; init; }

    /// <inheritdoc />
    public Trainer CurrentTrainer { get; init; }

    public UserIdentity(IHttpContextAccessor httpContextAccessor)
    {
        var identity = httpContextAccessor.HttpContext?.User.Identity as CustomIdentity ??
                       throw new InvalidOperationException("Cannot retrieve identity of current user.");

        Identity       = identity;
        CurrentTrainer = identity.Trainer;
    }
}
