using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using Keramatian.Models;
using Keramatian.Repository;
using Keramatian.ViewModel;

namespace Keramatian.Services.Impl
{
    public class LeatherCarpetService : ILeatherCarpetService
    {
        private readonly ILeatherCarpetRepository _leatherCarpetRepository;
        private readonly ISizeRepository _sizeRepository;
        public LeatherCarpetService(ILeatherCarpetRepository leatherCarpetRepository, ISizeRepository sizeRepository)
        {
            _leatherCarpetRepository = leatherCarpetRepository;
            _sizeRepository = sizeRepository;
        }
        public void SaveCarpet(LeatherCarpetDto leatherCarpetDto)
        {
            var leatherCarpet = leatherCarpetDto.LeatherCarpet;


            foreach (var assignedSizeData in leatherCarpetDto.Sizes.Where(ass => ass.Assigned))
            {
                var size = new Size { Id = assignedSizeData.SizeId };
                _sizeRepository.Attach(size);
                leatherCarpet.Sizes.Add(size);
            }
            _leatherCarpetRepository.Add(leatherCarpet);
        }

        public List<AssignedSizeData> PopulateSizeData()
        {
            var assignedCourses = _sizeRepository.All().Select(size => new AssignedSizeData
            {
                SizeName = size.Name,
                SizeId = size.Id,
                Assigned = false,
            }).ToList();
            return assignedCourses;
        }

        public LeatherCarpetDto GenerateCarpetDtoForEditForm(int leatherCarpetId)
        {
            var leatherCarpet = _leatherCarpetRepository.GetCarpetWithSize(leatherCarpetId);
            ICollection<AssignedSizeData> assignedSizeDatas = new Collection<AssignedSizeData>();
            foreach (var size in _sizeRepository.All())
            {
                assignedSizeDatas.Add(new AssignedSizeData { SizeId = size.Id, SizeName = size.Name, Assigned = leatherCarpet.Sizes.Contains(size) });
            }
            return new LeatherCarpetDto { LeatherCarpet = leatherCarpet, Sizes = assignedSizeDatas };
        }

        public void UpdateCarpet(LeatherCarpetDto leatherCarpetDto)
        {
            var leatherCarpet = _leatherCarpetRepository.GetCarpetWithSize(leatherCarpetDto.LeatherCarpet.Id);
            foreach (var assignedSizeData in leatherCarpetDto.Sizes)
            {
                if (assignedSizeData.Assigned)
                {
                    if (!leatherCarpet.Sizes.Any(x => x.Id == assignedSizeData.SizeId))
                    {
                        var size = new Size { Id = assignedSizeData.SizeId };
                        _sizeRepository.Attach(size);
                        leatherCarpet.Sizes.Add(size);
                    }
                }
                else
                {

                    if (leatherCarpet.Sizes.Any(x => x.Id == assignedSizeData.SizeId))
                    {
                        var size = _sizeRepository.ById(assignedSizeData.SizeId);

                        leatherCarpet.Sizes.Remove(size);
                    }
                }

            }

            leatherCarpet.BackgroundColor = leatherCarpetDto.LeatherCarpet.BackgroundColor;
            leatherCarpet.BarCode = leatherCarpetDto.LeatherCarpet.BarCode;
            leatherCarpet.Code = leatherCarpetDto.LeatherCarpet.Code;
            leatherCarpet.Design = leatherCarpetDto.LeatherCarpet.Design;
            leatherCarpet.Grade = leatherCarpetDto.LeatherCarpet.Grade;
            leatherCarpet.Plain = leatherCarpetDto.LeatherCarpet.Plain;
            leatherCarpet.Priority = leatherCarpetDto.LeatherCarpet.Priority;
            _leatherCarpetRepository.Update(leatherCarpet);
        }
    }
}