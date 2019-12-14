using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BD2.Data;
using BD2.Models;

namespace BD2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            return await _context.Users
                                .Include(u => u.Outpost)
                                .Include(u => u.Authorization)
                                .Select(u => u.GetDto())
                                .ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(long id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Entry(user).Reference(u => u.Outpost).Load();
            _context.Entry(user).Reference(u => u.Authorization).Load();
            return user.GetDto();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(long id, UserDto userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var user = _context.Users.First(u => u.Id == userDto.Id);
            user.Email = userDto.Email;
            user.Name = userDto.Name;
            user.Surname = userDto.Surname;
            user.Authorization = userDto.AuthorizationId == -1
                                    ? null
                                    : _context.Authorizations.First(a => a.Id == userDto.AuthorizationId);
            user.Outpost = userDto.OutpostId == -1
                            ? null
                            : _context.Outposts.First(o => o.Id == userDto.OutpostId);
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(UserDto userDto)
        {
            _context.Users.Add(new Models.User
            {
                Email = userDto.Email,
                Name = userDto.Name,
                Surname = userDto.Surname,
                Authorization = userDto.AuthorizationId == -1
                                ? null
                                : _context.Authorizations.First(a => a.Id == userDto.AuthorizationId),
                Outpost = userDto.OutpostId == -1
                            ? null
                            : _context.Outposts.First(o => o.Id == userDto.OutpostId)
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = userDto.Id }, userDto);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDto>> DeleteUser(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user.GetDto();
        }

        private bool UserExists(long id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
