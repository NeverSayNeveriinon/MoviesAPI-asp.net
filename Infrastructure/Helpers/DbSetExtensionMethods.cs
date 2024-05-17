using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers;

public static class DbSetExtensionMethods
{
    /// <summary>
    /// A User Defined Method that is functional when you want to include several entities
    /// </summary>
    /// <param name="dbSet"></param>
    /// <param name="includeEntities">The Exact Name of Navigation Properties as a comma seperated string</param>
    /// <typeparam name="TEntity"></typeparam>
    /// <returns></returns>
    public static IQueryable<TEntity> IncludeEntities<TEntity> (this DbSet<TEntity> dbSet, string includeEntities = "") where TEntity: class
    {
        IQueryable<TEntity> query = dbSet;

        if (string.IsNullOrEmpty(includeEntities))
        {
            query = query.AsQueryable();
        }

        else
        {
            foreach (var includeEntity in includeEntities.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeEntity);
            }
        }

        return query;
    }
}