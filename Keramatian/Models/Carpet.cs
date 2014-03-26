using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Keramatian.Models
{
    public class Carpet : IEntity
    {
        public Carpet()
        {
            Sizes = new List<Size>();
        }
        public int Id { get; set; }
        public string Code { get; set; }
        public virtual BackgroundColor BackgroundColor { get; set; }

        public string OldDesign { get; set; }
        public virtual Grade Grade { get; set; }

        public virtual Plain Plain { get; set; }
        public string BarCode { get; set; }
        public string ImageName { get; set; }
        public string ImagePath { get; set; }
        public string HighQualityImagePath { get; set; }
        public int? Priority { get; set; }
        public  List<Size> Sizes { get; set; }
        public Design Design { get; set; }



    }
}