using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class EnumMemberUpsertService<TDbContext> : UpsertService<TDbContext, EnumMember>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _attributeLists;
        private readonly IUpsertUniqueListService<TDbContext, DocumentationComment, DocumentationCommentList, DocumentationCommentListAssociation> _documentationComments;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public EnumMemberUpsertService(ICacheService<EnumMember> cache, TDbContext database, ILogger<UpsertService<TDbContext, EnumMember>> logger, IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> attributeLists, IUpsertUniqueListService<TDbContext, DocumentationComment, DocumentationCommentList, DocumentationCommentListAssociation> documentationComments, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.EnumMembers)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(EnumMember)}={record.EqualsValue}:{record.IdentifierId}:{record.DocumentationCommentListId}";
            _attributeLists = attributeLists ?? throw new ArgumentNullException(nameof(attributeLists));
            _documentationComments = documentationComments ?? throw new ArgumentNullException(nameof(documentationComments));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<EnumMember> AssignUpsertedReferences(EnumMember record)
        {
            record.AttributeListCollection = await _attributeLists.UpsertAsync(record.AttributeListCollection);
            record.AttributeListCollectionId = record.AttributeListCollection?.AttributeListCollectionId ?? record.AttributeListCollectionId;
            record.DocumentationCommentList = await _documentationComments.UpsertAsync(record.DocumentationCommentList);
            record.DocumentationCommentListId = record.DocumentationCommentList?.DocumentationCommentListId ?? record.DocumentationCommentListId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(EnumMember record)
        {
            yield return record.AttributeListCollection;
            yield return record.DocumentationCommentList;
            yield return record.Identifier;
        }

        protected override Expression<Func<EnumMember, bool>> FindExisting(EnumMember record)
            => existing
                => ((existing.EqualsValue == null && record.EqualsValue == null) || (existing.EqualsValue == record.EqualsValue))
                && ((existing.AttributeListCollectionId == null && record.AttributeListCollectionId == null) || (existing.AttributeListCollectionId == record.AttributeListCollectionId))
                && ((existing.DocumentationCommentListId == null && record.DocumentationCommentListId == null) || (existing.DocumentationCommentListId == record.DocumentationCommentListId))
                && existing.IdentifierId == record.IdentifierId;
    }
}
