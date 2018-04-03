using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class FieldListUpsertService<TDbContext> : UpsertService<TDbContext, FieldList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public FieldListUpsertService(ICacheService<FieldList> cache, TDbContext database, ILogger<UpsertService<TDbContext, FieldList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.FieldLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(FieldList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<FieldList> AssignUpsertedReferences(FieldList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(FieldList record)
        {
            yield return record.FieldListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<FieldList, bool>> FindExisting(FieldList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
