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
        public BaseListAssociation(BaseType baseType, BaseList baseList = null)
        {
            BaseType = baseType;
            BaseList = baseList;
        }
        public BaseListAssociation(Identifier baseType, BaseList baseList = null)
            : this(new BaseType(baseType), baseList)
        {
        }
        public BaseListAssociation(string baseType, BaseList baseList = null)
            : this(new Identifier(baseType), baseList)
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

        public void SetRecord(BaseType record)
        {
            BaseType = record;
            BaseTypeId = BaseType.BaseTypeId;
        }
    }
}
