using Common.EntityFrameworkServices;
using ProtoBuf;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevOps.Primitives.TypeScript
{
    [ProtoContract]
    [Table("Parameters", Schema = nameof(TypeScript))]
    public class Parameter : IUniqueListRecord
    {
        public Parameter() { }
        public Parameter(Identifier identifier, Identifier type, Expression defaultValue = null, DecoratorList attributeListCollection = null)
        {
            Identifier = identifier;
            Type = type;
            DefaultValue = defaultValue;
            AttributeListCollection = attributeListCollection;
        }
        public Parameter(string identifier, string type, Expression defaultValue = null, DecoratorList attributeListCollection = null)
            : this(new Identifier(identifier), new Identifier(type), defaultValue, attributeListCollection)
        {
        }

        [Key]
        [ProtoMember(1)]
        public int ParameterId { get; set; }

        [ProtoMember(2)]
        public DecoratorList AttributeListCollection { get; set; }
        [ProtoMember(3)]
        public int? AttributeListCollectionId { get; set; }

        [ProtoMember(4)]
        public Expression DefaultValue { get; set; }
        [ProtoMember(5)]
        public int? DefaultValueId { get; set; }

        [ProtoMember(6)]
        public Identifier Identifier { get; set; }
        [ProtoMember(7)]
        public int IdentifierId { get; set; }

        [ProtoMember(8)]
        public Identifier Type { get; set; }
        [ProtoMember(9)]
        public int TypeId { get; set; }
    }
}
