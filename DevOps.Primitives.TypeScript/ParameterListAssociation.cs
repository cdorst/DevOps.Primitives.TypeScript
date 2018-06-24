using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ParameterListAssociations", Schema = nameof(TypeScript))]
    public class ParameterListAssociation : IUniqueListAssociation<Parameter>
    {
        public ParameterListAssociation() { }
        public ParameterListAssociation(
            in Parameter parameter,
            in ParameterList parameterList = default)
        {
            Parameter = parameter;
            ParameterList = parameterList;
        }
        public ParameterListAssociation(
            in Identifier identifier,
            in Identifier type,
            in DocumentationComment comment,
            in Expression defaultValue = default,
            in DecoratorList decoratorList = default,
            in ParameterList parameterList = default)
            : this (new Parameter(in identifier, in type, in comment, in defaultValue, in decoratorList), in parameterList)
        {
        }
        public ParameterListAssociation(
            in string identifier,
            in string type,
            in string comment,
            in Expression defaultValue = default,
            in DecoratorList decoratorList = default,
            in ParameterList parameterList = default)
            : this(
                  new Identifier(in identifier),
                  new Identifier(in type),
                  new DocumentationComment(in comment),
                  in defaultValue,
                  in decoratorList,
                  in parameterList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ParameterListAssociationId { get; set; }

        [ProtoMember(2)]
        public Parameter Parameter { get; set; }
        [ProtoMember(3)]
        public int ParameterId { get; set; }

        [ProtoMember(4)]
        public ParameterList ParameterList { get; set; }
        [ProtoMember(5)]
        public int ParameterListId { get; set; }

        public Parameter GetRecord() => Parameter;

        public void SetRecord(in Parameter record)
        {
            Parameter = record;
            ParameterId = record.ParameterId;
        }
    }
}
