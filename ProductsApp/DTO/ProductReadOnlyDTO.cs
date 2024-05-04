namespace ProductsApp.DTO
{
    public class ProductReadOnlyDTO: BaseDTO
    {
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
        public string? Colour { get; set; }
    }
}
