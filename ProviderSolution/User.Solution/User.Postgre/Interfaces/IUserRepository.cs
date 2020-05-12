using System;
using System.Collections.Generic;
using System.Text;

namespace User.Postgre.Interfaces
{
    public interface IUserRepository
    {
        Models.User GetUserById(int id);
    }
}
