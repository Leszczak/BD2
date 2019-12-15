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
        public async Task<ActionResult<IEnumerable<LocalItemDto>>> GetLocalItems()
        {
            return await _context.LocalItems
                                .Include(li => li.Item)
                                .Include(li => li.Outpost)
                                .Select(li => li.GetDto()).ToListAsync();
        }

        // GET: api/LocalItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LocalItemDto>> GetLocalItem(long id)
        {
            var localItem = await _context.LocalItems.FindAsync(id);

            if (localItem == null)
            {
                return NotFound();
            }

            _context.Entry(localItem).Reference(li => li.Item).Load();
            _context.Entry(localItem).Reference(li => li.Outpost).Load();
            return localItem.GetDto();
        }

        // PUT: api/LocalItems/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocalItem(long id, LocalItemDto localItemDto)
        {
            if (id != localItemDto.Id)
            {
                return BadRequest();
            }

            var localItem = _context.LocalItems.First(li => li.Id == localItemDto.Id);
            localItem.Item = _context.Items.First(i => i.Id == localItemDto.ItemId);
            localItem.Outpost = _context.Outposts.First(o => o.Id == localItemDto.OutpostId);
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
        public async Task<ActionResult<LocalItemDto>> PostLocalItem(LocalItemDto localItemDto)
        {
            _context.LocalItems.Add(new LocalItem
            {
                Item = _context.Items.First(i => i.Id == localItemDto.ItemId),
                Outpost = _context.Outposts.First(o => o.Id == localItemDto.OutpostId)
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocalItem", new { id = localItemDto.Id }, localItemDto);
        }

        // DELETE: api/LocalItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LocalItemDto>> DeleteLocalItem(long id)
        {
            var localItem = await _context.LocalItems.FindAsync(id);
            if (localItem == null)
            {
                return NotFound();
            }

            _context.LocalItems.Remove(localItem);
            _context.Values.RemoveRange(
                _context.Values.Where(v => v.LocalItem == localItem));
            await _context.SaveChangesAsync();
            return localItem.GetDto();
        }

        private bool LocalItemExists(long id)
        {
            return _context.LocalItems.Any(e => e.Id == id);
        }
    }
}
