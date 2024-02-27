using Enquiry.DataAccess.Context;
using Enquiry.Domain.Interfaces.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Enquiry.DataAccess.Implementation.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly EnquiryDbContext dbContext;
        private readonly ILogger logger;

        public GenericRepository(EnquiryDbContext dbContext,ILogger logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task<bool> AddAsync(T entity)
        {
            try
            {
                logger.LogInformation("Inside AddAsync method of GenericRepository");
                var res = await dbContext.Set<T>().AddAsync(entity);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occurred in AddAsync method of GenericRepository: {0}", ex.StackTrace);
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                logger.LogInformation("Inside GetAllAsync method of GenericRepository");
                return await dbContext.Set<T>().ToListAsync();
               
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occurred in GetAllAsync method of GenericRepository: {0}", ex.StackTrace);
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            try
            {
                logger.LogInformation("Inside GetByIdAsync method of GenericRepository");
                return await dbContext.Set<T>().FindAsync(id);

            }
            catch (Exception ex)
            {
                logger.LogError("Exception occurred in GetByIdAsync method of GenericRepository: {0}", ex.StackTrace);
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task<bool> UpdateAsync(int id,T entity)
        {
            try
            {
                logger.LogInformation("Inside Update method of GenericRepository");
                var existingEntity=dbContext.Set<T>().Find(id);
                if (existingEntity == null) { return false; }

                existingEntity = entity;
                var res=dbContext.Set<T>().Update(existingEntity);
                await dbContext.SaveChangesAsync();
                if (res == null)
                {
                    logger.LogInformation("something went wrong");
                    return false;
                }
                logger.LogInformation("Entity updated Successfully");
                return true;
                
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occurred in Update method of GenericRepository: {0}", ex.StackTrace);
                throw new Exception(ex.StackTrace);
            }
        }

        public async Task< bool> SaveAsync()
        {
            logger.LogInformation("Inside Save method of GenericRepository");
            return await dbContext.SaveChangesAsync() >= 1;
    
        }

        public async Task<bool> PermanentDeleteAsync(int id)
        {
            try
            {
                logger.LogInformation("Inside Remove method of GenericRepository");
               var res= await dbContext.Set<T>().FindAsync(id);
                if (res == null)
                {
                    logger.LogInformation("Entity not found");
                    return false;
                }
                logger.LogInformation("Entity found");
                dbContext.Set<T>().Remove(res);
                logger.LogInformation("Entity Removed Successfully");
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError("Exception occurred in Remove method of GenericRepository: {0}", ex.StackTrace);
                throw new Exception(ex.StackTrace);
            }
        }
        
    }
}
