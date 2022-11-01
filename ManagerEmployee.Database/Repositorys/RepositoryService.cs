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
    /// <summary>Thêm ,sửa,xóa</summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryService<T> : IRepositoryService<T> where T : class
    {
        private List<T> context;
        private BALabTestContext _context = null;
        private DbSet<T> data = null;
        public RepositoryService()
        {
            this._context = new BALabTestContext();
            data = _context.Set<T>();
        }
        /// <summary>Initializes a new instance of the <see cref="RepositoryService{T}" /> class.</summary>
        /// <param name="_context">The context.</param>
        public RepositoryService(BALabTestContext _context)
        {
            this._context = _context;
            data = _context.Set<T>();
        }

        /// <summary>Gets all.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await data.ToListAsync();
        }

        /// <summary>Gets the by identifier.</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual async Task<T> GetById(Guid Id)
        {
            return await data.FindAsync(Id);
        }

        /// <summary>Inserts the specified data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        public virtual T Insert(T data)
        {
            var result = _context.Set<T>();
            result.Add(data);
            return data;
        }

        /// <summary>Insertses the specified data.</summary>
        /// <param name="data">The data.</param>
        public virtual void Inserts(List<T> data)
        {
            var result = _context.Set<T>();
            result.AddRange(data);
        }

        /// <summary>Updates the specified data.</summary>
        /// <param name="data">The data.</param>
        public virtual void Update(T data)
        {
            var result = _context.Set<T>();
            result.Update(data);
        }

        /// <summary>Updateses the specified data.</summary>
        /// <param name="data">The data.</param>
        public virtual void Updates(List<T> data)
        {
            var result = _context.Set<T>();
            result.UpdateRange(data);
        }

        /// <summary>Changes the is delete.</summary>
        /// <param name="data">The data.</param>
        public virtual void ChangeIsDelete(T data)
        {
            var result = _context.Set<T>();
            result.UpdateRange(data);
        }

    }
}
