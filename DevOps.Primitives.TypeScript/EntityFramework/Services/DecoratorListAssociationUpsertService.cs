using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class DecoratorListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, DecoratorListAssociation>
        where TDbContext : TypeScriptDbContext
    {
        public DecoratorListAssociationUpsertService(ICacheService<DecoratorListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, DecoratorListAssociation>> logger)
            : base(cache, database, logger, database.DecoratorListAssociations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(DecoratorListAssociation)}={record.DecoratorId}:{record.DecoratorListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(DecoratorListAssociation record)
        {
            yield return record.Decorator;
            yield return record.DecoratorList;
        }

        protected override Expression<Func<DecoratorListAssociation, bool>> FindExisting(DecoratorListAssociation record)
            => existing
                => existing.DecoratorId == record.DecoratorId
                && existing.DecoratorListId == record.DecoratorListId;
    }
}
