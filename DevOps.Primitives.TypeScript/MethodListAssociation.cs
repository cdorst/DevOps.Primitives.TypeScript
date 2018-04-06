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
        public MethodListAssociation(Identifier identifier, DocumentationComment comment, Block block = null, Identifier type = null, AccessModifiers? accessModifier = null, bool isAsync = false, ParameterList parameterList = null, DecoratorList decoratorList = null, TypeParameterList typeParameterList = null, MethodList methodList = null)
            : this(new Method(identifier, comment, block, type, accessModifier, isAsync, parameterList, decoratorList, typeParameterList), methodList)
        {
        }
        public MethodListAssociation(string identifier, string comment, Block block = null, string type = null, AccessModifiers? accessModifier = null, bool isAsync = false, ParameterList parameterList = null, DecoratorList decoratorList = null, TypeParameterList typeParameterList = null, MethodList methodList = null)
            : this(new Identifier(identifier), new DocumentationComment(comment), block, string.IsNullOrWhiteSpace(type) ? null : new Identifier(type), accessModifier, isAsync, parameterList, decoratorList, typeParameterList, methodList)
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
