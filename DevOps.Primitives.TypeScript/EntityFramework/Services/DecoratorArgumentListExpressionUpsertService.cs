using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class DecoratorArgumentListExpressionUpsertService<TDbContext> : UpsertService<TDbContext, DecoratorArgumentListExpression>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiMaxStringReference> _strings;

        public DecoratorArgumentListExpressionUpsertService(ICacheService<DecoratorArgumentListExpression> cache, TDbContext database, ILogger<UpsertService<TDbContext, DecoratorArgumentListExpression>> logger, IUpsertService<TDbContext, AsciiMaxStringReference> strings)
            : base(cache, database, logger, database.DecoratorArgumentListExpressions)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(DecoratorArgumentListExpression)}={record.ExpressionId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<DecoratorArgumentListExpression> AssignUpsertedReferences(DecoratorArgumentListExpression record)
        {
            record.Expression = await _strings.UpsertAsync(record.Expression);
            record.ExpressionId = record.Expression?.AsciiMaxStringReferenceId ?? record.ExpressionId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(DecoratorArgumentListExpression record)
        {
            yield return record.Expression;
        }

        protected override Expression<Func<DecoratorArgumentListExpression, bool>> FindExisting(DecoratorArgumentListExpression record)
            => existing => existing.ExpressionId == record.ExpressionId;
    }
}
