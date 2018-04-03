using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ParameterLists", Schema = nameof(TypeScript))]
    public class ParameterList : IUniqueList<Parameter, ParameterListAssociation>
    {
        public ParameterList() { }
        public ParameterList(List<ParameterListAssociation> parameterListAssociations, AsciiStringReference listIdentifier = null)
        {
            ParameterListAssociations = parameterListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ParameterList(ParameterListAssociation parameterListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<ParameterListAssociation> { parameterListAssociation }, listIdentifier)
        {
        }
        public ParameterList(Identifier identifier, Identifier type, Expression defaultValue = null, AsciiStringReference listIdentifier = null)
            : this(new ParameterListAssociation(identifier, type, defaultValue), listIdentifier)
        {
        }
        public ParameterList(string identifier, string type, Expression defaultValue = null, AsciiStringReference listIdentifier = null)
            : this(new Identifier(identifier), new Identifier(type), defaultValue, listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ParameterListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<ParameterListAssociation> ParameterListAssociations { get; set; }

        public List<ParameterListAssociation> GetAssociations() => ParameterListAssociations;

        public void SetRecords(List<Parameter> records)
        {
            ParameterListAssociations = UniqueListAssociationsFactory<Parameter, ParameterListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Parameter>.Create(records, r => r.ParameterId));
        }
    }
}
