using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ParameterListUpsertService<TDbContext> : UpsertService<TDbContext, ParameterList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public ParameterListUpsertService(ICacheService<ParameterList> cache, TDbContext database, ILogger<UpsertService<TDbContext, ParameterList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.ParameterLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ParameterList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<ParameterList> AssignUpsertedReferences(ParameterList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(ParameterList record)
        {
            yield return record.ParameterListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<ParameterList, bool>> FindExisting(ParameterList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
