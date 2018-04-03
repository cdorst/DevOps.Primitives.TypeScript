using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ParameterUpsertService<TDbContext> : UpsertService<TDbContext, Parameter>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _attributeLists;
        private readonly IUpsertService<TDbContext, Expression> _expressions;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public ParameterUpsertService(ICacheService<Parameter> cache, TDbContext database, ILogger<UpsertService<TDbContext, Parameter>> logger,
            IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> attributeLists,
            IUpsertService<TDbContext, Expression> expressions,
            IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.Parameters)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Parameter)}={record.AttributeListCollectionId}:{record.DefaultValueId}:{record.IdentifierId}:{record.TypeId}";
            _attributeLists = attributeLists ?? throw new ArgumentNullException(nameof(attributeLists));
            _expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<Parameter> AssignUpsertedReferences(Parameter record)
        {
            record.AttributeListCollection = await _attributeLists.UpsertAsync(record.AttributeListCollection);
            record.AttributeListCollectionId = record.AttributeListCollection?.AttributeListCollectionId ?? record.AttributeListCollectionId;
            record.DefaultValue = await _expressions.UpsertAsync(record.DefaultValue);
            record.DefaultValueId = record.DefaultValue?.ExpressionId ?? record.DefaultValueId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.Type = await _identifiers.UpsertAsync(record.Type);
            record.TypeId = record.Type?.IdentifierId ?? record.TypeId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Parameter record)
        {
            yield return record.AttributeListCollection;
            yield return record.DefaultValue;
            yield return record.Identifier;
            yield return record.Type;
        }

        protected override Expression<Func<Parameter, bool>> FindExisting(Parameter record)
            => existing
                => ((existing.AttributeListCollectionId == null && record.AttributeListCollectionId == null) || (existing.AttributeListCollectionId == record.AttributeListCollectionId))
                && ((existing.DefaultValueId == null && record.DefaultValueId == null) || (existing.DefaultValueId == record.DefaultValueId))
                && existing.IdentifierId == record.IdentifierId
                && existing.TypeId == record.TypeId;
    }
}
