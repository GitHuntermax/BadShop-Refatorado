using System;
using System.Collections.Generic;

namespace BadShopRefatorado
{
    class Program
    {
        static void Main()
        {
            var db = Database.Instance;
            db.Seed();

            var manager = new OrderManager();
            var report = new ReportService();

            var p1 = manager.CreateOrder(1, new List<(int, int)> { (1, 2), (2, 1) }, 0.1);
            var p2 = manager.CreateOrder(2, new List<(int, int)> { (3, 1), (4, 2) }, 10);
            var p3 = manager.CreateOrder(3, new List<(int, int)> { (999, 1) });

            if (p1 != null)
            {
                manager.UpdateStatus(p1.Id, "Shipped");
                manager.UpdateStatus(p1.Id, "Processing");
            }

            Console.WriteLine(report.GenerateClientReport());
            Console.WriteLine(report.GenerateSalesReport());

            manager.PrintOrdersForClient(1);

            manager.CreateOrder(1, new List<(int, int)> { (3, 20) });

            Console.WriteLine("Execução finalizada.");
        }
    }
}
