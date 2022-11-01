using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Database.Repositorys
{
    public interface  IRepositoryService<T> where T : class
    {
         Task<IEnumerable<T>> GetAll();
         Task<T> GetById(Guid Id);
         T Insert(T data);
         void Inserts(List<T> data);
         void Update(T data);
    }
}
