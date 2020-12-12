using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagement.API.Helpers;
using SchoolManagement.API.Models;
using SchoolManagement.API.Repositories;
using SchoolManagement.Data.DTOs;
using SchoolManagement.Domain;

namespace SchoolManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IUserRepository repository, IMapper mapper,
                              IOptions<AppSettings> appSettings,
                              IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _appSettings = appSettings;
            _httpContextAccessor = httpContextAccessor;
        }

       
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                model.Email = model.Email.ToLower();

                if (await _repository.UserExists(model.Email))
                    return BadRequest("Email already exists");

                var userToAdd = new Administrator()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                };

                var addedUser = await _repository.Register(userToAdd, model.Password);

                if (addedUser != null)
                {
                    return Ok(new
                    {
                        addedUser.Id,
                        addedUser.FirstName,
                        addedUser.LastName,
                        addedUser.Email
                    });
                  
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody]LoginModel model)
        {
            if(ModelState.IsValid)
            {
                var authenticatedUser = await _repository.Login(model.Email, model.Password);
                if(authenticatedUser != null)
                {
                    var token = GenerateJwtToken(authenticatedUser);

                    return Ok(new
                    {
                        token,
                    });
                }
                return Unauthorized();
            }
            else
            {
                return BadRequest("Email or Password are incorrect");
            }
        }

        [Helpers.Authorize]
        [HttpGet("GetUser")]
        public async Task<ActionResult<UserDTO>> GetUser()
        {
            // Gets the UserId from the current Request's Authorization header Bearer Token
            var userId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            // calling the UserRepository to get the user data
            var user = await _repository.GetUserById(userId);

            // case user does not exist
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var appUser = _mapper.Map<UserDTO>(user);

                return appUser;
            }
        }




        #region Private Methods
        /// <summary>
        /// Generate a JWT TOKEN
        /// </summary>
        /// <param name="user"></param>
        /// <returns> string</returns>

        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Secret").Value));

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appSettings.Value.Secret));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = creds,
                Expires = DateTime.Now.AddHours(12) //expires at the end (updated the 11 of febraury 2020)

            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion


    }
}
