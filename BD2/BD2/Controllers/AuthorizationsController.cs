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
    public class AuthorizationsController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthorizationsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Authorizations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorizationDto>>> GetAuthorizations()
        {
            return await _context.Authorizations.Select(a => a.GetDto()).ToListAsync();
        }

        // GET: api/Authorizations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorizationDto>> GetAuthorization(long id)
        {
            var authorization = await _context.Authorizations.FindAsync(id);

            if (authorization == null)
            {
                return NotFound();
            }

            return authorization.GetDto();
        }

        // PUT: api/Authorizations/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthorization(long id, AuthorizationDto authorizationDto)
        {
            if (id != authorizationDto.Id)
            {
                return BadRequest();
            }

            var authorization = await _context.Authorizations.FindAsync(id);
            authorization.IsGlobal = authorizationDto.IsGlobal;
            authorization.Name = authorizationDto.Name;
            authorization.CanEdit = authorizationDto.CanEdit;
            _context.Entry(authorization).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorizationExists(id))
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

        // POST: api/Authorizations
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AuthorizationDto>> PostAuthorization(AuthorizationDto authorization)
        {
            _context.Authorizations.Add(new Authorization
            {
                Name = authorization.Name,
                IsGlobal = authorization.IsGlobal,
                CanEdit = authorization.CanEdit
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAuthorization", new { id = authorization.Id }, authorization);
        }

        // DELETE: api/Authorizations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AuthorizationDto>> DeleteAuthorization(long id)
        {
            var authorization = await _context.Authorizations.FindAsync(id);
            if (authorization == null)
            {
                return NotFound();
            }

            _context.Authorizations.Remove(authorization);
            await _context.SaveChangesAsync();

            return authorization.GetDto();
        }

        private bool AuthorizationExists(long id)
        {
            return _context.Authorizations.Any(e => e.Id == id);
        }
    }
}
