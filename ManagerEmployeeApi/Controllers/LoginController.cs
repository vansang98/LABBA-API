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
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        /// <summary>Configuration</summary>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        private readonly IConfiguration _config;

        /// <summary>Employee service</summary>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        private readonly IEmployeeService _employeeService = null;

        /// <summary>Token service</summary>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        private readonly ITokenServiceJWT _tokenService = null;
        public LoginController(IEmployeeService employeeService, ITokenServiceJWT tokenService, IConfiguration config)
        {
            _config = config;
            _employeeService = employeeService;
            _tokenService = tokenService;
        }

        /// <summary>Đăng nhập thông tin nhân viên</summary>
        /// <param name="employeeLogin">The employee login.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
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
