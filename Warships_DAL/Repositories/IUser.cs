using System;
using System.Collections.Generic;
using System.Text;
using Warships_DAL.Models;

namespace Warships_DAL.Repositories
{
    public interface IUser
    {
        User Login(string login, string password);
        void ChangeAccountSpec(User u);

    }
}
