using AccountApi.Models;
using AccountApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AccountApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController(AccountService accountService, IHttpClientFactory httpClientFactory) : ControllerBase
    {
        private readonly HttpClient _client = httpClientFactory.CreateClient("VerificationApi");

        private readonly AccountService _accountService = accountService;

        [HttpPost("signup")]
        public async Task <ActionResult> SignUp(SignUpForm form)
        {
            var result = await _accountService.RegisterAsync(form.Email, form.Password);
            if (result.Succeeded)
            {
                await _client.PostAsJsonAsync("/api/verification/send", new { Email = form.Email });
                return Ok("User was created");
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify([FromBody] LoginForm form)
        {
            var isValid = await _accountService.VerifyAsync(form.Email, form.Password);

            if (!isValid)
                return Unauthorized();

            return Ok();
        }

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmEmail([FromBody] VerifyVerificationCodeRequest request)
        {
            var response = await _client.PostAsJsonAsync("/api/verification/verify", request);

            if (!response.IsSuccessStatusCode)
                return BadRequest(new { Error = "Invalid or expired verification code." });

            var updated = await _accountService.ConfirmEmailAsync(request.Email);
            if (!updated)
                return StatusCode(500, new { Error = "Could not update email confirmation." });

            return Ok(new { Message = "Email confirmed!" });
        }
    }
}
