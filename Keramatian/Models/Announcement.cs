using System.ComponentModel.DataAnnotations;

namespace Keramatian.Models
{
    public class Announcement : IEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summery { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string EnTitle { get; set; }
        public string EnSummery { get; set; }
        [DataType(DataType.MultilineText)]
        public string EnDescription { get; set; }

    }
}