using Halda.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Halda.BusinessLogic.Services.Interface.ICompany
{
    public interface IUserService
    {
        Task<bool> CreateUser(UserDTO userDto, CancellationToken token);
        Task<bool> UpdateUser(UserDTO userDto);
        Task<bool> DelteUser(string id);
        Task<UserDTO> GetUser(string id); 
        Task<IEnumerable<UserDTO>> GetUsersByComid(string comid);
    }
}
