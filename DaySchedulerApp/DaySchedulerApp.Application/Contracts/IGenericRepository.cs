using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaySchedulerApp.Application.Contracts
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(string id);
        Task Add(T entity);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
