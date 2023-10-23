using Chattr.Data;
using ClassLibrary.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary.Repositories.GenericRep
{
    internal class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ChattrContext _chattrContext;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(ChattrContext chattrContext)
        {
            _chattrContext = chattrContext;
            _table = _chattrContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _chattrContext.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _chattrContext.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _chattrContext.AddRange(entities);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _chattrContext.AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            _chattrContext.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _chattrContext.RemoveRange(entities);
        }

        public TEntity? FindById(object id)
        {
            return _table.Find(id);
        }

        public async Task<TEntity?> FindByIdAsync(object id)
        {
            return await _table.FindAsync(id);
        }

        public IQueryable<TEntity> GetAllAsQueryable()
        {
            return _table.AsQueryable();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var allAsync = await _table.AsNoTracking().ToListAsync();
            return allAsync;
        }

        public bool Save()
        {
            try
            {
                return _chattrContext.SaveChanges() > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                return await _chattrContext.SaveChangesAsync() > 0;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        public void Update(TEntity entity)
        {
            _table.Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entities)
        {
            _table.UpdateRange(entities);
        }
    }
}
