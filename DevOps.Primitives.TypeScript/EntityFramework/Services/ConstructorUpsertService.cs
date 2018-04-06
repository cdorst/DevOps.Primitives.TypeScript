using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class ConstructorUpsertService<TDbContext> : UpsertService<TDbContext, Constructor>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> _attributeLists;
        private readonly IUpsertService<TDbContext, Block> _blocks;
        private readonly IUpsertService<TDbContext, Identifier> _identifiers;
        private readonly IUpsertUniqueListService<TDbContext, Parameter, ParameterList, ParameterListAssociation> _parameterLists;

        public ConstructorUpsertService(ICacheService<Constructor> cache, TDbContext database, ILogger<UpsertService<TDbContext, Constructor>> logger,
            IUpsertUniqueListService<TDbContext, Decorator, DecoratorList, DecoratorListAssociation> attributeLists,
            IUpsertService<TDbContext, Block> blocks,
            IUpsertService<TDbContext, Identifier> identifiers,
            IUpsertUniqueListService<TDbContext, Parameter, ParameterList, ParameterListAssociation> parameterLists)
            : base(cache, database, logger, database.Constructors)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Constructor)}={record.AccessModifier}:{record.BlockId}:{record.DecoratorListId}:{record.IdentifierId}:{record.ParameterListId}";
            _attributeLists = attributeLists ?? throw new ArgumentNullException(nameof(attributeLists));
            _blocks = blocks ?? throw new ArgumentNullException(nameof(blocks));
            _identifiers = identifiers ?? throw new ArgumentNullException(nameof(identifiers));
            _parameterLists = parameterLists ?? throw new ArgumentNullException(nameof(parameterLists));
        }

        protected override async Task<Constructor> AssignUpsertedReferences(Constructor record)
        {
            record.DecoratorList = await _attributeLists.UpsertAsync(record.DecoratorList);
            record.DecoratorListId = record.DecoratorList?.DecoratorListId ?? record.DecoratorListId;
            record.Block = await _blocks.UpsertAsync(record.Block);
            record.BlockId = record.Block?.BlockId ?? record.BlockId;
            record.Identifier = await _identifiers.UpsertAsync(record.Identifier);
            record.IdentifierId = record.Identifier?.IdentifierId ?? record.IdentifierId;
            record.ParameterList = await _parameterLists.UpsertAsync(record.ParameterList);
            record.ParameterListId = record.ParameterList?.ParameterListId ?? record.ParameterListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Constructor record)
        {
            yield return record.DecoratorList;
            yield return record.Block;
            yield return record.Identifier;
            yield return record.ParameterList;
        }

        protected override Expression<Func<Constructor, bool>> FindExisting(Constructor record)
            => existing
                => ((existing.AccessModifier == null && record.AccessModifier == null) || (existing.AccessModifier == record.AccessModifier))
                && ((existing.DecoratorListId == null && record.DecoratorListId == null) || (existing.DecoratorListId == record.DecoratorListId))
                && existing.BlockId == record.BlockId
                && existing.IdentifierId == record.IdentifierId
                && ((existing.ParameterListId == null && record.ParameterListId == null) || (existing.ParameterListId == record.ParameterListId));
    }
}
