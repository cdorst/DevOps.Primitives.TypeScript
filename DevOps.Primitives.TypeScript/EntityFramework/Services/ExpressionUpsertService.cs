using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ExpressionUpsertService<TDbContext> : UpsertService<TDbContext, Expression>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiMaxStringReference> _strings;

        public ExpressionUpsertService(ICacheService<Expression> cache, TDbContext database, ILogger<UpsertService<TDbContext, Expression>> logger, IUpsertService<TDbContext, AsciiMaxStringReference> strings)
            : base(cache, database, logger, database.Expressions)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Expression)}={record.TextId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<Expression> AssignUpsertedReferences(Expression record)
        {
            record.Text = await _strings.UpsertAsync(record.Text);
            record.TextId = record.Text?.AsciiMaxStringReferenceId ?? record.TextId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Expression record)
        {
            yield return record.Text;
        }

        protected override Expression<Func<Expression, bool>> FindExisting(Expression record)
            => existing => existing.TextId == record.TextId;
    }
}
