using System.Collections.Generic;
using Keramatian.ViewModel;

namespace Keramatian.Services
{
    public interface ILeatherCarpetService
    {
        void SaveCarpet(LeatherCarpetDto leatherCarpetDto);
        List<AssignedSizeData> PopulateSizeData();
        LeatherCarpetDto GenerateCarpetDtoForEditForm(int leatherCarpetId);
        void UpdateCarpet(LeatherCarpetDto leatherCarpetDto);
    }
}
