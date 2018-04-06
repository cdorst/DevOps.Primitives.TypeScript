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
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _decoratorLists;
        private readonly IUpsertService<TDbContext, Block> _blocks;
        private readonly IUpsertService<TDbContext, DocumentationComment> _documentationComments;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertUniqueListService<TDbContext, Parameter, ParameterList, ParameterListAssociation> _parameterLists;
        private readonly IUpsertUniqueListService<TDbContext, TypeParameter, TypeParameterList, TypeParameterListAssociation> _typeParameterLists;

        public MethodUpsertService(ICacheService<Method> cache, TDbContext database, ILogger<UpsertService<TDbContext, Method>> logger,
            IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> decoratorLists,
            IUpsertService<TDbContext, Block> blocks,
            IUpsertService<TDbContext, DocumentationComment> documentationComments,
            IUpsertService<TDbContext, Identifier> identifiers,
            IUpsertUniqueListService<TDbContext, Parameter, ParameterList, ParameterListAssociation> parameterLists,
            IUpsertUniqueListService<TDbContext, TypeParameter, TypeParameterList, TypeParameterListAssociation> typeParameterLists)
            : base(cache, database, logger, database.Methods)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Method)}={record.AccessModifier}:{record.IsAsync}:{record.BlockId}:{record.DecoratorListId}:{record.DocumentationCommentId}:{record.IdentifierId}:{record.ParameterListId}:{record.TypeId}:{record.TypeParameterListId}";
            _blocks = blocks ?? throw new ArgumentNullException(nameof(blocks));
            _decoratorLists = decoratorLists ?? throw new ArgumentNullException(nameof(decoratorLists));
            _documentationComments = documentationComments ?? throw new ArgumentNullException(nameof(documentationComments));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _parameterLists = parameterLists ?? throw new ArgumentNullException(nameof(parameterLists));
            _typeParameterLists = typeParameterLists ?? throw new ArgumentNullException(nameof(typeParameterLists));
        }

        protected override async Task<Method> AssignUpsertedReferences(Method record)
        {
            record.DecoratorList = await _decoratorLists.UpsertAsync(record.DecoratorList);
            record.DecoratorListId = record.DecoratorList?.DecoratorListId ?? record.DecoratorListId;
            record.Block = await _blocks.UpsertAsync(record.Block);
            record.BlockId = record.Block?.BlockId ?? record.BlockId;
            record.DocumentationComment = await _documentationComments.UpsertAsync(record.DocumentationComment);
            record.DocumentationCommentId = record.DocumentationComment?.DocumentationCommentId ?? record.DocumentationCommentId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
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
            yield return record.Block;
            yield return record.DecoratorList;
            yield return record.DocumentationComment;
            yield return record.Identifier;
            yield return record.ParameterList;
            yield return record.Type;
            yield return record.TypeParameterList;
        }

        protected override Expression<Func<Method, bool>> FindExisting(Method record)
            => existing
                => ((existing.AccessModifier == null && record.AccessModifier == null) || (existing.AccessModifier == record.AccessModifier))
                && existing.IsAsync == record.IsAsync
                && ((existing.BlockId == null && record.BlockId == null) || (existing.BlockId == record.BlockId))
                && ((existing.DecoratorListId == null && record.DecoratorListId == null) || (existing.DecoratorListId == record.DecoratorListId))
                && existing.DocumentationCommentId == record.DocumentationCommentId
                && existing.IdentifierId == record.IdentifierId
                && ((existing.ParameterListId == null && record.ParameterListId == null) || (existing.ParameterListId == record.ParameterListId))
                && existing.TypeId == record.TypeId
                && ((existing.TypeParameterListId == null && record.TypeParameterListId == null) || (existing.TypeParameterListId == record.TypeParameterListId));
    }
}
