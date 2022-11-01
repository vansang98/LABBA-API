using ManagerEmployee.EfCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Database.Repositorys
{
    /// <summary>Hàm sử dụng chung, danh sách,thêm,sửa,xóa</summary>
    /// <Modified>
    ///  Name        Date       Comments
    ///  sangnv    11/10/2022    created
    /// </Modified>
    /// <typeparam name="T"></typeparam>
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        /// <summary>The context</summary>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        private BALabTestContext _context = null;

        /// <summary>The data</summary>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        private DbSet<T> data = null;

        public RepositoryService()
        {
            this._context = new BALabTestContext();
            data = _context.Set<T>();
        }

        /// <summary>Initializes a new instance of the <see cref="RepositoryService{T}" /> class.</summary>
        /// <param name="_context">The context.</param>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public RepositoryService(BALabTestContext _context)
        {
            this._context = _context;
            data = _context.Set<T>();
        }

        /// <summary>Lấy danh sách</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await data.ToListAsync();
        }

        /// <summary>Lấy đối tượng theo ID</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public virtual async Task<T> GetById(Guid Id)
        {
            return await data.FindAsync(Id);
        }

        /// <summary>Thêm một bản ghi</summary>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public virtual T Insert(T data)
        {
            var result = _context.Set<T>();
            result.Add(data);
            return data;
        }

        /// <summary>Thêm nhiều bản ghi</summary>
        /// <param name="data">The data.</param>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public virtual void Inserts(List<T> data)
        {
            var result = _context.Set<T>();
            result.AddRange(data);
        }

        /// <summary>Cập nhật một bản ghi</summary>
        /// <param name="data">The data.</param>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public virtual void Update(T data)
        {
            var result = _context.Set<T>();
            result.Update(data);
        }

        /// <summary>Cập nhật nhiều bản ghi</summary>
        /// <param name="data">The data.</param>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public virtual void Updates(List<T> data)
        {
            var result = _context.Set<T>();
            result.UpdateRange(data);
        }

        /// <summary>Xóa bản ghi</summary>
        /// <param name="data">The data.</param>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        public virtual void ChangeIsDelete(T data)
        {
            var result = _context.Set<T>();
            result.UpdateRange(data);
        }

    }
}
