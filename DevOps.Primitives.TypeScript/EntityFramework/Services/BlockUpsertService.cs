using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class BlockUpsertService<TDbContext> : UpsertService<TDbContext, Block>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertUniqueListService<TDbContext, Statement, StatementList, StatementListAssociation> _statementLists;

        public BlockUpsertService(ICacheService<Block> cache, TDbContext database, ILogger<UpsertService<TDbContext, Block>> logger, IUpsertUniqueListService<TDbContext, Statement, StatementList, StatementListAssociation> statementLists)
            : base(cache, database, logger, database.Blocks)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Block)}={record.StatementListId}";
            _statementLists = statementLists ?? throw new ArgumentNullException(nameof(statementLists));
        }

        protected override async Task<Block> AssignUpsertedReferences(Block record)
        {
            record.StatementList = await _statementLists.UpsertAsync(record.StatementList);
            record.StatementListId = record.StatementList?.StatementListId ?? record.StatementListId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Block record)
        {
            yield return record.StatementList;
        }

        protected override Expression<Func<Block, bool>> FindExisting(Block record)
            => existing => existing.StatementListId == record.StatementListId;
    }
}
