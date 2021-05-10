using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using cinemaAPI.Models;
using Microsoft.EntityFrameworkCore;
using cinemaAPI.Data;
using Microsoft.AspNetCore.Http;
using AuthenticationPlugin;

namespace cinemaAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {
        private CinemaDbContext _dbContext;

        public UsersController(CinemaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hi");
        }

        [HttpPost("[action]")]
        public IActionResult Register([FromBody] User user)
        {
            User userWithSameEmail = _dbContext.Users.Where(u => u.Email == user.Email).SingleOrDefault();
            if (userWithSameEmail != null)
            {
                BadRequest("User with the same email exists.");
            }

            User userObj = new User
            {
                Name = user.Name,
                Email = user.Email,
                Password = SecurePasswordHasherHelper.Hash(user.Password),
                Role = "Users"
            };
            _dbContext.Users.Add(userObj);
            _dbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
    }
}