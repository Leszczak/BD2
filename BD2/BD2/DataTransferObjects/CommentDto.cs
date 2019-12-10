using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class CommentDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public long PhotoId { get; set; }
        public long ItemId { get; set; }
        public long UserId { get; set; }
    }
}
