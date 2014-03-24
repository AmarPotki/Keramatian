using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Keramatian.Models
{
    public class Role :IEntity
    {
        public int Id { get;  set; }
        public string Name { get; set; }

    }
}