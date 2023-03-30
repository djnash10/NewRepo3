using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> GetAllUsers();
        Task<Users> GetUserById(int id);
        Task InsertUser(Users user);
        Task UpdateUser(Users user);
        Task DeleteUser(int id);
        Task<bool> CheckEmailExists(string email);
        
    }
}
