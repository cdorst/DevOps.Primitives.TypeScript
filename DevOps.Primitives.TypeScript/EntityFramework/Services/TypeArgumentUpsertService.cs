using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class TypeArgumentUpsertService<TDbContext> : UpsertService<TDbContext, TypeArgument>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public TypeArgumentUpsertService(ICacheService<TypeArgument> cache, TDbContext database, ILogger<UpsertService<TDbContext, TypeArgument>> logger, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.TypeArguments)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(TypeArgument)}={record.IdentifierId}";
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<TypeArgument> AssignUpsertedReferences(TypeArgument record)
        {
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(TypeArgument record)
        {
            yield return record.Identifier;
        }

        protected override Expression<Func<TypeArgument, bool>> FindExisting(TypeArgument record)
            => existing => existing.IdentifierId == record.IdentifierId;
    }
}
