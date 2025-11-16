namespace BadShopRefatorado
{
    public class Client
    {
        public int Id { get; }
        public string Name { get; }
        public string Email { get; }
        public string Address { get; }

        public Client(int id, string name, string email, string address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }
    }
}
