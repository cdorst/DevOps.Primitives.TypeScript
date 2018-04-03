using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class TypeParameterListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, TypeParameterListAssociation>
        where TDbContext : TypeScriptDbContext
    {
        public TypeParameterListAssociationUpsertService(ICacheService<TypeParameterListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, TypeParameterListAssociation>> logger)
            : base(cache, database, logger, database.TypeParameterListAssociations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(TypeParameterListAssociation)}={record.TypeParameterId}:{record.TypeParameterListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(TypeParameterListAssociation record)
        {
            yield return record.TypeParameter;
            yield return record.TypeParameterList;
        }

        protected override Expression<Func<TypeParameterListAssociation, bool>> FindExisting(TypeParameterListAssociation record)
            => existing
                => existing.TypeParameterId == record.TypeParameterId
                && existing.TypeParameterListId == record.TypeParameterListId;
    }
}
