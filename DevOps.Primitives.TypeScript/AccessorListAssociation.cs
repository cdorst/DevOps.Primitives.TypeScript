using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("AccessorListAssociations", Schema = nameof(TypeScript))]
    public class AccessorListAssociation : IUniqueListAssociation<Accessor>
    {
        public AccessorListAssociation() { }
        public AccessorListAssociation(Accessor accessor, AccessorList accessorList = null)
        {
            Accessor = accessor;
            AccessorList = accessorList;
        }
        public AccessorListAssociation(SyntaxToken syntaxToken, AccessorList accessorList = null)
            : this(new Accessor(syntaxToken), accessorList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int AccessorListAssociationId { get; set; }

        [ProtoMember(2)]
        public Accessor Accessor { get; set; }
        [ProtoMember(3)]
        public int AccessorId { get; set; }

        [ProtoMember(4)]
        public AccessorList AccessorList { get; set; }
        [ProtoMember(5)]
        public int AccessorListId { get; set; }

        public Accessor GetRecord() => Accessor;

        public void SetRecord(Accessor record)
        {
            Accessor = record;
            AccessorId = Accessor.AccessorId;
        }
    }
}
