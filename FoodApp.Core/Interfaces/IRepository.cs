using FoodApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task AddAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IQueryable<T>> GetAllAsync();
       Task< T> GetByIdAsync(int id);

         
    }
}
