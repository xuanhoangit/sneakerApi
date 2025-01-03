using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using UserService.Core.Interfaces;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories;
public class Repository<TEntity> : IRepository<TEntity> where TEntity :class
{
        protected readonly UserServiceDbContext _context;
        protected DbSet<TEntity> _dbSet;

        public Repository(UserServiceDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public TEntity Get(int? id)
        {
            return _dbSet.Find(id);
        }
        
        public TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> filter, string? includeProperties = null, bool tracked = true)
        {
            if (tracked)
            {
                IQueryable<TEntity> query = _dbSet;
                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }
            else
            {
                IQueryable<TEntity> query = _dbSet.AsNoTracking();
                query = query.Where(filter);
                if (includeProperties != null)
                {
                    foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    {
                        query = query.Include(includeProp);
                    }
                }
                return query.FirstOrDefault();
            }
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter=null, string? includeProperties = null)
        {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null) 
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.ToList();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.SingleOrDefault(predicate);
        }

        public bool Add(TEntity entity)
        {   
            using (var transaction=_context.Database.BeginTransaction())
            {   
                try
                {
                    _dbSet.Add(entity);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }

        public bool AddRange(IEnumerable<TEntity> entities)
        {   
            using (var transaction=_context.Database.BeginTransaction())
            {   
                try
                {
                    _dbSet.AddRange(entities);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }

        public bool Update(TEntity entity) 
        {               
            using (var transaction=_context.Database.BeginTransaction())
            {   
                try
                {
                    _dbSet.Update(entity);;
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        } 

        public bool Remove(TEntity entity)
        {   
            using (var transaction=_context.Database.BeginTransaction())
            {   
                try
                {
                    _dbSet.Remove(entity);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }

        public bool RemoveRange(IEnumerable<TEntity> entities)
        {   
            using (var transaction=_context.Database.BeginTransaction())
            {   
                try
                {
                    _dbSet.RemoveRange(entities);
                    _context.SaveChanges();
                    transaction.Commit();
                }
                catch (System.Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
            return true;
        }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, string? includeProperties = null)
    {
            IQueryable<TEntity> query = _dbSet;
            if (filter != null) 
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach(var includeProp in includeProperties.Split(new char[] { ','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
    }
}
