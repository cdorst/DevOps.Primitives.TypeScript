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
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _attributeLists;
        private readonly IUpsertUniqueListService<TDbContext, BaseType, BaseList, BaseListAssociation> _baseLists;
        private readonly IUpsertUniqueListService<TDbContext, ConstraintClause, ConstraintClauseList, ConstraintClauseListAssociation> _constraintClauseLists;
        private readonly IUpsertUniqueListService<TDbContext, Constructor, ConstructorList, ConstructorListAssociation> _constuctorLists;
        private readonly IUpsertUniqueListService<TDbContext, DocumentationComment, DocumentationCommentList, DocumentationCommentListAssociation> _documentationCommentLists;
        private readonly IUpsertUniqueListService<TDbContext, Field, FieldList, FieldListAssociation> _fieldLists;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertUniqueListService<TDbContext, ImportStatement, ImportStatementList, ImportStatementListAssociation> _importStatementLists;
        private readonly IUpsertUniqueListService<TDbContext, Method, MethodList, MethodListAssociation> _methodLists;
        private readonly IUpsertUniqueListService<TDbContext, SyntaxToken, ModifierList, ModifierListAssociation> _modifierLists;
        private readonly IUpsertService<TDbContext, Namespace> _namespaces;
        private readonly IUpsertUniqueListService<TDbContext, Property, PropertyList, PropertyListAssociation> _propertyLists;
        private readonly IUpsertUniqueListService<TDbContext, TypeParameter, TypeParameterList, TypeParameterListAssociation> _typeParameterLists;

        public InterfaceDeclarationUpsertService(ICacheService<InterfaceDeclaration> cache, TDbContext database, ILogger<UpsertService<TDbContext, InterfaceDeclaration>> logger,
            IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> attributeLists,
            IUpsertUniqueListService<TDbContext, BaseType, BaseList, BaseListAssociation> baseLists,
            IUpsertUniqueListService<TDbContext, ConstraintClause, ConstraintClauseList, ConstraintClauseListAssociation> constraintClauseLists,
            IUpsertUniqueListService<TDbContext, Constructor, ConstructorList, ConstructorListAssociation> constuctorLists,
            IUpsertUniqueListService<TDbContext, DocumentationComment, DocumentationCommentList, DocumentationCommentListAssociation> documentationCommentLists,
            IUpsertUniqueListService<TDbContext, Field, FieldList, FieldListAssociation> fieldLists,
            IUpsertService<TDbContext, Identifier> identifiers,
            IUpsertUniqueListService<TDbContext, ImportStatement, ImportStatementList, ImportStatementListAssociation> importStatementLists,
            IUpsertUniqueListService<TDbContext, Method, MethodList, MethodListAssociation> methodLists,
            IUpsertUniqueListService<TDbContext, SyntaxToken, ModifierList, ModifierListAssociation> modifierLists,
            IUpsertService<TDbContext, Namespace> namespaces,
            IUpsertUniqueListService<TDbContext, Property, PropertyList, PropertyListAssociation> propertyLists,
            IUpsertUniqueListService<TDbContext, TypeParameter, TypeParameterList, TypeParameterListAssociation> typeParameterLists)
            : base(cache, database, logger, database.InterfaceDeclarations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(InterfaceDeclaration)}={record.AttributeListCollectionId}:{record.BaseListId}:{record.ConstraintClauseListId}:{record.ConstructorListId}:{record.DocumentationCommentListId}:{record.FieldListId}:{record.IdentifierId}:{record.ImportStatementListId}:{record.MethodListId}:{record.ModifierListId}:{record.NamespaceId}:{record.PropertyListId}:{record.TypeParameterListId}";
            _attributeLists = attributeLists ?? throw new ArgumentNullException(nameof(attributeLists));
            _baseLists = baseLists ?? throw new ArgumentNullException(nameof(baseLists));
            _constraintClauseLists = constraintClauseLists ?? throw new ArgumentNullException(nameof(constraintClauseLists));
            _constuctorLists = constuctorLists ?? throw new ArgumentNullException(nameof(constuctorLists));
            _documentationCommentLists = documentationCommentLists ?? throw new ArgumentNullException(nameof(documentationCommentLists));
            _fieldLists = fieldLists ?? throw new ArgumentNullException(nameof(fieldLists));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _importStatementLists = importStatementLists ?? throw new ArgumentNullException(nameof(importStatementLists));
            _methodLists = methodLists ?? throw new ArgumentNullException(nameof(methodLists));
            _modifierLists = modifierLists ?? throw new ArgumentNullException(nameof(modifierLists));
            _namespaces = namespaces ?? throw new ArgumentNullException(nameof(namespaces));
            _propertyLists = propertyLists ?? throw new ArgumentNullException(nameof(propertyLists));
            _typeParameterLists = typeParameterLists ?? throw new ArgumentNullException(nameof(typeParameterLists));
        }

        protected override Action<InterfaceDeclaration, InterfaceDeclaration> AssignChanges
            => (existing, given)
                => existing.Export = given.Export;

        protected override async Task<InterfaceDeclaration> AssignUpsertedReferences(InterfaceDeclaration record)
        {
            record.AttributeListCollection = await _attributeLists.UpsertAsync(record.AttributeListCollection);
            record.AttributeListCollectionId = record.AttributeListCollection?.AttributeListCollectionId ?? record.AttributeListCollectionId;
            record.BaseList = await _baseLists.UpsertAsync(record.BaseList);
            record.BaseListId = record.BaseList?.BaseListId ?? record.BaseListId;
            record.ConstraintClauseList = await _constraintClauseLists.UpsertAsync(record.ConstraintClauseList);
            record.ConstraintClauseListId = record.ConstraintClauseList?.ConstraintClauseListId ?? record.ConstraintClauseListId;
            record.ConstructorList = await _constuctorLists.UpsertAsync(record.ConstructorList);
            record.ConstructorListId = record.ConstructorList?.ConstructorListId ?? record.ConstructorListId;
            record.DocumentationCommentList = await _documentationCommentLists.UpsertAsync(record.DocumentationCommentList);
            record.DocumentationCommentListId = record.DocumentationCommentList?.DocumentationCommentListId ?? record.DocumentationCommentListId;
            record.FieldList = await _fieldLists.UpsertAsync(record.FieldList);
            record.FieldListId = record.FieldList?.FieldListId ?? record.FieldListId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.ImportStatementList = await _importStatementLists.UpsertAsync(record.ImportStatementList);
            record.ImportStatementListId = record.ImportStatementList?.ImportStatementListId ?? record.ImportStatementListId;
            record.MethodList = await _methodLists.UpsertAsync(record.MethodList);
            record.MethodListId = record.MethodList?.MethodListId ?? record.MethodListId;
            record.ModifierList = await _modifierLists.UpsertAsync(record.ModifierList);
            record.ModifierListId = record.ModifierList?.ModifierListId ?? record.ModifierListId;
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
            yield return record.AttributeListCollection;
            yield return record.BaseList;
            yield return record.ConstraintClauseList;
            yield return record.ConstructorList;
            yield return record.DocumentationCommentList;
            yield return record.FieldList;
            yield return record.Identifier;
            yield return record.ImportStatementList;
            yield return record.MethodList;
            yield return record.ModifierList;
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
