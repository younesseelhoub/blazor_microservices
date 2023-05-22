namespace ProductUser.Microservices.Model
{
    public class Product
    {
        internal int id;

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Price { get; set; }
        public int StockQuantity { get; set; }
    }
}
