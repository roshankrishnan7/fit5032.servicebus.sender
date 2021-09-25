using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace fit5032.order_sender.Models
{
    public class OrderViewModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        [Display(Name = "Product")]
        public string ProductName { get; set; }

        [Required]
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
