using System;
using System.Collections.Generic;

namespace BadShopRefatorado
{
    public static class Factory
    {
        public static Order CreateOrder(int id, int clientId) => new Order(id, clientId);
        public static OrderItem CreateOrderItem(int productId, int qty, double price) => new OrderItem(productId, qty, price);
    }

    public class OrderManager
    {
        private readonly INotificationService notifier;
        private readonly InvoiceGenerator invoiceGen;

        public OrderManager()
        {
            notifier = new EmailNotificationService();
            invoiceGen = new InvoiceGenerator();
        }

        public Order CreateOrder(int clientId, List<(int productId, int qty)> items, double discountInput = 0)
        {
            var db = Database.Instance;

            var client = db.Clients.Find(c => c.Id == clientId);
            if (client == null)
            {
                Console.WriteLine("Cliente não encontrado.");
                return null!;
            }

            var order = Factory.CreateOrder(db.Orders.Count + 1, clientId);

            foreach (var it in items)
            {
                var prod = db.Products.Find(p => p.Id == it.productId);

                if (prod == null)
                {
                    order.Items.Add(Factory.CreateOrderItem(it.productId, it.qty, 1.0));
                }
                else
                {
                    if (prod.Stock < it.qty)
                        Console.WriteLine($"Atenção: Estoque insuficiente para {prod.Name}. Continuando mesmo assim.");

                    prod.ReduceStock(it.qty);
                    order.Items.Add(Factory.CreateOrderItem(prod.Id, it.qty, prod.Price));
                }
            }

            double total = order.Total;

            var strategy = DiscountFactory.GetStrategy(discountInput);
            double finalTotal = strategy.ApplyDiscount(total, discountInput);

            db.Orders.Add(order);

            string invoice = invoiceGen.Generate(order);
            notifier.Notify(client.Email, "Sua fatura", invoice);

            return order;
        }

        public void UpdateStatus(int orderId, string newStatus)
        {
            var db = Database.Instance;
            var order = db.Orders.Find(o => o.Id == orderId);
            if (order == null) return;

            order.Status = newStatus;

            var client = db.Clients.Find(c => c.Id == order.ClientId);
            if (client != null)
                notifier.Notify(client.Email, "Status do pedido atualizado", $"Pedido {order.Id} agora está {order.Status}");
        }

        public void PrintOrdersForClient(int clientId)
        {
            var db = Database.Instance;

            Console.WriteLine($"Pedidos do cliente {clientId}:");

            foreach (var o in db.Orders)
            {
                if (o.ClientId != clientId) continue;
                Console.WriteLine($"Pedido {o.Id} - Total {o.Total} - Status {o.Status}");
            }
        }
    }
}
