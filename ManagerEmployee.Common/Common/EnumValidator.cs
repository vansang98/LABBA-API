using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Common.Common
{
    /// <summary>Enum dùng cho validate dữ liệu</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public enum Validator
    {
        ERROR = 0,
        SUCCESS = 1,
        ERROR_EMAIL = 2,
        ERROR_PHONENUMBER = 3
    }

    /// <summary>Enum dùng cho giới tính</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public enum Gender
    {
        NULL = 0,
        MALE = 1,
        FEMALE = 2,
        MALE_FEMALE = 3,
    }

    /// <summary>Enum dùng cho đăng nhập</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public enum LoginEnum
    {
        ERROR = 2,
        SUCCESS = 1,
    }
}
