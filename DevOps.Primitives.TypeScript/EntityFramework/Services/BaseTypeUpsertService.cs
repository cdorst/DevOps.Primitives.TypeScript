using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class BaseTypeUpsertService<TDbContext> : UpsertService<TDbContext, BaseType>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertUniqueListService<TDbContext, TypeArgument, TypeArgumentList, TypeArgumentListAssociation> _typeArgumentLists;

        public BaseTypeUpsertService(ICacheService<BaseType> cache, TDbContext database, ILogger<UpsertService<TDbContext, BaseType>> logger, IUpsertService<TDbContext, Identifier> identifiers, IUpsertUniqueListService<TDbContext, TypeArgument, TypeArgumentList, TypeArgumentListAssociation> typeArgumentLists)
            : base(cache, database, logger, database.BaseTypes)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(BaseType)}={record.IdentifierId}:{record.TypeArgumentListId}";
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _typeArgumentLists = typeArgumentLists ?? throw new ArgumentNullException(nameof(typeArgumentLists));
        }

        protected override async Task<BaseType> AssignUpsertedReferences(BaseType record)
        {
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.TypeArgumentList = await _typeArgumentLists.UpsertAsync(record.TypeArgumentList);
            record.TypeArgumentListId = record.TypeArgumentList?.TypeArgumentListId ?? record.TypeArgumentListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(BaseType record)
        {
            yield return record.Identifier;
            yield return record.TypeArgumentList;
        }

        protected override Expression<Func<BaseType, bool>> FindExisting(BaseType record)
            => existing
                => existing.IdentifierId == record.IdentifierId
                && ((existing.TypeArgumentListId == null && record.TypeArgumentListId == null) || (existing.TypeArgumentListId == record.TypeArgumentListId));
    }
}
