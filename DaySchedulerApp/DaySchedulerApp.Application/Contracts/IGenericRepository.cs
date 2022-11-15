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
        Task<T> Add(T entity);
        Task Update(string id, T entity);
        Task Delete(string id);
    }
}
