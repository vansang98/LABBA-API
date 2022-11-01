using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerEmployee.Database.Repositorys
{
    public interface  IRepositoryService<T> where T : class
    {
        /// <summary>Lấy danh sách</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        Task<IEnumerable<T>> GetAll();

        /// <summary>Lấy đối tượng theo ID</summary>
        /// <param name="Id">The identifier.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        Task<T> GetById(Guid Id);

        /// <summary>Thêm một bản ghi</summary>
        /// <param name="data">The data.</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        T Insert(T data);

        /// <summary>Thêm nhiều bản ghi</summary>
        /// <param name="data">The data.</param>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        void Inserts(List<T> data);

        /// <summary>Cập nhật một bản ghi</summary>
        /// <param name="data">The data.</param>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        void Update(T data);

        /// <summary>Xóa bản ghi</summary>
        /// <param name="data">The data.</param>
        /// <Modified>
        /// Name     Date     Comments
        /// sangnv 11/1/2022 created
        /// </Modified>
        void ChangeIsDelete(T data);
    }
}
