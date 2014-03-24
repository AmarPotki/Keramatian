using System.Collections.Generic;

namespace Keramatian.Models
{
    public class Size : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual List<Carpet> Carpets { get; set; }
        public virtual List<LeatherCarpet> LeatherCarpets { get; set; }

    }
}