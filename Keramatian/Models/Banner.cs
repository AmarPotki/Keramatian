namespace Keramatian.Models
{
    public class Banner : IEntity
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        public string EnImagePath { get; set; }


    }
}