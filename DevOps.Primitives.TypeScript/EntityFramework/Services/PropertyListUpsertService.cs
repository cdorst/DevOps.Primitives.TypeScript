using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class PropertyListUpsertService<TDbContext> : UpsertService<TDbContext, PropertyList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public PropertyListUpsertService(ICacheService<PropertyList> cache, TDbContext database, ILogger<UpsertService<TDbContext, PropertyList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.PropertyLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(PropertyList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<PropertyList> AssignUpsertedReferences(PropertyList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(PropertyList record)
        {
            yield return record.PropertyListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<PropertyList, bool>> FindExisting(PropertyList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
