using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ConstraintClauseListUpsertService<TDbContext> : UpsertService<TDbContext, ConstraintClauseList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public ConstraintClauseListUpsertService(ICacheService<ConstraintClauseList> cache, TDbContext database, ILogger<UpsertService<TDbContext, ConstraintClauseList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.ConstraintClauseLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ConstraintClauseList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<ConstraintClauseList> AssignUpsertedReferences(ConstraintClauseList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(ConstraintClauseList record)
        {
            yield return record.ConstraintClauseListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<ConstraintClauseList, bool>> FindExisting(ConstraintClauseList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
