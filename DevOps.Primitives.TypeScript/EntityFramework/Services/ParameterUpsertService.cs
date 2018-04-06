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
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _decoratorLists;
        private readonly IUpsertService<TDbContext, DocumentationComment> _documentationComments;
        private readonly IUpsertService<TDbContext, Expression> _expressions;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public ParameterUpsertService(ICacheService<Parameter> cache, TDbContext database, ILogger<UpsertService<TDbContext, Parameter>> logger,
            IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> decoratorLists,
            IUpsertService<TDbContext, DocumentationComment> documentationComments,
            IUpsertService<TDbContext, Expression> expressions,
            IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.Parameters)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Parameter)}={record.IsReadonly}:{record.DecoratorListId}:{record.DefaultValueId}:{record.DocumentationCommentId}:{record.IdentifierId}:{record.TypeId}";
            _decoratorLists = decoratorLists ?? throw new ArgumentNullException(nameof(decoratorLists));
            _documentationComments = documentationComments ?? throw new ArgumentNullException(nameof(documentationComments));
            _expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<Parameter> AssignUpsertedReferences(Parameter record)
        {
            record.DecoratorList = await _decoratorLists.UpsertAsync(record.DecoratorList);
            record.DecoratorListId = record.DecoratorList?.DecoratorListId ?? record.DecoratorListId;
            record.DefaultValue = await _expressions.UpsertAsync(record.DefaultValue);
            record.DefaultValueId = record.DefaultValue?.ExpressionId ?? record.DefaultValueId;
            record.DocumentationComment = await _documentationComments.UpsertAsync(record.DocumentationComment);
            record.DocumentationCommentId = record.DocumentationComment?.DocumentationCommentId ?? record.DocumentationCommentId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.Type = await _identifiers.UpsertAsync(record.Type);
            record.TypeId = record.Type?.IdentifierId ?? record.TypeId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Parameter record)
        {
            yield return record.DecoratorList;
            yield return record.DefaultValue;
            yield return record.DocumentationComment;
            yield return record.Identifier;
            yield return record.Type;
        }

        protected override Expression<Func<Parameter, bool>> FindExisting(Parameter record)
            => existing
                => ((existing.DecoratorListId == null && record.DecoratorListId == null) || (existing.DecoratorListId == record.DecoratorListId))
                && ((existing.DefaultValueId == null && record.DefaultValueId == null) || (existing.DefaultValueId == record.DefaultValueId))
                && existing.DocumentationCommentId == record.DocumentationCommentId
                && existing.IdentifierId == record.IdentifierId
                && existing.IsReadonly == record.IsReadonly
                && existing.TypeId == record.TypeId;
    }
}
