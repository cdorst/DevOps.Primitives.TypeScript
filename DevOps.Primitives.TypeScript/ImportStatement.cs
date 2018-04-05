using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ImportStatements", Schema = nameof(TypeScript))]
    public class ImportStatement : IUniqueListRecord
    {
        public ImportStatement() { }
        public ImportStatement(Identifier exportName, Identifier moduleName)
        {
            ExportName = exportName;
            ModuleName = moduleName;
        }
        public ImportStatement(string exportName, string moduleName) : this(new Identifier(exportName), new Identifier(moduleName)) { }

        [Key]
        [ProtoMember(1)]
        public int ImportStatementId { get; set; }

        [ProtoMember(2)]
        public Identifier ExportName { get; set; }
        [ProtoMember(3)]
        public int ExportNameId { get; set; }

        [ProtoMember(4)]
        public Identifier ModuleName { get; set; }
        [ProtoMember(5)]
        public int ModuleNameId { get; set; }

        public string GetImportStatementSyntax()
            => $"import {ExportName} from \"{ModuleName}\";";
    }
}
