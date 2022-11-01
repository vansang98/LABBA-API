using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.EfCore.Models
{
    public class JWTTokenResponse
    {
        /// <summary> /// Chuỗi token /// </summary>
        public string? Token { get; set; }

        /// <summary> /// Mã lỗi /// </summary>
        public int? Error { get; set; }

        /// <summary> /// Mã lỗi /// </summary>
        public string? Username { get; set; }

    }
}
