using Discord_Copycat.Data;
using Discord_Copycat.Models.Base;
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
        protected readonly DiscordContext _discordContext;
        protected readonly DbSet<TEntity> _table;

        public GenericRepository(DiscordContext discordContext)
        {
            _discordContext = discordContext;
            _table = _discordContext.Set<TEntity>();
        }

        public void Create(TEntity entity)
        {
            _discordContext.Add(entity);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _discordContext.AddAsync(entity);
        }

        public void CreateRange(IEnumerable<TEntity> entities)
        {
            _discordContext.AddRange(entities);
        }

        public async Task CreateRangeAsync(IEnumerable<TEntity> entities)
        {
            await _discordContext.AddRangeAsync(entities);
        }

        public void Delete(TEntity entity)
        {
            _discordContext.Remove(entity);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            _discordContext.RemoveRange(entities);
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
                return _discordContext.SaveChanges() > 0;
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
                return await _discordContext.SaveChangesAsync() > 0;
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
