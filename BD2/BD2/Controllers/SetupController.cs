using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BD2.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BD2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly DataContext _context;

        public SetupController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetSetup()
        {

            _context.Outposts.Add(new Models.Outpost
            {
                Name = "Name1",
                Location = "Town"
            });
            _context.Users.Add(new Models.User
            {
                Email = "test@abc.abc",
                Salt = "abc",
                Hash = "abc"
            });
            _context.Users.Add(new Models.User
            {
                Email = "test2@abc.abc",
                Salt = "abc",
                Hash = "abc",
                Name = "Aaa",
                Surname = "Bbb",
                Outpost = new Models.Outpost
                {
                    Name = "Name1",
                    Location = "Town"
                }
        });
            _context.SaveChanges();

            return NoContent();
        }
    }
}