using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class NamespaceUpsertService<TDbContext> : UpsertService<TDbContext, Namespace>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public NamespaceUpsertService(ICacheService<Namespace> cache, TDbContext database, ILogger<UpsertService<TDbContext, Namespace>> logger, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.Namespaces)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Namespace)}={record.IdentifierId}";
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<Namespace> AssignUpsertedReferences(Namespace record)
        {
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Namespace record)
        {
            yield return record.Identifier;
        }

        protected override Expression<Func<Namespace, bool>> FindExisting(Namespace record)
            => existing => existing.IdentifierId == record.IdentifierId;
    }
}
