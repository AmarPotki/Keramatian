using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keramatian.BootstrapperTasks
{
    public interface IBootstrapTask
    {
        void Execute();
        int Priority { get; }
    }
}
