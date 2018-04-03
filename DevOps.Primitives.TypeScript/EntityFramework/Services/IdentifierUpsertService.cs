using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class IdentifierUpsertService<TDbContext> : UpsertService<TDbContext, Identifier>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public IdentifierUpsertService(ICacheService<Identifier> cache, TDbContext database, ILogger<UpsertService<TDbContext, Identifier>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.Identifiers)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Identifier)}={record.NameId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<Identifier> AssignUpsertedReferences(Identifier record)
        {
            record.Name = await _strings.UpsertAsync(record.Name);
            record.NameId = record.Name?.AsciiStringReferenceId ?? record.NameId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Identifier record)
        {
            yield return record.Name;
        }

        protected override Expression<Func<Identifier, bool>> FindExisting(Identifier record)
            => existing => existing.NameId == record.NameId;
    }
}
