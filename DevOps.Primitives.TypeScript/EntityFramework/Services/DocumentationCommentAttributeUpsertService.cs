using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class DocumentationCommentAttributeUpsertService<TDbContext> : UpsertService<TDbContext, DocumentationCommentAttribute>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public DocumentationCommentAttributeUpsertService(ICacheService<DocumentationCommentAttribute> cache, TDbContext database, ILogger<UpsertService<TDbContext, DocumentationCommentAttribute>> logger, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.DocumentationCommentAttributes)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(DocumentationCommentAttribute)}={record.IdentifierId}";
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<DocumentationCommentAttribute> AssignUpsertedReferences(DocumentationCommentAttribute record)
        {
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.Value = await _identifiers.UpsertAsync(record.Value);
            record.ValueId = record.Value?.IdentifierId ?? record.ValueId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(DocumentationCommentAttribute record)
        {
            yield return record.Identifier;
            yield return record.Value;
        }

        protected override Expression<Func<DocumentationCommentAttribute, bool>> FindExisting(DocumentationCommentAttribute record)
            => existing
                => existing.IdentifierId == record.IdentifierId
                && existing.ValueId == record.ValueId;
    }
}

