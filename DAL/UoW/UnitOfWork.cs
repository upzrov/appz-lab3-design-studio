using DAL.Interfaces;
using DAL.Entities;
using DAL.Repositories;

namespace DAL.UoW
{
    public class UnitOfWork(DesignStudioDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = [];

        public IRepository<T> GetRepository<T>() where T : class
        {
            var type = typeof(T);
            if (!_repositories.TryGetValue(type, out object? value))
            {
                value = new GenericRepository<T>(context);
                _repositories[type] = value;
            }
            return (IRepository<T>)value;
        }

        public void Save() => context.SaveChanges();

        public void Dispose()
        {
            context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}