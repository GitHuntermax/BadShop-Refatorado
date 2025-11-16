using System.Collections.Generic;

namespace BadShopRefatorado
{
    public sealed class Database
    {
        private static readonly System.Lazy<Database> _instance = new(() => new Database());
        public static Database Instance => _instance.Value;

        public List<Client> Clients { get; }
        public List<Product> Products { get; }
        public List<Order> Orders { get; }

        private Database()
        {
            Clients = new List<Client>();
            Products = new List<Product>();
            Orders = new List<Order>();
        }

        public void Seed()
        {
            Clients.Add(new Client(1, "Maria Silva", "maria@example.com", "Rua A, 123"));
            Clients.Add(new Client(2, "João Souza", "joao@example.com", "Av. B, 456"));
            Clients.Add(new Client(3, "Carlos Pereira", "carlos@example", "Travessa C"));

            Products.Add(new Product(1, "Caneta", 2.5, 100));
            Products.Add(new Product(2, "Caderno", 15.0, 50));
            Products.Add(new Product(3, "Mochila", 120.0, 10));
            Products.Add(new Product(4, "Estojo", 20.0, 0));
            Products.Add(new Product(5, "Caderno - Versão B", 15.0, 50));
        }

        public void Reset()
        {
            Clients.Clear();
            Products.Clear();
            Orders.Clear();
        }
    }
}
