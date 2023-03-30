using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    internal class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<bool> CheckEmailExists(string email)
        {
            var users = await _userRepository.GetAll();

            return users.Any(u => u.Email.ToLower() == email.ToLower());
        }

        public async Task DeleteUser(int id)
        {
            {
                // Validar que el usuario a eliminar exista en la base de datos
                if (await GetUserById(id) == null)
                {
                    throw new ArgumentException("user not found");
                }

                await _userRepository.Delete(id);
            }
        }

        public async Task<IEnumerable<Users>> GetAllUsers()
        {
            {
                try
                {
                    return await _userRepository.GetAll();
                }
                catch (Exception ex)
                {
                    throw new Exception("An error occurred while retrieving the list of users.", ex);
                }
            }
        }

        public async Task<Users> GetUserById(int id)
        {
            {
                try
                {
                    var user = await _userRepository.GetDetails(id);
                    if (user == null)
                    {
                        throw new Exception($"User with id {id} not found.");
                    }
                    return user;
                }
                catch (Exception ex)
                {
                    throw new Exception($"An error occurred while retrieving user with id {id}.", ex);
                }
            }
        }

        public async Task InsertUser(Users user)
        {
            if (await CheckEmailExists(user.Email))
            {
                throw new Exception("Email already exists");
            }

            await _userRepository.Insert(user);
        }

        public async Task UpdateUser(Users user)
        {
            {
                // Validar que el usuario a actualizar exista en la base de datos
                if (await GetUserById(user.Id) == null)
                {
                    throw new ArgumentException("user not found");
                }

                await _userRepository.Update(user);
            }
        }
    }
}
