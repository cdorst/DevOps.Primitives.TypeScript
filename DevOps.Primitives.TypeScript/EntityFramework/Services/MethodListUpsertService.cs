using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class MethodListUpsertService<TDbContext> : UpsertService<TDbContext, MethodList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public MethodListUpsertService(ICacheService<MethodList> cache, TDbContext database, ILogger<UpsertService<TDbContext, MethodList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.MethodLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(MethodList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<MethodList> AssignUpsertedReferences(MethodList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(MethodList record)
        {
            yield return record.MethodListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<MethodList, bool>> FindExisting(MethodList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
