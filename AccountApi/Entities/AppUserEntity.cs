using Microsoft.AspNetCore.Identity;

namespace AccountApi.Entities;


// inherits all I need from identityUser. im setting email and password in service
public class AppUserEntity : IdentityUser
{

}
