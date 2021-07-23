using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repository.IRepository
{
    public interface IUserRepository
    {
        bool insertUser(UserModel user);

        List<UserModel> listUsers();

        UserModel viewUser(int id);

        bool modifyUser(UserModel user);

        bool deleteUser(int id);

        int loginUser(string username, string password);
    }
}
