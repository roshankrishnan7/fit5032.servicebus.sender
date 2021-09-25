using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fit5032.order_sender.Models
{
    public class OrderMessage
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
    }
}
