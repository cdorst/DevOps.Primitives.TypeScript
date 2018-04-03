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
        public ArgumentListAssociation(Argument argument, ArgumentList argumentList = null)
        {
            Argument = argument;
            ArgumentList = argumentList;
        }
        public ArgumentListAssociation(Identifier argument, ArgumentList argumentList = null)
            : this(new Argument(argument), argumentList)
        {
        }
        public ArgumentListAssociation(string argument, ArgumentList argumentList = null)
            : this(new Identifier(argument), argumentList)
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

        public void SetRecord(Argument record)
        {
            Argument = record;
            ArgumentId = Argument.ArgumentId;
        }
    }
}
