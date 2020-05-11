using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoWork454.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string CompanyName { get; set; }
        public string Phone { get; set; }

        public string Date_registered { get; set; }
        public string PasswordHash { get; set; }

        public UserRole UserRole { get; set; }

        public virtual ICollection<Order> UserOrders { get; set; }
    }
}
