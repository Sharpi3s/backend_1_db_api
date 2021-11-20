using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebbApi.Data;
using WebbApi.Entities;
using WebbApi.Models.authentication;
using WebbApi.Models.UserModels;
//using System.Diagnostics;

namespace WebbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly SqlContext _context;

        public AuthenticationController(SqlContext context)
        {
            _context = context;
        }

        [HttpPost("SignUp")]
        public async Task<ActionResult> SignUp(SignUpModel model)
        {
            if (!string.IsNullOrEmpty(model.Email) && !string.IsNullOrEmpty(model.Password))
            {
                var _userExists = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

                if (_userExists == null)
                {
                    //UserAdress userAdress = new UserAdress();
                    UserAdress userAdress = new();
                    userAdress = await _context.UserAdresses.Where(x => x.Adress == model.Adress && x.City == model.City).FirstOrDefaultAsync();

                    if (userAdress == null)
                    //if (userAdress.Id == 0)
                        {
                        var _address = _context.UserAdresses.Add(new UserAdress
                        {
                            Adress = model.Adress,
                            Zip = model.Zip,
                            City = model.City
                        });
                        await _context.SaveChangesAsync();
                        userAdress = await _context.UserAdresses.Where(x => x.Adress == model.Adress && x.City == model.City).FirstOrDefaultAsync();
                    }

                    var user = new User
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email,
                        Password = model.Password,
                        Admin = model.Admin,
                        UserAdressesId = userAdress.Id
                    };

                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();

                    var _user = new SigninModel
                    {
                        Email = model.Email,
                        Password = model.Password
                    };

                    var r = await SignIn(_user);
                    return CreatedAtAction("SignIn", r);

                }
                else
                {
                    return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "A user with the same email address aldready exists" }));
                }
            }
            else
            {
                return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "All required fields are not set" }));
            }
        }


        [HttpPost("SignIn")]
        public async Task<ActionResult> SignIn(SigninModel model)
        {

            var user = await _context.Users.Where(x => x.Email == model.Email).FirstOrDefaultAsync();

            if (user != null)
            {
                if (user.Password == model.Password)
                {
                    //return new OkObjectResult(JsonConvert.SerializeObject(new { userId = user.Id, Admin = user.Admin, sessionId = Guid.NewGuid().ToString() }));
                    return new OkObjectResult(JsonConvert.SerializeObject((userId: user.Id, user.Admin, sessionId: Guid.NewGuid().ToString())));
                }

                return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "incorrect email address or password" }));
            }

            return new BadRequestObjectResult(JsonConvert.SerializeObject(new { message = "incorrect email address or password" }));

        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> ChangeUser(int id, UpdateUser model)
        {
            
            var _user = await _context.Users.FindAsync(id);

            if (id != _user.Id)
            {
                return BadRequest();
            }

            var user = new User
            {
                Id = _user.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Password = _user.Password,
                Admin = model.Admin,
                UserAdressesId = _user.UserAdressesId
            };

            var _adress = await _context.UserAdresses.FindAsync(_user.UserAdressesId);

            var adress = new UserAdress
            {
                Id = _adress.Id,
                Adress = model.Adress,
                Zip = model.Zip,
                City = model.City
            };

            _context.Entry(_user).State = EntityState.Detached;
            _context.Entry(user).State = EntityState.Modified;
            _context.Entry(_adress).State = EntityState.Detached;
            _context.Entry(adress).State = EntityState.Modified;

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
        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

    }


}
