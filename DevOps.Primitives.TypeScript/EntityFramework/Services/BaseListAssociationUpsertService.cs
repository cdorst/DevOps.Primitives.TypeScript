using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class BaseListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, BaseListAssociation>
        where TDbContext : TypeScriptDbContext
    {
        public BaseListAssociationUpsertService(ICacheService<BaseListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, BaseListAssociation>> logger)
            : base(cache, database, logger, database.BaseListAssociations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(BaseListAssociation)}={record.BaseTypeId}:{record.BaseListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(BaseListAssociation record)
        {
            yield return record.BaseType;
            yield return record.BaseList;
        }

        protected override Expression<Func<BaseListAssociation, bool>> FindExisting(BaseListAssociation record)
            => existing
                => existing.BaseTypeId == record.BaseTypeId
                && existing.BaseListId == record.BaseListId;
    }
}
