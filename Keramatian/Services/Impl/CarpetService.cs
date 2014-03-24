using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Keramatian.Models;
using Keramatian.Repository;
using System.Collections;
using Keramatian.ViewModel;

namespace Keramatian.Services.Impl
{
    public class CarpetService : ICarpetService
    {
        private readonly ICarpetRepository _carpetRepository;
        private readonly ISizeRepository _sizeRepository;
        public CarpetService(ICarpetRepository carpetRepository, ISizeRepository sizeRepository)
        {
            _carpetRepository = carpetRepository;
            _sizeRepository = sizeRepository;
        }

        public void SaveCarpet(CarpetDto carpetDto)
        {
            var carpet = carpetDto.Carpet;


            foreach (var assignedSizeData in carpetDto.Sizes.Where(ass => ass.Assigned))
            {
                var size = new Size { Id = assignedSizeData.SizeId };
                _sizeRepository.Attach(size);
                carpet.Sizes.Add(size);
            }
            _carpetRepository.Add(carpet);
        }

        public ICollection<AssignedSizeData> PopulateSizeData()
        {
            var assignedCourses = _sizeRepository.All().Select(size => new AssignedSizeData
                                                                           {
                                                                               SizeName = size.Name,
                                                                               SizeId = size.Id,
                                                                               Assigned = false,
                                                                           }).ToList();
            return assignedCourses;
        }

        public CarpetDto GenerateCarpetDtoForEditForm(int carpetId)
        {
            var carpet = _carpetRepository.GetCarpetWithSize(carpetId);
            ICollection<AssignedSizeData> assignedSizeDatas = new Collection<AssignedSizeData>();
            foreach (var size in _sizeRepository.All())
            {
                assignedSizeDatas.Add(new AssignedSizeData { SizeId = size.Id, SizeName = size.Name, Assigned = carpet.Sizes.Contains(size) });
            }
            return new CarpetDto { Carpet = carpet, Sizes = assignedSizeDatas };

        }

        public void UpdateCarpet(CarpetDto carpetDto)
        {
            var carpet = _carpetRepository.GetCarpetWithSize(carpetDto.Carpet.Id);
            foreach (var assignedSizeData in carpetDto.Sizes)
            {
                if (assignedSizeData.Assigned)
                {
                    if (!carpet.Sizes.Any(x => x.Id == assignedSizeData.SizeId))
                    {
                        var size = new Size { Id = assignedSizeData.SizeId };
                        _sizeRepository.Attach(size);
                        carpet.Sizes.Add(size);
                    }
                }
                else
                {

                    if (carpet.Sizes.Any(x => x.Id == assignedSizeData.SizeId))
                    {
                        var size = _sizeRepository.ById(assignedSizeData.SizeId);

                        carpet.Sizes.Remove(size);
                    }
                }

            }

            carpet.BackgroundColor = carpetDto.Carpet.BackgroundColor;
            carpet.BarCode = carpetDto.Carpet.BarCode;
            carpet.Code = carpetDto.Carpet.Code;
            carpet.Design = carpetDto.Carpet.Design;
            carpet.Grade = carpetDto.Carpet.Grade;
            carpet.Plain = carpetDto.Carpet.Plain;
            carpet.Priority = carpetDto.Carpet.Priority;

            _carpetRepository.Update(carpet);
        }
    }
}