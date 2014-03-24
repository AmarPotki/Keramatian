namespace Keramatian.Models
{
    public class Catalog : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
    }
}