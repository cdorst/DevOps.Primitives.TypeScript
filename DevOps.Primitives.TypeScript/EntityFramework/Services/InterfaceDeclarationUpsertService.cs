using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class InterfaceDeclarationUpsertService<TDbContext> : UpsertService<TDbContext, InterfaceDeclaration>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _decoratorLists;
        private readonly IUpsertUniqueListService<TDbContext, BaseType, BaseList, BaseListAssociation> _baseLists;
        private readonly IUpsertService<TDbContext, DocumentationComment> _documentationComments;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertUniqueListService<TDbContext, ImportStatement, ImportStatementList, ImportStatementListAssociation> _importStatementLists;
        private readonly IUpsertUniqueListService<TDbContext, Method, MethodList, MethodListAssociation> _methodLists;
        private readonly IUpsertService<TDbContext, Namespace> _namespaces;
        private readonly IUpsertUniqueListService<TDbContext, Property, PropertyList, PropertyListAssociation> _propertyLists;
        private readonly IUpsertUniqueListService<TDbContext, TypeParameter, TypeParameterList, TypeParameterListAssociation> _typeParameterLists;

        public InterfaceDeclarationUpsertService(ICacheService<InterfaceDeclaration> cache, TDbContext database, ILogger<UpsertService<TDbContext, InterfaceDeclaration>> logger,
            IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> attributeLists,
            IUpsertUniqueListService<TDbContext, BaseType, BaseList, BaseListAssociation> baseLists,
            IUpsertService<TDbContext, DocumentationComment> documentationComments,
            IUpsertService<TDbContext, Identifier> identifiers,
            IUpsertUniqueListService<TDbContext, ImportStatement, ImportStatementList, ImportStatementListAssociation> importStatementLists,
            IUpsertUniqueListService<TDbContext, Method, MethodList, MethodListAssociation> methodLists,
            IUpsertService<TDbContext, Namespace> namespaces,
            IUpsertUniqueListService<TDbContext, Property, PropertyList, PropertyListAssociation> propertyLists,
            IUpsertUniqueListService<TDbContext, TypeParameter, TypeParameterList, TypeParameterListAssociation> typeParameterLists)
            : base(cache, database, logger, database.InterfaceDeclarations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(InterfaceDeclaration)}={record.DecoratorListId}:{record.ExtendsList}:{record.DocumentationCommentId}:{record.IdentifierId}:{record.ImportStatementListId}:{record.MethodListId}:{record.NamespaceId}:{record.PropertyListId}:{record.TypeParameterListId}";
            _decoratorLists = attributeLists ?? throw new ArgumentNullException(nameof(attributeLists));
            _baseLists = baseLists ?? throw new ArgumentNullException(nameof(baseLists));
            _documentationComments = documentationComments ?? throw new ArgumentNullException(nameof(documentationComments));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _importStatementLists = importStatementLists ?? throw new ArgumentNullException(nameof(importStatementLists));
            _methodLists = methodLists ?? throw new ArgumentNullException(nameof(methodLists));
            _namespaces = namespaces ?? throw new ArgumentNullException(nameof(namespaces));
            _propertyLists = propertyLists ?? throw new ArgumentNullException(nameof(propertyLists));
            _typeParameterLists = typeParameterLists ?? throw new ArgumentNullException(nameof(typeParameterLists));
        }

        protected override async Task<InterfaceDeclaration> AssignUpsertedReferences(InterfaceDeclaration record)
        {
            record.DecoratorList = await _decoratorLists.UpsertAsync(record.DecoratorList);
            record.DecoratorListId = record.DecoratorList?.DecoratorListId ?? record.DecoratorListId;
            record.ExtendsList = await _baseLists.UpsertAsync(record.ExtendsList);
            record.ExtendsListId = record.ExtendsList?.BaseListId ?? record.ExtendsListId;
            record.DocumentationComment = await _documentationComments.UpsertAsync(record.DocumentationComment);
            record.DocumentationCommentId = record.DocumentationComment?.DocumentationCommentId ?? record.DocumentationCommentId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.ImportStatementList = await _importStatementLists.UpsertAsync(record.ImportStatementList);
            record.ImportStatementListId = record.ImportStatementList?.ImportStatementListId ?? record.ImportStatementListId;
            record.MethodList = await _methodLists.UpsertAsync(record.MethodList);
            record.MethodListId = record.MethodList?.MethodListId ?? record.MethodListId;
            record.Namespace = await _namespaces.UpsertAsync(record.Namespace);
            record.NamespaceId = record.Namespace?.NamespaceId ?? record.NamespaceId;
            record.PropertyList = await _propertyLists.UpsertAsync(record.PropertyList);
            record.PropertyListId = record.PropertyList?.PropertyListId ?? record.PropertyListId;
            record.TypeParameterList = await _typeParameterLists.UpsertAsync(record.TypeParameterList);
            record.TypeParameterListId = record.TypeParameterList?.TypeParameterListId ?? record.TypeParameterListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(InterfaceDeclaration record)
        {
            yield return record.DecoratorList;
            yield return record.ExtendsList;
            yield return record.Constructor;
            yield return record.DocumentationComment;
            yield return record.Identifier;
            yield return record.ImportStatementList;
            yield return record.MethodList;
            yield return record.Namespace;
            yield return record.PropertyList;
            yield return record.TypeParameterList;
        }

        protected override Expression<Func<InterfaceDeclaration, bool>> FindExisting(InterfaceDeclaration record)
            => existing
                => existing.IdentifierId == record.IdentifierId
                && existing.NamespaceId == record.NamespaceId;
    }
}
