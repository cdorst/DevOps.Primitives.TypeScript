using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ConstraintClauseListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, ConstraintClauseListAssociation>
        where TDbContext : TypeScriptDbContext
    {
        public ConstraintClauseListAssociationUpsertService(ICacheService<ConstraintClauseListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, ConstraintClauseListAssociation>> logger)
            : base(cache, database, logger, database.ConstraintClauseListAssociations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ConstraintClauseListAssociation)}={record.ConstraintClauseId}:{record.ConstraintClauseListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(ConstraintClauseListAssociation record)
        {
            yield return record.ConstraintClause;
            yield return record.ConstraintClauseList;
        }

        protected override Expression<Func<ConstraintClauseListAssociation, bool>> FindExisting(ConstraintClauseListAssociation record)
            => existing
                => existing.ConstraintClauseId == record.ConstraintClauseId
                && existing.ConstraintClauseListId == record.ConstraintClauseListId;
    }
}
