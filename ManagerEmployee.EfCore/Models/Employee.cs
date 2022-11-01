using FluentValidation;
using System;
using System.Collections.Generic;

namespace ManagerEmployee.EfCore.Models
{
    /// <summary>Class của nhân viên</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public partial class Employee
    {
        /// <summary>  Id nhân viên /// </summary>
        public Guid Id { get; set; }

        /// <summary>  Tên nhân viên /// </summary>
        public string Fullname { get; set; }

        /// <summary>  Tài khoản nhân viên /// </summary>
        public string? Username { get; set; }

        /// <summary>  Mât khẩu nhân viên /// </summary>
        public string? Password { get; set; }

        /// <summary>  Email nhân viên /// </summary>
        public string Email { get; set; } = null!;

        /// <summary>  Ngày sinh nhân viên /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>  Giới tính nhân viên /// </summary>
        public byte Gender { get; set; }

        /// <summary>  Địa chỉ nhân viên /// </summary>
        public string? Address { get; set; }

        /// <summary>  Số điện thoại nhân viên /// </summary>
        public string Phonenumber { get; set; } = null!;

        /// <summary>  Trạng thái nhân viên /// </summary>
        public bool? Isdelete { get; set; }

        /// <summary>  Ngày tạo /// </summary>
        public DateTime Creattime { get; set; }

        /// <summary>  Người tạo /// </summary>
        public Guid Creatuser { get; set; }

        /// <summary>  Ngày sửa  /// </summary>
        public DateTime? Updatetime { get; set; }

        /// <summary>  Người sửa  /// </summary>
        public Guid? Updateuser { get; set; }
    }

    /// <summary>Dữ liệu trả ra có phân trang</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public class DateEmployee
    {
        /// <summary> Danh sách nhân viên /// </summary>
        public List<Employee> employee { get; set; }

        /// <summary>Tổng số bản ghi /// </summary>
        public int totalCount { get; set; }

        /// <summary>Độ dài page /// </summary>
        public int pageSize { get; set; }

        /// <summary>Số page /// </summary>
        public int currentPage { get; set; }

        /// <summary>Tổng số trang /// </summary>
        public int totalPages { get; set; }
    }

    /// <summary>Validator dữ liệu đầu vào của nhân viên</summary>
    /// <Modified>
    /// Name     Date     Comments
    /// sangnv 11/1/2022 created
    /// </Modified>
    public class EmployeeValidator : AbstractValidator<Employee>
    {
        public EmployeeValidator()
        {
            RuleFor(x => x.Fullname).Length(0, 50).NotNull().NotEmpty();
            RuleFor(x => x.Username).NotNull();
            RuleFor(x => x.Password).NotNull().NotNull().NotEmpty();
            RuleFor(x => x.Email).EmailAddress().NotNull();
            RuleFor(x => x.Birthday).NotEmpty().LessThan(p => new DateTime((DateTime.Now.Year - 18),DateTime.Now.Month,DateTime.Now.Day));
            RuleFor(x => x.Phonenumber).MinimumLength(10).MaximumLength(11).NotNull().NotEmpty();
        }
    }
}
