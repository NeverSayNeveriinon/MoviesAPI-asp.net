using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Helpers.Extensions;

public static class DbSetExtensionMethods
{
    public static IQueryable<TEntity> IncludeEntities<TEntity> (this DbSet<TEntity> dbSet, string includeEntities = "") where TEntity: class
    {
        IQueryable<TEntity> query = null!;

        if (string.IsNullOrEmpty(includeEntities))
        {
            query = dbSet.AsQueryable().AsNoTracking();
        }

        else
        {
            foreach (var includeEntity in includeEntities.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = dbSet.Include(includeEntity).AsNoTracking();
            }
        }

        return query;
    }
}