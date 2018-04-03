using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class PropertyListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, PropertyListAssociation>
        where TDbContext : TypeScriptDbContext
    {
        public PropertyListAssociationUpsertService(ICacheService<PropertyListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, PropertyListAssociation>> logger)
            : base(cache, database, logger, database.PropertyListAssociations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(PropertyListAssociation)}={record.PropertyId}:{record.PropertyListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(PropertyListAssociation record)
        {
            yield return record.Property;
            yield return record.PropertyList;
        }

        protected override Expression<Func<PropertyListAssociation, bool>> FindExisting(PropertyListAssociation record)
            => existing
                => existing.PropertyId == record.PropertyId
                && existing.PropertyListId == record.PropertyListId;
    }
}
