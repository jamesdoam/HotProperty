﻿using HotProperty_PropertyAPI.Models;
using HotProperty_PropertyAPI.Models.Dto;
using HotProperty_PropertyAPI.Repository.IRepository;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;
using System.Net;

namespace HotProperty_PropertyAPI.Controllers
{
    [Route("api/UserAuth")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        protected APIResponse _response;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            this._response = new();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequestDTO)
        {
            var loginResponse = await _userRepository.Login(loginRequestDTO);
            //check if User is null or token is null or empty
            if(loginResponse.User == null || string.IsNullOrEmpty(loginResponse.Token))
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Username or password is incorrect");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDTO registrationRequestDTO)
        {
            //1st check if username from dto is unique
            //if not return bad request with message
            //if yes, check if the returned user is valid.
            //if not, return bad request with message
            //if all ok, return OK
            bool isUserNameUnique = _userRepository.IsUniqueUser(registrationRequestDTO.UserName);
            if (!isUserNameUnique)
            {
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Username already exists");
                return BadRequest(_response);
            }
            var user = await _userRepository.Register(registrationRequestDTO);
            if (user == null)
            {
                _response.StatusCode=HttpStatusCode.BadRequest;
                _response.IsSuccess = false;
                _response.ErrorMessage.Add("Something is wrong while register new user");
                return BadRequest(_response);
            }

            _response.StatusCode = HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);

        }
    }
}