using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.EfCore.Models
{
    /// <summary>Thông tin đăng nhập của nhân viên</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public partial class EmployeeLogin
    {
        /// <summary>  Tên đăng nhập /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>  Mật khẩu /// </summary>
        public string? Password { get; set; }

        /// <summary>  /// </summary>
        public string? role { get; set; }
    }
}
