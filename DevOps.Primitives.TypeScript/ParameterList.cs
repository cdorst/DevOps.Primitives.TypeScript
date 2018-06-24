using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static DevOps.Primitives.TypeScript.StringConstants;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ParameterLists", Schema = nameof(TypeScript))]
    public class ParameterList : IUniqueList<Parameter, ParameterListAssociation>
    {
        public ParameterList() { }
        public ParameterList(
            in List<ParameterListAssociation> parameterListAssociations,
            in AsciiStringReference listIdentifier = default)
        {
            ParameterListAssociations = parameterListAssociations;
            ListIdentifier = listIdentifier;
        }
        public ParameterList(
            in ParameterListAssociation parameterListAssociation,
            in AsciiStringReference listIdentifier = default)
            : this(new List<ParameterListAssociation> { parameterListAssociation }, in listIdentifier)
        {
        }
        public ParameterList(
            in Identifier identifier,
            in Identifier type,
            in DocumentationComment comment,
            in Expression defaultValue = default,
            in DecoratorList decoratorList = default,
            in AsciiStringReference listIdentifier = default)
            : this(new ParameterListAssociation(in identifier, in type, in comment, in defaultValue, in decoratorList), in listIdentifier)
        {
        }
        public ParameterList(
            in string identifier,
            in string type,
            in string comment,
            in Expression defaultValue = default,
            in DecoratorList decoratorList = default,
            in AsciiStringReference listIdentifier = default)
            : this(
                  new Identifier(in identifier),
                  new Identifier(in type),
                  new DocumentationComment(in comment),
                  in defaultValue,
                  in decoratorList,
                  in listIdentifier)
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

        public void SetRecords(in List<Parameter> records)
        {
            ParameterListAssociations = UniqueListAssociationsFactory<Parameter, ParameterListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<Parameter>.Create(in records, r => r.ParameterId));
        }

        public string GetParameterListSyntax()
            => string.Join(CommaSpace, this.GetRecords().Select(p => p.GetParameterSyntax()));
    }
}
