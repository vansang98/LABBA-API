using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Common.Common
{
    public enum Validator
    {
        ERROR = 0,
        SUCCESS = 1,
        ERROR_EMAIL = 2,
        ERROR_PHONENUMBER = 3
    }
    public enum Gender
    {
        NULL = 0,
        MALE = 1,
        FEMALE = 2,
        MALE_FEMALE = 3,
    }
    public enum LoginEnum
    {
        ERROR = 2,
        SUCCESS = 1,
    }
}
