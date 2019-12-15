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
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ValueDto>>> GetValues()
        {
            return await _context.Values
                                .Include(v => v.Atribute)
                                .Include(v => v.LocalItem)
                                .Select(v => v.GetDto())
                                .ToListAsync();
        }

        // GET: api/Values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ValueDto>> GetValue(long id)
        {
            var value = await _context.Values.FindAsync(id);

            if (value == null)
            {
                return NotFound();
            }

            _context.Entry(value).Reference(v => v.Atribute).Load();
            _context.Entry(value).Reference(v => v.LocalItem).Load();
            return value.GetDto();
        }

        // PUT: api/Values/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutValue(long id, ValueDto valueDto)
        {
            if (id != valueDto.Id)
            {
                return BadRequest();
            }

            var value = _context.Values.First(v => v.Id == valueDto.Id);
            value.Content = valueDto.Content;
            value.Atribute = _context.Atributes.First(a => a.Id == valueDto.AtributeId);
            value.LocalItem = _context.LocalItems.First(li => li.Id == valueDto.LocalItemId);
            _context.Entry(value).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValueExists(id))
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

        // POST: api/Values
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<ValueDto>> PostValue(ValueDto valueDto)
        {
            _context.Values.Add(new Value
            { 
                Content = valueDto.Content,
                Atribute = _context.Atributes.First(a => a.Id == valueDto.AtributeId),
                LocalItem = _context.LocalItems.First(li => li.Id == valueDto.LocalItemId)
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetValue", new { id = valueDto.Id }, valueDto);
        }

        // DELETE: api/Values/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ValueDto>> DeleteValue(long id)
        {
            var value = await _context.Values.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }

            _context.Values.Remove(value);
            await _context.SaveChangesAsync();

            return value.GetDto();
        }

        private bool ValueExists(long id)
        {
            return _context.Values.Any(e => e.Id == id);
        }
    }
}
