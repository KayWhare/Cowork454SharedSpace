using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoWork454.Models
{
    public class Product
    {
        public int Id { get; set; }

        public virtual Item Item { get; set; }

        public bool isAvailable { get; set; }

        public string Date_available { get; set; }


        [JsonIgnore]
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
