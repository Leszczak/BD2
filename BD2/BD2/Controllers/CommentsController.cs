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
    public class CommentsController : ControllerBase
    {
        private readonly DataContext _context;

        public CommentsController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetComments()
        {
            return await _context.Comments
                                .Include(c => c.Photo)
                                .Include(c => c.User)
                                .Include(c => c.Item)
                                .Select(c => c.GetDto())
                                .ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> GetComment(long id)
        {
            var comment = await _context.Comments.FindAsync(id);

            if (comment == null)
            {
                return NotFound();
            }

            _context.Entry(comment).Reference(c => c.Photo).Load();
            _context.Entry(comment).Reference(c => c.User).Load();
            _context.Entry(comment).Reference(c => c.Item).Load();
            return comment.GetDto();
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutComment(long id, CommentDto commentDto)
        {
            if (id != commentDto.Id)
            {
                return BadRequest();
            }

            var comment = _context.Comments.First(c => c.Id == id);
            comment.Title = comment.Title;
            comment.Text = comment.Text;
            comment.Item = _context.Items.First(i => i.Id == commentDto.ItemId);
            comment.Photo = commentDto.PhotoId == -1
                            ? null
                            : _context.Photos.First(p => p.Id == commentDto.PhotoId);
            comment.User = _context.Users.First(u => u.Id == commentDto.UserId);
            _context.Entry(comment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CommentDto>> PostComment(CommentDto comment)
        {
            _context.Comments.Add(new Comment { 
                Title = comment.Title,
                Text = comment.Text,
                Photo = comment.PhotoId == -1  
                        ? null 
                        : _context.Photos.First(p => p.Id == comment.PhotoId),
                User = _context.Users.First(u => u.Id == comment.UserId),
                Item = _context.Items.First(i => i.Id == comment.ItemId)
        });
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetComment", new { id = comment.Id }, comment);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CommentDto>> DeleteComment(long id)
        {
            var comment = await _context.Comments.FindAsync(id);
            _context.Entry(comment).Reference(c => c.Photo).Load();
            if (comment == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(comment);
            if (comment.Photo != null)
                _context.Photos.Remove(comment.Photo);
            await _context.SaveChangesAsync();
            return comment.GetDto();
        }

        private bool CommentExists(long id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
