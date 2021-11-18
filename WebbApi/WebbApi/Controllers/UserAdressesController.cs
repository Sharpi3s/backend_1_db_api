using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebbApi.Data;
using WebbApi.Entities;

namespace WebbApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAdressesController : ControllerBase
    {
        private readonly SqlContext _context;

        public UserAdressesController(SqlContext context)
        {
            _context = context;
        }

        // GET: api/UserAdresses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserAdress>>> GetUserAdresses()
        {
            return await _context.UserAdresses.ToListAsync();
        }

        // GET: api/UserAdresses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserAdress>> GetUserAdress(int id)
        {
            var userAdress = await _context.UserAdresses.FindAsync(id);

            if (userAdress == null)
            {
                return NotFound();
            }

            return userAdress;
        }

        // PUT: api/UserAdresses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAdress(int id, UserAdress userAdress)
        {
            if (id != userAdress.Id)
            {
                return BadRequest();
            }

            _context.Entry(userAdress).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAdressExists(id))
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

        // POST: api/UserAdresses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserAdress>> PostUserAdress(UserAdress userAdress)
        {
            _context.UserAdresses.Add(userAdress);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserAdress", new { id = userAdress.Id }, userAdress);
        }

        // DELETE: api/UserAdresses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAdress(int id)
        {
            var userAdress = await _context.UserAdresses.FindAsync(id);
            if (userAdress == null)
            {
                return NotFound();
            }

            _context.UserAdresses.Remove(userAdress);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAdressExists(int id)
        {
            return _context.UserAdresses.Any(e => e.Id == id);
        }
    }
}
