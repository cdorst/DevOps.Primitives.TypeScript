using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Methods", Schema = nameof(TypeScript))]
    public class Method : IUniqueListRecord
    {
        public Method() { }
        public Method(Identifier identifier, Identifier type, ParameterList parameterList = null, Expression arrowClauseExpression = null, Block block = null, ModifierList modifierList = null, DocumentationCommentList documentationCommentList = null, DecoratorList attributes = null)
        {
            ArrowClauseExpressionValue = arrowClauseExpression;
            AttributeListCollection = attributes;
            Block = block;
            DocumentationCommentList = documentationCommentList;
            Identifier = identifier;
            ModifierList = modifierList;
            ParameterList = parameterList;
            Type = type;
        }
        public Method(string identifier, string type, ParameterList parameterList = null, Expression arrowClauseExpression = null, Block block = null, ModifierList modifierList = null, DocumentationCommentList documentationCommentList = null, DecoratorList attributes = null)
            : this(new Identifier(identifier), new Identifier(type), parameterList, arrowClauseExpression, block, modifierList, documentationCommentList, attributes)
        {
        }
        public Method(string identifier, string type, ParameterList parameterList = null, Block block = null, ModifierList modifierList = null, DocumentationCommentList documentationCommentList = null, DecoratorList attributes = null)
            : this(new Identifier(identifier), new Identifier(type), parameterList, null, block, modifierList, documentationCommentList, attributes)
        {
        }
        public Method(string identifier, string type, string arrowClauseExpression, ParameterList parameterList = null, ModifierList modifierList = null, DocumentationCommentList documentationCommentList = null, DecoratorList attributes = null)
            : this(new Identifier(identifier), new Identifier(type), parameterList, new Expression(arrowClauseExpression), null, modifierList, documentationCommentList, attributes)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MethodId { get; set; }

        [ProtoMember(2)]
        public Expression ArrowClauseExpressionValue { get; set; }
        [ProtoMember(3)]
        public int? ArrowClauseExpressionValueId { get; set; }

        [ProtoMember(4)]
        public DecoratorList AttributeListCollection { get; set; }
        [ProtoMember(5)]
        public int? AttributeListCollectionId { get; set; }

        [ProtoMember(6)]
        public Block Block { get; set; }
        [ProtoMember(7)]
        public int? BlockId { get; set; }

        [ProtoMember(8)]
        public DocumentationCommentList DocumentationCommentList { get; set; }
        [ProtoMember(9)]
        public int? DocumentationCommentListId { get; set; }

        [ProtoMember(10)]
        public Identifier Identifier { get; set; }
        [ProtoMember(11)]
        public int IdentifierId { get; set; }

        [ProtoMember(12)]
        public ModifierList ModifierList { get; set; }
        [ProtoMember(13)]
        public byte? ModifierListId { get; set; }

        [ProtoMember(14)]
        public ParameterList ParameterList { get; set; }
        [ProtoMember(15)]
        public int? ParameterListId { get; set; }

        [ProtoMember(16)]
        public Identifier Type { get; set; }
        [ProtoMember(17)]
        public int TypeId { get; set; }

        [ProtoMember(18)]
        public TypeParameterList TypeParameterList { get; set; }
        [ProtoMember(19)]
        public int? TypeParameterListId { get; set; }

        [ProtoMember(20)]
        public ConstraintClauseList ConstraintClauseList { get; set; }
        [ProtoMember(21)]
        public int? ConstraintClauseListId { get; set; }
    }
}
