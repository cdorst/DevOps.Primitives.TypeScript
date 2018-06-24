using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ImportStatements", Schema = nameof(TypeScript))]
    public class ImportStatement : IUniqueListRecord
    {
        public ImportStatement() { }
        public ImportStatement(
            in Identifier exportName,
            in Identifier moduleName)
        {
            ExportName = exportName;
            ModuleName = moduleName;
        }
        public ImportStatement(
            in string exportName,
            in string moduleName)
            : this(new Identifier(in exportName), new Identifier(in moduleName)) { }

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
            => Concat("import ", ExportName, " from \"", ModuleName, "\";");
    }
}
