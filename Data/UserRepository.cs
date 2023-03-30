using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnection _dbConnection;
        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task Delete(int id)
        {
            var sql = @" DELETE FROM Person
                         WHERE Id = @Id ";

            await _dbConnection.ExecuteAsync(sql,
            new
            {
                Id = id
            });
        }

        public async Task<IEnumerable<Users>> GetAll()
        {
            var sql = @" SELECT Id
                                ,FirstName 
                                ,LastName
                                ,Identification
                                ,Phone
                                ,Email
                                ,Password
                          FROM Person ";

            return await _dbConnection.QueryAsync<Users>(sql, new { });
        }

        public async Task<Users> GetDetails(int id)
        {
            var sql = @" SELECT Id
                                ,FirstName 
                                ,LastName
                                ,Identification
                                ,Phone
                                ,Email
                                ,Password
                          FROM Person 
                          WHERE Id = @Id";

            return await _dbConnection.QueryFirstOrDefaultAsync<Users>(sql, new { Id = id });
        }

        public async Task Insert(Users users)
        {
            var sql = @" INSERT INTO Person (FirstName, LastName, Identification, Phone, Email, Password) 
                         VALUES(@FirstName, @LastName, @Identification, @Phone, @Email, @Password) ";

            await _dbConnection.ExecuteAsync(sql,
            new
            {
                users.Id,
                users.FirstName,
                users.LastName,
                users.Identification,
                users.Phone,
                users.Email,
                users.Password
            });
        }

        public async Task Update(Users users)
        {
            var sql = @" UPDATE Person 
                            SET FirstName = @FirstName,
                                LastName = @LastName,
                                Identification = @Identification,
                                Phone = @Phone,
                                Email = @Email,
                                Password = @Password
                            WHERE Id = @Id ";

            await _dbConnection.ExecuteAsync(sql,
            new
            {
                users.Id,
                users.FirstName,
                users.LastName,
                users.Identification,
                users.Phone,
                users.Email,
                users.Password
            });
        }
    }
}
