using System;
using System.Collections.Generic;

namespace ShopeeFood.Models
{
    public partial class Order
    {
        public Order()
        {
            Products = new HashSet<Product>();
        }

        public int Id { get; set; }
        public string UserName { get; set; } = null!;
        public DateTime CreatedDate { get; set; }
        public int TotalPrice { get; set; }
        public int UserId { get; set; }

        public virtual User User { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
