using Microsoft.AspNetCore.Mvc;
using Steam_1.DAL;
using Steam_1.Models;
using System.Collections.Generic;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Steam_1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        //GET: api/<UsersController>
        [HttpGet]
        public IEnumerable<AppUser> Get()
        {
            return AppUser.Read();
        }


        [HttpPost("Register")]
        public IActionResult Post([FromBody] AppUser user)
        {
            try
            {
                int rowsAffected = user.Insert();
                if (rowsAffected > 0)
                {
                    return Ok(new { message = "User registered successfully" });
                }
                else if (rowsAffected == 0)
                {
                    return Conflict(new { message = "Email is already registered" });
                }
                else
                {
                    return Conflict(new { message = "Failed to register user" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        [HttpPost("Login")]
        public IActionResult Login(string Email, string Password)
        {
            try
            {
                AppUser user = new AppUser();
                bool isLoggedIn = user.Login(Email, Password);

                if (isLoggedIn)
                {
                    var (userId, name) = user.GetUserIdByEmail(Email);
                    bool isAdmin = user.CheckIfAdmin(Email); // בדיקה אם המשתמש הוא Admin

                    if (userId > 0)
                    {
                        return Ok(new
                        {
                            message = "Login successful",
                            userId = userId,
                            name = name,
                            isAdmin = isAdmin // החזרת מידע אם המשתמש הוא Admin
                        });
                    }

                    return NotFound(new { message = "User ID not found" });
                }

                return Unauthorized(new { message = "Invalid email or password" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }



        [HttpPut("UpdateUser/{id}")]
        public IActionResult Put(int id, [FromBody] AppUser user)
        {
            Console.WriteLine($"Received ID: {id}, User ID: {user.Id}, Email: {user.Email}, Name: {user.Name}");

            if (id != user.Id)
            {
                return BadRequest(new { message = "User ID mismatch" });
            }

            AppUser updatedUser = user.Update();

            if (updatedUser == null)
            {
                return Conflict(new { message = "User not found or email is already in use" });
            }

            return Ok(updatedUser);
        }

        [HttpPut("UpdateIsActive/{id}")]
        public IActionResult UpdateIsActive(int id, [FromBody] bool isActive)
        {
            AppUser user = new AppUser { Id = id }; // יצירת אובייקט AppUser עם Id בלבד
            bool isUpdated = user.UpdateIsActive(isActive);

            if (isUpdated)
            {
                return Ok(new { message = $"User with ID {id} updated successfully", isActive });
            }

            return NotFound(new { message = "User not found or update failed" });
        }

        [HttpGet("UserDetails")]
        public Object GetUserDetails()
        {
            AppUser user = new AppUser();
            return user.GetUserDetails(); // קריאה ישירה למתודה והחזרת התוצאה
        }


    }
}
