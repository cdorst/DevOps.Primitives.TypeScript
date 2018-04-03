using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ConstraintListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, ConstraintListAssociation>
        where TDbContext : TypeScriptDbContext
    {
        public ConstraintListAssociationUpsertService(ICacheService<ConstraintListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, ConstraintListAssociation>> logger)
            : base(cache, database, logger, database.ConstraintListAssociations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ConstraintListAssociation)}={record.ConstraintId}:{record.ConstraintListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(ConstraintListAssociation record)
        {
            yield return record.Constraint;
            yield return record.ConstraintList;
        }

        protected override Expression<Func<ConstraintListAssociation, bool>> FindExisting(ConstraintListAssociation record)
            => existing
                => existing.ConstraintId == record.ConstraintId
                && existing.ConstraintListId == record.ConstraintListId;
    }
}
