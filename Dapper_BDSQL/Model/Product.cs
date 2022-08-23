namespace Dapper_BDSQL
{
    class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Price { get; set; }

        public Product()
        {
            Id = 0;
            Name = string.Empty;
            Category = string.Empty;
            Price = string.Empty;
        }

        public Product(int id, string name, string category, string price)
        {
            Id = id;
            Name = name;
            Category = category;
            Price = price;
        }

        public override string ToString()
        {
            return $"Name [{Name}], Category [{Category}], Price [{Price}]";
        }

    }
}
