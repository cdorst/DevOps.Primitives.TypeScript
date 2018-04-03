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
        public ParameterListAssociation(Parameter parameter, ParameterList parameterList = null)
        {
            Parameter = parameter;
            ParameterList = parameterList;
        }
        public ParameterListAssociation(Identifier identifier, Identifier type, Expression defaultValue = null, ParameterList parameterList = null)
            : this (new Parameter(identifier, type, defaultValue), parameterList)
        {
        }
        public ParameterListAssociation(string identifier, string type, Expression defaultValue = null, ParameterList parameterList = null)
            : this(new Identifier(identifier), new Identifier(type), defaultValue, parameterList)
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

        public void SetRecord(Parameter record)
        {
            Parameter = record;
            ParameterId = Parameter.ParameterId;
        }
    }
}
