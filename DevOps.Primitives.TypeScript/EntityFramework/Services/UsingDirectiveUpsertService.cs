using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class UsingDirectiveUpsertService<TDbContext> : UpsertService<TDbContext, UsingDirective>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public UsingDirectiveUpsertService(ICacheService<UsingDirective> cache, TDbContext database, ILogger<UpsertService<TDbContext, UsingDirective>> logger, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.UsingDirectives)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(UsingDirective)}={record.IdentifierId}";
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<UsingDirective> AssignUpsertedReferences(UsingDirective record)
        {
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(UsingDirective record)
        {
            yield return record.Identifier;
        }

        protected override Expression<Func<UsingDirective, bool>> FindExisting(UsingDirective record)
            => existing => existing.IdentifierId == record.IdentifierId;
    }
}
