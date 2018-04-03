using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ImportStatementUpsertService<TDbContext> : UpsertService<TDbContext, ImportStatement>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;

        public ImportStatementUpsertService(ICacheService<ImportStatement> cache, TDbContext database, ILogger<UpsertService<TDbContext, ImportStatement>> logger, IUpsertService<TDbContext, Identifier> identifiers)
            : base(cache, database, logger, database.ImportStatements)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ImportStatement)}={record.ModuleNameId}";
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
        }

        protected override async Task<ImportStatement> AssignUpsertedReferences(ImportStatement record)
        {
            record.ExportName = await _identifiers.UpsertAsync(record.ExportName);
            record.ExportNameId = record.ExportName?.IdentifierId ?? record.ExportNameId;
            record.ModuleName = await _identifiers.UpsertAsync(record.ModuleName);
            record.ModuleNameId = record.ModuleName?.IdentifierId ?? record.ModuleNameId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(ImportStatement record)
        {
            yield return record.ExportName;
            yield return record.ModuleName;
        }

        protected override Expression<Func<ImportStatement, bool>> FindExisting(ImportStatement record)
            => existing
                => existing.ExportNameId == record.ExportNameId
                && existing.ModuleNameId == record.ModuleNameId;
    }
}
