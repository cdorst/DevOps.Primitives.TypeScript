using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class DecoratorListUpsertService<TDbContext> : UpsertService<TDbContext, DecoratorList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public DecoratorListUpsertService(ICacheService<DecoratorList> cache, TDbContext database, ILogger<UpsertService<TDbContext, DecoratorList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.DecoratorLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(DecoratorList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<DecoratorList> AssignUpsertedReferences(DecoratorList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(DecoratorList record)
        {
            yield return record.DecoratorListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<DecoratorList, bool>> FindExisting(DecoratorList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
