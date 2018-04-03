using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class DecoratorUpsertService<TDbContext> : UpsertService<TDbContext, Decorator>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, DecoratorArgumentListExpression> _decoratorArgumentListExpressions;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public DecoratorUpsertService(ICacheService<Decorator> cache, TDbContext database, ILogger<UpsertService<TDbContext, Decorator>> logger, IUpsertService<TDbContext, DecoratorArgumentListExpression> decoratorArgumentListExpressions, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.Decorators)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Decorator)}={record.DecoratorArgumentListExpressionId}:{record.IdentifierId}";
            _decoratorArgumentListExpressions = decoratorArgumentListExpressions ?? throw new ArgumentNullException(nameof(decoratorArgumentListExpressions));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<Decorator> AssignUpsertedReferences(Decorator record)
        {
            record.DecoratorArgumentListExpression = await _decoratorArgumentListExpressions.UpsertAsync(record.DecoratorArgumentListExpression);
            record.DecoratorArgumentListExpressionId = record.DecoratorArgumentListExpression?.DecoratorArgumentListExpressionId ?? record.DecoratorArgumentListExpressionId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Decorator record)
        {
            yield return record.DecoratorArgumentListExpression;
            yield return record.Identifier;
        }

        protected override Expression<Func<Decorator, bool>> FindExisting(Decorator record)
            => existing
                => ((existing.DecoratorArgumentListExpressionId == null && record.DecoratorArgumentListExpressionId == null) || (existing.DecoratorArgumentListExpressionId == record.DecoratorArgumentListExpressionId))
                && existing.IdentifierId == record.IdentifierId;
    }
}
