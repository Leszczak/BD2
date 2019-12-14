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
    public class GlobalAtributesController : ControllerBase
    {
        private readonly DataContext _context;

        public GlobalAtributesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/GlobalAtributes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GlobalAtributeDto>>> GetGlobalAtributes()
        {
            return await _context.GlobalAtributes
                                .Include(ga => ga.ItemGlobalAtributes)
                                .Select(ga => ga.GetDto())
                                .ToListAsync();
        }

        // GET: api/GlobalAtributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GlobalAtributeDto>> GetGlobalAtribute(long id)
        {
            var globalAtribute = await _context.GlobalAtributes.FindAsync(id);

            if (globalAtribute == null)
            {
                return NotFound();
            }

            _context.Entry(globalAtribute).Reference(ga => ga.ItemGlobalAtributes).Load();
            return globalAtribute.GetDto();
        }

        // PUT: api/GlobalAtributes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGlobalAtribute(long id, GlobalAtributeDto globalAtributeDto)
        {
            if (id != globalAtributeDto.Id)
            {
                return BadRequest();
            }

            var globalAtribute = _context.GlobalAtributes.First(ga => ga.Id == id);
            globalAtribute.Name = globalAtributeDto.Name;
            globalAtribute.Content = globalAtributeDto.Content;
            globalAtribute.ItemGlobalAtributes = globalAtributeDto.ItemIds
                                    .Select(ii => _context.ItemGlobalAtributes
                                        .First(ia =>
                                            ia.GlobalAtributeId == globalAtributeDto.Id
                                            && ia.ItemId == ii))
                                    .ToList();
            _context.Entry(globalAtribute).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GlobalAtributeExists(id))
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

        // POST: api/GlobalAtributes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<GlobalAtributeDto>> PostGlobalAtribute(GlobalAtributeDto globalAtribute)
        {
            _context.GlobalAtributes.Add(new GlobalAtribute
            {
                Name = globalAtribute.Name,
                Content = globalAtribute.Content,
                ItemGlobalAtributes = globalAtribute.ItemIds
                                        .Select(ii => _context.ItemGlobalAtributes
                                            .First(ia =>
                                                ia.GlobalAtributeId == globalAtribute.Id
                                                && ia.ItemId == ii))
                                        .ToList()
        });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGlobalAtribute", new { id = globalAtribute.Id }, globalAtribute);
        }

        // DELETE: api/GlobalAtributes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GlobalAtributeDto>> DeleteGlobalAtribute(long id)
        {
            var globalAtribute = await _context.GlobalAtributes.FindAsync(id);
            if (globalAtribute == null)
            {
                return NotFound();
            }

            _context.GlobalAtributes.Remove(globalAtribute);
            await _context.SaveChangesAsync();

            return globalAtribute.GetDto();
        }

        private bool GlobalAtributeExists(long id)
        {
            return _context.GlobalAtributes.Any(e => e.Id == id);
        }
    }
}
