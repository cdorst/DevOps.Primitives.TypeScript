using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
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
        private readonly IUpsertService<TDbContext, DocumentationComment> _documentationComments;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertService<TDbContext, UnicodeStringReference> _strings;

        public EnumMemberUpsertService(ICacheService<EnumMember> cache, TDbContext database, ILogger<UpsertService<TDbContext, EnumMember>> logger, IUpsertService<TDbContext, UnicodeStringReference> strings, IUpsertService<TDbContext, DocumentationComment> documentationComments, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.EnumMembers)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(EnumMember)}={record.DocumentationCommentId}:{record.EqualsValueId}:{record.IdentifierId}";
            _documentationComments = documentationComments ?? throw new ArgumentNullException(nameof(documentationComments));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<EnumMember> AssignUpsertedReferences(EnumMember record)
        {
            record.DocumentationComment = await _documentationComments.UpsertAsync(record.DocumentationComment);
            record.DocumentationCommentId = record.DocumentationComment?.DocumentationCommentId ?? record.DocumentationCommentId;
            record.EqualsValue = await _strings.UpsertAsync(record.EqualsValue);
            record.EqualsValueId = record.EqualsValue?.UnicodeStringReferenceId ?? record.EqualsValueId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(EnumMember record)
        {
            yield return record.DocumentationComment;
            yield return record.EqualsValue;
            yield return record.Identifier;
        }

        protected override Expression<Func<EnumMember, bool>> FindExisting(EnumMember record)
            => existing
                => existing.DocumentationCommentId == record.DocumentationCommentId
                && ((existing.EqualsValueId == null && record.EqualsValueId == null) || (existing.EqualsValueId == record.EqualsValueId))
                && existing.IdentifierId == record.IdentifierId;
    }
}
