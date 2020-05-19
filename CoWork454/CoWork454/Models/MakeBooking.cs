using System;
namespace CoWork454.Models
{
    public class MakeBooking
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string Date { get; set; }

        public string TimeStart { get; set; }

        public string TimeFinish { get; set; }

        public  ProductClass ProductClass{ get; set; }

    }
}
