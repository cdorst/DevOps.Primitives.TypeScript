using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class AccessorListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, AccessorListAssociation>
        where TDbContext : TypeScriptDbContext
    {
        public AccessorListAssociationUpsertService(ICacheService<AccessorListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, AccessorListAssociation>> logger)
            : base(cache, database, logger, database.AccessorListAssociations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(AccessorListAssociation)}={record.AccessorId}:{record.AccessorListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(AccessorListAssociation record)
        {
            yield return record.Accessor;
            yield return record.AccessorList;
        }

        protected override Expression<Func<AccessorListAssociation, bool>> FindExisting(AccessorListAssociation record)
            => existing
                => existing.AccessorId == record.AccessorId
                && existing.AccessorListId == record.AccessorListId;
    }
}
