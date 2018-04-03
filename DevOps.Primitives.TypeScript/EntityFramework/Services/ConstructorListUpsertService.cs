using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ConstructorListUpsertService<TDbContext> : UpsertService<TDbContext, ConstructorList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public ConstructorListUpsertService(ICacheService<ConstructorList> cache, TDbContext database, ILogger<UpsertService<TDbContext, ConstructorList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.ConstructorLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ConstructorList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<ConstructorList> AssignUpsertedReferences(ConstructorList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(ConstructorList record)
        {
            yield return record.ConstructorListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<ConstructorList, bool>> FindExisting(ConstructorList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
