using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ImportStatementListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, ImportStatementListAssociation>
        where TDbContext : TypeScriptDbContext
    {
        public ImportStatementListAssociationUpsertService(ICacheService<ImportStatementListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, ImportStatementListAssociation>> logger)
            : base(cache, database, logger, database.ImportStatementListAssociations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ImportStatementListAssociation)}={record.ImportStatementId}:{record.ImportStatementListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(ImportStatementListAssociation record)
        {
            yield return record.ImportStatement;
            yield return record.ImportStatementList;
        }

        protected override Expression<Func<ImportStatementListAssociation, bool>> FindExisting(ImportStatementListAssociation record)
            => existing
                => existing.ImportStatementId == record.ImportStatementId
                && existing.ImportStatementListId == record.ImportStatementListId;
    }
}
