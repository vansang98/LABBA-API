using ManagerEmployee.EfCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Common.Common.TokenService
{
    /// <summary>Token JWT</summary>
    public interface ITokenServiceJWT
    {
        string BuildToken(string key, string issuer, Employee employee);
    }
}
