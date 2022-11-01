using ManagerEmployee.Common.Common;
using ManagerEmployee.EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;

namespace ManagerEmployee.Database.Services
{
    /// <summary>Nhân viên quản lý thêm sửa xóa</summary>
    public class EmployeeService : IEmployeeService 
    {
        private List<Employee> context;
        private DbSet<Employee> data = null;
        private BALabTestContext _context = null;
        private Security security = null;
        private readonly Microsoft.Extensions.Logging.ILogger<EmployeeService> _logger;
        private IValidator<Employee> _validator;
        public EmployeeService()
        {
            this._context = new BALabTestContext();
            data = _context.Set<Employee>();
        }
        public EmployeeService(BALabTestContext _context , Security _security, ILogger<EmployeeService> logger, IValidator<Employee> validator)
        {
            this._context = _context;
            data = _context.Set<Employee>();
            security = _security;
            this._logger = logger;
            _validator = validator;
        }

        /// <summary>
        /// lấy thông tin nhân viên để đăng nhập
        /// </summary>
        /// <param name="Phonenumber"></param>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<Employee> GetLogin(string Phonenumber, string Password)
        {
            var login = new Employee();
            try
            {
                login = _context.Employees.First(p => (p.Phonenumber == Phonenumber || p.Email == Phonenumber) && (p.Password == security.ConvertString(Password)));
            }
            catch(Exception ex)
            {
                _logger.LogError($"Error:{ex.Message},{ex.StackTrace}");
                login = null;
            }
            return login;
         }

        /// <summary>
        /// lấy bản ghi theo điều kiện lọc
        /// </summary>
        /// <param name="datafind"></param>
        /// <returns></returns>
        public async Task<DateEmployee> GetFindData(FindData filter)
        {
            _logger.LogError("LOggg");

            try
            {
                // truy vấn
                IEnumerable<Employee> list = _context.Employees.Where(x => x.Isdelete == false);

                if (!string.IsNullOrEmpty(filter.dataFind))
                {
                    list = list.Where(x => x.Email.Contains(filter.dataFind.Trim()) || x.Fullname.Contains(filter.dataFind.Trim()));
                }

                if (filter.gender != (int)Gender.NULL)
                {
                    list = list.Where(x => x.Gender == filter.gender);
                }

                if (filter.valueBirthdayFrom != null)
                {
                    list = list.Where(x => x.Birthday > filter.valueBirthdayFrom);
                }

                if (filter.valueBirthdayTo != null)
                {
                    list = list.Where(x => x.Birthday < filter.valueBirthdayTo);
                }
                /// phân trang
                var result = new DateEmployee();
                result.employee = list.OrderByDescending(x => x.Creattime).Skip(filter.skip).Take(filter.take).ToList();
                result.totalCount = list.Count();
                result.totalPages = list.Count();
                return result;
            }
            catch(Exception ex) 
            {
                _logger.LogError($"Error:{ex.Message},{ex.StackTrace}");
                return null;
            }
         }

        /// <summary>
        /// lấy bản ghi theo ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<Employee> GetById(Guid Id)
        {
            var employee = new Employee();
            try
            {
                employee = _context.Employees.First(c => (c.Id == Id)&(c.Isdelete == false));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error:{ex.Message},{ex.StackTrace}");
            }
            return employee;
        }

        /// <summary>
        /// true: Tồn tại bản ghi ;  false: Chưa tồn tại bản ghi
        /// </summary>
        /// <param name="Email"></param>
        /// <returns></returns>
        public async Task<bool> CheckEmail(string Email,bool InsertOrUpdate,Guid Id)
        {
            var findEmail = new Employee();
            if (InsertOrUpdate)
            {
                findEmail = _context.Employees.FirstOrDefault(c => (c.Email == Email) & (c.Isdelete != true));
            }
            else
            {
                findEmail = _context.Employees.FirstOrDefault(c => (c.Email == Email) & (c.Isdelete != true) & (c.Id != Id));
            }
            return findEmail != null ? true : false;
        }

        /// <summary>
        /// true: Tồn tại bản ghi ;  false: Chưa tồn tại bản ghi
        /// </summary>
        /// <param name="Phonenumber"></param>
        /// <returns></returns>
        public async Task<bool> CheckPhoneNumber(string Phonenumber, bool InsertOrUpdate,Guid Id)
        {
            var findPhonenumber = new Employee();
            if(InsertOrUpdate)
            {
                findPhonenumber = _context.Employees.FirstOrDefault(c => (c.Phonenumber == Phonenumber) & (c.Isdelete != true));
            }
            else
            {
                findPhonenumber = _context.Employees.FirstOrDefault(c => (c.Phonenumber == Phonenumber) & (c.Isdelete != true) & (c.Id != Id));
            }
            return findPhonenumber != null ? true : false;
        }

        /// <summary>
        /// 0: Thêm lỗi ; 1: Thêm thành công ; 2 : Trùng Email ; 3: Trùng SDT
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<byte> Insert(Employee employee)
        {
            byte bl = (byte)Validator.ERROR;
            bool Valid = false;
            /// Dữ liệu đầu vào bị null trả ra kết quả thêm lỗi {0}
            if (employee == null) return bl;
            /// Validate
            ValidationResult result = await _validator.ValidateAsync(employee);
            if (!result.IsValid)
            {
                bl = (byte)Validator.ERROR;
            }
            try
            {
                ///check trùng email
                if (employee.Email != "" && (await CheckEmail(employee.Email,true,new Guid())))
                {
                    Valid = true;
                    bl = (byte)Validator.ERROR_EMAIL;
                }
                ///check trùng số điện thoại
                if (!Valid && (await CheckPhoneNumber(employee.Phonenumber, true , new Guid())))
                {
                    Valid = true;
                    bl = (byte)Validator.ERROR_PHONENUMBER;
                }
                if (!Valid)
                { 
                    Employee dataInsert = new Employee();
                    dataInsert.Fullname = employee.Fullname;
                    dataInsert.Username = employee.Phonenumber;
                    dataInsert.Password = security.ConvertString(employee.Password);
                    dataInsert.Email = employee.Email;
                    dataInsert.Birthday = employee.Birthday;
                    dataInsert.Gender = employee.Gender;
                    dataInsert.Address = employee.Address;
                    dataInsert.Phonenumber = employee.Phonenumber;
                    dataInsert.Isdelete = false;
                    dataInsert.Creattime = DateTime.Now;
                    var insert = await _context.AddAsync(dataInsert);
                    await _context.SaveChangesAsync();
                    if (insert == null)
                    {
                        Valid = false;
                    }
                    bl = (byte)Validator.SUCCESS;
                }                       
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error:{ex.Message},{ex.StackTrace}");
                bl = (byte)Validator.ERROR;
            }
            return bl;
        }

        /// <summary>
        /// 0: Sửa lỗi ; 1: Sửa thành công ;
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<byte> Update(Employee employee ,Guid Id)
        {
            byte bl = (byte)Validator.ERROR;
            bool Valid = false;
            if (Id==Guid.Empty) return bl; /// Dữ liệu đầu vào bị null trả ra kết quả thêm lỗi {0}
            ValidationResult result = await _validator.ValidateAsync(employee);

            if (!result.IsValid)
            {
                bl = (byte)Validator.ERROR;
            }
            try
            {
                ///check trùng email
                if (employee.Email != "" && (await CheckEmail(employee.Email,false, Id)))
                {
                    Valid = true;
                    bl = (byte)Validator.ERROR_EMAIL;
                }
                ///check trùng số điện thoại
                if (!Valid && (await CheckPhoneNumber(employee.Phonenumber, false, Id)))
                {
                    Valid = true;
                    bl = (byte)Validator.ERROR_PHONENUMBER;
                }
                if (!Valid)
                {
                    /// update lại dữ liệu
                    using (var context = new BALabTestContext())
                    {
                        var product = await (from p in context.Employees where (p.Id == Id) select p).FirstOrDefaultAsync();
                        if (product != null)
                        {
                            product.Fullname = employee.Fullname;
                            product.Email = employee.Email;
                            product.Birthday = employee.Birthday;
                            product.Gender = employee.Gender;
                            product.Address = employee.Address;
                            product.Phonenumber = employee.Phonenumber;
                            product.Updatetime = DateTime.Now;
                            product.Updateuser = employee.Updateuser;
                            product.Creatuser = employee.Creatuser;
                            var update = _context.Update(product);
                            await _context.SaveChangesAsync();
                            if (update != null)
                            {
                                bl = (byte)Validator.SUCCESS;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //_logger.LogInformation("Hello World.");
                _logger.LogError($"Error:{ex.Message},{ex.StackTrace}");
                bl = (byte)Validator.ERROR;
            }
            return bl;
        }

        /// <summary>
        /// 0: Xóa bị lỗi ; 1: Xóa thành công ;
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public async Task<bool> Delete(Guid Id)
        {
            bool bl = true;
            try
            {
                using (var context = new BALabTestContext())
                {

                    // Lấy Id nhân viên để lấy ra thông tin
                    var product = await (from p in context.Employees where (p.Id == Id) select p).FirstOrDefaultAsync();
                    // Thay đổi trạng thái nhân viên thành False
                    if (product != null)
                    {
                        product.Isdelete = true;
                        _context.Update(product);
                        await context.SaveChangesAsync();
                    }
                }
              
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error:{ex.Message},{ex.StackTrace}");
                bl = false;
            }
            return bl;
        }
    }
}
