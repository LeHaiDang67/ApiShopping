using System;
using System.Collections.Generic;

namespace ShopeeFood.Models
{
    public partial class TodoItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsComplete { get; set; }
    }
}
