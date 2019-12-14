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
    public class ItemsController : ControllerBase
    {
        private readonly DataContext _context;

        public ItemsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> GetItems()
        {
            return await _context.Items
                                .Include(i => i.Photo)
                                .Include(i => i.ItemAtributes)
                                .Include(i => i.ItemGlobalAtributes)
                                .Include(i => i.ItemGroups)
                                .Select(i => i.GetDto()).ToListAsync();
        }

        // GET: api/Items/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemDto>> GetItem(long id)
        {
            var item = await _context.Items.FindAsync(id);

            if (item == null)
            {
                return NotFound();
            }

            _context.Entry(item).Reference(i => i.Photo).Load();
            _context.Entry(item).Reference(i => i.ItemAtributes).Load();
            _context.Entry(item).Reference(i => i.ItemGlobalAtributes).Load();
            _context.Entry(item).Reference(i => i.ItemGroups).Load();
            return item.GetDto();
        }

        // PUT: api/Items/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItem(long id, ItemDto itemDto)
        {
            if (id != itemDto.Id)
            {
                return BadRequest();
            }

            var item = _context.Items.First(i => i.Id == itemDto.Id);
            item.Name = itemDto.Name;
            item.Description = itemDto.Description;
            item.Photo = itemDto.PhotoId == -1
                         ? null
                         : _context.Photos.First(p => p.Id == itemDto.PhotoId);
            item.ItemAtributes = itemDto.AtributeIds.Select(ai =>
                                    _context.ItemAtributes.First(ia =>
                                        ia.ItemId == itemDto.Id
                                        && ia.AtributeId == ai))
                                    .ToList();
            item.ItemGlobalAtributes = itemDto.GlobalAtributeIds.Select(gai =>
                                    _context.ItemGlobalAtributes.First(iga =>
                                        iga.ItemId == itemDto.Id
                                        && iga.GlobalAtributeId == gai))
                                    .ToList();
            item.ItemGroups = itemDto.GroupIds.Select(gi =>
                                    _context.ItemGroups.First(ig =>
                                        ig.ItemId == itemDto.Id
                                        && ig.GroupId == gi))
                                    .ToList();
            _context.Entry(item).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(id))
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

        // POST: api/Items
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ItemDto>> PostItem(ItemDto itemDto)
        {
            _context.Items.Add(new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                Photo = itemDto.PhotoId == -1
                        ? null
                        : _context.Photos.First(p => p.Id == itemDto.PhotoId),
                ItemAtributes = itemDto.AtributeIds.Select(ai =>
                                    _context.ItemAtributes.First(ia =>
                                        ia.ItemId == itemDto.Id
                                        && ia.AtributeId == ai))
                                    .ToList(),
                ItemGlobalAtributes = itemDto.GlobalAtributeIds.Select(gai =>
                                    _context.ItemGlobalAtributes.First(iga =>
                                        iga.ItemId == itemDto.Id
                                        && iga.GlobalAtributeId == gai))
                                    .ToList(),
                ItemGroups = itemDto.GroupIds.Select(gi =>
                                    _context.ItemGroups.First(ig =>
                                        ig.ItemId == itemDto.Id
                                        && ig.GroupId == gi))
                                    .ToList()
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = itemDto.Id }, itemDto);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemDto>> DeleteItem(long id)
        {
            var item = await _context.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            await _context.SaveChangesAsync();

            return item.GetDto();
        }

        private bool ItemExists(long id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
