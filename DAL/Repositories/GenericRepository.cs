using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class GenericRepository<T>(DesignStudioDbContext context) : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public IEnumerable<T> GetAll() => [.. _dbSet];
        public T? Get(int id) => _dbSet.Find(id);
        public void Create(T item) => _dbSet.Add(item);
        public void Update(T item) => _dbSet.Update(item);
        public void Delete(int id)
        {
            T? item = _dbSet.Find(id);
            if (item != null) _dbSet.Remove(item);
        }
    }
}