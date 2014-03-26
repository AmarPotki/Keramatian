using System.ComponentModel.DataAnnotations;

namespace Keramatian.Models
{
    public class Design : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DataType(DataType.MultilineText)]
        public string History { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public bool IsCarpet { get; set; }
        public int? Priority { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}