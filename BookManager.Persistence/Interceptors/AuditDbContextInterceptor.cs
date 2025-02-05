using BookManager.Domain.Entities.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BookManager.Persistence.Interceptors
{
    public class AuditDbContextInterceptor:SaveChangesInterceptor
    {
        private static readonly Dictionary<EntityState, Action<DbContext, IBaseAuditEntity>> Behaviors = new()
        {

            { EntityState.Added, AddBehavior },

            { EntityState.Modified, ModifiedBehavior },

        };

        private static void AddBehavior(DbContext dbContext, IBaseAuditEntity entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            dbContext.Entry(entity).Property(x => x.UpdatedAt).IsModified = false;
        }
        private static void ModifiedBehavior(DbContext dbContext, IBaseAuditEntity entity)
        {
            dbContext.Entry(entity).Property(x => x.CreatedAt).IsModified = false;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
            CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
            {
                if (entityEntry.Entity is not IBaseAuditEntity baseAuditEntity)
                {
                    continue;
                }

                Behaviors[entityEntry.State](eventData.Context, baseAuditEntity);

            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

    }
}
