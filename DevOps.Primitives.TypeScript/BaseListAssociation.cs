using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("BaseListAssociations", Schema = nameof(TypeScript))]
    public class BaseListAssociation : IUniqueListAssociation<BaseType>
    {
        public BaseListAssociation() { }
        public BaseListAssociation(
            in BaseType baseType,
            in BaseList baseList = default)
        {
            BaseType = baseType;
            BaseList = baseList;
        }
        public BaseListAssociation(
            in Identifier baseType,
            in BaseList baseList = default)
            : this(new BaseType(in baseType), in baseList)
        {
        }
        public BaseListAssociation(
            in string baseType,
            in BaseList baseList = default)
            : this(new Identifier(in baseType), in baseList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int BaseListAssociationId { get; set; }

        [ProtoMember(2)]
        public BaseType BaseType { get; set; }
        [ProtoMember(3)]
        public int BaseTypeId { get; set; }

        [ProtoMember(4)]
        public BaseList BaseList { get; set; }
        [ProtoMember(5)]
        public int BaseListId { get; set; }

        public BaseType GetRecord() => BaseType;

        public void SetRecord(in BaseType record)
        {
            BaseType = record;
            BaseTypeId = record.BaseTypeId;
        }
    }
}
