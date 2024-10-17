using AutoMapper;
using Halda.BusinessLogic.Services.Interface.ICompany;
using Halda.Core.Models;
using Halda.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Halda.DataAccess.Repositories;

namespace Halda.BusinessLogic.Services.Repository.Company
{

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;   
        }
        public async Task<bool> CreateUser(UserDTO userDto, CancellationToken token)
        {
            try
            {
                var data = _mapper.Map<User>(userDto);

                // Add the user asynchronously
                await _unitOfWork.userRepository.AddAsync(data);

                // Save changes asynchronously with cancellation support
                await _unitOfWork.Save(token);

                return true;
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation
                return false;
            }
            catch (Exception ex)
            {
                // Log the exception if needed
                return false;
            }
        }

        public Task<bool> DelteUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserDTO> GetUser(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDTO>> GetUsersByComid(string comid)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(UserDTO userDto)
        {
            throw new NotImplementedException();
        }
    }
}
