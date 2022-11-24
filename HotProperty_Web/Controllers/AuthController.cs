using HotProperty_Utility;
using HotProperty_Web.Models;
using HotProperty_Web.Models.Dto;
using HotProperty_Web.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace HotProperty_Web.Controllers
{
    public class AuthController:Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDTO obj = new();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginRequestDTO obj)
        {
            //use the LoginAsync method from AuthService, send an API request with Data = obj
            //then receive an API response
            APIResponse response = await _authService.LoginAsync<APIResponse>(obj);
            if (response !=null && response.IsSuccess)
            {
                //if ok, convert the response.result to a response DTO
                LoginResponseDTO responseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(Convert.ToString(response.Result));
                
                HttpContext.Session.SetString(SD.SessionToken, responseDTO.Token);
                return RedirectToAction("Index", "Home"); //Home controller, index action
            }
            ModelState.AddModelError("CustomError", response.ErrorMessages.FirstOrDefault());
            return View(obj);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegistrationRequestDTO obj)
        {
            //use the RegisterAsync method from AuthService, send an API request with Data = obj
            //then receive an API response
            APIResponse response = await _authService.RegisterAsync<APIResponse>(obj);
            if (response !=null && response.IsSuccess)
            {
                return RedirectToAction("Login");
            }

            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            HttpContext.Session.SetString(SD.SessionToken, "");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
