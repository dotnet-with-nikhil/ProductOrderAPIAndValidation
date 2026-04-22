namespace dotnet_example_clean_arch_with_entity_framework.DOTs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
