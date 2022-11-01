using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.EfCore.Models
{
    /// <summary>Dữ liêu Token trả ra khi đăng nhập vào API</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public class JWTTokenResponse
    {
        /// <summary> /// Chuỗi token /// </summary>
        public string? Token { get; set; }

        /// <summary> /// Mã lỗi /// </summary>
        public int? Error { get; set; }

        /// <summary> /// Tên tài khoản /// </summary>
        public string? Username { get; set; }

    }
}
