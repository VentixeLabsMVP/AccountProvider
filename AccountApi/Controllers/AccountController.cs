using AccountApi.Models;
using AccountApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AccountService accountService) : ControllerBase
    {
        private readonly AccountService _accountService = accountService;

        [HttpPost("signup")]
        public async Task <ActionResult> SignUp(SignUpForm form)
        {
            var result = await _accountService.RegisterAsync(form.Email, form.Password);
            if (result.Succeeded)
                return Ok("Användare skapad");

            return BadRequest(result.Errors);
        }
    }
}
