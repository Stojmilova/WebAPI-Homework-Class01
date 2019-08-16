using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Homework01.Controllers
{
    public class User
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public static List<User> users = new List<User>()
        {
            new User()
            {
                FirstName = "Bob",
                LastName = "Bobsky",
                Age = 15
            },
            new User()
            {
                FirstName = "Rob",
                LastName = "Robsky",
                Age = 25
            },
            new User()
            {
                FirstName = "Pop",
                LastName = "Popsky",
                Age = 10
            }
        };

        [HttpGet]
        public ActionResult<List<User>> Get()
        {
            return users;
        }
        [HttpGet("{id}")]
        public ActionResult<User> Get(int id)
        {
            try
            {
                return users[id - 1];
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound(new { message = $"User with id: {id} is not found!", suggestion = 1 });

            }
            catch (Exception ex)
            {
                return BadRequest("BROKEN REQUEST - " + ex.Message);
            }

        }
        [HttpGet("{id}/checkIsAdult")]
        public ActionResult<string>IsUserAdult(int id)
        {
            try
            {
                User adultUser = users.FirstOrDefault(x => x.Age >= 18);
                if (users[id - 1] == adultUser)
                {
                    return $"User with FIRST NAME: {users[id-1].FirstName}, LAST NAME: {users[id - 1].LastName}, ID: {id} is ADULT!";
                }else
                {
                    return $"User with FIRST NAME: {users[id - 1].FirstName}, LAST NAME: {users[id - 1].LastName}, ID: {id} is NOT ADULT!";
                }
            }
            catch (ArgumentOutOfRangeException)
            {

                return NotFound($"User with id: {id} is not found!");
            }
            catch (Exception ex)
            {
                return BadRequest($"Bad Reguest : {ex.Message}");
            }
        }
        [HttpPost]
        public IActionResult Post()
        {
            string body;
            using (StreamReader sr = new StreamReader(Request.Body))
            {
                body = sr.ReadToEnd();
            }
            User user = JsonConvert.DeserializeObject<User>(body);
            users.Add(user);
            return Ok($"User with id {users.Count - 1} has been added!");
        }
    }
}