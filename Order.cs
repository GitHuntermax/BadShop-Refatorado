using System;
using System.Collections.Generic;

namespace BadShopRefatorado
{
    public class Order
    {
        public int Id { get; }
        public int ClientId { get; }
        public List<OrderItem> Items { get; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; }

        public Order(int id, int clientId)
        {
            Id = id;
            ClientId = clientId;
            Status = "New";
            CreatedAt = DateTime.Now;
            Items = new List<OrderItem>();
        }

        public double Total
        {
            get
            {
                double sum = 0;
                foreach (var i in Items)
                    sum += i.Total;
                return sum;
            }
        }
    }
}
