using System.Text;

namespace BadShopRefatorado
{
    public class InvoiceGenerator
    {
        public string Generate(Order order)
        {
            var db = Database.Instance;
            var sb = new StringBuilder();

            var client = db.Clients.Find(c => c.Id == order.ClientId);

            sb.AppendLine("FATURA");
            sb.AppendLine($"Pedido: {order.Id}");
            sb.AppendLine($"Cliente: {client?.Name ?? "Desconhecido"}");
            sb.AppendLine($"Data: {order.CreatedAt}");

            double subtotal = 0;

            foreach (var item in order.Items)
            {
                var product = db.Products.Find(p => p.Id == item.ProductId);
                string name = product != null ? product.Name : "Desconhecido";
                double line = item.UnitPrice * item.Quantity;
                subtotal += line;
                sb.AppendLine($"{name} - {item.Quantity} x {item.UnitPrice} = {line}");
            }

            double tax = subtotal * 0.2;
            double total = subtotal + tax;

            sb.AppendLine($"Impostos: {tax}");
            sb.AppendLine($"TOTAL: {total}");

            return sb.ToString();
        }
    }
}
