using ManagerEmployee.EfCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Common.Common.TokenService
{
    /// <summary>Token JWT</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public interface ITokenServiceJWT
    {
        /// <summary>Tạo chuỗi Token</summary>
        /// <param name="key">The key.</param>
        /// <param name="issuer">The issuer.</param>
        /// <param name="employee">The employee.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        string BuildToken(string key, string issuer, Employee employee);
    }
}
