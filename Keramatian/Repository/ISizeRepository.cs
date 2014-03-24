using Keramatian.Models;

namespace Keramatian.Repository
{
    public interface ISizeRepository : IRepository<Size>
    {
        void Attach(Size size);
    }
}
