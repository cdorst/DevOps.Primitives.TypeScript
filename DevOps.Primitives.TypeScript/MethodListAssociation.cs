using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("MethodListAssociations", Schema = nameof(TypeScript))]
    public class MethodListAssociation : IUniqueListAssociation<Method>
    {
        public MethodListAssociation() { }
        public MethodListAssociation(Method method, MethodList methodList = null)
        {
            Method = method;
            MethodList = methodList;
        }
        public MethodListAssociation(Identifier identifier, Identifier type, ParameterList parameterList = null, Expression arrowClauseExpression = null, Block block = null, ModifierList modifierList = null, DocumentationCommentList documentationCommentList = null, DecoratorList attributes = null, MethodList methodList = null)
            : this(new Method(identifier, type, parameterList, arrowClauseExpression, block, modifierList, documentationCommentList, attributes), methodList)
        {
        }
        public MethodListAssociation(string identifier, string type, ParameterList parameterList = null, Expression arrowClauseExpression = null, Block block = null, ModifierList modifierList = null, DocumentationCommentList documentationCommentList = null, DecoratorList attributes = null, MethodList methodList = null)
            : this(new Identifier(identifier), new Identifier(type), parameterList, arrowClauseExpression, block, modifierList, documentationCommentList, attributes, methodList)
        {
        }
        public MethodListAssociation(string identifier, string type, string arrowClauseExpression, ParameterList parameterList = null, ModifierList modifierList = null, DocumentationCommentList documentationCommentList = null, DecoratorList attributes = null, MethodList methodList = null)
            : this(new Identifier(identifier), new Identifier(type), parameterList, new Expression(arrowClauseExpression), null, modifierList, documentationCommentList, attributes, methodList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int MethodListAssociationId { get; set; }

        [ProtoMember(2)]
        public Method Method { get; set; }
        [ProtoMember(3)]
        public int MethodId { get; set; }

        [ProtoMember(4)]
        public MethodList MethodList { get; set; }
        [ProtoMember(5)]
        public int MethodListId { get; set; }

        public Method GetRecord() => Method;

        public void SetRecord(Method record)
        {
            Method = record;
            MethodId = Method.MethodId;
        }
    }
}
