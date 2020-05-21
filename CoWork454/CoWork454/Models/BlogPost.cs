using System;

namespace CoWork454.Models
{

    public class BlogPost
    {

        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTimeOffset PostDate { get; set; }

        public string Title { get; set; }

        public string Blurb { get; set; }

        public string Article { get; set; }

        public string ImageUrl { get; set; }

        public bool IsFeatured { get; set; }


        public virtual User User{ get; set; }


    }
}
