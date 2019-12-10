using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BD2.Models
{
    public class Comment
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public Photo Photo { get; set; }
        public Item Item { get; set; }
        public User User { get; set; }
        public CommentDto GetDto()
        {
            return new CommentDto
            {
                Id = this.Id,
                Title = this.Title,
                Text = this.Text,
                PhotoId = this.Photo.Id,
                ItemId = this.Item.Id,
                UserId = this.User.Id
            };
        }
    }
}
