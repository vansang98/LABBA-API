using ManagerEmployee.EfCore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Database.Services
{
    /// <summary>IEmployeeService</summary>
    public interface IEmployeeService
    {
        /// <summary>Gets the login.</summary>
        /// <param name="Phonenumber">The phonenumber.</param>
        /// <param name="Email">The email.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<Employee> GetLogin(string Phonenumber, string Password);

        /// <summary>Gets the find data.</summary>
        /// <param name="datafind">The datafind.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<DateEmployee> GetFindData(FindData datafind);

        /// <summary>Inserts the specified employee.</summary>
        /// <param name="employee">The employee.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<byte> Insert(Employee employee);

        /// <summary>Gets the by identifier.</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<Employee> GetById(Guid Id);

        /// <summary>Updates the specified employee.</summary>
        /// <param name="employee">The employee.</param>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<byte> Update(Employee employee, Guid Id);

        /// <summary>Deletes the specified identifier.</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        Task<bool> Delete(Guid Id);
    }
}
