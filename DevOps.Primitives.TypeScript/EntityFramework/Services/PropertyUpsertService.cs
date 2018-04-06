using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class PropertyUpsertService<TDbContext> : UpsertService<TDbContext, Property>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _decoratorLists;
        private readonly IUpsertService<TDbContext, DocumentationComment> _documentationComments;
        private readonly IUpsertService<TDbContext, Expression> _expressions;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public PropertyUpsertService(ICacheService<Property> cache, TDbContext database, ILogger<UpsertService<TDbContext, Property>> logger,
            IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> decoratorLists,
            IUpsertService<TDbContext, DocumentationComment> documentationComments,
            IUpsertService<TDbContext, Expression> expressions,
            IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.Properties)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Property)}={record.AccessModifier}:{record.IsStatic}:{record.IsReadonly}:{record.DecoratorListId}:{record.DocumentationCommentId}:{record.IdentifierId}:{record.TypeId}";
            _decoratorLists = decoratorLists ?? throw new ArgumentNullException(nameof(decoratorLists));
            _documentationComments = documentationComments ?? throw new ArgumentNullException(nameof(documentationComments));
            _expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<Property> AssignUpsertedReferences(Property record)
        {
            record.DecoratorList = await _decoratorLists.UpsertAsync(record.DecoratorList);
            record.DecoratorListId = record.DecoratorList?.DecoratorListId ?? record.DecoratorListId;
            record.DocumentationComment = await _documentationComments.UpsertAsync(record.DocumentationComment);
            record.DocumentationCommentId = record.DocumentationComment?.DocumentationCommentId ?? record.DocumentationCommentId;
            record.DefaultValue = await _expressions.UpsertAsync(record.DefaultValue);
            record.DefaultValueId = record.DefaultValue?.ExpressionId ?? record.DefaultValueId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.Type = await _identifiers.UpsertAsync(record.Type);
            record.TypeId = record.Type?.IdentifierId ?? record.TypeId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Property record)
        {
            yield return record.DecoratorList;
            yield return record.DefaultValue;
            yield return record.DocumentationComment;
            yield return record.Identifier;
            yield return record.Type;
        }

        protected override Expression<Func<Property, bool>> FindExisting(Property record)
            => existing
                => ((existing.AccessModifier == null && record.AccessModifier == null) || (existing.AccessModifier == record.AccessModifier))
                && existing.IsStatic == record.IsStatic
                && existing.IsReadonly == record.IsReadonly
                && ((existing.DecoratorListId == null && record.DecoratorListId == null) || (existing.DecoratorListId == record.DecoratorListId))
                && existing.DocumentationCommentId == record.DocumentationCommentId
                && existing.IdentifierId == record.IdentifierId
                && existing.TypeId == record.TypeId;
    }
}
