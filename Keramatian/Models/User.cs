namespace Keramatian.Models
{
    public class User : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual Role Role { get; set; }

    }
}