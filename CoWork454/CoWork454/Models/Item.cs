using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWork454.Models
{
    public class Item
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Details { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
