using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class StatementListUpsertService<TDbContext> : UpsertService<TDbContext, StatementList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public StatementListUpsertService(ICacheService<StatementList> cache, TDbContext database, ILogger<UpsertService<TDbContext, StatementList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.StatementLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(StatementList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<StatementList> AssignUpsertedReferences(StatementList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(StatementList record)
        {
            yield return record.StatementListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<StatementList, bool>> FindExisting(StatementList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
