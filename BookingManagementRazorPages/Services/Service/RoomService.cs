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
            List<Room> room = new List<Room>();
            room = _unitOfWork.RoomRepository.Entities
                .Where(r => r.DeletedAt == null)
                .ToList();
            return room;
        }
    }
}
