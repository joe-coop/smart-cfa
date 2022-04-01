using Smart.FA.Catalog.Core.Domain.User.Dto;
using Smart.FA.Catalog.Core.Domain.User.Enumerations;
using Smart.FA.Catalog.Core.Services;

namespace Smart.FA.Catalog.Infrastructure.Services;

public class FakeAccountUserStrategy : IUserStrategy
{
    public Task<UserDto> GetAsync(string userId)
        => userId == "1" ? Task.FromResult(new UserDto(userId, "Victor", "van Duynen", ApplicationType.Account.Name))
            : Task.FromResult(new UserDto(userId, "Maxime", "Poulain", ApplicationType.Account.Name));
}
