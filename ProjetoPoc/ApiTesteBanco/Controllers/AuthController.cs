using ApiTesteBanco.Dto;
using ApiTesteBanco.Service;
using ApiTesteBanco.Service.Inerfaces;
using ApiTesteBanco.Settings;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace ApiTesteBanco.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public AuthController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] Login request)
        {
            // Validar as credenciais (essa é uma simulação, você precisa substituir por sua lógica)
            var token = await _loginService.LogarApiAsync(request);
            if (!string.IsNullOrEmpty(token))
                return Ok(new { Token = token });

            return Unauthorized("Credenciais inválidas");
        }

    }
}
