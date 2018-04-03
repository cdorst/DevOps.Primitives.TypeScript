using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("UsingDirectiveListAssociations", Schema = nameof(TypeScript))]
    public class UsingDirectiveListAssociation : IUniqueListAssociation<UsingDirective>
    {
        public UsingDirectiveListAssociation() { }
        public UsingDirectiveListAssociation(UsingDirective usingDirective, UsingDirectiveList usingDirectiveList = null)
        {
            UsingDirective = usingDirective;
            UsingDirectiveList = usingDirectiveList;
        }
        public UsingDirectiveListAssociation(Identifier usingDirective, UsingDirectiveList usingDirectiveList = null)
            : this(new UsingDirective(usingDirective), usingDirectiveList)
        {
        }
        public UsingDirectiveListAssociation(string usingDirective, UsingDirectiveList usingDirectiveList = null)
            : this(new Identifier(usingDirective), usingDirectiveList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int UsingDirectiveListAssociationId { get; set; }

        [ProtoMember(2)]
        public UsingDirective UsingDirective { get; set; }
        [ProtoMember(3)]
        public int UsingDirectiveId { get; set; }

        [ProtoMember(4)]
        public UsingDirectiveList UsingDirectiveList { get; set; }
        [ProtoMember(5)]
        public int UsingDirectiveListId { get; set; }

        public UsingDirective GetRecord() => UsingDirective;

        public void SetRecord(UsingDirective record)
        {
            UsingDirective = record;
            UsingDirectiveId = UsingDirective.UsingDirectiveId;
        }
    }
}
