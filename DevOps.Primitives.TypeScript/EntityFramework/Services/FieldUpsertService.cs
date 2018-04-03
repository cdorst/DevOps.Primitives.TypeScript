using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class FieldUpsertService<TDbContext> : UpsertService<TDbContext, Field>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _attributeLists;
        private readonly IUpsertUniqueListService<TDbContext, DocumentationComment, DocumentationCommentList, DocumentationCommentListAssociation> _documentationCommentLists;
        private readonly IUpsertService<TDbContext, Expression> _expressions;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertUniqueListService<TDbContext, SyntaxToken, ModifierList, ModifierListAssociation> _modifierLists;

        public FieldUpsertService(ICacheService<Field> cache, TDbContext database, ILogger<UpsertService<TDbContext, Field>> logger,
            IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> attributeLists,
            IUpsertUniqueListService<TDbContext, DocumentationComment, DocumentationCommentList, DocumentationCommentListAssociation> documentationCommentLists,
            IUpsertService<TDbContext, Expression> expressions,
            IUpsertService<TDbContext, Identifier> identifiers,
            IUpsertUniqueListService<TDbContext, SyntaxToken, ModifierList, ModifierListAssociation> modifierLists)
            : base(cache, database, logger, database.Fields)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Field)}={record.AttributeListCollectionId}:{record.DocumentationCommentListId}:{record.IdentifierId}:{record.InitializerId}:{record.ModifierListId}:{record.TypeId}";
            _attributeLists = attributeLists ?? throw new ArgumentNullException(nameof(attributeLists));
            _documentationCommentLists = documentationCommentLists ?? throw new ArgumentNullException(nameof(documentationCommentLists));
            _expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _modifierLists = modifierLists ?? throw new ArgumentNullException(nameof(modifierLists));
        }

        protected override async Task<Field> AssignUpsertedReferences(Field record)
        {
            record.AttributeListCollection = await _attributeLists.UpsertAsync(record.AttributeListCollection);
            record.AttributeListCollectionId = record.AttributeListCollection?.AttributeListCollectionId ?? record.AttributeListCollectionId;
            record.DocumentationCommentList = await _documentationCommentLists.UpsertAsync(record.DocumentationCommentList);
            record.DocumentationCommentListId = record.DocumentationCommentList?.DocumentationCommentListId ?? record.DocumentationCommentListId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.Initializer = await _expressions.UpsertAsync(record.Initializer);
            record.InitializerId = record.Initializer?.ExpressionId ?? record.InitializerId;
            record.ModifierList = await _modifierLists.UpsertAsync(record.ModifierList);
            record.ModifierListId = record.ModifierList?.ModifierListId ?? record.ModifierListId;
            record.Type = await _identifiers.UpsertAsync(record.Type);
            record.TypeId = record.Type?.IdentifierId ?? record.TypeId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Field record)
        {
            yield return record.AttributeListCollection;
            yield return record.DocumentationCommentList;
            yield return record.Identifier;
            yield return record.Initializer;
            yield return record.ModifierList;
            yield return record.Type;
        }

        protected override Expression<Func<Field, bool>> FindExisting(Field record)
            => existing
                => ((existing.AttributeListCollectionId == null && record.AttributeListCollectionId == null) || (existing.AttributeListCollectionId == record.AttributeListCollectionId))
                && ((existing.DocumentationCommentListId == null && record.DocumentationCommentListId == null) || (existing.DocumentationCommentListId == record.DocumentationCommentListId))
                && existing.IdentifierId == record.IdentifierId
                && ((existing.InitializerId == null && record.InitializerId == null) || (existing.InitializerId == record.InitializerId))
                && ((existing.ModifierListId == null && record.ModifierListId == null) || (existing.ModifierListId == record.ModifierListId))
                && existing.TypeId == record.TypeId;
    }
}
