using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class DocumentationCommentUpsertService<TDbContext> : UpsertService<TDbContext, DocumentationComment>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiMaxStringReference> _strings;

        public DocumentationCommentUpsertService(ICacheService<DocumentationComment> cache, TDbContext database, ILogger<UpsertService<TDbContext, DocumentationComment>> logger, IUpsertService<TDbContext, AsciiMaxStringReference> strings)
            : base(cache, database, logger, database.DocumentationComments)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(DocumentationComment)}={record.TextId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<DocumentationComment> AssignUpsertedReferences(DocumentationComment record)
        {
            record.Text = await _strings.UpsertAsync(record.Text);
            record.TextId = record.Text?.AsciiMaxStringReferenceId ?? record.TextId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(DocumentationComment record)
        {
            yield return record.Text;
        }

        protected override Expression<Func<DocumentationComment, bool>> FindExisting(DocumentationComment record)
            => existing
                => existing.TextId == record.TextId;
    }
}
