using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Database.Repositorys
{
    public interface  IRepositoryService<T> where T : class
    {
        /// <summary>Gets all.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>Gets the by identifier.</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<T> GetById(Guid Id);

        /// <summary>Inserts the specified data.</summary>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        T Insert(T data);

        /// <summary>Insertses the specified data.</summary>
        /// <param name="data">The data.</param>
        void Inserts(List<T> data);

        /// <summary>Updates the specified data.</summary>
        /// <param name="data">The data.</param>
        void Update(T data);
    }
}
