using System.Collections.Generic;
using System.Collections.ObjectModel;
using Keramatian.Models;

namespace Keramatian.ViewModel
{
    public class LeatherCarpetDto
    {
        public LeatherCarpetDto()
        {
            Sizes = new Collection<AssignedSizeData>();
        }
        public LeatherCarpet LeatherCarpet { get; set; }
        public virtual ICollection<AssignedSizeData> Sizes { get; set; }
    }
}