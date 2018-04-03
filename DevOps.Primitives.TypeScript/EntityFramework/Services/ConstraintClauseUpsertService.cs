using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ConstraintClauseUpsertService<TDbContext> : UpsertService<TDbContext, ConstraintClause>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertUniqueListService<TDbContext, Constraint, ConstraintList, ConstraintListAssociation> _constraintList;

        public ConstraintClauseUpsertService(ICacheService<ConstraintClause> cache, TDbContext database, ILogger<UpsertService<TDbContext, ConstraintClause>> logger, IUpsertUniqueListService<TDbContext, Constraint, ConstraintList, ConstraintListAssociation> constraintList, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.ConstraintClauses)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ConstraintClause)}={record.ConstraintListId}:{record.IdentifierId}";
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _constraintList = constraintList ?? throw new ArgumentNullException(nameof(constraintList));
        }

        protected override async Task<ConstraintClause> AssignUpsertedReferences(ConstraintClause record)
        {
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.ConstraintList = await _constraintList.UpsertAsync(record.ConstraintList);
            record.ConstraintListId = record.ConstraintList?.ConstraintListId ?? record.ConstraintListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(ConstraintClause record)
        {
            yield return record.Identifier;
            yield return record.ConstraintList;
        }

        protected override Expression<Func<ConstraintClause, bool>> FindExisting(ConstraintClause record)
            => existing
                => existing.ConstraintListId == record.ConstraintListId
                && existing.IdentifierId == record.IdentifierId;
    }
}
