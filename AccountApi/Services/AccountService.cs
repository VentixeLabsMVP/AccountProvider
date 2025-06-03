using AccountApi.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace AccountApi.Services;

public class AccountService
{
    private readonly UserManager<AppUserEntity> _userManager;

    public AccountService(UserManager<AppUserEntity> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IdentityResult> RegisterAsync(string email, string password)
    {
        var user = new AppUserEntity { UserName = email, Email = email, EmailConfirmed = false };
        return await _userManager.CreateAsync(user, password);
    }

    public async Task<bool> VerifyAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null || !user.EmailConfirmed)
            return false;

        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<bool> ConfirmCodeAsync(string email, string code)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return false;

        if (user.SecurityStamp != code)
            return false;

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);
        return true;
    }

    public async Task<bool> ConfirmEmailAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return false;

        user.EmailConfirmed = true;
        await _userManager.UpdateAsync(user);
        return true;
    }

}
