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
    public class GroupsController : ControllerBase
    {
        private readonly DataContext _context;

        public GroupsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Groups
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupDto>>> GetGroups()
        {
            return await _context.Groups
                                .Include(g => g.ItemGroups)
                                .Select(g => g.GetDto()).ToListAsync();
        }

        // GET: api/Groups/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GroupDto>> GetGroup(long id)
        {
            var @group = _context.Groups.Include(g => g.ItemGroups).First(g => g.Id == id);

            if (@group == null)
            {
                return NotFound();
            }

            return @group.GetDto();
        }

        // PUT: api/Groups/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroup(long id, GroupDto @groupDto)
        {
            if (id != @groupDto.Id)
            {
                return BadRequest();
            }
            var group = _context.Groups.First(g => g.Id == id);
            group.Name = groupDto.Name;
            group.Description = groupDto.Description;
            group.ItemGroups = groupDto.ItemIds.Select(ii =>
                                _context.ItemGroups.First(ig =>
                                    ig.GroupId == groupDto.Id
                                    && ig.ItemId == ii))
                                .ToList();
            _context.Entry(@group).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GroupExists(id))
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

        // POST: api/Groups
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<GroupDto>> PostGroup(GroupDto groupDto)
        {
            Group group = new Group
            {
                Id = groupDto.Id,
                Name = groupDto.Name,
                Description = groupDto.Description

            };
            foreach (long ii in groupDto.ItemIds)
            {
                if (_context.Items.Any(i => i.Id == ii))
                {
                    ItemGroup temp = new ItemGroup
                    {
                        Item = _context.Items.First(i => i.Id == ii),
                        Group = group
                    };
                    _context.ItemGroups.Add(temp);
                }
                else
                    return BadRequest();
            }

            _context.Groups.Add(group);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGroup", new { id = @group.Id }, @group);
        }

        // DELETE: api/Groups/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupDto>> DeleteGroup(long id)
        {
            var @group = await _context.Groups.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }

            _context.Groups.Remove(@group);
            await _context.SaveChangesAsync();

            return @group.GetDto();
        }

        private bool GroupExists(long id)
        {
            return _context.Groups.Any(e => e.Id == id);
        }
    }
}
