using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using VerticalArchApp.API.Data;

namespace VerticalArchApp.API.Services
{
    #region Interface
    public interface IServiceBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        Task CreateAsync(T enity);
        void Update(T entity);
        void Delete(T entity);
    }
    #endregion

    #region Implementation
    public abstract class ServiceBase<T> : IServiceBase<T> where T : class
    {
        private readonly CompanyEmpContext _db;
        public ServiceBase(CompanyEmpContext db)
        {
            _db = db;
        }
        public void Create(T entity) => _db.Set<T>().Add(entity);
        public async Task CreateAsync(T entity) => await _db.Set<T>().AddAsync(entity);
        public void Update(T entity) => _db.Set<T>().Update(entity);
        public void Delete(T entity) => _db.Set<T>().Remove(entity);

        public IQueryable<T> FindAll(bool trackChanges) =>
            !trackChanges ? _db.Set<T>().AsNoTracking() : _db.Set<T>();


        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>

            !trackChanges ? _db.Set<T>()
                            .Where(expression)
                            .AsNoTracking() : _db.Set<T>()
                            .Where(expression);
    }
    #endregion
}
