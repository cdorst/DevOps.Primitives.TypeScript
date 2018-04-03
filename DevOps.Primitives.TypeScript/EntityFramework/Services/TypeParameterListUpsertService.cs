using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class TypeParameterListUpsertService<TDbContext> : UpsertService<TDbContext, TypeParameterList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public TypeParameterListUpsertService(ICacheService<TypeParameterList> cache, TDbContext database, ILogger<UpsertService<TDbContext, TypeParameterList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.TypeParameterLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(TypeParameterList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<TypeParameterList> AssignUpsertedReferences(TypeParameterList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(TypeParameterList record)
        {
            yield return record.TypeParameterListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<TypeParameterList, bool>> FindExisting(TypeParameterList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
