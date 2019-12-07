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
    public class LocalItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public LocalItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/LocalItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocalItem>>> GetLocalItems()
        {
            return await _context.LocalItems.ToListAsync();
        }

        // GET: api/LocalItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocalItem>> GetLocalItem(long id)
        {
            var localItem = await _context.LocalItems.FindAsync(id);

            if (localItem == null)
            {
                return NotFound();
            }

            return localItem;
        }

        // PUT: api/LocalItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocalItem(long id, LocalItem localItem)
        {
            if (id != localItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(localItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocalItemExists(id))
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

        // POST: api/LocalItems
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LocalItem>> PostLocalItem(LocalItem localItem)
        {
            _context.LocalItems.Add(localItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocalItem", new { id = localItem.Id }, localItem);
        }

        // DELETE: api/LocalItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LocalItem>> DeleteLocalItem(long id)
        {
            var localItem = await _context.LocalItems.FindAsync(id);
            if (localItem == null)
            {
                return NotFound();
            }

            _context.LocalItems.Remove(localItem);
            await _context.SaveChangesAsync();

            return localItem;
        }

        private bool LocalItemExists(long id)
        {
            return _context.LocalItems.Any(e => e.Id == id);
        }
    }
}
