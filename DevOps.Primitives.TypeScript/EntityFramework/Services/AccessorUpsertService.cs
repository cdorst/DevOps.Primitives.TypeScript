using Common.EntityFrameworkServices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DevOps.Primitives.TypeScript.EntityFramework.Services
{
    public class AccessorUpsertService<TDbContext> : UpsertService<TDbContext, Accessor>
        where TDbContext : TypeScriptDbContext
    {
        private readonly IUpsertService<TDbContext, Block> _blocks;
        private readonly IUpsertService<TDbContext, SyntaxToken> _syntaxTokens;

        public AccessorUpsertService(ICacheService<Accessor> cache, TDbContext database, ILogger<UpsertService<TDbContext, Accessor>> logger, IUpsertService<TDbContext, Block> blocks, IUpsertService<TDbContext, SyntaxToken> syntaxTokens)
            : base(cache, database, logger, database.Accessors)
        {
            CacheKey = record => $"{nameof(TypeScript)}.{nameof(Accessor)}={record.BodyId}:{record.SyntaxTokenId}";
            _blocks = blocks ?? throw new ArgumentNullException(nameof(blocks));
            _syntaxTokens = syntaxTokens ?? throw new ArgumentNullException(nameof(syntaxTokens));
        }

        protected override async Task<Accessor> AssignUpsertedReferences(Accessor record)
        {
            record.Body = await _blocks.UpsertAsync(record.Body);
            record.BodyId = record.Body?.BlockId ?? record.BodyId;
            record.SyntaxToken = await _syntaxTokens.UpsertAsync(record.SyntaxToken);
            record.SyntaxTokenId = record.SyntaxToken?.SyntaxTokenId ?? record.SyntaxTokenId;
            return record;
        }

        protected override IEnumerable<object> EnumerateReferences(Accessor record)
        {
            yield return record.Body;
            yield return record.SyntaxToken;
        }

        protected override Expression<Func<Accessor, bool>> FindExisting(Accessor record)
            => existing
                => ((existing.BodyId == null && record.BodyId == null) || (existing.BodyId == record.BodyId))
                && existing.SyntaxTokenId == record.SyntaxTokenId;
    }
}
