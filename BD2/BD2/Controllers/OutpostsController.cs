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
        public async Task<ActionResult<IEnumerable<OutpostDto>>> GetOutposts()
        {
            return await _context.Outposts.Select(o => o.GetDto()).ToListAsync();
        }

        // GET: api/Outposts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OutpostDto>> GetOutpost(long id)
        {
            var outpost = await _context.Outposts.FindAsync(id);

            if (outpost == null)
            {
                return NotFound();
            }

            return outpost.GetDto();
        }

        // PUT: api/Outposts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOutpost(long id, OutpostDto outpostDto)
        {
            if (id != outpostDto.Id)
            {
                return BadRequest();
            }

            var outpost = _context.Outposts.First(o => o.Id == outpostDto.Id);
            outpost.Name = outpostDto.Name;
            outpost.Location = outpostDto.Location;
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
        public async Task<ActionResult<OutpostDto>> PostOutpost(OutpostDto outpostDto)
        {
            _context.Outposts.Add(new Outpost
            {
                Name = outpostDto.Name,
                Location = outpostDto.Location
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOutpost", new { id = outpostDto.Id }, outpostDto);
        }

        // DELETE: api/Outposts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<OutpostDto>> DeleteOutpost(long id)
        {
            var outpost = await _context.Outposts.FindAsync(id);
            if (outpost == null)
            {
                return NotFound();
            }

            _context.Outposts.Remove(outpost);
            foreach (User u in _context.Users.Where(u => u.Outpost == outpost))
                u.Outpost = null;
            foreach (LocalItem li in _context.LocalItems.Where(li=> li.Outpost == outpost))
                li.Outpost = null;
            await _context.SaveChangesAsync();
            return outpost.GetDto();
        }

        private bool OutpostExists(long id)
        {
            return _context.Outposts.Any(e => e.Id == id);
        }
    }
}
