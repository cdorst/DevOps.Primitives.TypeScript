using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class TypeArgumentListUpsertService<TDbContext> : UpsertService<TDbContext, TypeArgumentList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public TypeArgumentListUpsertService(ICacheService<TypeArgumentList> cache, TDbContext database, ILogger<UpsertService<TDbContext, TypeArgumentList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.TypeArgumentLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(TypeArgumentList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<TypeArgumentList> AssignUpsertedReferences(TypeArgumentList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(TypeArgumentList record)
        {
            yield return record.TypeArgumentListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<TypeArgumentList, bool>> FindExisting(TypeArgumentList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
