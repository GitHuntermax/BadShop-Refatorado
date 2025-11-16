using System;

namespace BadShopRefatorado
{
    public interface INotificationService
    {
        void Notify(string to, string subject, string message);
    }

    public class EmailNotificationService : INotificationService
    {
        public void Notify(string to, string subject, string message)
        {
            Console.WriteLine("=== EMAIL ===");
            Console.WriteLine($"Para: {to}");
            Console.WriteLine($"Assunto: {subject}");
            Console.WriteLine(message);
        }
    }
}
