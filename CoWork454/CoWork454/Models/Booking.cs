using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoWork454.Models
{
    public class Booking
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public DateTimeOffset Date_start { get; set; }

        public DateTimeOffset Date_end { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
    }
}
