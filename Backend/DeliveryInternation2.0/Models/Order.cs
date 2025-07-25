﻿using System.Text.Json.Serialization;
using DeliveryInternation2._0.Models.Enums;

namespace DeliveryInternation2._0.Models
{
    public class Order
    {
        public Guid id { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DeliveryTime { get; set; }
        public DateTime? OrderTime { get; set; }
        public decimal Price { get; set; } 
        public string Address { get; set; } = string.Empty;
        public Status Status { get; set; }
        public Cart Cart { get; set; } = new Cart();
        public List<DishInOrder> DishInOrders { get; set; } = new();
    }
}
