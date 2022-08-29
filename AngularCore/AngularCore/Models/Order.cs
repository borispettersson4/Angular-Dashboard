using System;

namespace Core.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public virtual Customer Customer { get; set; }
        public decimal Total { get; set; }
        public DateTime Placed { get; set; }
        public DateTime? Fulfilled { get; set; }
    }
}