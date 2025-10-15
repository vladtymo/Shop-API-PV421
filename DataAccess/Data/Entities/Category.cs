namespace DataAccess.Data.Entities
{
    public class Category : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // ----- navigation properties
        public ICollection<Product>? Products { get; set; }
    }
}
