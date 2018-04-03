using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ConstraintUpsertService<TDbContext> : UpsertService<TDbContext, Constraint>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public ConstraintUpsertService(ICacheService<Constraint> cache, TDbContext database, ILogger<UpsertService<TDbContext, Constraint>> logger, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.Constraints)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Constraint)}={record.IdentifierId}";
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<Constraint> AssignUpsertedReferences(Constraint record)
        {
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Constraint record)
        {
            yield return record.Identifier;
        }

        protected override Expression<Func<Constraint, bool>> FindExisting(Constraint record)
            => existing => existing.IdentifierId == record.IdentifierId;
    }
}
