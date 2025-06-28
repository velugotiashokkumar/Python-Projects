using FinalProject;
using Home.Dto_s;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
//using System.IO;

namespace Home.Services
{
    public class AuthServices
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public AuthServices(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public bool Register(RegisterDto dto)
        {
            if (_context.Users.Any(u => u.UserName == dto.UserName))
            {
                return false; // User already exists
            }
            var user = new User
            {
                UserName = dto.UserName,
                Password = dto.Password,
                Role = dto.Role
            };
            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

        //public bool RegisterPatient(string name, string password)
        //{
        //    if (_context.Users.Any(u => u.UserName == name))
        //    {
        //        return false; // User already exists
        //    }
        //    var user = new User
        //    {
        //        UserName = name,
        //        Password = password,
        //        Role = "Patient"

        //    };
        //    _context.Users.Add(user);
        //    _context.SaveChanges();
        //    return true;
        //}
        //public bool RegisterDoctor(string name, string password)
        //{
        //    if (_context.Users.Any(u => u.UserName == name))
        //    {
        //        return false; // User already exists
        //    }
        //    var user = new User
        //    {
        //        UserName = name,
        //        Password = password,
        //        Role = "Doctor"
        //    };
        //    _context.Users.Add(user);
        //    _context.SaveChanges();
        //    return true;
        //}
        //public bool RegisterStaff(string name, string password)
        //{
        //    if (_context.Users.Any(u => u.UserName == name))
        //    {
        //        return false; // User already exists
        //    }
        //    var user = new User
        //    {
        //        UserName = name,
        //        Password = password,
        //        Role = "Staff"
        //    };
        //    _context.Users.Add(user);
        //    _context.SaveChanges();
        //    return true;
        //}

        public string Login(LoginDto dto)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == dto.UserName && u.Password == dto.Password);
            if (user == null)
            {
                return null;
            }
            // Generate JWT token here
            var token = GenerateToken(user);
            return token;
        }

        public string GenerateToken(User user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim("RoleId", user.RoleId.ToString())

            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
