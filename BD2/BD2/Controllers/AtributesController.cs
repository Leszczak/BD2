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
    public class AtributesController : ControllerBase
    {
        private readonly DataContext _context;

        public AtributesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Atributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Atribute>>> GetAtributes()
        {
            return await _context.Atributes.ToListAsync();
        }

        // GET: api/Atributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Atribute>> GetAtribute(long id)
        {
            var atribute = await _context.Atributes.FindAsync(id);

            if (atribute == null)
            {
                return NotFound();
            }

            return atribute;
        }

        // PUT: api/Atributes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAtribute(long id, Atribute atribute)
        {
            if (id != atribute.Id)
            {
                return BadRequest();
            }

            _context.Entry(atribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtributeExists(id))
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

        // POST: api/Atributes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Atribute>> PostAtribute(Atribute atribute)
        {
            _context.Atributes.Add(atribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAtribute", new { id = atribute.Id }, atribute);
        }

        // DELETE: api/Atributes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Atribute>> DeleteAtribute(long id)
        {
            var atribute = await _context.Atributes.FindAsync(id);
            if (atribute == null)
            {
                return NotFound();
            }

            _context.Atributes.Remove(atribute);
            await _context.SaveChangesAsync();

            return atribute;
        }

        private bool AtributeExists(long id)
        {
            return _context.Atributes.Any(e => e.Id == id);
        }
    }
}
