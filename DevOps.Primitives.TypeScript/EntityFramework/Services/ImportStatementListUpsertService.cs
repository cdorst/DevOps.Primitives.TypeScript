using Common.EntityFrameworkServices;
using DevOps.Primitives.Strings;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ImportStatementListUpsertService<TDbContext> : UpsertService<TDbContext, ImportStatementList>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, AsciiStringReference> _strings;

        public ImportStatementListUpsertService(ICacheService<ImportStatementList> cache, TDbContext database, ILogger<UpsertService<TDbContext, ImportStatementList>> logger, IUpsertService<TDbContext, AsciiStringReference> strings)
            : base(cache, database, logger, database.ImportStatementLists)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ImportStatementList)}={record.ListIdentifierId}";
            _strings = strings ?? throw new ArgumentNullException(nameof(strings));
        }

        protected override async Task<ImportStatementList> AssignUpsertedReferences(ImportStatementList record)
        {
            record.ListIdentifier = await _strings.UpsertAsync(record.ListIdentifier);
            record.ListIdentifierId = record.ListIdentifier?.AsciiStringReferenceId ?? record.ListIdentifierId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(ImportStatementList record)
        {
            yield return record.ImportStatementListAssociations;
            yield return record.ListIdentifier;
        }

        protected override Expression<Func<ImportStatementList, bool>> FindExisting(ImportStatementList record)
            => existing => existing.ListIdentifierId == record.ListIdentifierId;
    }
}
