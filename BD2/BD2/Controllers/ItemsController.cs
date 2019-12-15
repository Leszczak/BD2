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
            var item = _context.Items
                                .Include(i => i.Photo)
                                .Include(i => i.ItemAtributes)
                                .Include(i => i.ItemGlobalAtributes)
                                .Include(i => i.ItemGroups)
                                .First(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

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
            Item item = new Item
            {
                Name = itemDto.Name,
                Description = itemDto.Description,
                Photo = itemDto.PhotoId == -1
                        ? null
                        : _context.Photos.First(p => p.Id == itemDto.PhotoId)
            };

            foreach (long ai in itemDto.AtributeIds)
            {
                if (_context.Atributes.Any(a => a.Id == ai))
                {
                    ItemAtribute temp = new ItemAtribute
                    {
                        Item = item,
                        Atribute = _context.Atributes.First(a => a.Id == ai)
                    };
                    _context.ItemAtributes.Add(temp);
                }
                else
                    return BadRequest();
            }
            foreach (long gai in itemDto.GlobalAtributeIds)
            {
                if (_context.GlobalAtributes.Any(ga => ga.Id == gai))
                {
                    ItemGlobalAtribute temp = new ItemGlobalAtribute
                    {
                        Item = item,
                        GlobalAtribute = _context.GlobalAtributes.First(ga => ga.Id == gai)
                    };
                    _context.ItemGlobalAtributes.Add(temp);
                }
                else
                    return BadRequest();
            }
            foreach (long gi in itemDto.GroupIds)
            {
                if (_context.Groups.Any(g => g.Id == gi))
                {
                    ItemGroup temp = new ItemGroup
                    {
                        Item = item,
                        Group = _context.Groups.First(g => g.Id == gi)
                    };
                    _context.ItemGroups.Add(temp);
                }
                else
                    return BadRequest();
            }

            _context.Items.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetItem", new { id = itemDto.Id }, itemDto);
        }

        // DELETE: api/Items/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ItemDto>> DeleteItem(long id)
        {
            var item = _context.Items
                                .Include(i => i.Photo)
                                .First(i => i.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            _context.Items.Remove(item);
            if (item.Photo != null)
                _context.Photos.Remove(item.Photo);
            _context.ItemGroups.RemoveRange(
                _context.ItemGroups.Where(ig => ig.Item == item));
            _context.ItemAtributes.RemoveRange(
                _context.ItemAtributes.Where(iga => iga.Item == item));
            _context.ItemGlobalAtributes.RemoveRange(
                _context.ItemGlobalAtributes.Where(ia => ia.Item == item));
            _context.Comments.RemoveRange(
                _context.Comments.Where(c => c.Item == item));
            _context.LocalItems.RemoveRange(
                _context.LocalItems.Where(li => li.Item == item));
            await _context.SaveChangesAsync();
            return item.GetDto();
        }

        private bool ItemExists(long id)
        {
            return _context.Items.Any(e => e.Id == id);
        }
    }
}
