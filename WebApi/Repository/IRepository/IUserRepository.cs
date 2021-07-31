using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    public interface IUserRepository
    {
        bool InsertUser(UserModel user);

        List<UserModel> ListUsers();

        UserModel ViewUser(int id);

        bool ModifyUser(UserModel user);

        bool DeleteUser(int id);

        int LoginUser(string username, string password);
    }
}
