using System.Collections.Generic;
using Keramatian.Models;
using Keramatian.ViewModel;

namespace Keramatian.Services
{
    public interface ICarpetService
    {
        void SaveCarpet(CarpetDto carpetDto);
        ICollection<AssignedSizeData> PopulateSizeData();
        CarpetDto GenerateCarpetDtoForEditForm(int carpetId);
        void UpdateCarpet(CarpetDto carpetDto);
    }
}
