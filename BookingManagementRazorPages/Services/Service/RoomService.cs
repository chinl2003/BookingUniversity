using Repositories.Entities;
using Repositories.IRepository;
using Services.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Service
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RoomService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public List<Room> GetRooms()
        {
            var approvedBookingDetailIds = _unitOfWork.ApprovalHistoryRepository.Entities
                .Where(ah => ah.ApprovalByManager == true)
                .Select(ah => ah.BookingDetailId)
                .Distinct()
                .ToList();

            var roomIds = _unitOfWork.BookingDetailRepository.Entities
                .Where(bd => approvedBookingDetailIds.Contains(bd.Id) && bd.DeletedAt == null)
                .Select(bd => bd.RoomId)
                .Distinct()
                .ToList();

            List<Room> room = new List<Room>();

            if (roomIds.Count == 0)
            {
                room = _unitOfWork.RoomRepository.Entities
                    .Where(r => r.DeletedAt == null)
                    .ToList();
            }
            else
            {
                room = _unitOfWork.RoomRepository.Entities
                    .Where(r => roomIds.Contains(r.Id) && r.DeletedAt == null)
                    .ToList();
            }

            return room;
        }

    }
}
