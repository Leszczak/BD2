﻿using System;
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
        public async Task<ActionResult<IEnumerable<AtributeDto>>> GetAtributes()
        {
            return await _context.Atributes
                                .Include(a => a.ItemAtributes)
                                .Select(a => a.ToDto())
                                .ToListAsync();
        }

        // GET: api/Atributes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AtributeDto>> GetAtribute(long id)
        {
            var atribute = _context.Atributes.Include(a => a.ItemAtributes).FirstOrDefault(a => a.Id == id);

            if (atribute == null)
            {
                return NotFound();
            }

            return atribute.ToDto();
        }

        // PUT: api/Atributes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAtribute(long id, AtributeDto atributeDto)
        {
            if (id != atributeDto.Id)
            {
                return BadRequest();
            }

            var atribute = await _context.Atributes.FindAsync(id);
            atribute.Name = atributeDto.Name;
            foreach(long ii in atributeDto.ItemIds)
            {
                if(_context.Items.Any(i => i.Id == ii))
                {
                    if (!_context.ItemAtributes.Any(ia =>
                                                     ia.ItemId == ii
                                                     && ia.AtributeId == atribute.Id))
                    {
                        ItemAtribute temp = new ItemAtribute
                        {
                            Item = _context.Items.First(i => i.Id == ii),
                            Atribute = atribute
                        };
                        _context.ItemAtributes.Add(temp);
                    }
                }
                else
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
        public async Task<ActionResult<Atribute>> PostAtribute(AtributeDto atributeDto)
        {
            Atribute atribute = new Atribute
            {
                Name = atributeDto.Name
            };
            foreach(long ii in atributeDto.ItemIds)
            {
                if(_context.Items.Any(i => i.Id == ii))
                {
                    ItemAtribute temp = new ItemAtribute
                    {
                        Item = _context.Items.First(i => i.Id == ii),
                        Atribute = atribute
                    };
                    _context.ItemAtributes.Add(temp);
                }
                else
                    return BadRequest();
            }

            _context.Atributes.Add(atribute);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAtribute", new { id = atribute.Id }, atribute);
        }

        // DELETE: api/Atributes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AtributeDto>> DeleteAtribute(long id)
        {
            var atribute = await _context.Atributes.FindAsync(id);
            if (atribute == null)
            {
                return NotFound();
            }

            _context.Atributes.Remove(atribute);
            _context.Values.RemoveRange(
                _context.Values.Where(v => v.Atribute == atribute));
            _context.ItemAtributes.RemoveRange(
                _context.ItemAtributes.Where(ia => ia.Atribute == atribute));

            await _context.SaveChangesAsync();
            return atribute.ToDto();
        }

        private bool AtributeExists(long id)
        {
            return _context.Atributes.Any(e => e.Id == id);
        }
    }
}
