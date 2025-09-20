namespace MTOV_Domain.Interface
{
    using System.Linq.Expressions;

    public interface IBaseRepository<T> : IBaseRepository where T : class
    {
        //DB Context goes here for Dapper integration
    }

    public interface IBaseRepository
    {
        /// <summary>
        /// Get an Entity of specified type based on predicate
        /// </summary>
        /// <typeparam name="TEntity">Type of Entity</typeparam>
        /// <param name="predicate">Predicate</param>
        /// <returns>Entity</returns>
        Task<TEntity> GetFirst<TEntity>(Expression<Func<TEntity, bool>> predicate) where TEntity : class;
    }
}