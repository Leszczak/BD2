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
    public class PhotosController : ControllerBase
    {
        private readonly DataContext _context;

        public PhotosController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Photos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhotoDto>>> GetPhotos()
        {
            return await _context.Photos.Select(p => p.GetDto()).ToListAsync();
        }

        // GET: api/Photos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhotoDto>> GetPhoto(long id)
        {
            var photo = await _context.Photos.FindAsync(id);

            if (photo == null)
            {
                return NotFound();
            }

            return photo.GetDto();
        }

        // PUT: api/Photos/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhoto(long id, PhotoDto photoDto)
        {
            if (id != photoDto.Id)
            {
                return BadRequest();
            }

            var photo = _context.Photos.First(p => p.Id == photoDto.Id);
            photo.Comment = photoDto.Comment;
            _context.Entry(photo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhotoExists(id))
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

        // POST: api/Photos
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<PhotoDto>> PostPhoto(PhotoDto photoDto)
        {
            _context.Photos.Add(new Photo
            {
                Comment = photoDto.Comment
            });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhoto", new { id = photoDto.Id }, photoDto);
        }

        // DELETE: api/Photos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PhotoDto>> DeletePhoto(long id)
        {
            var photo = await _context.Photos.FindAsync(id);
            if (photo == null)
            {
                return NotFound();
            }

            _context.Photos.Remove(photo);
            await _context.SaveChangesAsync();

            return photo.GetDto();
        }

        private bool PhotoExists(long id)
        {
            return _context.Photos.Any(e => e.Id == id);
        }
    }
}
