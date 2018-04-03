using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class BaseListUpsertService<TDbContext> : UpsertService<TDbContext, BaseList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public BaseListUpsertService(ICacheService<BaseList> cache, TDbContext database, ILogger<UpsertService<TDbContext, BaseList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.BaseLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(BaseList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<BaseList> AssignUpsertedReferences(BaseList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(BaseList record)
        {
            yield return record.BaseListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<BaseList, bool>> FindExisting(BaseList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
