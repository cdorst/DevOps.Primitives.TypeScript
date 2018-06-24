using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("ArgumentListAssociations", Schema = nameof(TypeScript))]
    public class ArgumentListAssociation : IUniqueListAssociation<Argument>
    {
        public ArgumentListAssociation() { }
        public ArgumentListAssociation(
            in Argument argument,
            in ArgumentList argumentList = default)
        {
            Argument = argument;
            ArgumentList = argumentList;
        }
        public ArgumentListAssociation(
            in Identifier argument,
            in ArgumentList argumentList = default)
            : this(new Argument(in argument), in argumentList)
        {
        }
        public ArgumentListAssociation(
            in string argument,
            in ArgumentList argumentList = default)
            : this(new Identifier(in argument), in argumentList)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ArgumentListAssociationId { get; set; }

        [ProtoMember(2)]
        public Argument Argument { get; set; }
        [ProtoMember(3)]
        public int ArgumentId { get; set; }

        [ProtoMember(4)]
        public ArgumentList ArgumentList { get; set; }
        [ProtoMember(5)]
        public int ArgumentListId { get; set; }

        public Argument GetRecord() => Argument;

        public void SetRecord(in Argument record)
        {
            Argument = record;
            ArgumentId = record.ArgumentId;
        }
    }
}
