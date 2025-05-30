using AccountApi.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AccountApi.Data
{
    public class AppIdentityDbContext(DbContextOptions options) : IdentityDbContext<AppUserEntity>(options)
    {
    }
}
