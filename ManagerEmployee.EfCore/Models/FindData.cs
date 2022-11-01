using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.EfCore.Models
{
    public partial class FindData
    {
        /// <summary>  Ngày sinh từ ngày /// </summary>
        public DateTime? valueBirthdayFrom { get; set; }

        /// <summary>  Ngày sinh đến ngày /// </summary>
        public DateTime? valueBirthdayTo { get; set; } 

        /// <summary>  Giới tính Nhân Viên /// </summary>
        public int? gender { get; set; }

        /// <summary>  Email hoặc họ tên nhân viên/// </summary>
        public string? dataFind { get; set; }

        /// <summary>  Email hoặc họ tên nhân viên/// </summary>
        public int skip { get; set; }

        /// <summary>  Email hoặc họ tên nhân viên/// </summary>
        public int take { get; set; }

    }
}
