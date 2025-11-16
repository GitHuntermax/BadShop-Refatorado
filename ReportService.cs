using System.Text;

namespace BadShopRefatorado
{
    public class ReportService
    {
        public string GenerateClientReport()
        {
            var db = Database.Instance;
            var sb = new StringBuilder();

            sb.AppendLine("=== Relatório de Clientes ===");
            foreach (var c in db.Clients)
                sb.AppendLine($"{c.Id} - {c.Name} - {c.Email}");

            return sb.ToString();
        }

        public string GenerateSalesReport()
        {
            var db = Database.Instance;
            var sb = new StringBuilder();

            sb.AppendLine("=== Relatório de Vendas ===");
            foreach (var o in db.Orders)
                sb.AppendLine($"Pedido {o.Id} - Cliente {o.ClientId} - Total {o.Total}");

            return sb.ToString();
        }
    }
}
