using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Constructors", Schema = nameof(TypeScript))]
    public class Constructor : IUniqueListRecord
    {
        public Constructor() { }
        public Constructor(
            Identifier identifier,
            Block block,
            ParameterList parameterList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList decoratorList = null)
        {
            Identifier = identifier;
            Block = block;
            ParameterList = parameterList;
            DocumentationCommentList = documentationCommentList;
            DecoratorList = decoratorList;
        }
        public Constructor(
            string identifier,
            Block block,
            ParameterList parameterList = null,
            DocumentationCommentList documentationCommentList = null,
            DecoratorList decoratorList = null)
            : this(new Identifier(identifier), block, parameterList, documentationCommentList, decoratorList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ConstructorId { get; set; }

        [ProtoMember(2)]
        public Block Block { get; set; }
        [ProtoMember(3)]
        public int BlockId { get; set; }

        [ProtoMember(4)]
        public DecoratorList DecoratorList { get; set; }
        [ProtoMember(5)]
        public int? DecoratorListId { get; set; }

        [ProtoMember(6)]
        public DocumentationCommentList DocumentationCommentList { get; set; }
        [ProtoMember(7)]
        public int? DocumentationCommentListId { get; set; }

        [ProtoMember(8)]
        public Identifier Identifier { get; set; }
        [ProtoMember(9)]
        public int IdentifierId { get; set; }

        [ProtoMember(10)]
        public ParameterList ParameterList { get; set; }
        [ProtoMember(11)]
        public int? ParameterListId { get; set; }
    }
}
