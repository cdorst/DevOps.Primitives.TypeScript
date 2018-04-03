using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ArgumentUpsertService<TDbContext> : UpsertService<TDbContext, Argument>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public ArgumentUpsertService(ICacheService<Argument> cache, TDbContext database, ILogger<UpsertService<TDbContext, Argument>> logger, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.Arguments)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Argument)}={record.IdentifierId}";
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<Argument> AssignUpsertedReferences(Argument record)
        {
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Argument record)
        {
            yield return record.Identifier;
        }

        protected override Expression<Func<Argument, bool>> FindExisting(Argument record)
            => existing => existing.IdentifierId == record.IdentifierId;
    }
}
