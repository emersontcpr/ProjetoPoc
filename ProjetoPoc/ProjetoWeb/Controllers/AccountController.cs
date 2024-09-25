
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using ProjetoWeb.ConsultaApi;
using ProjetoWeb.Models;
using ProjetoWeb.Models.Json;
using System.Diagnostics;

namespace PocClientes.Application.Controllers
{

    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IHttpClientFactory _clientFactory;
        private readonly WebApiPoc _apiClient;
        public AccountController(ILogger<AccountController> logger, WebApiPoc apiClient)
        {
            _logger = logger;
            _apiClient = apiClient;
        }

        [HttpGet("login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var response = await _apiClient.PostAsync<LoginModel, TokenResponse>("api/Auth/login", model);

 
            if (response.IsSuccessStatusCode)
            {
              
                    // Armazenar o token em um cookie (ou outro local, como localStorage)
                    HttpContext.Response.Cookies.Append("AuthToken", response.Data.Token, new CookieOptions
                    {
                        HttpOnly = true,
                        Secure = true,
                        SameSite = SameSiteMode.Strict, // Para evitar CSRF
                        Expires = DateTime.UtcNow.AddMinutes(30)
                    });

                    return RedirectToAction("Index", "Cliente");
             
            }
            ViewBag.ErrorMessage = response.ErrorMessage;
            return View(model);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
          Response.Cookies.Delete("AuthToken");

            return RedirectToAction("Login", "Account");
        }
    }


   
}
