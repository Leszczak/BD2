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
    public class OutpostsController : ControllerBase
    {
        private readonly DataContext _context;

        public OutpostsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Outposts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Outpost>>> GetOutposts()
        {
            return await _context.Outposts.ToListAsync();
        }

        // GET: api/Outposts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Outpost>> GetOutpost(long id)
        {
            var outpost = await _context.Outposts.FindAsync(id);

            if (outpost == null)
            {
                return NotFound();
            }

            return outpost;
        }

        // PUT: api/Outposts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOutpost(long id, Outpost outpost)
        {
            if (id != outpost.Id)
            {
                return BadRequest();
            }

            _context.Entry(outpost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OutpostExists(id))
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

        // POST: api/Outposts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Outpost>> PostOutpost(Outpost outpost)
        {
            _context.Outposts.Add(outpost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOutpost", new { id = outpost.Id }, outpost);
        }

        // DELETE: api/Outposts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Outpost>> DeleteOutpost(long id)
        {
            var outpost = await _context.Outposts.FindAsync(id);
            if (outpost == null)
            {
                return NotFound();
            }

            _context.Outposts.Remove(outpost);
            await _context.SaveChangesAsync();

            return outpost;
        }

        private bool OutpostExists(long id)
        {
            return _context.Outposts.Any(e => e.Id == id);
        }
    }
}
