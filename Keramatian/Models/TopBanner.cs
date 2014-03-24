namespace Keramatian.Models
{
    public class TopBanner:IEntity
    {
        public int Id { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }

        // english area
        public string EnImagePath { get; set; }
    }
}