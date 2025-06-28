using FinalProject;
using Home.Dto_s;
using Home.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Home.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthServices _authServices;
        public AuthController(AuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            var token = _authServices.Login(dto);
            if (token == null)
            {
                return Unauthorized("Invalid Username or password");
            }
            return Ok(new { token });
        }

        //[HttpPost("Register")]
        //public IActionResult Register([FromBody] RegisterDto dto)
        //{
        //    var success = _authServices.Register(dto);
        //    if (!success)
        //    {
        //        return BadRequest("User already exists");
        //    }
        //    return Ok("User registered successfully");
        //}

        //[HttpPost("Register_Patient")]
        //[AllowAnonymous]
        //public IActionResult RegisterPatient([FromBody] LoginDto dto)
        //{
        //    var success = _authServices.RegisterPatient(dto.UserName, dto.Password);
        //    if (!success)
        //    {
        //        return BadRequest("Patient already exists");
        //    }
        //    return Ok("Patient registered successfully");
        //}
        //[HttpPost("Register_Doctor")]
        //[AllowAnonymous]
        //public IActionResult RegisterDoctor([FromBody] LoginDto dto)
        //{
        //    var success = _authServices.RegisterDoctor(dto.UserName, dto.Password);
        //    if (!success)
        //    {
        //        return BadRequest("Doctor already exists");
        //    }
        //    return Ok("Doctor registered successfully");
        //}
       
    }
}
