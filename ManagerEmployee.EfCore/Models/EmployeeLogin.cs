using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.EfCore.Models
{
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
