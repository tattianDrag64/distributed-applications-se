using BaseLibrary.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerLibrary.Repositories
{
    public interface IAuthentication
    {
        Task<bool> ValidateUser(LoginDTO userForAuth);
        Task<string> CreateToken();
    }
}
