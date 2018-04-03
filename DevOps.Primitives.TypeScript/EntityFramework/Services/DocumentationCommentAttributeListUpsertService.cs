using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class DocumentationCommentAttributeListUpsertService<TDbContext> : UpsertService<TDbContext, DocumentationCommentAttributeList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public DocumentationCommentAttributeListUpsertService(ICacheService<DocumentationCommentAttributeList> cache, TDbContext database, ILogger<UpsertService<TDbContext, DocumentationCommentAttributeList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.DocumentationCommentAttributeLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(DocumentationCommentAttributeList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<DocumentationCommentAttributeList> AssignUpsertedReferences(DocumentationCommentAttributeList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(DocumentationCommentAttributeList record)
        {
            yield return record.DocumentationCommentAttributeListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<DocumentationCommentAttributeList, bool>> FindExisting(DocumentationCommentAttributeList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
