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

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("BaseLists", Schema = nameof(TypeScript))]
    public class BaseList : IUniqueList<BaseType, BaseListAssociation>
    {
        public BaseList() { }
        public BaseList(List<BaseListAssociation> baseListAssociations, AsciiStringReference listIdentifier = null)
        {
            BaseListAssociations = baseListAssociations;
            ListIdentifier = listIdentifier;
        }
        public BaseList(BaseListAssociation baseListAssociation, AsciiStringReference listIdentifier = null)
            : this(new List<BaseListAssociation> { baseListAssociation }, listIdentifier)
        {
        }
        public BaseList(Identifier baseType, AsciiStringReference listIdentifier = null)
            : this(new BaseListAssociation(baseType), listIdentifier)
        {
        }
        public BaseList(string baseType, AsciiStringReference listIdentifier = null)
            : this(new Identifier(baseType), listIdentifier)
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

        public void SetRecords(List<BaseType> records)
        {
            BaseListAssociations = UniqueListAssociationsFactory<BaseType, BaseListAssociation>.Create(records);
            ListIdentifier = new AsciiStringReference(
                UniqueListIdentifierFactory<BaseType>.Create(records, r => r.BaseTypeId));
        }

        public string GetBaseListSyntax(BaseListKind listKind)
            => $"{listKind.GetStringValue()} {string.Join(CommaSpace, this.GetRecords().Select(each => each.GetBaseTypeSyntax()))}";
    }
}
