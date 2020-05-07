using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWork454.Models
{
    public class Order
    {

        public int Id { get; set; }
        public string TransactionNumber { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; }
        public virtual ICollection<Booking> Bookings { get; set; }

    }
}
