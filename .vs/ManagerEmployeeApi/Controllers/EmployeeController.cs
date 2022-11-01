using Elasticsearch.Net;
using ManagerEmployee.Database.Repositorys;
using ManagerEmployee.Database.Services;
using ManagerEmployee.EfCore.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using Nest;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Serilog;
namespace ManagerEmployeeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>Quản lí nhân viên thêm sửa xóa</summary>
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService = null;
        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        #region
        /// <summary>
        /// lấy danh sách nhân viên theo điều kiện lọc
        /// </summary>   
        /// <param name="datafind"></param>
        /// <returns></returns>
        [HttpPost()]
        [Route("/getlist/")]
        public async Task<IActionResult> GetList([FromBody] FindData finddata)
        {
            try
            {
                DateEmployee employee = await _employeeService.GetFindData(finddata);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Lấy thông tin nhân viên băng Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet()]
        [Route("/getbyid/{id}")]
        public async Task<IActionResult> GetById([System.Web.Http.FromUri] Guid id)
        {
            try
            {
                Employee employee = await _employeeService.GetById(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>
        /// Thêm mới thông tin nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/addpost")]
        public async Task<IActionResult> AddPost([FromBody] Employee data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    byte result = await _employeeService.Insert(data);
                    return Ok(result);
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }
            return BadRequest();
        }

        /// <summary>
        /// Sửa dữ liệu nhân viên bằng ID
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/update/{id}")]
        public async Task<IActionResult> Update([System.Web.Http.FromUri] Guid id,[FromBody]  Employee data)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    byte result = await _employeeService.Update(data , id);
                    return Ok(result);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Xóa dữ liệu nhân viên bằng ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("/delete/{id}")]
        public async Task<IActionResult> Delete(Guid Id)
        {
            try
            {
                return Ok(await _employeeService.Delete(Id));

            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
        #endregion
    }
}
