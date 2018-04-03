using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class MethodUpsertService<TDbContext> : UpsertService<TDbContext, Method>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _attributeLists;
        private readonly IUpsertService<TDbContext, Block> _blocks;
        private readonly IUpsertUniqueListService<TDbContext, ConstraintClause, ConstraintClauseList, ConstraintClauseListAssociation> _constraintClauseList;
        private readonly IUpsertUniqueListService<TDbContext, DocumentationComment, DocumentationCommentList, DocumentationCommentListAssociation> _documentationCommentLists;
        private readonly IUpsertService<TDbContext, Expression> _expressions;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertUniqueListService<TDbContext, SyntaxToken, ModifierList, ModifierListAssociation> _modifierLists;
        private readonly IUpsertUniqueListService<TDbContext, Parameter, ParameterList, ParameterListAssociation> _parameterLists;
        private readonly IUpsertUniqueListService<TDbContext, TypeParameter, TypeParameterList, TypeParameterListAssociation> _typeParameterLists;

        public MethodUpsertService(ICacheService<Method> cache, TDbContext database, ILogger<UpsertService<TDbContext, Method>> logger,
            IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> attributeLists,
            IUpsertService<TDbContext, Block> blocks,
            IUpsertUniqueListService<TDbContext, ConstraintClause, ConstraintClauseList, ConstraintClauseListAssociation> constraintClauseList,
            IUpsertUniqueListService<TDbContext, DocumentationComment, DocumentationCommentList, DocumentationCommentListAssociation> documentationCommentLists,
            IUpsertService<TDbContext, Expression> expressions,
            IUpsertService<TDbContext, Identifier> identifiers,
            IUpsertUniqueListService<TDbContext, SyntaxToken, ModifierList, ModifierListAssociation> modifierLists,
            IUpsertUniqueListService<TDbContext, Parameter, ParameterList, ParameterListAssociation> parameterLists,
            IUpsertUniqueListService<TDbContext, TypeParameter, TypeParameterList, TypeParameterListAssociation> typeParameterLists)
            : base(cache, database, logger, database.Methods)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Method)}={record.ArrowClauseExpressionValueId}:{record.AttributeListCollectionId}:{record.BlockId}:{record.DocumentationCommentListId}:{record.IdentifierId}:{record.ModifierListId}:{record.ParameterListId}:{record.TypeId}:{record.TypeParameterListId}";
            _attributeLists = attributeLists ?? throw new ArgumentNullException(nameof(attributeLists));
            _blocks = blocks ?? throw new ArgumentNullException(nameof(blocks));
            _constraintClauseList = constraintClauseList ?? throw new ArgumentNullException(nameof(constraintClauseList));
            _documentationCommentLists = documentationCommentLists ?? throw new ArgumentNullException(nameof(documentationCommentLists));
            _expressions = expressions ?? throw new ArgumentNullException(nameof(expressions));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _modifierLists = modifierLists ?? throw new ArgumentNullException(nameof(modifierLists));
            _parameterLists = parameterLists ?? throw new ArgumentNullException(nameof(parameterLists));
            _typeParameterLists = typeParameterLists ?? throw new ArgumentNullException(nameof(typeParameterLists));
        }

        protected override async Task<Method> AssignUpsertedReferences(Method record)
        {
            record.ArrowClauseExpressionValue = await _expressions.UpsertAsync(record.ArrowClauseExpressionValue);
            record.ArrowClauseExpressionValueId = record.ArrowClauseExpressionValue?.ExpressionId ?? record.ArrowClauseExpressionValueId;
            record.AttributeListCollection = await _attributeLists.UpsertAsync(record.AttributeListCollection);
            record.AttributeListCollectionId = record.AttributeListCollection?.AttributeListCollectionId ?? record.AttributeListCollectionId;
            record.Block = await _blocks.UpsertAsync(record.Block);
            record.BlockId = record.Block?.BlockId ?? record.BlockId;
            record.ConstraintClauseList = await _constraintClauseList.UpsertAsync(record.ConstraintClauseList);
            record.ConstraintClauseListId = record.ConstraintClauseList?.ConstraintClauseListId ?? record.ConstraintClauseListId;
            record.DocumentationCommentList = await _documentationCommentLists.UpsertAsync(record.DocumentationCommentList);
            record.DocumentationCommentListId = record.DocumentationCommentList?.DocumentationCommentListId ?? record.DocumentationCommentListId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.ModifierList = await _modifierLists.UpsertAsync(record.ModifierList);
            record.ModifierListId = record.ModifierList?.ModifierListId ?? record.ModifierListId;
            record.ParameterList = await _parameterLists.UpsertAsync(record.ParameterList);
            record.ParameterListId = record.ParameterList?.ParameterListId ?? record.ParameterListId;
            record.Type = await _identifiers.UpsertAsync(record.Type);
            record.TypeId = record.Type?.IdentifierId ?? record.TypeId;
            record.TypeParameterList = await _typeParameterLists.UpsertAsync(record.TypeParameterList);
            record.TypeParameterListId = record.TypeParameterList?.TypeParameterListId ?? record.TypeParameterListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Method record)
        {
            yield return record.ArrowClauseExpressionValue;
            yield return record.AttributeListCollection;
            yield return record.Block;
            yield return record.ConstraintClauseList;
            yield return record.DocumentationCommentList;
            yield return record.Identifier;
            yield return record.ModifierList;
            yield return record.ParameterList;
            yield return record.Type;
            yield return record.TypeParameterList;
        }

        protected override Expression<Func<Method, bool>> FindExisting(Method record)
            => existing
                => ((existing.ArrowClauseExpressionValueId == null && record.ArrowClauseExpressionValueId == null) || (existing.ArrowClauseExpressionValueId == record.ArrowClauseExpressionValueId))
                && ((existing.AttributeListCollectionId == null && record.AttributeListCollectionId == null) || (existing.AttributeListCollectionId == record.AttributeListCollectionId))
                && ((existing.BlockId == null && record.BlockId == null) || (existing.BlockId == record.BlockId))
                && ((existing.ConstraintClauseListId == null && record.ConstraintClauseListId == null) || (existing.ConstraintClauseListId == record.ConstraintClauseListId))
                && ((existing.DocumentationCommentListId == null && record.DocumentationCommentListId == null) || (existing.DocumentationCommentListId == record.DocumentationCommentListId))
                && existing.IdentifierId == record.IdentifierId
                && ((existing.ModifierListId == null && record.ModifierListId == null) || (existing.ModifierListId == record.ModifierListId))
                && ((existing.ParameterListId == null && record.ParameterListId == null) || (existing.ParameterListId == record.ParameterListId))
                && existing.TypeId == record.TypeId
                && ((existing.TypeParameterListId == null && record.TypeParameterListId == null) || (existing.TypeParameterListId == record.TypeParameterListId));
    }
}
