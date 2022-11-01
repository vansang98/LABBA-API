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
        public RepositoryService(BALabTestContext _context)
        {
            this._context = _context;
            data = _context.Set<T>();
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await data.ToListAsync();
        }
        public virtual async Task<T> GetById(Guid Id)
        {
            return await data.FindAsync(Id);
        }
        public virtual T Insert(T data)
        {
            var result = _context.Set<T>();
            result.Add(data);
            return data;
        }
        public virtual void Inserts(List<T> data)
        {
            var result = _context.Set<T>();
            result.AddRange(data);
        }
        public virtual void Update(T data)
        {
            var result = _context.Set<T>();
            result.Update(data);
        }
        public virtual void Updates(List<T> data)
        {
            var result = _context.Set<T>();
            result.UpdateRange(data);
        }
        public virtual void ChangeIsDelete(T data)
        {
            var result = _context.Set<T>();
            result.UpdateRange(data);
        }

    }
}
