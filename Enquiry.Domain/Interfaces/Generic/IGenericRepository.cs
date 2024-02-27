using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.Domain.Interfaces.Generic
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        //IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<bool> AddAsync(T entity);
        // public void AddRange(IEnumerable<T> entities);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> PermanentDeleteAsync(int id);
        //public Task<bool> UpdateAsync(int id,T entity);
        //public string UploadFile(IFormFile file, string type);
        public Task <bool> SaveAsync();
    }
}
