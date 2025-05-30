using Microsoft.AspNetCore.Identity;

namespace AccountApi.Services;

public class AccountService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountService(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        var user = new IdentityUser { UserName = email, Email = email };
        return await _userManager.CreateAsync(user, password);
    }
}
