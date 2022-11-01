using ManagerEmployee.Database.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using ManagerEmployee.EfCore.Models;
using ManagerEmployee.Common.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using ManagerEmployee.Common.Common.TokenService;
namespace ManagerEmployeeApi.Controllers
{
    /// <summary>Đăng nhập quản lí nhân viên</summary>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IEmployeeService _employeeService = null; 
        private readonly ITokenServiceJWT _tokenService = null;
        private Security _security = null;
        public LoginController(IEmployeeService employeeService, Security security, ITokenServiceJWT tokenService, IConfiguration config)
        {
            _config = config;
            _employeeService = employeeService;
            _security = security;
            _tokenService = tokenService;
        }

        /// <summary>
        /// lấy danh sách nhân viên
        /// </summary>
        /// <returns>0-Lỗi ; Token-Thành công ; 2-Tài khoản không đúng; 3-Sai mật khẩu:</returns>
        [Route("/login")]
        [HttpPost()]
        public async Task<IActionResult> Login([FromBody] EmployeeLogin employeeLogin)
        {
            try
            {
                Employee employee = await _employeeService.GetLogin(employeeLogin.PhoneNumber, employeeLogin.Password);
                if(employee == null)
                {
                    return Ok(new JWTTokenResponse { Error = (int)LoginEnum.ERROR });
                }
                else
                {
                    /// Tạo token sau khi tài khoản | mật khẩu đúng
                    string Token = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), employee);
                    if (Token != null)
                    {
                        return Ok(new JWTTokenResponse { Token = Token, Username = employee.Fullname });
                    }
                    else
                    {
                        return (BadRequest());
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
