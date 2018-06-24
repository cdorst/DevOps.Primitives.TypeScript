using Common.EntityFrameworkServices;
using Common.EntityFrameworkServices.Factories;
using Common.EnumStringValues;
using DevOps.Primitives.Strings;
using ProtoBuf;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using static DevOps.Primitives.TypeScript.StringConstants;
using static System.String;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("BaseLists", Schema = nameof(TypeScript))]
    public class BaseList : IUniqueList<BaseType, BaseListAssociation>
    {
        public BaseList() { }
        public BaseList(
            in List<BaseListAssociation> baseListAssociations,
            in AsciiStringReference listIdentifier = default)
        {
            BaseListAssociations = baseListAssociations;
            ListIdentifier = listIdentifier;
        }
        public BaseList(
            in BaseListAssociation baseListAssociation,
            in AsciiStringReference listIdentifier = default)
            : this(new List<BaseListAssociation> { baseListAssociation }, in listIdentifier)
        {
        }
        public BaseList(
            in Identifier baseType,
            in AsciiStringReference listIdentifier = default)
            : this(new BaseListAssociation(in baseType), in listIdentifier)
        {
        }
        public BaseList(
            in string baseType,
            in AsciiStringReference listIdentifier = default)
            : this(new Identifier(in baseType), in listIdentifier)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int BaseListId { get; set; }

        [ProtoMember(2)]
        public AsciiStringReference ListIdentifier { get; set; }
        [ProtoMember(3)]
        public int ListIdentifierId { get; set; }

        [ProtoMember(4)]
        public List<BaseListAssociation> BaseListAssociations { get; set; }

        public List<BaseListAssociation> GetAssociations() => BaseListAssociations;

        public void SetRecords(in List<BaseType> records)
        {
            BaseListAssociations = UniqueListAssociationsFactory<BaseType, BaseListAssociation>.Create(in records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<BaseType>.Create(in records, r => r.BaseTypeId));
        }

        public string GetBaseListSyntax(in BaseListKind listKind)
            => Concat(listKind.GetStringValue(), " ", Join(CommaSpace, this.GetRecords().Select(each => each.GetBaseTypeSyntax())));
    }
}
