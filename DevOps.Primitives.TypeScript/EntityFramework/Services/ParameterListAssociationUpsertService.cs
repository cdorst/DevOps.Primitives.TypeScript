using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ParameterListAssociationUpsertService<TDbContext> : UpsertService<TDbContext, ParameterListAssociation>
        where TDbContext : TypeScriptDbContext
    {
        public ParameterListAssociationUpsertService(ICacheService<ParameterListAssociation> cache, TDbContext database, ILogger<UpsertService<TDbContext, ParameterListAssociation>> logger)
            : base(cache, database, logger, database.ParameterListAssociations)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(ParameterListAssociation)}={record.ParameterId}:{record.ParameterListId}";
        }

        protected override IEnumerable<object> EnumerateReferences(ParameterListAssociation record)
        {
            yield return record.Parameter;
            yield return record.ParameterList;
        }

        protected override Expression<Func<ParameterListAssociation, bool>> FindExisting(ParameterListAssociation record)
            => existing
                => existing.ParameterId == record.ParameterId
                && existing.ParameterListId == record.ParameterListId;
    }
}
