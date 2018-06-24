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
        public MethodListAssociation(
            in Method method,
            in MethodList methodList = default)
        {
            Method = method;
            MethodList = methodList;
        }
        public MethodListAssociation(
            in Identifier identifier,
            in DocumentationComment comment,
            in Block block = default,
            in Identifier type = default,
            in AccessModifiers? accessModifier = default,
            in bool isAsync = default,
            in ParameterList parameterList = default,
            in DecoratorList decoratorList = default,
            in TypeParameterList typeParameterList = default,
            in MethodList methodList = default)
            : this(new Method(in identifier, in comment, in block, in type, in accessModifier, in isAsync, in parameterList, in decoratorList, in typeParameterList), in methodList)
        {
        }
        public MethodListAssociation(
            in string identifier,
            in string comment,
            in Block block = default,
            in string type = default,
            in AccessModifiers? accessModifier = default,
            in bool isAsync = default,
            in ParameterList parameterList = default,
            in DecoratorList decoratorList = default,
            in TypeParameterList typeParameterList = default,
            in MethodList methodList = default)
            : this(new Identifier(in identifier), new DocumentationComment(in comment), in block, string.IsNullOrWhiteSpace(type) ? null : new Identifier(in type), in accessModifier, in isAsync, in parameterList, in decoratorList, in typeParameterList, in methodList)
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

        public void SetRecord(in Method record)
        {
            Method = record;
            MethodId = record.MethodId;
        }
    }
}
