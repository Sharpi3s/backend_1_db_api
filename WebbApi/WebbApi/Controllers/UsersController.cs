using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebbApi.Data;
using WebbApi.Entities;
using WebbApi.Models.UserModels;

namespace WebbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly SqlContext _context;

        public UsersController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetUsers>>> GetUsers()
        {
            var Users = new List<GetUsers>();

            foreach (var item in await _context.Users.ToListAsync())
            {
                Users.Add(new Models.UserModels.GetUsers
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    //Password = item.Password,
                    Admin = item.Admin,
                    UserAdressesId = item.UserAdressesId
                });
            }


            return Users;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetUsers>> GetUser(int id)
        {
            var user = await _context.Users.FindAsync(id);



            if (user == null)
            {
                return NotFound();
            }
            else
            {
                var newUser = new GetUsers
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    //Password = user.Password,
                    Admin = user.Admin,
                    UserAdressesId = user.UserAdressesId
                };
                return newUser;
            }


        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<User>> PostUser(User user)
        //{

        //    if (!string.IsNullOrWhiteSpace(user.FirstName) && !string.IsNullOrWhiteSpace(user.LastName) && !string.IsNullOrWhiteSpace(user.Email))
        //    {

        //        var exists = _context.UserAdresses.Where(x => x.Adress == user.UserAdresses.Adress).FirstOrDefault();
                
        //        var patternEmail = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";
        //        //En stor bokstav, En liten bokstav, Ett specialtecken, minst 8 bokstäver långt
        //        var patternPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{8,15}$";

        //        var emailExists = _context.Users.Where(x => x.Email == user.Email).FirstOrDefaultAsync();


        //        if (Regex.IsMatch(user.Email, patternEmail) && Regex.IsMatch(user.Password, patternPassword))
        //        {
        //            //if (emailExists.Email)
        //            //{

        //            //}

        //            if (exists == null)
        //            {
        //                _context.Users.Add(user);
        //                await _context.SaveChangesAsync();

        //                return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //            }
        //            var newUser = new User
        //            {
        //                FirstName = user.FirstName,
        //                LastName = user.LastName,
        //                Email = user.Email,
        //                Password = user.Password,
        //                Admin = user.Admin,
        //                UserAdressesId = exists.Id
        //            };


        //            _context.Users.Add(newUser);
        //            await _context.SaveChangesAsync();

        //            return CreatedAtAction("GetUser", new { id = user.Id }, newUser);

        //        }

        //        else
        //        {
        //            return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = $"Email is wrong format" }));
        //        }



        //    }

        //    return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = $"All fields must contain values." }));

        //}

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
