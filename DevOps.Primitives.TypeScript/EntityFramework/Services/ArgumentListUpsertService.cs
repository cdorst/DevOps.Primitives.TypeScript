using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ArgumentListUpsertService<TDbContext> : UpsertService<TDbContext, ArgumentList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public ArgumentListUpsertService(ICacheService<ArgumentList> cache, TDbContext database, ILogger<UpsertService<TDbContext, ArgumentList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.ArgumentLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ArgumentList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<ArgumentList> AssignUpsertedReferences(ArgumentList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(ArgumentList record)
        {
            yield return record.ArgumentListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<ArgumentList, bool>> FindExisting(ArgumentList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
