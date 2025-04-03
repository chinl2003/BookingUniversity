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
    public class UserService:IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public User? GetAccountMemberByEmail(string email, string password)
        {
            return _unitOfWork.UserRepository.Entities
                .FirstOrDefault(p => p.Email != null
                     && p.Email.Equals(email)
                     && p.Password != null
                     && p.Password.Equals(password));
        }
    }
}
