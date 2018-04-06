using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class EnumDeclarationUpsertService<TDbContext> : UpsertService<TDbContext, EnumDeclaration>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, DocumentationComment> _documentationComments;
        private readonly IUpsertUniqueListService<TDbContext, EnumMember, EnumMemberList, EnumMemberListAssociation> _enumMemberLists;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertUniqueListService<TDbContext, ImportStatement, ImportStatementList, ImportStatementListAssociation> _importStatementLists;
        private readonly IUpsertService<TDbContext, Namespace> _namespaces;

        public EnumDeclarationUpsertService(ICacheService<EnumDeclaration> cache, TDbContext database, ILogger<UpsertService<TDbContext, EnumDeclaration>> logger,
            IUpsertService<TDbContext, DocumentationComment> documentationComments,
            IUpsertUniqueListService<TDbContext, EnumMember, EnumMemberList, EnumMemberListAssociation> enumMemberLists,
            IUpsertService<TDbContext, Identifier> identifiers,
            IUpsertUniqueListService<TDbContext, ImportStatement, ImportStatementList, ImportStatementListAssociation> importStatementLists,
            IUpsertService<TDbContext, Namespace> namespaces)
            : base(cache, database, logger, database.EnumDeclarations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(EnumDeclaration)}={record.EnumMemberListId}:{record.DocumentationCommentId}:{record.IdentifierId}:{record.ImportStatementListId}:{record.NamespaceId}";
            _documentationComments = documentationComments ?? throw new ArgumentNullException(nameof(documentationComments));
            _enumMemberLists = enumMemberLists ?? throw new ArgumentNullException(nameof(enumMemberLists));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _importStatementLists = importStatementLists ?? throw new ArgumentNullException(nameof(importStatementLists));
            _namespaces = namespaces ?? throw new ArgumentNullException(nameof(namespaces));
        }

        protected override Action<EnumDeclaration, EnumDeclaration> AssignChanges
            => (existing, given)
                => existing.Export = given.Export;

        protected override async Task<EnumDeclaration> AssignUpsertedReferences(EnumDeclaration record)
        {
            record.DocumentationComment = await _documentationComments.UpsertAsync(record.DocumentationComment);
            record.DocumentationCommentId = record.DocumentationComment?.DocumentationCommentId ?? record.DocumentationCommentId;
            record.EnumMemberList = await _enumMemberLists.UpsertAsync(record.EnumMemberList);
            record.EnumMemberListId = record.EnumMemberList?.EnumMemberListId ?? record.EnumMemberListId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.ImportStatementList = await _importStatementLists.UpsertAsync(record.ImportStatementList);
            record.ImportStatementListId = record.ImportStatementList?.ImportStatementListId ?? record.ImportStatementListId;
            record.Namespace = await _namespaces.UpsertAsync(record.Namespace);
            record.NamespaceId = record.Namespace?.NamespaceId ?? record.NamespaceId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(EnumDeclaration record)
        {
            yield return record.EnumMemberList;
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

        protected override Expression<Func<EnumDeclaration, bool>> FindExisting(EnumDeclaration record)
            => existing
                => existing.IdentifierId == record.IdentifierId
                && existing.NamespaceId == record.NamespaceId;
    }
}
