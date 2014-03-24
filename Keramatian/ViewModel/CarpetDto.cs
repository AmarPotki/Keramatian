using System.Collections.Generic;
using System.Collections.ObjectModel;
using Keramatian.Models;

namespace Keramatian.ViewModel
{
    public class CarpetDto
    {
        public CarpetDto()
        {
            Sizes = new Collection<AssignedSizeData>();
        }
        public Carpet Carpet { get; set; }
        public virtual ICollection<AssignedSizeData> Sizes { get; set; }
    }
}