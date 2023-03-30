using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> GetAll();
        Task<Users> GetDetails(int id);
        Task Insert(Users users);
        Task Update(Users users);
        Task Delete(int id);
    }
}
