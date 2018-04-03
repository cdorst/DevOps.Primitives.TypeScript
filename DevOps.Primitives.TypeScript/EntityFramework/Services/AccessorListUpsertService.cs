using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class AccessorListUpsertService<TDbContext> : UpsertService<TDbContext, AccessorList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public AccessorListUpsertService(ICacheService<AccessorList> cache, TDbContext database, ILogger<UpsertService<TDbContext, AccessorList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.AccessorLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(AccessorList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<AccessorList> AssignUpsertedReferences(AccessorList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(AccessorList record)
        {
            yield return record.AccessorListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<AccessorList, bool>> FindExisting(AccessorList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
